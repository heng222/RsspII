/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BPL
//
// 创 建 人：zhh_217
// 创建日期：09/08/2011 14:32:29 
// 邮    箱：zhh_217@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司 2009-2015 保留所有权利
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace BJMT.RsspII4net.SAI.TTS
{
    /// <summary>
    /// 三重时间戳
    /// </summary>
    class TripleTimestamp
    {
        #region "Filed"

        private ITripleTimestampObserver _observer = null;
        #endregion

        #region "Constructor"
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="observer"></param>
        public TripleTimestamp(ITripleTimestampObserver observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException();
            }

            _observer = observer;
        }
        #endregion

        #region "Properties"
        /// <summary>
        /// 获取当前的时间戳
        /// 
        /// 1. Environment.TickCount: 该属性的值从系统计时器派生，并以 32 位有符号整数的形式存储。
        ///    因此，如果系统连续运行，TickCount 将在约 24.9 天内从零递增至 Int32.MaxValue，然后
        ///    跳至 Int32.MinValue（这是一个负数），再在接下来的 24.9 天内递增至零。
        /// 
        /// 2. TickCount属性的分辨率小于 500 毫秒。
        /// 
        /// 参见："http://msdn.microsoft.com/zh-cn/library/system.environment.tickcount(v=vs.80).aspx?cs-save-lang=1&amp;cs-lang=csharp#code-snippet-1
        /// 注：如果TTS的时间戳使用8个字节表示，则可以使用DateTime.Now.Ticks(long)/10000。
        /// </summary>
        public static UInt32 CurrentTimestamp { get { return (UInt32)Environment.TickCount / 10; } }

        /// <summary>
        /// 获取一个值，用于对方上一次传送给本方的时间戳。
        /// </summary>
        public UInt32 RemoteLastSendTimestamp { get; private set; }

        /// <summary>
        /// 获取一个值，用于表示上一次从对方接收到消息时的时间戳。
        /// </summary>
        public UInt32 LocalLastRecvTimeStamp { get; private set; }
        
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        /// <summary>
        /// 复位
        /// </summary>
        public void Reset()
        {
            this.RemoteLastSendTimestamp = 0;
            this.LocalLastRecvTimeStamp = 0;
        }

        /// <summary>
        /// 更新“上一次接收方时间戳”。
        /// </summary>
        /// <param name="newValue">新的时间戳。</param>
        public void UpdateRemoteLastSendTimestamp(uint newValue)
        {
            // 如果发送方上一次的时间戳大于新的时间戳，则说明发生了“过零点”现象。
            if (this.RemoteLastSendTimestamp > newValue)
            {
                _observer.OnTimestampZeroPassed(newValue, this.RemoteLastSendTimestamp);
            }

            this.RemoteLastSendTimestamp = newValue; 
        }

        /// <summary>
        /// 更新“上一次收到消息时的时间戳”。
        /// </summary>
        /// <param name="newValue">新的时间戳。</param>
        public void UpdateLocalLastRecvTimeStamp(uint newValue)
        {
            this.LocalLastRecvTimeStamp = newValue;
        }

        public override string ToString()
        {
            var sb = new StringBuilder(200);

            sb.AppendFormat("当前时间戳={0}，上一次接收方时间戳={1}，上一次收到消息时的时间戳={2}。\r\n",
                    TripleTimestamp.CurrentTimestamp, this.RemoteLastSendTimestamp, this.LocalLastRecvTimeStamp);

            return sb.ToString();
        }
        #endregion

    }
}
