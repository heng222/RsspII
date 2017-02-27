/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：Microunion Foundation Component Library
//
// 创 建 人：zhh_217
// 创建日期：08/31/2011 08:53:27 
// 邮    箱：zhh_217@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司 2009-2015 保留所有权利
//
//----------------------------------------------------------------*/

using System;
using System.Text;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace BJMT.RsspII4net.Utilities
{

    /// <summary>
    /// 产品缓存池类。参见OS中生产者-消费者问题中的缓存池。
    /// </summary>
    class ProductCache<TProduct> : IDisposable
    {
        #region "Filed"

        private bool _disposed = false;

        /// <summary>
        /// 产品缓存
        /// </summary>
        private ThreadSafetyList<TProduct> _productQueue = null;

        /// <summary>
        /// 缓存最大长度
        /// </summary>
        private UInt32 _queueMaxLength = UInt32.MaxValue;

        /// <summary>
        /// -1表示立即通知消费者，大于0表示产品放入缓存后的延迟通知时间（ms）。
        /// </summary>
        private Int32 _semephoreWatiTime = Timeout.Infinite;

        /// <summary>
        /// 处理线程
        /// </summary>
        private Thread _dataHandleThread = null;
        /// <summary>
        /// 线程名称
        /// </summary>
        private string _dataHandleThreadName = "缓存池处理线程";

        /// <summary>
        /// 信号量
        /// </summary>
        private Semaphore _cacheSemaphore = null;

        #endregion

        #region "Constructor"
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ProductCache()
        {
        }

        /// <summary>
        /// 终结函数
        /// </summary>
        ~ProductCache()
        {
            this.Dispose(false);
        }
        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="timeout">表示产品从放入缓存池到通知给消费的时延(ms)，零值表示立即通知。</param>
        public ProductCache(Int32 timeout)
        {
            if (timeout < 0)
            {
                throw new ArgumentException("产品从放入缓存池到通知给消费的时延值必须为一个非负值。");
            }

            this.TimeOut = timeout;
        }
        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="capacity">缓存池最大产品个数</param>
        /// <param name="timeout">表示产品从放入缓存池到通知给消费的时延(ms)，零值表示立即通知。</param>
        public ProductCache(UInt32 capacity, Int32 timeout)
        {
            if (timeout < 0)
            {
                throw new ArgumentException("产品从放入缓存池到通知给消费的时延值必须为一个非负值。");
            }

            this.Capacity = capacity;
            this.TimeOut = timeout;
        }
        #endregion

        #region "Properties"
        /// <summary>
        /// 一个事件，当有产品产生时引发
        /// </summary>
        public event EventHandler<ProductCreatedEventArgs<TProduct>> ProductCreated;

        /// <summary>
        /// 获取一个值，用于表示缓存池是否处于打开状态。
        /// </summary>
        public bool IsOpen
        {
            get { return _dataHandleThread != null; }
        }

        /// <summary>
        /// 获取缓存池中产品的个数
        /// </summary>
        public Int32 Count
        {
            get
            {
                if (_productQueue != null)
                {
                    return _productQueue.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 获取/设置一个值，用于表示缓存池中可存放产品的最大个数
        /// </summary>
        public UInt32 Capacity
        {
            get { return _queueMaxLength; }
            set { _queueMaxLength = value; }
        }
        /// <summary>
        /// 是否为延迟通知
        /// </summary>
        public bool DelayNotify
        {
            get { return _semephoreWatiTime > 0; }
        }

        /// <summary>
        /// 获取/设置一个值，用于表示产品从放入缓存池到通知给消费的时延(ms)。
        /// </summary>
        public Int32 TimeOut
        {
            get 
            { 
                if (_semephoreWatiTime == -1)
                {
                    return 0;
                }
                else
                {
                    return _semephoreWatiTime; 
                }
            }

            set
            {
                if (this.IsOpen)
                {
                    throw new InvalidOperationException("不可以打开之后设置此值。");
                }

                if (value == 0)
                {
                    _semephoreWatiTime = -1; 
                }
                else
                {
                    _semephoreWatiTime = value; 
                }
            }
        }

        /// <summary>
        /// 获取/设置一个值，用于表示数据处理线程的名称。
        /// </summary>
        public string ThreadName
        {
            get { return _dataHandleThreadName; }
            set { _dataHandleThreadName = (value != null) ? value : ""; }
        }
        #endregion

        #region "Override methods"
        #endregion

        #region "Private methods"
        /// <summary>
        /// 处理线程
        /// </summary>
        private void ThreadEntry()
        {
            try
            {
                while (true)
                {                    
                    _cacheSemaphore.WaitOne(_semephoreWatiTime);
                    
                    List<TProduct> products = this.DequeueAll();

                    if (products.Count > 0)
                    {
                        ProductCreatedEventArgs<TProduct> args = new ProductCreatedEventArgs<TProduct>(products);
                        NotifyEvent(args);
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }

        /// <summary>
        /// 通知事件
        /// </summary>
        private void NotifyEvent(ProductCreatedEventArgs<TProduct> args)
        {
            try
            {
                if (this.ProductCreated != null)
                {
                    var operators = this.ProductCreated.GetInvocationList();

                    foreach (var item in operators)
                    {
                        try
                        {
                            item.DynamicInvoke(this, args);
                        }
                        catch (System.Exception )
                        {
                        }
                    }
                }
            }
            catch (System.Exception )
            {

            }
        }
        
        /// <summary>
        /// 取出第一个产品
        /// </summary>
        /// <returns></returns>
        private TProduct DequeueHead()
        {
            lock (_productQueue.SyncRoot)
            {
                TProduct result = default(TProduct);

                if (_productQueue.Count > 0)
                {
                    result = _productQueue[0];
                    _productQueue.RemoveAt(0);
                }

                return result;
            }
        }

        /// <summary>
        /// 取出所有产品
        /// </summary>
        /// <returns></returns>
        private List<TProduct> DequeueAll()
        {
            lock (_productQueue.SyncRoot)
            {
                List<TProduct> result = new List<TProduct>();
                result.AddRange(_productQueue);

                // 清空队列
                _productQueue.Clear();

                // 清空信号量
                while (_cacheSemaphore.WaitOne(0)) ;

                return result;
            }
        }
        #endregion

        #region "Public methods"

        /// <summary>
        /// 打开缓存池
        /// </summary>
        public void Open()
        {
            // 检查是否打开
            if (this.IsOpen)
            {
                throw new ApplicationException("缓冲池已经打开");
            }

            // Create product queue.
            _productQueue = new ThreadSafetyList<TProduct>();

            // Create semaphore
            _cacheSemaphore = new Semaphore(0, Int32.MaxValue);            

            // Create work-thread
            _dataHandleThread = new Thread(new ThreadStart(ThreadEntry));
            _dataHandleThread.Name = _dataHandleThreadName;
            _dataHandleThread.IsBackground = true;
            _dataHandleThread.Start();
        }

        /// <summary>
        /// 关闭缓冲池
        /// </summary>
        public void Close()
        {
            ((IDisposable)this).Dispose();
        }

        /// <summary>
        /// 添加一个产品皮缓存池队首。
        /// </summary>
        /// <param name="product"></param>
        public void AddHead(TProduct product)
        {
            // 检查是否打开
            if (!this.IsOpen)
            {
                throw new ApplicationException("添加产品前必须先打开缓冲池");
            }

            if (this.Count >= this.Capacity)
            {
                throw new ApplicationException("产品缓冲池已满");
            }

            // Enqueue
            lock (_productQueue.SyncRoot)
            {
                _productQueue.Insert(0, product);

                if (!this.DelayNotify)
                {
                    _cacheSemaphore.Release();
                }
            }
        }

        /// <summary>
        /// 添加一个产品皮缓存池队尾。
        /// </summary>
        /// <param name="product"></param>
        public void AddTail(TProduct product)
        {
            // 检查是否打开
            if (!this.IsOpen)
            {
                return;
            }

            if (this.Count >= this.Capacity)
            {
                throw new ApplicationException("产品缓冲池已满");
            }

            // Enqueue
            lock (_productQueue.SyncRoot)
            {
                _productQueue.Add(product);

                if (!this.DelayNotify)
                {
                    _cacheSemaphore.Release();
                }
            }
        }

        /// <summary>
        /// 清空缓存池。
        /// </summary>
        /// <returns>返回清空前的元素。</returns>
        public List<TProduct> Clear()
        {
            return this.DequeueAll();
        }

        public TProduct GetAt(int index)
        {
            return _productQueue[index];
        }

        /// <summary>
        /// 返回缓存中的所有数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TProduct> GetData()
        {
            lock (_productQueue.SyncRoot)
            {
                return _productQueue.ToList();
            }
        }
        #endregion


        #region IDisposable 成员

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    try
                    {
                        _productQueue.Clear();

                        if (_dataHandleThread != null)
                        {
                            _dataHandleThread.Abort();
                        }

                        if (_cacheSemaphore != null)
                        {
                            _cacheSemaphore.Close();
                        }
                    }
                    catch (System.Exception )
                    {
                    }
                    finally
                    {
                        this.ProductCreated = null;
                        _dataHandleThread = null;
                        _cacheSemaphore = null;
                    }

                }

                _disposed = true;
            }
        }
        
        void IDisposable.Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
