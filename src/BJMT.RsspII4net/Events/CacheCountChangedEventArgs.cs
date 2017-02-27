using System;

namespace BJMT.RsspII4net.Events
{
    /// <summary>
    /// 一个事件参数类，当缓存中元素个数发生改变时使用。
    /// </summary>
    public abstract class CacheCountChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="name">缓存的名称。</param>
        /// <param name="count">缓存中的个数。</param>
        public CacheCountChangedEventArgs(string name, int count)
        {
            this.Name = name;
            this.Count = count;
        }

        /// <summary>
        /// 获取/设置一个值，用于表示缓存的名称。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 获取/设置一个值，用于表示缓存中当前的元素个数。
        /// </summary>
        public int Count { get; private set; }
    }

    /// <summary>
    /// 一个事件参数类，当发送缓存中元素个数发生改变时使用。
    /// </summary>
    public class OutgoingCacheCountChangedEventArgs : CacheCountChangedEventArgs
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="name">缓存的名称。</param>
        /// <param name="count">缓存中的个数。</param>
        public OutgoingCacheCountChangedEventArgs(string name, int count)
            : base(name, count)
        {
        }
    }

    /// <summary>
    /// 一个事件参数类，当接收缓存中元素个数发生改变时使用。
    /// </summary>
    public class IncomingCacheCountChangedEventArgs : CacheCountChangedEventArgs
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="name">缓存的名称。</param>
        /// <param name="count">缓存中的个数。</param>
        public IncomingCacheCountChangedEventArgs(string name, int count)
            : base(name, count)
        {
        }
    }

}
