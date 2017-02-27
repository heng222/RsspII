/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-15 15:23:28 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using BJMT.RsspII4net.SAI.EC.Frames;

namespace BJMT.RsspII4net.SAI.EC.State
{
    /// <summary>
    /// 表示主动方处于EC断开状态。
    /// </summary>
    class EcDisconnectedState : EcState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public EcDisconnectedState(ISaiStateContext context, EcDefenseStrategy strategy)
            : base(context, strategy)
        {

        }
        #endregion

        #region "Properties"
        #endregion

        #region "Override methods"
        public override void HandleMaslConnected()
        {
            try
            {
                this.SendEcStartFrame();
                LogUtility.Info(string.Format("{0}: 主动方发送EC Start1并启动Tsync。", this.Context.RsspEP.ID));

                // 主动方启动EC Tsync。
                this.Context.StartHandshakeTimer();

                // 设置下一个状态。
                this.Context.CurrentState = new EcWaitingforStart2State(this);
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }

        public override void SendUserData(OutgoingPackage package)
        {
            
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
