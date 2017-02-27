/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-15 15:21:08 
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

namespace BJMT.RsspII4net.SAI.TTS.State
{
    /// <summary>
    /// 一个状态，表示被动方正在等待OffsetStart消息。
    /// </summary>
    class TtsWaitingforStartState : TtsState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public TtsWaitingforStartState(ISaiStateContext context, TtsDefenseStrategy strategy)
            : base(context, strategy)
        {

        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        protected override void HandleTtsOffsetStartFrame(SaiTtsFrameOffsetStart startFrame)
        {
            // 收到OffsetStart，停止握手超时计时器。
            LogUtility.Info(string.Format("{0}: 收到的OffsetStart报文. 停止Tres_start计时器。",
                this.Context.RsspEP.ID));
            this.Context.StopHandshakeTimer();

            // 发送OffsetAnswer1
            var seqNo = (ushort)this.Context.SeqNoManager.GetAndUpdateSendSeq();

            var offsetAnswer1 = new SaiTtsFrameOffsetAnswer1(seqNo,
                TripleTimestamp.CurrentTimestamp, 
                this.TTS.RemoteLastSendTimestamp,
                this.TTS.LocalLastRecvTimeStamp, 0);

            this.Context.NextLayer.SendUserData(offsetAnswer1.GetBytes());            
            LogUtility.Info(string.Format("{0}: 发送OffsetAnswer1.", this.Context.RsspEP.ID));

            // 更新时间戳
            this.Calculator.ResTimestamp2 = offsetAnswer1.SenderTimestamp;

            // 启动Tres_start计时器，等待OffsetAnswer2报文。
            LogUtility.Info(string.Format("{0}: 启动Tres_start计时器，等待OffsetAnswer2报文。", this.Context.RsspEP.ID));
            this.Context.StartHandshakeTimer();

            // 更新状态。
            this.Context.CurrentState = new TtsWaitingforAnswer2State(this);
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
