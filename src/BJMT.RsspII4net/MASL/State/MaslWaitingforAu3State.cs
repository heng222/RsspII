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
using BJMT.RsspII4net.Exceptions;
using BJMT.RsspII4net.MASL.Frames;

namespace BJMT.RsspII4net.MASL.State
{
    class MaslWaitingforAu3State : MaslState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public MaslWaitingforAu3State(IMaslStateContext context)
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
            throw new NotAu3ObtainedAfterAu2Exception();
        }

        public override void HandleAu2Frame(MaslAu2Frame frame)
        {
            throw new NotAu3ObtainedAfterAu2Exception();
        }

        public override void HandleArFrame(MaslArFrame frame)
        {
            throw new NotAu3ObtainedAfterAu2Exception();
        }

        public override void HandleAu3Frame(Frames.MaslAu3Frame au3Frame)
        {
            var expectedDir = MaslFrameDirection.Client2Server;
            if (au3Frame.Direction != expectedDir)
            {
                throw new DirectionFlagException(expectedDir);
            }

            // 收到AU3，关闭握手计时器。
            this.Context.StopHandshakeTimer();

            // 验证MAC
            var actualMac = au3Frame.MAC;
            var expectedMac = this.Context.AuMessageMacCalculator.CalcAu3MAC(au3Frame, this.Context.RsspEP.LocalID);
            if (!ArrayHelper.Equals(expectedMac, actualMac))
            {
                throw new MacInAu3Exception(string.Format("Au3消息Mac检验失败，期望值={0}，实际值={1}",
                    HelperTools.ConvertToString(expectedMac),
                    HelperTools.ConvertToString(actualMac)));
            }

            // 发送AR
            var arPkt = this.Context.AuMessageBuilder.BuildArPacket();
            this.Context.AleConnection.SendUserData(arPkt);

            // 连接建立。
            this.Context.Connected = true;
        }

        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
