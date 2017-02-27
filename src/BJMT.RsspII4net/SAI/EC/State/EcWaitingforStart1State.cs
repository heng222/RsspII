/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-15 16:43:03 
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
    /// 表示被动方正在等待EcStart，即第一个EcStart。
    /// </summary>
    class EcWaitingforStart1State : EcState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public EcWaitingforStart1State(ISaiStateContext context, EcDefenseStrategy strategy)
            : base(context, strategy)
        {

        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"

        protected override void HandleEcStartFrame(SaiEcFrameStart frame)
        {
            this.Context.StopHandshakeTimer();
            LogUtility.Info(string.Format("{0}: 被动方收到EcStart1，回应EC Start2。新状态：EcConnectedState",
                this.Context.RsspEP.ID));

            // 启动Remote EcCounter。
            this.DefenseStrategy.StartRemoteCounter(this.Context.RsspEP.RemoteID, frame.Interval, frame.InitialValue);

            // 被动方发送EcStart回复。
            this.SendEcStartFrame();

            // 设置下一状态为EcConnectedState
            this.Context.CurrentState = new EcConnectedState(this);

            this.Context.Connected = true;
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
