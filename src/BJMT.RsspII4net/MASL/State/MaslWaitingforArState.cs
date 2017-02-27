/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-12 21:03:41 
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
    class MaslWaitingforArState : MaslState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public MaslWaitingforArState(IMaslStateContext context)
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
            throw new NotArObtainedAfterAu3Exception();
        }

        public override void HandleAu2Frame(MaslAu2Frame frame)
        {
            throw new NotArObtainedAfterAu3Exception();
        }

        public override void HandleAu3Frame(MaslAu3Frame frame)
        {
            throw new NotArObtainedAfterAu3Exception();
        }

        public override void HandleArFrame(Frames.MaslArFrame arFrame)
        {
            var expectedDir = MaslFrameDirection.Server2Client;
            if (arFrame.Direction != expectedDir)
            {
                throw new DirectionFlagException(expectedDir);
            }

            // 收到AR，关闭握手计时器。
            this.Context.StopHandshakeTimer();

            // 验证MAC
            var actualMac = arFrame.MAC;
            var expectedMac = this.Context.AuMessageMacCalculator.CalcArMAC(arFrame, this.Context.RsspEP.LocalID);

            if (!ArrayHelper.Equals(expectedMac, actualMac))
            {
                throw new MacInArException(string.Format("Ar消息Mac检验失败，期望值={0}，实际值={1}",
                    HelperTools.ConvertToString(expectedMac),
                    HelperTools.ConvertToString(actualMac)));
            }

            this.Context.Connected = true;
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
