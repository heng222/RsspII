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
    /// 一个状态，表示被动方正在等待OffsetAnswer2消息。
    /// </summary>
    class TtsWaitingforAnswer2State : TtsState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public TtsWaitingforAnswer2State(TtsState preState)
            : base(preState)
        {

        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        protected override void HandleTtsOffsetAnswer2Frame(Frames.SaiTtsFrameOffsetAnswer2 answer2)
        {
            // 收到Answer2，停止计时器
            this.Context.StopHandshakeTimer();
            LogUtility.Info(string.Format("{0}: 收到OffsetAnswer2报文，停止Tres_start计时器。",
                this.Context.RsspEP.ID));

            // 更新时间戳
            this.Calculator.ResTimestamp3 = this.TTS.LocalLastRecvTimeStamp;
            this.Calculator.InitTimestamp2 = answer2.SenderLastRecvTimestamp;
            this.Calculator.InitTimestamp3 = answer2.SenderTimestamp;

            // 应答方计算时钟偏移
            this.Calculator.EstimateResOffset();
            LogUtility.Info(string.Format("{0}: 应答方估算的时钟偏移，OffsetMin = {1}, OffsetMax = {2}",
                this.Context.RsspEP.ID, this.Calculator.ResMinOffset, this.Calculator.ResMaxOffset));


            // 发送OffsetEst
            var seq = (ushort)this.Context.SeqNoManager.GetAndUpdateSendSeq();
            var estimateFrame = new SaiTtsFrameEstimate(seq,
                TripleTimestamp.CurrentTimestamp, TTS.RemoteLastSendTimestamp, TTS.LocalLastRecvTimeStamp, 
                this.Calculator.ResMinOffset,
                this.Calculator.ResMaxOffset);

            this.Context.NextLayer.SendUserData(estimateFrame.GetBytes()); 
            LogUtility.Info(string.Format("{0}: 发送OffsetEst。", this.Context.RsspEP.ID));


            // 启动计时器，等待OffsetEnd报文。
            LogUtility.Info(string.Format("{0}: 启动计时器，等待OffsetEnd报文。",
                this.Context.RsspEP.ID));
            this.Context.StartHandshakeTimer();

            // 更新状态。
            this.Context.CurrentState = new TtsWaitingforEndState(this);
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
