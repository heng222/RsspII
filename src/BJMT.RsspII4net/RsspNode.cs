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
using System.Linq;
using System.Text;
using BJMT.RsspII4net.Config;
using BJMT.RsspII4net.Events;
using BJMT.RsspII4net.Exceptions;
using BJMT.RsspII4net.Infrastructure.Services;
using BJMT.RsspII4net.SAI;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net
{
    /// <summary>
    /// 基于RSSP-II通讯协议的节点定义。
    /// </summary>
    abstract class RsspNode : 
        ISaiConnectionObserver, 
        IAleTunnelEventNotifier        
    {
        private const int CacheCountThreshold = 100;
        private const int BlockingTimeThreshold = 3;

        #region "Filed"
        private bool _disposed = false;

        private RsspConfig _rsspConfig;
        
        /// <summary>
        /// 一个事件缓存池，用于存放“将要发送的用户数据包对象”。
        /// </summary>
        private ProductCache<OutgoingPackage> _productCacheSending = new ProductCache<OutgoingPackage>();

        /// <summary>
        /// 一个事件缓存池，用于存放“到达的用户数据包对象”。
        /// </summary>
        private ProductCache<IncomingPackage> _productCacheReceive = new ProductCache<IncomingPackage>();


        /// <summary>
        /// 上一次发送队列的个数。
        /// </summary>
        private int _lastSendingCacheCount = 0;
        /// <summary>
        /// 上一次接收队列的个数。
        /// </summary>
        private int _lastReceiveCacheCount = 0;

        /// <summary>
        /// 上一次发送队列的阻塞时间。（单位：秒）
        /// </summary>
        private int _lastSendQueueBlockingTime = 0;
        /// <summary>
        /// 上一次接收队列的阻塞时间。
        /// </summary>
        private int _lastReceiveQueueBlockingTime = 0;
        #endregion

        #region "Constructor"

        /// <summary>
        /// 创建一个RsspNode。
        /// </summary>
        protected RsspNode(RsspConfig config)
        {
            _rsspConfig = config;

            // 设置本节点的名称。
            var prefixName = config.IsInitiator ? "RSSP客户端" : "RSSP服务器";
            this.Name = string.Format("{0}{1}", prefixName, _rsspConfig.LocalID);

        }

        ~RsspNode()
        {
            this.Dispose(false);
        }
        #endregion

        #region "Properties"
        public string Name { get; private set; }
        #endregion

        #region "Virtual methods"
        protected abstract void OnOpen(); 

        protected abstract SaiConnection GetSaiConnection(string key);

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                if (disposing)
                {
                    // 关闭缓存池。
                    _productCacheReceive.Close();
                    _productCacheSending.Close();
                }
            }
        }
        #endregion

        #region "Override methods"
        #endregion

        #region "Private methods"

        private void CreateProductCache()
        {
            // 打开“RDT事件缓存池”。
            _productCacheReceive.ThreadName = string.Format("{0}接收队列线程", this.Name);
            _productCacheReceive.ProductCreated += OnIncomingUserDataProductCreated;
            _productCacheReceive.Open();

            // 打开发送缓存池。
            _productCacheSending.ThreadName = string.Format("{0}发送队列线程", this.Name);
            _productCacheSending.ProductCreated += OnOutgoingUserDataProductCreated;
            _productCacheSending.Open();
        }

        private void OnOutgoingUserDataProductCreated(object sender, ProductCreatedEventArgs<OutgoingPackage> e)
        {
            try
            {
                e.Products.ToList().ForEach(package =>
                {
                    try
                    {
                        this.SendOutgoingPackage(package);

                        this.NotityfyOutgoingUserDataEvent(package);
                    }
                    catch (System.Exception ex)
                    {
                        LogUtility.Error(string.Format("{0}", ex));
                    }
                });
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(string.Format("{0}", ex));
            }
        }

        private void NotityfyOutgoingUserDataEvent(OutgoingPackage package)
        {
            if (this.UserDataOutgoing != null)
            {
                var args = new UserDataOutgoingEventArgs(package);

                this.UserDataOutgoing.GetInvocationList().ToList().ForEach(handler =>
                {
                    try
                    {                        
                        handler.DynamicInvoke(null, args);
                    }
                    catch (System.Exception)
                    {
                    }
                });
            }
        }

        private void SendOutgoingPackage(OutgoingPackage package)
        {
            package.DestID.ToList().ForEach(remoteID =>
            {
                try
                {
                    var saiID = this.BuildSaiConnectionID(_rsspConfig.LocalID, remoteID);

                    var theSaiConnection = this.GetSaiConnection(saiID);

                    if (theSaiConnection != null && theSaiConnection.Connected)
                    {
                        theSaiConnection.SendUserData(package);
                    }
                }
                catch (System.Exception ex)
                {
                    LogUtility.Error(ex.ToString());
                }
            });
        }

        private void OnIncomingUserDataProductCreated(object sender, ProductCreatedEventArgs<IncomingPackage> e)
        {
            try
            {
                e.Products.ToList().ForEach(pkg =>
                {
                    NotifyIncomingUserDataEvent(pkg);
                });
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(string.Format("{0}", ex));
            }
        }

        private void NotifyIncomingUserDataEvent(IncomingPackage pkg)
        {
            if (this.UserDataIncoming != null)
            {
                this.UserDataIncoming.GetInvocationList().ToList().ForEach(handler =>
                {
                    try
                    {
                        var args = new UserDataIncomingEventArgs(pkg);
                        args.Package.QueuingDelay = (UInt32)((DateTime.Now - args.Package.CreationTime).TotalMilliseconds / 10);

                        handler.DynamicInvoke(null, args);
                    }
                    catch (System.Exception)
                    {
                    }
                });
            }
        }

        private void CheckOutgoingCacheThreshold()
        {
            try
            {
                var allPkg = _productCacheSending.GetData();
                int currentCount = allPkg.Count();

                #region "队列个数检查"
                try
                {
                    if (currentCount >= CacheCountThreshold && currentCount % CacheCountThreshold == 0)
                    {
                        LogUtility.Warn(string.Format("未发送的用户数据个数已达到阈值({0})的{1}倍。",
                            CacheCountThreshold, currentCount / CacheCountThreshold));
                    }

                    // “发送队列个数改变”事件通知。
                    if (currentCount != _lastSendingCacheCount
                        && this.OutgoingCacheCountChanged != null)
                    {
                        _lastSendingCacheCount = currentCount;
                        this.OutgoingCacheCountChanged(null, new OutgoingCacheCountChangedEventArgs(this.Name, currentCount));
                    }
                }
                catch (System.Exception /*ex*/)
                {
                }
                #endregion

                #region "时间检查"
                try
                {
                    if (currentCount > 0)
                    {
                        var farthestTime = allPkg.Min(p => p.CreationTime);
                        var blockingTime = Convert.ToInt32((DateTime.Now - farthestTime).TotalSeconds);

                        if (this.OutgoingCacheDelayed != null
                            && blockingTime > BlockingTimeThreshold
                            && (blockingTime != _lastSendQueueBlockingTime))
                        {
                            _lastSendQueueBlockingTime = blockingTime;

                            var args = new OutgoingCacheDelayedEventArgs(this.Name, currentCount, CacheCountThreshold,
                                farthestTime);
                            this.OutgoingCacheDelayed(null, args);
                        }
                    }
                }
                catch (System.Exception /*ex*/)
                {
                }
                #endregion
            }
            catch (System.Exception)
            {
            }
        }

        private void CheckIncomgCacheThreshold()
        {
            try
            {
                var incomingPackages = _productCacheReceive.GetData();
                var currentCount = incomingPackages.Count();

                #region "队列个数检查"
                try
                {
                    if (currentCount >= CacheCountThreshold && currentCount % CacheCountThreshold == 0)
                    {
                        var info = new StringBuilder(string.Format("未处理的用户数据个数已达到阈值({0})的{1}倍。",
                            CacheCountThreshold, currentCount / CacheCountThreshold));
                    }

                    // “接收队列个数改变”事件通知。
                    if (currentCount != _lastReceiveCacheCount
                        && this.IncomingCacheCountChanged != null)
                    {
                        _lastReceiveCacheCount = currentCount;
                        this.IncomingCacheCountChanged(null, new IncomingCacheCountChangedEventArgs(this.Name, currentCount));
                    }
                }
                catch (System.Exception /*ex*/)
                {
                }
                #endregion

                #region "时间检查"
                try
                {
                    if (currentCount > 0)
                    {
                        var farthestTime = incomingPackages.Min(p => p.CreationTime);
                        var blockingTime = Convert.ToInt32((DateTime.Now - farthestTime).TotalSeconds);

                        // “接收队列超过阈值”事件通知。
                        if (this.IncomingCacheDelayed != null
                            && (blockingTime > BlockingTimeThreshold)
                            && (blockingTime != _lastReceiveQueueBlockingTime))
                        {
                            _lastReceiveQueueBlockingTime = blockingTime;

                            var args = new IncomingCacheDelayedEventArgs(this.Name, currentCount, CacheCountThreshold,
                                farthestTime);

                            this.IncomingCacheDelayed(null, args);
                        }
                    }
                }
                catch (System.Exception /*ex*/)
                {
                }
                #endregion
            }
            catch (System.Exception)
            {
            }
        }
        #endregion

        #region "Protected methods"
        protected string BuildSaiConnectionID(UInt32 localID, UInt32 remoteID)
        {
            return string.Format("SaiConnection_{0}: LID={1}, RID={2}.",
                _rsspConfig.IsInitiator ? "主动方" : "被动方",
                localID, remoteID);
        }


        protected void NotifyTcpEndPointListeningEvent(TcpEndPointListeningEventArgs args)
        {
            try
            {
                if (this.TcpEndPointListening != null)
                {
                    this.TcpEndPointListening(null, args);
                }
            }
            catch (System.Exception)
            {
            }
        }

        protected void NotifyTcpEndPointListenFailedEvent(TcpEndPointListenFailedEventArgs args)
        {
            try
            {
                if (this.TcpEndPointListenFailed != null)
                {
                    this.TcpEndPointListenFailed(null, args);
                }
            }
            catch (System.Exception)
            {
            }
        }

        #endregion

        #region "Public methods"

        public void Open()
        {
            CreateProductCache();

            this.OnOpen();
        }

        public void Close()
        {
            this.Dispose();
        }

        public void Send(OutgoingPackage package)
        {
            _productCacheSending.AddTail(package);

            this.CheckOutgoingCacheThreshold();
        }

        public void Disconnect(UInt32 remoteID)
        {
            var key = this.BuildSaiConnectionID(_rsspConfig.LocalID, remoteID);

            var theSaiConnection = this.GetSaiConnection(key);

            if (theSaiConnection != null)
            {
                theSaiConnection.Disconnect(MaslErrorCode.NotDefined, 0);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public event EventHandler<LogCreatedEventArgs> LogCreated
        {
            add { LogUtility.LogCreated += value; }
            remove { LogUtility.LogCreated -= value; }
        }

        public event EventHandler<TcpConnectingEventArgs> TcpConnecting;
        public event EventHandler<TcpConnectedEventArgs> TcpConnected;
        public event EventHandler<TcpConnectFailedEventArgs> TcpConnectFailed;
        public event EventHandler<TcpDisconnectedEventArgs> TcpDisconnected;

        public event EventHandler<TcpEndPointListeningEventArgs> TcpEndPointListening;
        public event EventHandler<TcpEndPointListenFailedEventArgs> TcpEndPointListenFailed;

        public event EventHandler<NodeConnectedEventArgs> NodeConnected;
        public event EventHandler<NodeInterruptionEventArgs> NodeDisconnected;

        public event EventHandler<UserDataIncomingEventArgs> UserDataIncoming;
        public event EventHandler<UserDataOutgoingEventArgs> UserDataOutgoing;

        public event EventHandler<OutgoingCacheCountChangedEventArgs> OutgoingCacheCountChanged;
        public event EventHandler<IncomingCacheCountChangedEventArgs> IncomingCacheCountChanged;

        public event EventHandler<IncomingCacheDelayedEventArgs> IncomingCacheDelayed;
        public event EventHandler<OutgoingCacheDelayedEventArgs> OutgoingCacheDelayed;
        #endregion


        #region "ISaiConnectionObserver"
        void ISaiConnectionObserver.OnSaiConnected(uint localID, uint remoteID)
        {
            try
            {
                if (this.NodeConnected != null)
                {
                    var args = new NodeConnectedEventArgs(localID, remoteID);
                    this.NodeConnected(null, args);
                }
            }
            catch (System.Exception)
            {
            }
        }

        public virtual void OnSaiDisconnected(uint localID, uint remoteID)
        {
            try
            {
                if (this.NodeDisconnected != null)
                {
                    var args = new NodeInterruptionEventArgs(localID, remoteID);
                    this.NodeDisconnected(null, args);
                }
            }
            catch (System.Exception)
            {
            }
        }

        void ISaiConnectionObserver.OnSaiUserDataArrival(uint remoteID, byte[] userData, long timeDelay, MessageDelayDefenseTech defenseTech)
        {
            try
            {
                var pkg = new IncomingPackage(remoteID, userData, timeDelay, defenseTech);

                _productCacheReceive.AddTail(pkg);

                this.CheckIncomgCacheThreshold();
            }
            catch (System.Exception)
            {
            }
        }
        #endregion


        #region "ITunnelEventNotifier接口"
        void IAleTunnelEventNotifier.NotifyTcpConnecting(TcpConnectingEventArgs args)
        {
            try
            {
                if (this.TcpConnecting != null)
                {
                    this.TcpConnecting(null, args);
                }
            }
            catch (System.Exception)
            {            	
            }
        }

        void IAleTunnelEventNotifier.NotifyTcpConnected(TcpConnectedEventArgs args)
        {
            try
            {
                if (this.TcpConnected != null)
                {
                    this.TcpConnected(null, args);
                }
            }
            catch (System.Exception )
            {
            }
        }

        void IAleTunnelEventNotifier.NotifyTcpConnectFailure(TcpConnectFailedEventArgs args)
        {
            try
            {
                if (this.TcpConnectFailed != null)
                {
                    this.TcpConnectFailed(null, args);
                }
            }
            catch (System.Exception)
            {
            }
        }

        void IAleTunnelEventNotifier.NotifyTcpDisconnected(TcpDisconnectedEventArgs args)
        {
            try
            {
                if (this.TcpDisconnected != null)
                {
                    this.TcpDisconnected(null, args);
                }
            }
            catch (System.Exception)
            {
            }
        }
        #endregion
    }
}
