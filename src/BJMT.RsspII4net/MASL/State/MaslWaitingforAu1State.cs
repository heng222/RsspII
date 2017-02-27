/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-12 21:03:03 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.Exceptions;
using BJMT.RsspII4net.MASL.Frames;

namespace BJMT.RsspII4net.MASL.State
{
    class MaslWaitingforAu1State : MaslState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public MaslWaitingforAu1State(IMaslStateContext context)
            :base(context)
        {

        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        public override void HandleAu2Frame(MaslAu2Frame frame)
        {
            throw new MaslException(MaslErrorCode.SequenceIntegrityFailure);
        }

        public override void HandleAu3Frame(MaslAu3Frame frame)
        {
            throw new MaslException(MaslErrorCode.SequenceIntegrityFailure);
        }

        public override void HandleArFrame(MaslArFrame frame)
        {
            throw new MaslException(MaslErrorCode.SequenceIntegrityFailure);
        }

        public override void HandleAu1Frame(MaslAu1Frame au1Frame)
        {
            if (au1Frame.EncryAlgorithm != EncryptionAlgorithm.TripleDES)
            {
                throw new SafetyFeatureNotSupportedException();
            }

            var expectedDir = MaslFrameDirection.Client2Server;
            if (au1Frame.Direction != expectedDir)
            {
                throw new DirectionFlagException(expectedDir);
            }

            // 更新远程设备（主动方）类型。
            this.Context.RsspEP.RemoteEquipType = au1Frame.DeviceType;

            // 更新RandomB
            this.Context.MacCalculator.RandomB = au1Frame.RandomB;

            // 初始化Mac计算器。
            this.Context.MacCalculator.UpdateSessionKeys();

            // 设置下一个状态。
            this.Context.CurrentState = new MaslWaitingforAu3State(this.Context);

            // 启动握手计时器，等待AU3
            this.Context.StartHandshakeTimer();
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
