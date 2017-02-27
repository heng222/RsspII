/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-23 14:04:46 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Sockets;
using BJMT.RsspII4net.ALE.Config;
using BJMT.RsspII4net.ALE.Frames;
using BJMT.RsspII4net.Services;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.ALE
{
    /// <summary>
    /// ALE管理器。
    /// </summary>
    class AleManager : IAleListenerObserver, ITcpConnectionSlaveObserver, IDisposable
    {
        #region "Filed"
        private bool _disposed = false;

        /// <summary>
        /// 用于管理所有TCP监听点。
        /// </summary>
        private AleListener _aleListener;

        /// <summary>
        /// Key = ALE Connection ID.
        /// </summary>
        private ConcurrentDictionary<string, AleConnection> _aleConnections = new ConcurrentDictionary<string, AleConnection>();

        /// <summary>
        /// 临时的SlaveTcpConnection链表，收到握手请求后将被移到AleConnection链表中。
        /// </summary>
        private ThreadSafetyList<TcpConnection> _tcpConnections = new ThreadSafetyList<TcpConnection>();
        #endregion

        #region "Constructor"
        /// <summary>
        /// 创建一个主动方使用的ALE管理器。
        /// </summary>
        public AleManager(AleClientConfig config)
        {
            this.LocalID = config.LocalID;

            config.LinkInfo.ToList().ForEach(p =>
            {
                var key = this.BuildAleConnectionID(config.LocalID, p.Key);
                var value = new AleConnection(config.LocalID, p.Key, p.Value);
                _aleConnections.GetOrAdd(key, value);
            });
        }

        /// <summary>
        /// 创建一个被动方使用的ALE管理器。
        /// </summary>
        public AleManager(AleServerConfig config)
        {
            this.LocalID = config.LocalID;

            _aleListener = new AleListener(config.ListenEndPoints, this);
        }

        ~AleManager()
        {
            this.Dispose(false);
        }
        #endregion

        #region "Properties"
        /// <summary>
        /// 获取本地编号。
        /// </summary>
        public UInt32 LocalID { get; private set; }

        /// <summary>
        /// 一个事件，当ALE层正在建立连接时引发。
        /// </summary>
        event EventHandler<AleConnectingEventArgs> Connecting;

        /// <summary>
        /// 一个事件，当ALE层连接建立时引发。
        /// </summary>
        event EventHandler<AleConnectedEventArgs> Connected;

        /// <summary>
        /// 一个事件，当ALE层连接断开时引发。
        /// </summary>
        event EventHandler<AleDisconnectedEventArgs> Disconnected;

        /// <summary>
        /// 一个事件，当用户数据到达时引发。
        /// </summary>
        event EventHandler<AleUserDataReceivedEventArgs> DataReceived;

        #endregion

        #region "Virtual methods"
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                if (disposing)
                {
                    if (_aleListener != null)
                    {
                        _aleListener.Dispose();
                        _aleListener = null;
                    }
                    
                    _aleConnections.ToList().ForEach(p => p.Value.Dispose());
                    _aleConnections.Clear();

                    _tcpConnections.ToList().ForEach(p => p.Close());
                    _tcpConnections.Clear();
                }
            }
        }
        #endregion

        #region "Override methods"
        #endregion

        #region "Private methods"
        private string BuildAleConnectionID(UInt32 clientID, UInt32 serverID)
        {
            return string.Format("ALE Connection: ClientID={0}, ServerID={1}.", clientID, serverID);
        }
        #endregion

        #region "Public methods"
        public void Open()
        {
            _aleConnections.ToList().ForEach(p => 
            {
                p.Value.Open(); 
            });

            if (_aleListener != null)
            {
                _aleListener.Start();
            }
        }

        public void Send()
        {

        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion


        #region "IAleListenerObserver接口"

        void IAleListenerObserver.OnEndPointListening(TcpListener endPoint)
        {
            Console.WriteLine(string.Format("{0}. 正在监听 {1}", DateTime.Now, endPoint));
            // TODO: 通知观察器正在监听
            try
            {
            }
            catch (System.Exception /*ex*/)
            {

            }
        }

        void IAleListenerObserver.OnEndPointListenFailed(TcpListener endPoint, string message)
        {
            // TODO: 通知观察器监听失败。
            try
            {
            }
            catch (System.Exception /*ex*/)
            {

            }
        }

        void IAleListenerObserver.OnAcceptTcpClient(TcpClient tcpClient)
        {
            try
            {
                // TODO: 通知观察器接收到一个TCP连接。
                Console.WriteLine(string.Format("{0}. 接收到一个TCP客户端 {1}<->{2}", DateTime.Now,
                    tcpClient.Client.LocalEndPoint, tcpClient.Client.RemoteEndPoint));

                var newConnection = new TcpConnectionSlave(tcpClient, this);
                newConnection.Open();

                // 加入临时链表。
                _tcpConnections.Add(newConnection);
            }
            catch (System.Exception /*ex*/)
            {
            }
        }
        #endregion

        #region "ITcpConnectionObserver接口实现"

        void ITcpConnectionObserver.OnConnectionClosed(TcpConnection theConnection, string reason)
        {
            try
            {
                // Client连接后但没有发送任何数据就关闭时会进入此函数。

                _tcpConnections.Remove(theConnection);
                theConnection.Close();
            }
            catch (System.Exception /*ex*/)
            {            	
            }
        }

        void ITcpConnectionObserver.OnAleFrameArrival(TcpConnection theConnection, AleFrame theFrame)
        {
            try
            {
                // 只处理ConnectionRequest帧。
                if (theFrame.FrameType == AleFrameType.ConnectionRequest)
                {
                    var aleData = theFrame.UserData as AleDataConnectionRequest;
                    var key = this.BuildAleConnectionID(aleData.MasterID, aleData.SlaveID);

                    AleConnection aleConnection;
                    if (!_aleConnections.ContainsKey(key))
                    {
                        aleConnection = new AleConnection(aleData.SlaveID, aleData.MasterID);
                        _aleConnections.GetOrAdd(key, aleConnection);
                    }
                    else
                    {
                        aleConnection = _aleConnections[key];
                    }

                    // 交给AleConnection处理。
                    ((ITcpConnectionObserver)aleConnection).OnAleFrameArrival(theConnection, theFrame);

                    // 从临时链表中移除。
                    _tcpConnections.Remove(theConnection);
                }
            }
            catch (System.Exception /*ex*/)
            {
                _tcpConnections.Remove(theConnection);
                theConnection.Close();     	
            }
        }
        #endregion
    }
}
