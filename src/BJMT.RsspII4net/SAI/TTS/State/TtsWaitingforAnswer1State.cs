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
    /// 一个状态，表示主动方正在等待OffsetAnswer1消息。
    /// </summary>
    class TtsWaitingforAnswer1State : TtsState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public TtsWaitingforAnswer1State(TtsState preState)
            : base(preState)
        {

        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        protected override void  HandleTtsOffsetAnswer1Frame(SaiTtsFrameOffsetAnswer1 answer1)
        {
            // 收到Answer1，停止计时器
            LogUtility.Info(string.Format("{0}: 收到的OffsetAnswer1，停止Tint_start计时器。", this.Context.RsspEP.ID));
            this.Context.StopHandshakeTimer();

            // 更新时间戳
            this.Calculator.InitTimestamp2 = this.TTS.LocalLastRecvTimeStamp;
            this.Calculator.ResTimestamp1 = answer1.SenderLastRecvTimestamp;
            this.Calculator.ResTimestamp2 = answer1.SenderTimestamp;

            // 发起方计算偏移
            this.Calculator.EstimateInitOffset();

            // 记录日志
            LogUtility.Info(string.Format("{0}: 发起方估算时钟偏移，OffsetMin = {1}, OffsetMax = {2}",
                this.Context.RsspEP.ID, this.Calculator.InitiatorMinOffset, this.Calculator.InitiatorMaxOffset));

            // 发送OffsetAnswer2报文
            var seq = (ushort)this.Context.SeqNoManager.GetAndUpdateSendSeq();
            var answer2 = new SaiTtsFrameOffsetAnswer2(seq,
                TripleTimestamp.CurrentTimestamp, 
                this.TTS.RemoteLastSendTimestamp, 
                this.TTS.LocalLastRecvTimeStamp);

            this.Context.NextLayer.SendUserData(answer2.GetBytes());
            
            LogUtility.Info(string.Format("{0}: 发送OffsetAnswer2。", this.Context.RsspEP.ID));


            // 启动计时器,等待OffsetEst报文
            LogUtility.Info(string.Format("{0}: 启动Tint_start计时器，等待OffsetEst报文",  this.Context.RsspEP.ID));
            this.Context.StartHandshakeTimer(); 

            // 更新状态。
            this.Context.CurrentState = new TtsWaitingforEstimateState(this);
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
