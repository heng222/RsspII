/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BPL
//
// 创 建 人：zhh_217
// 创建日期：09/08/2011 11:30:44 
// 邮    箱：zhh_217@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;

namespace BJMT.RsspII4net.SAI.TTS
{
    /// <summary>
    /// 时间偏移计算器，所有参数单位均为10ms。
    /// </summary>
    class TimeOffsetCalculator
    {
        #region "Filed"
        /// <summary>
        /// 是否为发起方。true表示发起方，false表示被动方。
        /// </summary>
        private bool _isInitiator;

        #endregion

        #region "Constructor"
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isInitiator">true表示为发起方，false表示为应答方</param>
        /// <param name="extraDelay">发送方子系统处理应用数据的附加延迟的总和</param>
        /// <param name="maxOffset">允许的最大时钟偏差。</param>
        public TimeOffsetCalculator(bool isInitiator, ushort extraDelay, uint maxOffset)
        {
            _isInitiator = isInitiator;

            this.ExtraDelay = extraDelay;
            this.MaxOffset = maxOffset;
        }
        #endregion

        #region "Properties"
        /// <summary>
        /// 获取/设置发起方与应答方时钟偏差估算值之间的最大差异数。
        /// </summary>
        public UInt32 MaxOffset { get; private set; }

        /// <summary>
        /// 获取/设置发送方处理应用数据的延迟时间。
        /// </summary>
        public ushort ExtraDelay { get; private set; }

        /// <summary>
        /// 获取/设置发起方第一个时间戳。
        /// </summary>
        public UInt32 InitTimestamp1 { get; set; }
        /// <summary>
        /// 获取/设置发起方第二个时间戳。
        /// </summary>
        public UInt32 InitTimestamp2 { get; set; }
        /// <summary>
        /// 获取/设置发起方第三个时间戳。
        /// </summary>
        public UInt32 InitTimestamp3 { get; set; }


        /// <summary>
        /// 获取/设置应答方第一个时间戳。
        /// </summary>
        public UInt32 ResTimestamp1 { get; set; }
        /// <summary>
        /// 获取/设置应答方第二个时间戳。
        /// </summary>
        public UInt32 ResTimestamp2 { get; set; }
        /// <summary>
        /// 获取/设置应答方第三个时间戳。
        /// </summary>
        public UInt32 ResTimestamp3 { get; set; }

        /// <summary>
        /// 获取发起方估算的最大偏移值。
        /// </summary>
        public Int64 InitiatorMaxOffset { get; private set; }
        /// <summary>
        /// 获取发起方估算的最小偏移值。
        /// </summary>
        public Int64 InitiatorMinOffset { get; private set; }


        /// <summary>
        /// 获取/设置应答方估算的最大偏移值。
        /// </summary>
        public Int64 ResMaxOffset { get; set; }
        /// <summary>
        /// 获取/设置应答方估算的最小偏移值。
        /// </summary>
        public Int64 ResMinOffset { get; set; }

        #endregion

        #region "Private methods"

        /// <summary>
        /// 将发送方的时间戳转换为接收方的时间戳。
        /// </summary>
        /// <param name="senderTimestamp">发起方时间戳</param>
        /// <returns>发起方时间戳对应的接收方时间戳</returns>
        private Int64 CovertTimestamp(UInt32 senderTimestamp)
        {
            if (_isInitiator)
            {
                return (Int64)senderTimestamp - (Int64)this.ExtraDelay + this.InitiatorMinOffset;
            }
            else
            {
                return (Int64)senderTimestamp - (Int64)this.ExtraDelay + this.ResMinOffset;
            }
        }
        #endregion

        #region "Public methods"
        /// <summary>
        /// 发起方进行时钟偏移估算
        /// </summary>
        public void EstimateInitOffset()
        {
            this.InitiatorMaxOffset = (Int64)this.InitTimestamp2 - (Int64)this.ResTimestamp2;
            this.InitiatorMinOffset = (Int64)this.InitTimestamp1 - (Int64)this.ResTimestamp1;
            
            LogUtility.Info(string.Format("发起方进行时钟偏移估算：minOffset = {0}, maxOffset = {1}",
                this.InitiatorMinOffset, this.InitiatorMaxOffset));
        }

        /// <summary>
        /// 应答方进行时钟偏移估算
        /// </summary>
        public void EstimateResOffset()
        {
            this.ResMaxOffset = (Int64)this.ResTimestamp3 - (Int64)this.InitTimestamp3;
            this.ResMinOffset = (Int64)this.ResTimestamp2 - (Int64)this.InitTimestamp2;

            LogUtility.Info(string.Format("应答方进行时钟偏移估算：minOffset = {0}, maxOffset = {1}",
                this.ResMinOffset, this.ResMaxOffset));
        }

        /// <summary>
        /// 发起方判断估算的偏移值是否有效
        /// </summary>
        /// <returns>true表示时钟偏移估算值有效，否则表示无效。</returns>
        public bool IsEstimationValid()
        {
            var value1 = Math.Abs(this.InitiatorMaxOffset + this.ResMinOffset);
            var value2 = Math.Abs(this.InitiatorMinOffset + this.ResMaxOffset);

            LogUtility.Info(string.Format("|Tinit_offset_max + Tres_offset_min| = {0}", value1));
            LogUtility.Info(string.Format("|Tinit_offset_min + Tres_offset_max| = {0}, Toffset_max = {1}", 
                value2, this.MaxOffset));

            return (value1 == 0) && (value2 < this.MaxOffset);
        }

        /// <summary>
        /// 更新时钟偏移
        /// </summary>
        /// <param name="minOffset">最小偏移值</param>
        /// <param name="maxOffset">最大偏移值</param>
        public void UpdateOffset(long minOffset, long maxOffset)
        {
            if (_isInitiator)
            {
                this.InitiatorMinOffset = minOffset;
                this.InitiatorMaxOffset = maxOffset;
            }
            else
            {
                this.ResMinOffset = minOffset;
                this.ResMaxOffset = maxOffset;
            }
        }

        /// <summary>
        /// 计算消息的延迟时间
        /// </summary>
        /// <param name="localCurrentTime">本地的当前时间戳。</param>
        /// <param name="remoteSendTime">对方的发送时间戳。</param>
        /// <returns>消息的时延</returns>
        public Int64 CalcTimeDelay(UInt32 localCurrentTime, UInt32 remoteSendTime)
        {
            var delay = (Int64)localCurrentTime - CovertTimestamp(remoteSendTime);

#if DEBUG
            if (delay < this.ExtraDelay)
            {
                LogUtility.Warn(String.Format("Delay = {0}, ReceiverCurrentTime = {1}, SenderTime = {2}\r\n",
                    delay, localCurrentTime, remoteSendTime));

                if (_isInitiator)
                {
                    LogUtility.Warn(String.Format("发起方使用的最小时钟偏移 = {0}\r\n", InitiatorMinOffset));
                }
                else
                {
                    LogUtility.Warn(String.Format("应答方使用的最小时钟偏移 = {0}\r\n", ResMinOffset));
                }
            }
#endif

            // 修正时延
            if (delay < this.ExtraDelay)
            {
                delay = this.ExtraDelay;
            }

            return delay;
        }
        #endregion

    }
}
