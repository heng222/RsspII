/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-22 15:43:44 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.ALE.Frames;
using BJMT.RsspII4net.ALE.State;
using BJMT.RsspII4net.Events;
using BJMT.RsspII4net.Infrastructure.Services;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.ALE
{
    /// <summary>
    /// 表示一个ALE连接。
    /// </summary>
    abstract class AleConnection : IAleTunnelObserver, 
        IAleStateContext, IDisposable
    {
        #region "Filed"
        private bool _disposed = false;

        private RsspEndPoint _rsspEndPoint = null;

        private AleState _currentState;

        /// <summary>
        /// AleTunnel集合
        /// </summary>
        private ThreadSafetyList<AleTunnel> _tunnels = new ThreadSafetyList<AleTunnel>();

        /// <summary>
        /// 表示已通过握手验证的Tunnel连接个数。
        /// </summary>
        private int _validTunnelCount = 0;
        private object _validTunnelCountSyncLock = new object();

        /// <summary>
        /// ALEConnection观察器
        /// </summary>
        private IAleConnectionObserver _observer;

        /// <summary>
        /// Tunnel事件观察器
        /// </summary>
        private IAleTunnelEventNotifier _tunnelEventNotifier;

        /// <summary>
        /// 状态的输入事件同步锁。
        /// </summary>
        private object _stateInputEventSyncLock = new object();

        /// <summary>
        /// 序号管理器
        /// </summary>
        private SeqNoManager _seqNoManager;
        /// <summary>
        /// AU消息提供器。
        /// </summary>
        private IAuMessageBuilder _auMsgBuilder;
        #endregion

        #region "Constructor"

        /// <summary>
        /// 构造函数。
        /// </summary>
        protected AleConnection(RsspEndPoint rsspEP,
            IAuMessageBuilder auMsgProvider,
            IAleConnectionObserver observer,
            IAleTunnelEventNotifier tunnelEventNotifier)
        {
            _rsspEndPoint = rsspEP;
            _auMsgBuilder = auMsgProvider;
            _observer = observer;
            _tunnelEventNotifier = tunnelEventNotifier;

            _seqNoManager = new SeqNoManager(0, UInt16.MaxValue, rsspEP.SeqNoThreshold);

            this.Initialize();
        }

        /// <summary>
        /// 终结函数。
        /// </summary>
        ~AleConnection()
        {
            this.Dispose(false);
        }
        #endregion

        #region "Properties"
        /// <summary>
        /// 获取一个值，用于表示ALE是否已连接。
        /// </summary>
        public bool Connected { get { return _validTunnelCount > 0; } }

        protected object StateEventLock { get { return _stateInputEventSyncLock; } }
        #endregion

        #region "IAleStateContext 实现"

        public AleState CurrentState { get { return _currentState; } set { _currentState = value; } }
        public RsspEndPoint RsspEP { get { return _rsspEndPoint; } }
        public IAleTunnelEventNotifier TunnelEventNotifier { get { return _tunnelEventNotifier; } }
        IAuMessageBuilder IAleStateContext.AuMsgBuilder { get { return _auMsgBuilder; } }
        IAleConnectionObserver IAleStateContext.Observer { get { return _observer; } }
        SeqNoManager IAleStateContext.SeqNoManager { get { return _seqNoManager; } }
        IEnumerable<AleTunnel> IAleStateContext.Tunnels { get { return _tunnels.AsReadOnly(); } }

        public bool ContainsTunnel(AleTunnel tunnel)
        {
            return _tunnels.Contains(tunnel);
        }

        public void AddConnection(AleTunnel item)
        {
            lock (_tunnels.SyncRoot)
            {
                if (!_tunnels.Contains(item))
                {
                    _tunnels.Add(item);
                }
            }
        }

        public void IncreaseValidConnection()
        {
            lock (_validTunnelCountSyncLock)
            {
                var preCount = _validTunnelCount;
                _validTunnelCount++;
                
                LogUtility.Info(string.Format("{0}: I有效连接个数={1}", _rsspEndPoint.ID, _validTunnelCount));

                if (preCount == 0 && _validTunnelCount == 1)
                {
                    this.OnConnectionChanged(true);
                }
            }
        }

        public void DescreaseValidConnection()
        {
            lock (_validTunnelCountSyncLock)
            {
                var preCount = _validTunnelCount;

#if DEBUG
                _validTunnelCount--;
#else
                
                if (_validTunnelCount > 0)
                {
                    _validTunnelCount--;
                }
#endif
                LogUtility.Info(string.Format("{0}: D有效连接个数= {1}", _rsspEndPoint.ID, _validTunnelCount));

                if (preCount > 0 && _validTunnelCount == 0)
                {
                    this.OnConnectionChanged(false);
                }
            }
        }

        public void RemoveCloseConnection(AleTunnel theConnection)
        {
            if (theConnection.IsHandShaken)
            {
                this.DescreaseValidConnection();
                theConnection.IsHandShaken = false;
            }
            
            _tunnels.Remove(theConnection);
            theConnection.Close();
        }
        #endregion

        #region "Virtual methods"
        protected abstract AleState GetInitialState();

        protected abstract void HandleTunnelDisconnected(AleTunnel theConnection, string reason);

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                if (disposing)
                {
                    _tunnels.ToList().ForEach(p => p.Close());
                    _tunnels.Clear();
                }
            }
        }
        #endregion

        #region "Override methods"
        public override string ToString()
        {
            var sb = new StringBuilder(200);

            sb.AppendFormat("{0}（{1}）：是否连接={2}，当前状态={3}，Tunnel个数= {4}。\r\n", 
                this.GetType().Name, _rsspEndPoint.ID, this.Connected,
                _currentState.GetType().Name, _tunnels.Count);

            sb.AppendFormat("发送序号 = {0}，确认序号 = {1}。\r\n", _seqNoManager.SendSeq, _seqNoManager.AckSeq);

            _tunnels.AsReadOnly().ToList().ForEach(p=>
            {
                sb.AppendFormat("{0}，本地EP={1}，远程EP={2}，是否连接={3}，是否握手={4}。\r\n", 
                    p.GetType().Name, p.LocalEndPoint, p.RemoteEndPoint, p.Connected, p.IsHandShaken);
            });


            return sb.ToString();
        }
        #endregion

        #region "Private methods"

        private void Initialize()
        {
            _currentState = this.GetInitialState();

            _seqNoManager.Initlialize();
        }

        private bool CheckFrame(AleFrame theFrame)
        {
            // 1. 检查帧类型。
            // CR帧与CC帧固定有效。
            if (theFrame.FrameType == AleFrameType.ConnectionRequest 
                || theFrame.FrameType == AleFrameType.ConnectionConfirm)
            {
                return true;
            }

            // 2. 检查序列号。
            // D类服务中序号才有效，A类服务中的序号都是0。
            if (_rsspEndPoint.ServiceType == ServiceType.A)
            {
                return true;
            }
            else
            {
                return _seqNoManager.IsExpected(theFrame.SequenceNo);
            }
        }

        /// <summary>
        /// 处理ALE连接变化事件。
        /// </summary>
        /// <param name="connected">true表示Ale由断到连，false表示ALE由连到断。</param>
        private void OnConnectionChanged(bool connected)
        {
            try
            {
                if (connected)
                {
                    _currentState = new AleConnectedState(this);

                    _observer.OnAleConnected();
                }
                else
                {
                    this.Initialize();
                    
                    _observer.OnAleDisconnected(); 
                }
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());	
            }
        }
        #endregion

        #region "Public methods"

        public void Open()
        {
            _tunnels.ToList().ForEach(p => p.Open());
        }

        public void SendUserData(byte[] maslPacket)
        {
            _currentState.SendUserData(maslPacket);
        }

        public void Disconnect(byte[] diSaPDU)
        {
            _currentState.Disconnect(diSaPDU);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void HandleHandshakeTimeout()
        {
            _tunnels.AsReadOnly().ToList().ForEach(p => p.HandleHandshakeTimeout());

            this.Initialize();
        }

        public void AddAleServerTunnel(AleServerTunnel tunnel)
        {
            tunnel.Observer = this;
            this.AddConnection(tunnel);

            //tunnel.IsHandShaken = true;
            //this.IncreaseValidConnection();

            // 事件通知：接收到一个新的TCP连接。
            var args = new TcpConnectedEventArgs(tunnel.ID,
                this.RsspEP.LocalID, tunnel.LocalEndPoint,
                this.RsspEP.RemoteID, tunnel.RemoteEndPoint);
            this.TunnelEventNotifier.NotifyTcpConnected(args);
        }
        #endregion

        #region "IAleTunnelObserver 接口实现"

        void IAleTunnelObserver.OnTcpDisconnected(AleTunnel theConnection, string reason)
        {
            try
            {
                LogUtility.Info(string.Format("{0}: A TCP Connection disconnected. LEP = {1}, REP = {2}",
                    this.RsspEP.ID, theConnection.LocalEndPoint, theConnection.RemoteEndPoint));

                this.HandleTunnelDisconnected(theConnection, reason);

                // 通知观察器连接关闭。
                var args = new TcpDisconnectedEventArgs(theConnection.ID, 
                    _rsspEndPoint.LocalID, theConnection.LocalEndPoint,
                    _rsspEndPoint.RemoteID, theConnection.RemoteEndPoint);
                _tunnelEventNotifier.NotifyTcpDisconnected(args);
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }

        void IAleTunnelObserver.OnAleFrameArrival(AleTunnel theConnection, AleFrame theFrame)
        {
            try
            {
                lock (_stateInputEventSyncLock)
                {
                    if (CheckFrame(theFrame))
                    {
                        // D类服务时，序列号有效；A类服务时，序列号无效。
                        if (_rsspEndPoint.ServiceType == ServiceType.D
                            && theFrame.FrameType == AleFrameType.DataTransmission)
                        {
                            _seqNoManager.UpdateAckSeq(theFrame.SequenceNo);
                        }

                        // 
                        if (theFrame.FrameType == AleFrameType.ConnectionRequest)
                        {
                            LogUtility.Info(string.Format("{0}: Received a CR frame on tcp connection(LEP={1}, REP={2}).",
                                this.RsspEP.ID, theConnection.LocalEndPoint, theConnection.RemoteEndPoint));

                            theConnection.Observer = this;

                            _currentState.HandleConnectionRequestFrame(theConnection, theFrame);
                        }
                        else if (theFrame.FrameType == AleFrameType.ConnectionConfirm)
                        {
                            LogUtility.Info(string.Format("{0}: Received a CC frame on tcp connection(LEP={1}, REP={2})).",
                                this.RsspEP.ID, theConnection.LocalEndPoint, theConnection.RemoteEndPoint));

                            _currentState.HandleConnectionConfirmFrame(theConnection, theFrame);
                        }
                        else if (theFrame.FrameType == AleFrameType.Disconnect)
                        {
                            _currentState.HandleDiFrame(theConnection, theFrame);
                        }
                        else if (theFrame.FrameType == AleFrameType.DataTransmission)
                        {
                            _currentState.HandleDataTransmissionFrame(theConnection, theFrame);
                        }
                    }
                    else if (_seqNoManager.IsBeyondRange(theFrame.SequenceNo))
                    {
                        throw new Exception(string.Format("检测到丢包，当前确认序号={0}，收到的发送序号={1}，TCP ID = {2}", 
                            _seqNoManager.AckSeq, theFrame.SequenceNo, theConnection.ID));
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(string.Format("ALE: {0} : {1}", _rsspEndPoint.ID, ex));
                theConnection.Disconnect();
            }
        }
        #endregion

    }
}
