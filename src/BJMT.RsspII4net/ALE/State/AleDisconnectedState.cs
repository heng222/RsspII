/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-12 19:34:11 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.ALE.Frames;
using BJMT.RsspII4net.Events;
using System.Diagnostics;

namespace BJMT.RsspII4net.ALE.State
{
    /// <summary>
    /// 表示ALE主动方的断开状态。
    /// </summary>
    class AleDisconnectedState : AleState
    {
        public AleDisconnectedState(IAleStateContext context)
            :base(context)
        {

        }

        public override void HandleTcpConnected(AleClientTunnel theConnection)
        {
            // 复位序号。
            this.Context.SeqNoManager.Initlialize();

            // TCP连接成功后发送ALE连接请求。
            this.SendConnectionRequestFrame(theConnection);

            // 通知观察器TCP连接已建立。
            var args = new TcpConnectedEventArgs(theConnection.ID, 
                this.Context.RsspEP.LocalID, theConnection.LocalEndPoint,
                this.Context.RsspEP.RemoteID, theConnection.RemoteEndPoint);
            this.Context.TunnelEventNotifier.NotifyTcpConnected(args);

            // 设置下一个状态。
            this.Context.CurrentState = new AleWaitingForCcState(this.Context);

            // 启动握手超时计时器。
            theConnection.StartHandshakeTimer();
        }


        #region "private methods"
        #endregion
    }
}
