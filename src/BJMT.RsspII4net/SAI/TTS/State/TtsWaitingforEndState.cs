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
    /// 一个状态，表示被动方正在等待OffsetEnd消息。
    /// </summary>
    class TtsWaitingforEndState : TtsState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public TtsWaitingforEndState(TtsState preState)
            : base(preState)
        {

        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        protected override void HandleTtsOffsetEndFrame(SaiTtsFrameOffsetEnd offsetEndFrame)
        {
            // 收到OffsetEnd，停止Tres_start计时器
            LogUtility.Info(string.Format("{0}: 收到OffsetEnd报文，停止Tres_start计时器。", this.Context.RsspEP.ID));
            this.Context.StopHandshakeTimer();

            if (offsetEndFrame.Valid)
            {
                LogUtility.Info(string.Format("{0}: 时钟偏移估算结果有效，SAI层连接成功。", this.Context.RsspEP.ID));
            }
            else
            {
                LogUtility.Info(string.Format("{0}: 时钟偏移估算结果无效，无法建立SAI连接。", this.Context.RsspEP.ID));
            }

            // 
            this.Context.Connected = offsetEndFrame.Valid;

            if (!offsetEndFrame.Valid)
            {
                throw new Exception(string.Format("{0}: 时钟偏移估算结果无效，无法建立SAI连接。", this.Context.RsspEP.ID));
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
