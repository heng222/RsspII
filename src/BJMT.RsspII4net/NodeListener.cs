/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-28 8:42:04 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net
{
    class NodeListener
    {
        #region "Filed"
        private bool _disposed = false;
        private List<TcpListener> _tcpListeners = new List<TcpListener>();
        private INodeListenerObserver _observer;
        #endregion

        #region "Constructor"
        public NodeListener(IEnumerable<IPEndPoint> ipEndPoints, INodeListenerObserver observer)
        {
            if (observer == null || ipEndPoints == null || ipEndPoints.Count() == 0)
            {
                throw new ArgumentException();
            }

            _observer = observer;

            ipEndPoints.ToList().ForEach(p =>
            {
                var item = new TcpListener(p);
                _tcpListeners.Add(item);
            });

            // 订阅网络变化事件
            NetworkChange.NetworkAvailabilityChanged += OnNetworkAvailabilityChanged;
            NetworkChange.NetworkAddressChanged += OnNetworkAddressChanged;
        }

        ~NodeListener()
        {
            this.Dispose(false);
        }
        #endregion

        #region "Properties"

        #endregion

        #region "Virtual methods"
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                if (disposing)
                {
                    NetworkChange.NetworkAvailabilityChanged -= OnNetworkAvailabilityChanged;
                    NetworkChange.NetworkAddressChanged -= OnNetworkAddressChanged;

                    _tcpListeners.ToList().ForEach(p => p.Stop());
                    _tcpListeners.Clear();
                }
            }
        }
        #endregion

        #region "Override methods"
        #endregion

        #region "Private methods"

        private void BeginAccept(TcpListener theListener)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    if (!_disposed)
                    {
                        theListener.Start();
                        theListener.BeginAcceptTcpClient(AcceptCallback, theListener);

                        // 事件通知。                    
                        _observer.OnEndPointListening(theListener);
                    }
                }
                catch (System.Exception ex)
                {
                    // 事件通知。                
                    _observer.OnEndPointListenFailed(theListener, ex.Message);

                    // 重新尝试
                    Thread.Sleep(5000);
                    this.BeginAccept(theListener);
                }
            });
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            var theListener = ar.AsyncState as TcpListener;
            TcpClient tcpClient = null;

            try
            {
                tcpClient = theListener.EndAcceptTcpClient(ar);     

                // 继续监听
                this.BeginAccept(theListener);
            }
            catch (ObjectDisposedException ex)
            {
                _observer.OnEndPointListenFailed(theListener, ex.Message);
            }
            catch (System.Exception)
            {
                this.BeginAccept(theListener);
            }

            try
            {
                if (tcpClient != null)
                {
                    _observer.OnAcceptTcpClient(tcpClient);
                }
            }
            catch (System.Exception )
            {
                tcpClient.Close();
            }
        }

        /// <summary>
        /// 当网络的可用性更改时
        /// </summary>
        private void OnNetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            try
            {
                var ips = HelperTools.LocalIpAddress;

                foreach (var listener in _tcpListeners)
                {
                    UpdateListenerStatus(ips, listener);
                }
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 当网络的IP地址发生变化时
        /// </summary>
        private void OnNetworkAddressChanged(object sender, EventArgs e)
        {
            try
            {
                var ips = HelperTools.LocalIpAddress;

                foreach (var listener in _tcpListeners)
                {
                    UpdateListenerStatus(ips, listener);
                }
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }

        private void UpdateListenerStatus(IEnumerable<IPAddress> ips, TcpListener listener)
        {
            try
            {
                if (!listener.Server.IsBound)
                {
                    this.BeginAccept(listener);
                }
                else
                {
                    try
                    {
                        var listenIP = ((IPEndPoint)listener.Server.LocalEndPoint).Address;
                        if (!ips.Contains(listenIP))
                        {
                            listener.Stop();
                        }
                    }
                    catch (System.Exception )
                    {
                        listener.Stop();
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }
        #endregion

        #region "Public methods"
        public void Start()
        {
            _tcpListeners.ToList().ForEach(p =>
            {
                this.BeginAccept(p);
            });
        }
        
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
