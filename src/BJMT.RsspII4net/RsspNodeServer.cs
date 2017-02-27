/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-26 15:47:34 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using BJMT.RsspII4net.ALE;
using BJMT.RsspII4net.ALE.Frames;
using BJMT.RsspII4net.Config;
using BJMT.RsspII4net.Events;
using BJMT.RsspII4net.Infrastructure.Services;
using BJMT.RsspII4net.SAI;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net
{
    /// <summary>
    /// 基于RSSP-II通讯协议的服务器端节点定义。
    /// </summary>
    class RsspNodeServer : RsspNode,
        IRsspNode,
        INodeListenerObserver,
        IAleServerTunnelObserver
    {
        #region "Filed"
        private bool _disposed = false;

        private RsspServerConfig _rsspConfig;

        /// <summary>
        /// 用于管理所有TCP监听点。
        /// </summary>
        private NodeListener _nodeListener;

        /// <summary>
        /// 临时的AleServerTunnel链表，收到握手请求后将被委托到SaiConnection处理。
        /// </summary>
        private ThreadSafetyList<AleServerTunnel> _serverTunnels = new ThreadSafetyList<AleServerTunnel>();

        /// <summary>
        /// Key = SaiConnection ID.
        /// </summary>
        private Dictionary<string, SaiConnectionServer> _saiConnections = new Dictionary<string, SaiConnectionServer>();
        private object _saiConnectionsLock = new object();

        /// <summary>
        /// TCP连接事件同步处理锁。
        /// </summary>
        private object _acceptEventSyncLock = new object();
        #endregion

        #region "Constructor"
        /// <summary>
        /// 创建一个被动方使用的ALE管理器。
        /// </summary>
        public RsspNodeServer(RsspServerConfig config)
            : base(config)
        {
            _rsspConfig = config;

            _nodeListener = new NodeListener(config.ListenEndPoints, this);
        }
        #endregion

        #region "Properties"

        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        public override void OnSaiDisconnected(uint localID, uint remoteID)
        {
            try
            {
                lock (_saiConnectionsLock)
                {
                    var id = this.BuildSaiConnectionID(localID, remoteID);
                    var sai = _saiConnections[id];
                    _saiConnections.Remove(id);
                    sai.Dispose();
                }

                base.OnSaiDisconnected(localID, remoteID);
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                if (disposing)
                {
                    if (_nodeListener != null)
                    {
                        _nodeListener.Dispose();
                        _nodeListener = null;
                    }

                    lock (_saiConnectionsLock)
                    {
                        _saiConnections.ToList().ForEach(p => p.Value.Dispose());
                        _saiConnections.Clear();
                    }

                    _serverTunnels.ToList().ForEach(p => p.Close());
                    _serverTunnels.Clear();
                }

                base.Dispose(disposing);
            }
        }

        protected override void OnOpen()
        {
            if (_nodeListener != null)
            {
                _nodeListener.Start();
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder(200);

            // TCP临时连接。
            sb.AppendFormat("临时AleTunnel个数={0}。\r\n", _serverTunnels.Count);
            _serverTunnels.AsReadOnly().ToList().ForEach(p =>
            {
                sb.AppendFormat("{0}，本地EP={1}，远程EP={2}，是否连接={3}，是否握手={4}。\r\n",
                    p.GetType().Name, p.LocalEndPoint, p.RemoteEndPoint, p.Connected, p.IsHandShaken);
            });

            // 安全连接。
            sb.AppendFormat("Sai通道个数={0}。\r\n", _saiConnections.Count);
            var index = 1;
            _saiConnections.Values.ToList().ForEach(p =>
            {
                sb.AppendFormat("\r\n\r\n第{0}个：{1}", index++, p);
            });

            sb.AppendFormat("\r\n");

            return sb.ToString();
        }
        #endregion

        #region "Private methods"
        protected override SaiConnection GetSaiConnection(string key)
        {
            lock (_saiConnectionsLock)
            {
                SaiConnectionServer theConnection;

                _saiConnections.TryGetValue(key, out theConnection);

                return theConnection;
            }
        }

        private void AddSaiConnection(string key, SaiConnectionServer value)
        {
            lock (_saiConnectionsLock)
            {
                _saiConnections.Add(key, value);
            }
        }

        private bool GetClientID(IPEndPoint clientEP, out uint clientID)
        {
            clientID = 0;

            var result = false;

            if (_rsspConfig != null && _rsspConfig.AcceptableClients != null)
            {
                foreach (var item in _rsspConfig.AcceptableClients)
                {
                    result = HelperTools.ContainsEndPoint(item.Value, clientEP);
                    if (result)
                    {
                        clientID = item.Key;
                    }
                }
            }

            return result;
        }

        private SaiConnectionServer CreateSaiConnectionServer(uint remoteID)
        {
            var rsspEP = new RsspEndPoint(_rsspConfig.LocalID, remoteID,
                _rsspConfig.ApplicationType, _rsspConfig.LocalEquipType,
                false, _rsspConfig.SeqNoThreshold,
                _rsspConfig.EcInterval, _rsspConfig.AuthenticationKeys,
                _rsspConfig.AcceptableClients);

            var result = new SaiConnectionServer(rsspEP, this, this);

            return result;
        }
        #endregion

        #region "Public methods"

        public List<uint> GetConnectedNodeID()
        {
            lock (_saiConnectionsLock)
            {
                return _saiConnections.Where(p => p.Value.Connected).
                    Select(p => p.Value.RemoteID).ToList();
            }
        }
        #endregion


        #region "INodeListenerObserver接口"

        void INodeListenerObserver.OnEndPointListening(TcpListener listener)
        {
            try
            {
                var args = new TcpEndPointListeningEventArgs(_rsspConfig.LocalID, listener.LocalEndpoint as IPEndPoint);
                this.NotifyTcpEndPointListeningEvent(args);
            }
            catch (System.Exception)
            {
            }
        }

        void INodeListenerObserver.OnEndPointListenFailed(TcpListener listener, string message)
        {
            try
            {
                var args = new TcpEndPointListenFailedEventArgs(_rsspConfig.LocalID, listener.LocalEndpoint as IPEndPoint);
                this.NotifyTcpEndPointListenFailedEvent(args);
            }
            catch (System.Exception)
            {
            }
        }

        void INodeListenerObserver.OnAcceptTcpClient(TcpClient tcpClient)
        {
            try
            {
                lock (_acceptEventSyncLock)
                {
                    LogUtility.Info(string.Format("Accept a new tcp client, LEP = {0}, REP={1}.",
                        tcpClient.Client.LocalEndPoint, tcpClient.Client.RemoteEndPoint));

                    SaiConnectionServer saiConnection = null;

                    // 查找客户端的ID
                    //uint clientID;
                    //var clientIdFound = this.GetClientID(tcpClient.Client.RemoteEndPoint as IPEndPoint, out clientID);

                    //if (clientIdFound)
                    //{
                    //    // 查找对应的SaiConnection。
                    //    var saiID = this.BuildSaiConnectionID(_rsspConfig.LocalID, clientID);
                    //    saiConnection = this.GetSaiConnection(saiID) as SaiConnectionServer;

                    //    if (saiConnection == null)
                    //    {
                    //        saiConnection = this.CreateSaiConnectionServer(clientID);
                    //        this.AddSaiConnection(saiID, saiConnection);
                    //        saiConnection.Open();
                    //    }
                    //}

                    // SaiConnection是否有效
                    var saiConnectionValid = saiConnection != null;

                    // 创建一个Ale通道。
                    var newTunnel = new AleServerTunnel(tcpClient, this, !saiConnectionValid);
                    if (saiConnectionValid)
                    {
                        saiConnection.AddAleServerTunnel(newTunnel);
                    }
                    else
                    {
                        // 加入临时链表。
                        _serverTunnels.Add(newTunnel);

                        // 当临时连接个数超过规定值时，记录日志。
                        var count = _serverTunnels.Count;
                        if (count > 20)
                        {
                            LogUtility.Warn(string.Format("临时连接的TCP个数已达到{0}个。", count));
                        }
                    }

                    // 打开。
                    newTunnel.Open();
                }
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(string.Format("{0}", ex));
            }
        }
        #endregion




        #region "IAleTunnelObserver接口实现"

        void IAleTunnelObserver.OnTcpDisconnected(AleTunnel theConnection, string reason)
        {
            try
            {
                // 进入此函数表示客户端连接后没有发送任何数据就关闭（可能是客户端主动关闭，也可能是在规定时间里没有发送CR帧而被服务器关闭）。

                _serverTunnels.Remove(theConnection as AleServerTunnel);
                theConnection.Close();
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
                // 只处理ConnectionRequest帧。
                if (theFrame.FrameType != AleFrameType.ConnectionRequest)
                {
                    throw new Exception(string.Format("在临时AleTunnel上收到了非CR帧，关闭此连接！本地终结点={0}，远程终结点={1}。",
                        theConnection.LocalEndPoint, theConnection.RemoteEndPoint));
                }

                // 确认此连接是“AleServerTunnel对象”。
                var tunnel = theConnection as AleServerTunnel;
                if (tunnel == null)
                {
                    throw new Exception(string.Format("在非AleServerTunnel上收到了CR帧！本地终结点={0}，远程终结点={1}。",
                        theConnection.LocalEndPoint, theConnection.RemoteEndPoint));
                }

                // 处理ALE帧。
                var aleCrData = theFrame.UserData as AleConnectionRequest;
                var key = this.BuildSaiConnectionID(aleCrData.ResponderID, aleCrData.InitiatorID);

                SaiConnectionServer saiConnection;
                lock (_saiConnectionsLock)
                {
                    saiConnection = this.GetSaiConnection(key) as SaiConnectionServer;
                    if (saiConnection == null)
                    {
                        saiConnection = this.CreateSaiConnectionServer(aleCrData.InitiatorID);

                        this.AddSaiConnection(key, saiConnection);

                        saiConnection.Open();
                    }
                }

                // 交给SaiConnection处理。
                saiConnection.HandleAleConnectionRequestFrame(tunnel, theFrame);

                // 从临时链表中移除。
                _serverTunnels.Remove(tunnel);
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(string.Format("{0}", ex));
                _serverTunnels.Remove(theConnection as AleServerTunnel);
                theConnection.Close();     	
            }
        }
        #endregion
    }
}
