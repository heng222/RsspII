/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-15 15:30:31 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Diagnostics;
using System.Linq;
using BJMT.RsspII4net.SAI.TTS.Frames;

namespace BJMT.RsspII4net.SAI.TTS.State
{
    abstract class TtsState : SaiState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        protected TtsState(TtsState preState)
            : base(preState.Context)
        {
            this.DefenseStrategy = preState.DefenseStrategy;
        }

        protected TtsState(ISaiStateContext context, TtsDefenseStrategy strategy)
            : base(context)
        {
            this.DefenseStrategy = strategy;
        }
        #endregion

        #region "Properties"
        protected TtsDefenseStrategy DefenseStrategy { get; set; }

        protected TripleTimestamp TTS { get { return this.DefenseStrategy.LocalTts; } }
        protected TimeOffsetCalculator Calculator { get { return this.DefenseStrategy.Calculator; } }
        #endregion

        #region "Virtual methods"
        protected virtual void HandleTtsOffsetStartFrame(SaiTtsFrameOffsetStart frame)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }

        protected virtual void HandleTtsOffsetAnswer1Frame(SaiTtsFrameOffsetAnswer1 frame)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }

        protected virtual void HandleTtsOffsetAnswer2Frame(SaiTtsFrameOffsetAnswer2 frame)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }

        protected virtual void HandleTtsEstimateFrame(SaiTtsFrameEstimate frame)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }

        protected virtual void HandleTtsOffsetEndFrame(SaiTtsFrameOffsetEnd frame)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }

        protected virtual void HandleTtsAppFrame(SaiTtsFrameAppData frame)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }
        #endregion

        #region "Override methods"

        public override void HandleMaslConnected()
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }

        public override void SendUserData(OutgoingPackage package)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }

        protected override void HandleTtsFrame(SaiTtsFrame ttsFrame)
        {
            // 更新TTS
            this.TTS.UpdateRemoteLastSendTimestamp(ttsFrame.SenderTimestamp);
            this.TTS.UpdateLocalLastRecvTimeStamp(TripleTimestamp.CurrentTimestamp);

            // 分类消息。
            if (ttsFrame.FrameType == SaiFrameType.TTS_OffsetStart)
            {
                this.HandleTtsOffsetStartFrame(ttsFrame as SaiTtsFrameOffsetStart);
            }
            else if (ttsFrame.FrameType == SaiFrameType.TTS_OffsetAnswer1)
            {
                this.HandleTtsOffsetAnswer1Frame(ttsFrame as SaiTtsFrameOffsetAnswer1);
            }
            else if (ttsFrame.FrameType == SaiFrameType.TTS_OffsetAnswer2)
            {
                this.HandleTtsOffsetAnswer2Frame(ttsFrame as SaiTtsFrameOffsetAnswer2);
            }
            else if (ttsFrame.FrameType == SaiFrameType.TTS_OffsetEstimate)
            {
                this.HandleTtsEstimateFrame(ttsFrame as SaiTtsFrameEstimate);
            }
            else if (ttsFrame.FrameType == SaiFrameType.TTS_OffsetEnd)
            {
                this.HandleTtsOffsetEndFrame(ttsFrame as SaiTtsFrameOffsetEnd);
            }
            else if (ttsFrame.FrameType == SaiFrameType.TTS_AppData)
            {
                this.HandleTtsAppFrame(ttsFrame as SaiTtsFrameAppData);
            }
            else
            {
                throw new Exception(string.Format("{0}: {1} 中收到非Tts帧。",
                    this.Context.RsspEP.ID, this.GetType().Name));
            }
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion
    }
}
