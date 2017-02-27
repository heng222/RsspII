/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-28 8:43:54 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using BJMT.RsspII4net.ALE;
using BJMT.RsspII4net.ALE.Frames;
using BJMT.RsspII4net.Config;
using BJMT.RsspII4net.Exceptions;
using BJMT.RsspII4net.Infrastructure.Services;
using BJMT.RsspII4net.MASL;
using BJMT.RsspII4net.SAI.Events;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.SAI
{
    abstract class SaiConnection : IMaslConnectionObserver, 
        ISaiStateContext,
        IHandshakeTimeoutObserver, 
        ISaiFrameTransport,
        IDisposable
    {
        /// <summary>
        /// Sai握手中的超时时间（毫秒）。
        /// </summary>
        private const int HandshakeTimeout = 3000;

        #region "Filed"
        private bool _disposed = false;

        private bool _connected = false;

        private RsspEndPoint _rsspEndPoint = null;

        private SaiState _currentState = null;

        private SeqNoManager _seqNoManager;

        private ISaiConnectionObserver _observer;

        protected MaslConnection _maslConnection;

        private DefenseStrategy _defenseStrategy;

        private HandshakeTimeoutManager _handshakeTimeoutMgr;
        #endregion

        #region "Constructor"

        /// <summary>
        /// 构造函数。
        /// </summary>
        private SaiConnection(RsspEndPoint rsspEP, ISaiConnectionObserver observer)
        {
            _rsspEndPoint = rsspEP;
            _observer = observer;
            _seqNoManager = new SeqNoManager(0, ushort.MaxValue, rsspEP.SeqNoThreshold);
            _handshakeTimeoutMgr = new HandshakeTimeoutManager(SaiConnection.HandshakeTimeout, this);

            this.Initialize();
        }

        /// <summary>
        /// 创建一个适用于主动方的SAI Connection。
        /// </summary>
        protected SaiConnection(RsspEndPoint rsspEP, IEnumerable<RsspTcpLinkConfig> linkConfig,
            ISaiConnectionObserver observer,
            IAleTunnelEventNotifier tunnelEventNotifier)
            : this(rsspEP, observer)
        {
            _maslConnection = new MaslConnectionClient(rsspEP, linkConfig, this, tunnelEventNotifier);
        }

        /// <summary>
        /// 创建一个适用于被动方的SAI Connection。
        /// </summary>
        protected SaiConnection(RsspEndPoint rsspEP,
            ISaiConnectionObserver observer,
            IAleTunnelEventNotifier tunnelEventNotifier)
            : this(rsspEP, observer)
        {
            _maslConnection = new MaslConnectionServer(rsspEP, this, tunnelEventNotifier);
        }

        ~SaiConnection()
        {
            this.Dispose(false);
        }
        #endregion

        #region "Properties"

        public uint RemoteID { get { return _rsspEndPoint.RemoteID; } }
        #endregion

        #region "ISaiStateContext 接口"
        public SaiState CurrentState { get { return _currentState; } set { _currentState = value; } }
        public RsspEndPoint RsspEP { get { return _rsspEndPoint; } }
        DefenseStrategy ISaiStateContext.DefenseStrategy { get { return _defenseStrategy; } set { _defenseStrategy = value; } }
        MaslConnection ISaiStateContext.NextLayer { get { return _maslConnection; } }
        SeqNoManager ISaiStateContext.SeqNoManager { get { return _seqNoManager; } }
        ISaiConnectionObserver ISaiStateContext.Observer { get { return _observer; } }
        ISaiFrameTransport ISaiStateContext.FrameTransport { get { return this; } }

        /// <summary>
        /// 获取一个值，用于表示SAI是否已连接。
        /// </summary>
        public bool Connected
        {
            get { return _connected; }

            set
            {
                var preValue = _connected;
                _connected = value;

                if (!preValue && value)
                {
                    _observer.OnSaiConnected(_rsspEndPoint.LocalID, _rsspEndPoint.RemoteID);
                }
                else if (preValue && !value)
                {
                    _observer.OnSaiDisconnected(_rsspEndPoint.LocalID, _rsspEndPoint.RemoteID);
                }
            }
        }
        public void StartHandshakeTimer()
        {
            _handshakeTimeoutMgr.Start();
        }

        public void StopHandshakeTimer()
        {
            _handshakeTimeoutMgr.Stop();
        }
        #endregion

        #region "Virtual methods"
        protected abstract SaiState GetInitialState(DefenseStrategy strategy);
        protected abstract DefenseStrategy GetDefenseStrategy();
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                this.Connected = false;

                if (disposing)
                {
                    if (_handshakeTimeoutMgr != null)
                    {
                        _handshakeTimeoutMgr.Dispose();
                        _handshakeTimeoutMgr = null;
                    }

                    if (_maslConnection != null)
                    {
                        _maslConnection.Dispose();
                        _maslConnection = null;
                    }

                    if (_defenseStrategy != null)
                    {
                        _defenseStrategy.Dispose();
                        _defenseStrategy = null;
                    }
                }
            }
        }
        #endregion

        #region "Override methods"
        public override string ToString()
        {
            var sb = new StringBuilder(200);

            sb.AppendFormat("{0}（{1}）：是否连接={2}，当前状态={3}。\r\n",
                this.GetType().Name, _rsspEndPoint.ID, this.Connected,
                _currentState.GetType().Name);

            sb.AppendFormat("发送序号 = {0}，确认序号 = {1}。\r\n", _seqNoManager.SendSeq, _seqNoManager.AckSeq);

            sb.AppendFormat("Sai安全终结点：{0}\r\n", _rsspEndPoint);

            if (_defenseStrategy != null)
            {
                sb.AppendFormat("防御技术：{0}\r\n", _defenseStrategy);
            }
            else
            {
                sb.AppendFormat("防御技术：未初始化。\r\n");
            }

            sb.AppendFormat("\r\n{0}", _maslConnection);

            return sb.ToString();
        }
        #endregion

        #region "Private methods"
        /// <summary>
        /// 初始化防御技术与状态。
        /// </summary>
        private void Initialize()
        {
            if (_defenseStrategy != null)
            {
                _defenseStrategy.Dispose();
            }

            _defenseStrategy = this.GetDefenseStrategy();

            _currentState = this.GetInitialState(_defenseStrategy);

            _seqNoManager.Initlialize();
        }

        private bool CheckFrame(SaiFrame saiFrame)
        {
            if (this.Connected)
            {
                return _seqNoManager.IsExpected(saiFrame.SequenceNo);
            }
            else
            {
                if (saiFrame.FrameType == SaiFrameType.EC_Start
                    || saiFrame.FrameType == SaiFrameType.TTS_OffsetStart
                    || saiFrame.FrameType == SaiFrameType.TTS_OffsetAnswer1)
                {
                    return true;
                }
                else
                {
                    return _seqNoManager.IsExpected(saiFrame.SequenceNo);
                }
            }
        }
        #endregion

        #region "Public methods"

        public void Open()
        {
            _maslConnection.Open();
        }

        public void SendUserData(OutgoingPackage package)
        {
            _currentState.SendUserData(package);
        }

        public void Disconnect(MaslErrorCode majorReason, byte minorReason)
        {
            _maslConnection.Disconnect(majorReason, minorReason);

            this.Initialize();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region "IMaslConnectionObserver接口"
        void IMaslConnectionObserver.OnMaslConnected()
        {
            _currentState.HandleMaslConnected();
        }

        void IMaslConnectionObserver.OnMaslDisconnected()
        {
            this.Connected = false;

            // 
            this.Initialize();
        }

        void IMaslConnectionObserver.OnMaslUserDataArrival(byte[] maslUserData)
        {
            try
            {
                var frame = SaiFrame.Parse(maslUserData);

                if (this.CheckFrame(frame))
                {
                    _seqNoManager.UpdateAckSeq(frame.SequenceNo);

                    _currentState.HandleFrame(frame);

                    if (this.SaiFrameReceived != null)
                    {
                        this.SaiFrameReceived(null, new SaiFrameIncomingEventArgs(frame));
                    }
                }
                else if (_seqNoManager.IsBeyondRange(frame.SequenceNo))
                {
                    throw new Exception(string.Format("检测到丢包，当前确认序号={0}，收到的发送序号={1}。",
                        _seqNoManager.AckSeq, frame.SequenceNo));
                }
                else
                {
                    // 序号值为旧值，丢弃即可。
                }                
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(string.Format("Sai: {0} \r\n{1}", _rsspEndPoint.ID, ex));

                this.Disconnect(MaslErrorCode.NotDefined, 0);
            }
        }
        #endregion

        #region "IHandshakeTimeoutObserver接口实现"
        void IHandshakeTimeoutObserver.OnHandshakeTimeout()
        {
            try
            {
                this.Disconnect(MaslErrorCode.NotDefined, 0);
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }
        #endregion

        #region "ISaiFrameTransport接口"
        uint ISaiFrameTransport.NextSendSeq()
        {
            return _seqNoManager.GetAndUpdateSendSeq();
        }

        void ISaiFrameTransport.SendSaiFrame(SaiFrame saiFrame)
        {
            var bytes = saiFrame.GetBytes();
            _maslConnection.SendUserData(bytes);
        }

        public event EventHandler<SaiFrameIncomingEventArgs> SaiFrameReceived;
        #endregion
    }
}
