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
    /// 一个状态，表示主动方正在等待OffsetEstimate消息。
    /// </summary>
    class TtsWaitingforEstimateState : TtsState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public TtsWaitingforEstimateState(TtsState preState)
            : base(preState)
        {

        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        protected override void HandleTtsEstimateFrame(SaiTtsFrameEstimate estimateFrame)
        {
            // 收到OffsetEst，停止计时器
            LogUtility.Info(string.Format("{0}: 收到OffsetEst报文，停止Tint_start计时器。", this.Context.RsspEP.ID));
            this.Context.StopHandshakeTimer();
            
            // 更新时间戳
            this.Calculator.ResMinOffset = estimateFrame.OffsetMin;
            this.Calculator.ResMaxOffset = estimateFrame.OffsetMax;

            // 检查估算值
            var valid = this.Calculator.IsEstimationValid();

            // 更新连接状态。
            this.Context.Connected = valid;

            // 发送OffsetEnd。
            var seq = (ushort)this.Context.SeqNoManager.GetAndUpdateSendSeq();
            var offsetEndFrame = new SaiTtsFrameOffsetEnd(seq,
                TripleTimestamp.CurrentTimestamp, TTS.RemoteLastSendTimestamp, TTS.LocalLastRecvTimeStamp, valid);
            this.Context.NextLayer.SendUserData(offsetEndFrame.GetBytes());

            // 
            if (valid)
            {
                LogUtility.Info(string.Format("{0}: 发送OffsetEnd，时钟偏移验证通过，SAI层连接成功。", this.Context.RsspEP.ID));
            }
            else
            {
                throw new Exception(string.Format("{0}: 时钟偏移估算无效，SAI层连接失败。", this.Context.RsspEP.ID));
            }

            // 更新状态。
            this.Context.CurrentState = new TtsConnectedState(this);
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
