/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-12 21:03:21 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Linq;
using BJMT.RsspII4net.Utilities;
using BJMT.RsspII4net.MASL.Frames;
using BJMT.RsspII4net.Exceptions;

namespace BJMT.RsspII4net.MASL.State
{
    class MaslWaitingforAu2State : MaslState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public MaslWaitingforAu2State(IMaslStateContext context)
            :base(context)
        {

        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        public override void HandleAu1Frame(MaslAu1Frame frame)
        {
            throw new NotAu2ObtainedAfterAu1Exception();
        }

        public override void HandleAu3Frame(MaslAu3Frame frame)
        {
            throw new NotAu2ObtainedAfterAu1Exception();
        }

        public override void HandleArFrame(MaslArFrame frame)
        {
            throw new NotAu2ObtainedAfterAu1Exception();
        }

        public override void HandleAu2Frame(MaslAu2Frame au2Frame)
        {
            if (au2Frame.EncryAlgorithm != EncryptionAlgorithm.TripleDES)
            {
                throw new SafetyFeatureNotSupportedException();
            }

            var expectedDir = MaslFrameDirection.Server2Client;
            if (au2Frame.Direction != expectedDir)
            {
                throw new DirectionFlagException(expectedDir);
            }

            var expectedId = this.Context.RsspEP.RemoteID & 0xFFFFFF;
            if (au2Frame.ServerID != expectedId)
            {
                throw new IdentifyInAu2Exception(string.Format("AU2中无效的CTCS ID，期望值={0}（0x{0:X2}），实际值={1}（0x{1:X2}）。",
                    expectedId, au2Frame.ServerID));
            }

            // 更新远程设备（被动方）类型。
            this.Context.RsspEP.RemoteEquipType = au2Frame.DeviceType;

            // 更新RandomA
            this.Context.MacCalculator.RandomA = au2Frame.RandomA;

            // 初始化Mac计算器。
            this.Context.MacCalculator.UpdateSessionKeys();

            // 验证MAC。
            var actualMac = au2Frame.MAC;
            var expectedMac = this.Context.AuMessageMacCalculator.CalcAu2MAC(au2Frame, this.Context.RsspEP.LocalID);
            if (!ArrayHelper.Equals(expectedMac, actualMac))
            {
                throw new MacInAu2Exception(string.Format("Au2消息Mac检验失败，期望值={0}，实际值={1}",
                    HelperTools.ConvertToString(expectedMac),
                    HelperTools.ConvertToString(actualMac)));
            }

            // 发送AU3。
            var au3Pkt = this.Context.AuMessageBuilder.BuildAu3Packet();
            this.Context.AleConnection.SendUserData(au3Pkt);

            // 启动握手计时器，等待AR
            this.Context.StartHandshakeTimer();

            // 设置下一个状态。
            this.Context.CurrentState = new MaslWaitingforArState(this.Context);
        }

        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion
    }
}
