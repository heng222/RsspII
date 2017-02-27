/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-15 15:20:51 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.SAI.EC.Frames;

namespace BJMT.RsspII4net.SAI.EC.State
{
    /// <summary>
    /// 一个状态，表示主动方或被动方已连接（使用EC防御技术）。
    /// </summary>
    class EcConnectedState : EcState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public EcConnectedState(EcState preState)
            : base(preState)
        {

        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        public override void SendUserData(OutgoingPackage package)
        {
            var ecValue = this.DefenseStrategy.GetLocalEcValue();
            var seqNo = (ushort)this.Context.SeqNoManager.GetAndUpdateSendSeq();
            var frame = new SaiEcFrameApplication(seqNo, ecValue, package.UserData);

            var bytes = frame.GetBytes();

            this.Context.NextLayer.SendUserData(bytes);
        }

        protected override void HandleEcAskForAckFrame(SaiEcFrameAskForAck askForAckFrame)
        {
            // 提取用户数据并通知观察器。
            if (askForAckFrame.UserData != null)
            {
                var timeDelay = this.DefenseStrategy.CalcTimeDelay(askForAckFrame);
                var remoteID = this.Context.RsspEP.RemoteID;

                this.Context.Observer.OnSaiUserDataArrival(remoteID, askForAckFrame.UserData, timeDelay, MessageDelayDefenseTech.EC);
            }

            // 回复Acknowlegment。
            var ecValue = this.DefenseStrategy.GetLocalEcValue();
            var seqNo = (ushort)this.Context.SeqNoManager.GetAndUpdateSendSeq();
            var frame = new SaiEcFrameAcknowlegment(seqNo, ecValue, null);

            var bytes = frame.GetBytes();

            this.Context.NextLayer.SendUserData(bytes);
        }

        protected override void HandleEcAcknowlegmentFrame(SaiEcFrameAcknowlegment ackFrame)
        {
            // 提取用户数据并通知观察器。
            if (ackFrame.UserData != null)
            {
                var timeDelay = this.DefenseStrategy.CalcTimeDelay(ackFrame);
                var remoteID = this.Context.RsspEP.RemoteID;

                this.Context.Observer.OnSaiUserDataArrival(remoteID, ackFrame.UserData, timeDelay, MessageDelayDefenseTech.EC);
            }
        }

        protected override void HandleEcAppFrame(SaiEcFrameApplication frame)
        {
            var timeDelay = this.DefenseStrategy.CalcTimeDelay(frame);
            var remoteID = this.Context.RsspEP.RemoteID;

            this.Context.Observer.OnSaiUserDataArrival(remoteID, frame.UserData, timeDelay, MessageDelayDefenseTech.EC);
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
