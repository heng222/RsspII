/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-16 13:00:02 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.SAI.TTS.Frames;

namespace BJMT.RsspII4net.SAI.TTS
{
    /// <summary>
    /// TTS防御策略。
    /// </summary>
    class TtsDefenseStrategy : DefenseStrategy, ITripleTimestampObserver, ITimeOffsetUpdaterObserver
    {
        #region "TTS技术参数"

        /// <summary>
        /// 发起方与应答方时钟偏差估算值之间的最大差异数。（单位：10ms），默认值50。
        /// </summary>
        public const UInt32 MaxDifference = 50;

        /// <summary>
        /// 附加处理延迟的估计值（单位：10ms），默认值3，表示30ms
        /// </summary>
        public const UInt16 ExtraDelay = 3;

        /// <summary>
        /// 时钟偏移更新周期。（单位：秒），默认值300秒，即5分钟。
        /// </summary>
        public const UInt16 TimeOffserUpdateInterval = 300;

        #endregion

        #region "Filed"
        private bool _disposed = false;

        private TimeOffsetUpdater _timeOffsetUpdater;
        #endregion

        #region "Constructor"
        public TtsDefenseStrategy(ISaiFrameTransport frameTransport, bool isInitiator)
        {
            this.Calculator = new TimeOffsetCalculator(isInitiator, ExtraDelay, MaxDifference);

            this.LocalTts = new TripleTimestamp(this);

            _timeOffsetUpdater = new TimeOffsetUpdater(frameTransport, this, TimeOffserUpdateInterval);
        }
        #endregion

        #region "Properties"

        /// <summary>
        /// 本地三重时间戳对象。
        /// </summary>
        public TripleTimestamp LocalTts { get; private set; }

        /// <summary>
        /// 时钟偏移计算器。
        /// </summary>
        public TimeOffsetCalculator Calculator { get; private set; }
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"

        protected override long CalcTtsTimeDelay(SaiTtsFrame ttsFrame) 
        {
            return this.Calculator.CalcTimeDelay(TripleTimestamp.CurrentTimestamp, ttsFrame.SenderTimestamp);
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                if (disposing)
                {
                    if (_timeOffsetUpdater != null)
                    {
                        _timeOffsetUpdater.Dispose();
                        _timeOffsetUpdater = null;
                    }
                }

                base.Dispose(disposing);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder(200);

            sb.AppendFormat("TTS防御，{0}\r\n", this.LocalTts);

            return sb.ToString();
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"

        #endregion


        #region "TripleTimestamp接口"
        void ITripleTimestampObserver.OnTimestampZeroPassed(uint latestTimestamp, uint lastTimestamp)
        {
            // 过零点时进行TTS时钟偏移修正。
            _timeOffsetUpdater.UpdateClockOffset();
        }
        #endregion

        #region "ITimeOffsetUpdater"
        uint ITimeOffsetUpdaterObserver.RemoteLastSendTimestamp { get { return this.LocalTts.RemoteLastSendTimestamp; } }

        uint ITimeOffsetUpdaterObserver.LocalLastRecvTimeStamp { get { return this.LocalTts.LocalLastRecvTimeStamp; } }

        void ITimeOffsetUpdaterObserver.OnTimeOffsetUpdated(long minOffset, long maxOffset)
        {
            this.Calculator.UpdateOffset(minOffset, maxOffset);
        }
        #endregion
    }
}
