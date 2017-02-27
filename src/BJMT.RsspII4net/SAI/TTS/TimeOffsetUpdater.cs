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
using BJMT.RsspII4net.SAI.Events;
using BJMT.RsspII4net.SAI.TTS.Frames;

namespace BJMT.RsspII4net.SAI.TTS
{
    /// <summary>
    /// 时钟偏移更新器的观察器接口。
    /// </summary>
    interface ITimeOffsetUpdaterObserver
    {
        uint RemoteLastSendTimestamp { get; }

        uint LocalLastRecvTimeStamp { get; }

        void OnTimeOffsetUpdated(Int64 minOffset, Int64 maxOffset);
    }

    /// <summary>
    /// 时钟偏移更新器。
    /// </summary>
    class TimeOffsetUpdater : IDisposable
    {
        /// <summary>
        /// 时钟偏移修正的最小间隔。（单位：秒）
        /// </summary>
        private const byte MinInterval = 10;

        #region "Filed"

        private bool _disposed = false;

        /// <summary>
        /// 时钟偏移更新计时器
        /// </summary>
        private System.Timers.Timer _timer = null;

        private ITimeOffsetUpdaterObserver _observer = null;

        private ISaiFrameTransport _ttsFrameTransport = null;

        /// <summary>
        /// 上一次发送修正请求报文时的时间戳。
        /// </summary>
        private uint _lastRequestTimestamp;

        /// <summary>
        /// 上一次回应时钟修正报文的时间。
        /// </summary>
        private DateTime _lastResponseTime;

        #endregion

        #region "Constructor"
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="frameTransport"></param>
        /// <param name="observer"></param>
        /// <param name="interval">TTS更新周期（单位：秒）</param>
        public TimeOffsetUpdater(ISaiFrameTransport frameTransport, ITimeOffsetUpdaterObserver observer, int interval)
        {
            if (interval < MinInterval)
            {
                throw new ArgumentException(string.Format("TTS更新周期不能小于{0}秒。", MinInterval));
            }

            _ttsFrameTransport = frameTransport;
            _ttsFrameTransport.SaiFrameReceived += OnSaiFrameReceived;

            _observer = observer;

            this.CreateTimer(interval);

            this.StartTimer();
        }

        ~TimeOffsetUpdater()
        {
            Dispose(false);
        }
        #endregion

        #region "Properties"
        #endregion

        #region "Override methods"
        #endregion

        #region "Private methods"
        private void CreateTimer(int interval)
        {
            _timer = new System.Timers.Timer();
            _timer.Interval = interval * 1000;
            _timer.AutoReset = true;
            _timer.Elapsed += OnTimerElapsed;
        }

        private void CloseTimer()
        {
            if (_timer != null)
            {
                _timer.Close();
                _timer = null;
            }
        }

        private void StartTimer()
        {
            if (_timer != null)
            {
                _timer.Start();
            }
        }

        private void StopTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
            }
        }

        /// <summary>
        /// 更新计时器超时
        /// </summary>
        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                // 创建更新请求帧，发到对方。
                this.SendRequestFrame();
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }

        private void SendRequestFrame()
        {
            var seq = (ushort)_ttsFrameTransport.NextSendSeq();
            
            // 记录请求时间。
            _lastRequestTimestamp = TripleTimestamp.CurrentTimestamp;

            var reqFrame = new SaiTtsFrameAppData(seq,
                _lastRequestTimestamp,
                _observer.RemoteLastSendTimestamp,
                _observer.LocalLastRecvTimeStamp,
                null);

            _ttsFrameTransport.SendSaiFrame(reqFrame);
        }

        private void SendResponseFrame()
        {
            var seq = (ushort)_ttsFrameTransport.NextSendSeq();

            // 
            var reqFrame = new SaiTtsFrameAppData(seq,
                TripleTimestamp.CurrentTimestamp,
                _observer.RemoteLastSendTimestamp,
                _observer.LocalLastRecvTimeStamp,
                null);

            _ttsFrameTransport.SendSaiFrame(reqFrame);
        }

        private void OnSaiFrameReceived(object sender, SaiFrameIncomingEventArgs e)
        {
            try
            {
                if (e.Frame.FrameType != SaiFrameType.TTS_AppData)
                {
                    return;
                }

                var ttsAppFrame = e.Frame as SaiTtsFrameAppData;
                
                // 如果没有UserData，则说明是TTS时钟偏移修正请求或响应报文。
                if (ttsAppFrame.UserData != null)
                {
                    return;
                }

                // 检查收到的帧是否为响应报文。
                var isResponse = (ttsAppFrame.ReceiverLastSendTimestamp == _lastRequestTimestamp);

                if (isResponse)
                {
                    this.HandleResponseFrame(ttsAppFrame, TripleTimestamp.CurrentTimestamp);
                }
                else
                {
                    // 收到时钟偏移更新请求，则回复“更新应答”
                    var isRequest = (DateTime.Now - _lastResponseTime).TotalSeconds;
                    if (isRequest > MinInterval)
                    {
                        _lastResponseTime = DateTime.Now;

                        this.SendResponseFrame();
                    }
                    else
                    {
                        // 与上次发送响应的时间差值小于最小更新时间，不回应。
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 处理时钟偏移修正响应信息
        /// </summary>
        /// <param name="offsetRsp">响应报文</param>
        /// <param name="currentTimestamp">收到响应报文时的时间戳</param>
        private void HandleResponseFrame(SaiTtsFrame offsetRsp, UInt32 currentTimestamp)
        {
            try
            {
                long minOffset = (long)offsetRsp.ReceiverLastSendTimestamp - (long)offsetRsp.SenderLastRecvTimestamp;
                long maxOffset = (long)currentTimestamp - (long)offsetRsp.SenderTimestamp;

                // Notify observer
                _observer.OnTimeOffsetUpdated(minOffset, maxOffset);
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }
        #endregion

        #region "Public methods"

        /// <summary>
        /// 立即进行时钟偏移修正
        /// </summary>
        public void UpdateClockOffset()
        {
            this.StopTimer();

            this.SendRequestFrame();

            this.StartTimer();
        }
        #endregion


        #region IDisposable 成员

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_ttsFrameTransport != null)
                    {
                        _ttsFrameTransport.SaiFrameReceived -= OnSaiFrameReceived;
                    }

                    CloseTimer();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
