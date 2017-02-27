/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-16 11:16:37 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using BJMT.RsspII4net.SAI.EC;
using BJMT.RsspII4net.SAI.EC.Frames;
using BJMT.RsspII4net.SAI.EC.State;
using BJMT.RsspII4net.SAI.TTS;
using BJMT.RsspII4net.SAI.TTS.Frames;
using BJMT.RsspII4net.SAI.TTS.State;

namespace BJMT.RsspII4net.SAI
{
    /// <summary>
    /// 一个无效状态，仅在被动方使用。
    /// 当被动方收到EcStart或OffsetStart时将切换到一个有效的状态。
    /// </summary>
    class SaiInvalidState : SaiState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public SaiInvalidState(ISaiStateContext context)
            : base(context)
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
            // 启动握手超时计时器，等待EcStart1或TtsOffsetStart。
            this.Context.StartHandshakeTimer();
        }

        protected override void HandleEcFrame(SaiEcFrame ecFrame)
        {
            if (ecFrame.FrameType == SaiFrameType.EC_Start)
            {
                this.Context.RsspEP.DefenseTech = MessageDelayDefenseTech.EC;

                var strategy = new EcDefenseStrategy(this.Context.RsspEP.LocalID, this.Context.RsspEP.EcInterval);
                this.Context.DefenseStrategy = strategy;

                this.Context.CurrentState = new EcWaitingforStart1State(this.Context, strategy);
                this.Context.CurrentState.HandleFrame(ecFrame);
            }
            else
            {
                throw new Exception("SaiInvalideState状态时，收到的第一条帧不是ECStart。");
            }
        }

        protected override void HandleTtsFrame(SaiTtsFrame ttsFrame)
        {
            if (ttsFrame.FrameType == SaiFrameType.TTS_OffsetStart)
            {
                this.Context.RsspEP.DefenseTech = MessageDelayDefenseTech.TTS;

                var strategy = new TtsDefenseStrategy(this.Context.FrameTransport, false);
                this.Context.DefenseStrategy = strategy;

                this.Context.CurrentState = new TtsWaitingforStartState(this.Context, strategy);
                this.Context.CurrentState.HandleFrame(ttsFrame);
            }
            else
            {
                throw new Exception("SaiInvalideState状态时，收到的第一条帧不是OffsetStart。");
            }
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
