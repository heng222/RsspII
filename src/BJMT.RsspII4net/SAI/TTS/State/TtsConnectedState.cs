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
    /// 一个状态，表示主动方或被动方已连接（使用TTS防御技术）。
    /// </summary>
    class TtsConnectedState : TtsState
    {
        #region "Filed"
        public TtsConnectedState(TtsState preState)
            : base(preState)
        {

        }
        #endregion

        #region "Constructor"
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        protected override void HandleTtsAppFrame(SaiTtsFrameAppData frame)
        {
            if (frame.UserData != null)
            {
                // 消息时延
                var timeDelay = this.DefenseStrategy.CalcTimeDelay(frame);

                // 通知网络数据事件 
                this.Context.Observer.OnSaiUserDataArrival(this.Context.RsspEP.RemoteID,
                    frame.UserData, timeDelay, MessageDelayDefenseTech.TTS);
            }
        }

        public override void SendUserData(OutgoingPackage package)
        {
            // 更新发送序号
            var seq = (ushort)this.Context.SeqNoManager.GetAndUpdateSendSeq();

            // 计算用户数据在发送队列中的排队时延。
            package.QueuingDelay = (UInt32)((DateTime.Now - package.CreationTime).TotalMilliseconds / 10);

            // 计算发送方时间戳
            var currentTime = TripleTimestamp.CurrentTimestamp;
            var senderTimeStamp = currentTime;

            if (currentTime > (package.ExtraDelay + package.QueuingDelay))
            {
                senderTimeStamp = currentTime - package.ExtraDelay - package.QueuingDelay;
            }

            var dtFrame = new SaiTtsFrameAppData(seq,
                senderTimeStamp,
                this.TTS.RemoteLastSendTimestamp,
                this.TTS.LocalLastRecvTimeStamp,
                package.UserData);

            this.Context.NextLayer.SendUserData(dtFrame.GetBytes());
        }

        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
