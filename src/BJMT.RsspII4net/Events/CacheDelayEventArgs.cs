/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BPL
//
// 创 建 人：zhangheng
// 创建日期：2016-6-20 14:12:45 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司 2009，保留所有权利
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace BJMT.RsspII4net.Events
{
    /// <summary>
    /// 一个事件参数类，当缓存发生阻塞时使用。
    /// </summary>
    public abstract class CacheDelayEventArgs : EventArgs
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        /// <summary>
        /// 无参构造函数。
        /// </summary>
        public CacheDelayEventArgs()
        {

        }

        /// <summary>
        /// 带参数的构造函数。
        /// </summary>
        /// <param name="name">队列的名称。</param>
        /// <param name="count">队列中的个数。</param>
        /// <param name="threshold">阈值。</param>
        /// <param name="farthestTime">最远的时间戳。</param>
        public CacheDelayEventArgs(string name, int count, int threshold, DateTime farthestTime)
        {
            this.Name = name;
            this.Count = count;
            this.Threshold = threshold;
            this.FarthestTimeStamp = farthestTime;
        }
        #endregion

        #region "Properties"

        /// <summary>
        /// 获取/设置一个值，用于表示队列的名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 队列中的元素个数。
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 获取/设置一个值，用于表示个数的检查值。
        /// </summary>
        public int Threshold { get; set; }

        /// <summary>
        /// 最远的时间戳。
        /// </summary>
        public DateTime FarthestTimeStamp { get; set; }
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }

    /// <summary>
    /// 一个事件参数类，当发送缓存发生延迟时使用。
    /// </summary>
    public class OutgoingCacheDelayedEventArgs : CacheDelayEventArgs
    {
        /// <summary>
        /// 带参数的构造函数。
        /// </summary>
        /// <param name="name">队列的名称。</param>
        /// <param name="count">队列中的个数。</param>
        /// <param name="threshold">阈值。</param>
        /// <param name="farthestTime">最远的时间戳。</param>
        public OutgoingCacheDelayedEventArgs(string name, int count, int threshold, DateTime farthestTime)
            :base(name, count, threshold, farthestTime)
        {
        }
    }

    /// <summary>
    /// 一个事件参数类，当接收缓存发生延迟时使用。
    /// </summary>
    public class IncomingCacheDelayedEventArgs : CacheDelayEventArgs
    {
        /// <summary>
        /// 带参数的构造函数。
        /// </summary>
        /// <param name="name">队列的名称。</param>
        /// <param name="count">队列中的个数。</param>
        /// <param name="threshold">阈值。</param>
        /// <param name="farthestTime">最远的时间戳。</param>
        public IncomingCacheDelayedEventArgs(string name, int count, int threshold, DateTime farthestTime)
            : base(name, count, threshold, farthestTime)
        {
        }
    }
}
