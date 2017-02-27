/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-22 15:42:55 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Net;
using System.Net.Sockets;
using BJMT.RsspII4net.ALE.Frames;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.ALE
{
    abstract class AleTunnel : IHandshakeTimeoutObserver, IDisposable
    {
        /// <summary>
        /// 此值定义了多长时间里无数据包则发送Alive信息。（单位：毫秒）
        /// </summary>
        private const int AliveInterval = 1200;

        /// <summary>
        /// 此值定义了当发送KeepAlive后多长时间收不到回应则送下一个KeepAlive。（单位：毫秒）
        /// </summary>
        private const int AliveTimeout = 1000;

        /// <summary>
        /// 握手超时。
        /// </summary>
        private const int HandshakeTimeout = 3000;

        #region "Filed"

        private bool _disposed = false;
        
        /// <summary>
        /// 接收数据时的临时缓存。
        /// 长度取值参考MTU。
        /// </summary>
        private byte[] _recvBufCache = new byte[1500];

        /// <summary>
        /// ALE流解析器。
        /// </summary>
        private AleStreamParser _streamPaser = new AleStreamParser();
        
        /// <summary>
        /// 握手超时管理器。
        /// </summary>
        private HandshakeTimeoutManager _handshakeTimeoutMgr;
        #endregion

        #region "Constructor"
        /// <summary>
        /// 私有构造函数。
        /// </summary>
        private AleTunnel(IAleTunnelObserver observer)
        {
            this.Observer = observer;

            this.IsNormal = true;
            this.IsActive = true;

            _handshakeTimeoutMgr = new HandshakeTimeoutManager(AleTunnel.HandshakeTimeout, this);
        }

        /// <summary>
        /// 构造一个客户端使用的TCP连接。
        /// </summary>
        protected AleTunnel(IPAddress clientIP,
            IPEndPoint serverEndPoint,
            IAleTunnelObserver observer)
            :this(observer)
        {
            if (clientIP==null || serverEndPoint == null || observer == null)
            {
                throw new ArgumentNullException();
            }

            this.LocalEndPoint = new IPEndPoint(clientIP, 0);
            this.RemoteEndPoint = new IPEndPoint(IPAddress.Parse(serverEndPoint.Address.ToString()), serverEndPoint.Port);
            
            this.ID = string.Format("Client_{0}_{1}_{2}", this.LocalEndPoint, this.RemoteEndPoint, Guid.NewGuid());
        }

        /// <summary>
        /// 构造一个服务器端使用的TCP连接。
        /// </summary>
        protected AleTunnel(TcpClient client,
            IAleTunnelObserver observer)
            : this(observer)
        {
            if (client == null || observer == null)
            {
                throw new ArgumentNullException();
            }

            this.Client = client;
            this.InitializeTcpClient(true);

            this.LocalEndPoint = this.Client.Client.LocalEndPoint as IPEndPoint;
            this.RemoteEndPoint = this.Client.Client.RemoteEndPoint as IPEndPoint;

            this.ID = string.Format("Server_{0}_{1}_{2}", this.LocalEndPoint, this.RemoteEndPoint, Guid.NewGuid());
        }

        /// <summary>
        /// 终结函数
        /// </summary>
        ~AleTunnel()
        {
            this.Dispose(false);
        }
        #endregion

        #region "Properties"

        /// <summary>
        /// 获取此连接的唯一标识符
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// 获取一个值，用于表示Tcp连接是否已建立。
        /// </summary>
        public bool Connected
        {
            get
            {
                if (this.Client != null)
                {
                    return this.Client.Connected;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取一个值，用于表示此TCP连接是否已通过CR/CC握手。
        /// true表示已握手，fasle表示没有握手。
        /// </summary>
        public bool IsHandShaken { get; set; }

        /// <summary>
        /// 获取一个值，用于表示当前链路是否为正常链路。true表示正常链路，false表示冗余链路。仅A类服务时有效。
        /// TODO: IsNormal应该根据配置或A、D类服务来确定。
        /// </summary>
        public bool IsNormal { get; private set; }

        /// <summary>
        /// 获取一个值 ，用于表示当前链路是否处于活动状态。仅A类服务时有效。
        /// true表示活动，false表示非活动。
        /// </summary>
        public bool IsActive { get; private set; }

        public IAleTunnelObserver Observer { get; set; }

        protected TcpClient Client { get; set; }

        public IPEndPoint LocalEndPoint { get; private set; }

        public IPEndPoint RemoteEndPoint { get; private set; }
        #endregion

        #region "abstract/virtual methods"

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                if (disposing)
                {
                    _handshakeTimeoutMgr.Dispose();

                    this.CloseTcpClient();
                }
            }
        }

        protected abstract void OnOpen();
        protected abstract void OnReceiveCallbackException(Exception ex);
        protected abstract void OnHandshakeTimeout();

        public abstract void Disconnect();
        #endregion

        #region "Override methods"
        #endregion

        #region "Private/Protected methods"
        protected void InitializeTcpClient(bool keepAliveEnabled)
        {
            this.Client.SendBufferSize = AleStreamParser.AleStreamMaxLength * 5;
            this.Client.ReceiveBufferSize = AleStreamParser.AleStreamMaxLength * 5;

            if (keepAliveEnabled)
            {
                HelperTools.SetKeepAlive(this.Client.Client, AleTunnel.AliveInterval, AleTunnel.AliveTimeout);
            }
        }

        protected void CloseTcpClient()
        {
            try
            {
                if (this.Client != null)
                {
                    this.Client.Close();
                    this.Client = null;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }
        
        protected void HandleDisconnected(string reason)
        {
            this.Observer.OnTcpDisconnected(this, reason);
        }
        
        protected void BeginReceive()
        {
            this.Client.GetStream().BeginRead(_recvBufCache, 0, _recvBufCache.Length,
                ReceiveCallback, _recvBufCache);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Read data from the remote device.
                int count = this.Client.GetStream().EndRead(ar);
                var bytes = ar.AsyncState as byte[];

                if (count > 0)
                {
                    this.HandleDataReceived(bytes, count);

                    // Get the rest of the data.
                    this.BeginReceive();
                }
                else
                {
                    throw new Exception(string.Format("远程主机主动关闭TCP连接，本地终结点是{0}，远程终结点是{1}。", 
                        this.LocalEndPoint, this.RemoteEndPoint));
                }
            }
            catch (System.Exception ex)
            {
                this.HandleDisconnected(ex.Message);

                // 调用模板方法让子类处理。
                this.OnReceiveCallbackException(ex);
            }
        }

        private void HandleDataReceived(byte[] buffer, int bytesRead)
        {
            try
            {
                var aleFrameBytes = _streamPaser.ParseTcpStream(buffer, bytesRead);

                aleFrameBytes.ForEach(p =>
                {
                    var aleFrame = AleFrame.Parse(p);
                    this.Observer.OnAleFrameArrival(this, aleFrame);
                });
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());
                // 方式1：异常全部忽略，继续异步接收。
                // 方式2：断开连接。
                //this.Disconnect();
            }
        }

        #endregion

        #region "Public methods"

        public void Open()
        {
            this.OnOpen();
        }

        public void Close()
        {
            this.Dispose();
        }

        public void Send(byte[] data)
        {
            if (this.Connected && !_disposed)
            {
                this.Client.GetStream().Write(data, 0, data.Length);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void StartHandshakeTimer()
        {
            _handshakeTimeoutMgr.Start();
        }

        public void StopHandshakeTimer()
        {
            _handshakeTimeoutMgr.Stop();
        }

        /// <summary>
        /// 处理握手超时。
        /// </summary>
        public void HandleHandshakeTimeout()
        {
            this.OnHandshakeTimeout();
        }
        #endregion

        #region "IHandshakeTimeoutObserver接口实现"
        void IHandshakeTimeoutObserver.OnHandshakeTimeout()
        {
            try
            {
                this.HandleHandshakeTimeout();
            }
            catch (System.Exception /*ex*/)
            {
            }
        }
        #endregion
    }
}
