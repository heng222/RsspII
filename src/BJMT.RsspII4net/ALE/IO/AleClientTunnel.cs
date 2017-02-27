/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-22 15:43:10 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;

namespace BJMT.RsspII4net.ALE
{
    class AleClientTunnel : AleTunnel
    {
        /// <summary>
        /// 重连间隔（毫秒）
        /// </summary>
        public const int RetryTimeout = 5000;

        #region "Filed"
        private bool _disposed = false;

        private IAleClientTunnelObserver _observer;
        #endregion

        #region "Constructor"

        /// <summary>
        /// 构造一个主动发起请求的连接对象。
        /// </summary>
        public AleClientTunnel(IPAddress clientIP, IPEndPoint serverEndPoint, IAleClientTunnelObserver observer)
            : base(clientIP, serverEndPoint, observer)
        {
            _observer = observer;
        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                base.Dispose(disposing);
            }
        }
        protected override void OnOpen()
        {
            this.BeginConnect();
        }

        protected override void OnReceiveCallbackException(Exception ex)
        {
            try
            {
                if (!_disposed)
                {
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(AleClientTunnel.RetryTimeout);
                        this.BeginConnect();
                    });
                }
            }
            catch (Exception ex1)
            {
                LogUtility.Error(ex1.ToString());
            }
        }

        protected override void OnHandshakeTimeout()
        {
            try
            {
                // 关闭Socket以引发ReceiveCallBack抛出异常，
                // ReceiveCallBack异常会调用OnReceiveCallbackException，即可引发重连。
                this.CloseTcpClient();
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }

        public override void Disconnect()
        {
            try
            {
                // 关闭Socket以引发ReceiveCallBack抛出异常，
                // ReceiveCallBack异常会调用OnReceiveCallbackException，即可引发重连。
                this.CloseTcpClient();
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }
        #endregion

        #region "Private methods"

        private void ConnectionCallback(IAsyncResult ar)
        {
            try
            {
                // 完成异步连接。
                this.Client.EndConnect(ar);

                // 正确性检查
                var expectedEndpoint = this.LocalEndPoint;
                var actualEndPoint = (IPEndPoint)this.Client.Client.LocalEndPoint;

                if (expectedEndpoint.Address.ToString() != IPAddress.Any.ToString()
                    && expectedEndpoint.Address.ToString() != actualEndPoint.Address.ToString())
                {
                    string msg = string.Format("绑定的IP地址({0})与返回的IP地址({1})不一致！",
                        expectedEndpoint.Address, actualEndPoint.Address);

                    throw new ApplicationException(msg);
                }

                // 连接成功后更新Port
                expectedEndpoint.Port = actualEndPoint.Port;

                // 事件通知。
                _observer.OnTcpConnected(this);

                // 开始接收数据。
                this.BeginReceive();
            }
            catch (Exception ex)
            {
                _observer.OnTcpConnectFailure(this, ex.Message);

                // 如果已连接成功，则通知链接断开。
                if (this.LocalEndPoint.Port != 0)
                {
                    this.HandleDisconnected(ex.Message);
                }

                // 5秒后尝试重连。
                Thread.Sleep(AleClientTunnel.RetryTimeout);
                this.BeginConnect();
            }
        }
        #endregion

        #region "Public methods"
        public void BeginConnect()
        {
            try
            {
                if (!_disposed)
                {
                    // 事件通知。
                    _observer.OnTcpConnecting(this);

                    // 关闭旧Socket。
                    this.CloseTcpClient();

                    // 创建新Socket。
                    this.LocalEndPoint.Port = 0;
                    this.Client = new TcpClient(this.LocalEndPoint);
                    this.InitializeTcpClient(true);

                    // 开始连接。
                    this.Client.BeginConnect(this.RemoteEndPoint.Address, this.RemoteEndPoint.Port,
                        ConnectionCallback, null);
                }
            }
            catch (Exception)
            {
                if (!_disposed)
                {
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(AleClientTunnel.RetryTimeout);
                        this.BeginConnect(); 
                    });
                }
            }
        }
        #endregion

    }
}
