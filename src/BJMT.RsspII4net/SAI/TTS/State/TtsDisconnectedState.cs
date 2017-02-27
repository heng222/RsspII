/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-16 11:25:09 
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
    /// 表示主动方处理TTS断开状态。
    /// </summary>
    class TtsDisconnectedState : TtsState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public TtsDisconnectedState(ISaiStateContext context, TtsDefenseStrategy strategy)
            : base(context, strategy)
        {

        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"

        public override void HandleMaslConnected()
        {
            // 发送OffsetStart
            var seqNo = (ushort)this.Context.SeqNoManager.GetAndUpdateSendSeq();
            var offsetStart = new SaiTtsFrameOffsetStart(seqNo, TripleTimestamp.CurrentTimestamp, 0);
            this.Context.NextLayer.SendUserData(offsetStart.GetBytes());

            // 记录日志
            LogUtility.Info(string.Format("{0}: 发送OffsetStart。", this.Context.RsspEP.ID));

            // 更新时间戳
            this.Calculator.InitTimestamp1 = offsetStart.SenderTimestamp;

            // 启动计时器
            LogUtility.Info(string.Format("{0}: 启动Tint_start计时器，等待OffsetAnswer1报文",
                this.Context.RsspEP.ID));
            this.Context.StartHandshakeTimer();

            // 更新状态。
            this.Context.CurrentState = new TtsWaitingforAnswer1State(this);
        }

        public override void SendUserData(OutgoingPackage package)
        {
            // Do nothing.
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
