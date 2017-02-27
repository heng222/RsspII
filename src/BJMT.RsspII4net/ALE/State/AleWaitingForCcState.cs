/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-12 19:35:56 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System.Diagnostics;
using System.Linq;
using BJMT.RsspII4net.ALE.Frames;
using BJMT.RsspII4net.Events;
using System;

namespace BJMT.RsspII4net.ALE.State
{
    /// <summary>
    /// 表示ALE主动方正在等待ConnectionConfirm帧。
    /// </summary>
    class AleWaitingForCcState : AleState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public AleWaitingForCcState(IAleStateContext context)
            :base(context)
        {

        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"

        public override void HandleTcpConnected(AleClientTunnel theConnection)
        {
            // TCP连接成功后发送ALE连接请求。
            this.SendConnectionRequestFrame(theConnection);

            // 通知观察器TCP连接已建立。
            var args = new TcpConnectedEventArgs(theConnection.ID, 
                this.Context.RsspEP.LocalID, theConnection.LocalEndPoint,
                this.Context.RsspEP.RemoteID, theConnection.RemoteEndPoint);
            this.Context.TunnelEventNotifier.NotifyTcpConnected(args);

            // 启动超时检测计时器。
            theConnection.StartHandshakeTimer();
        }
        
        public override void HandleConnectionConfirmFrame(AleTunnel theConnection, AleFrame theFrame)
        {
            // 复位序号。
            this.Context.SeqNoManager.Initlialize();

            // 停止超时检测计时器。
            theConnection.StopHandshakeTimer();

            // 检查CC帧。
            var ccData = theFrame.UserData as AleConnectionConfirm;
            this.CheckCcFrame(ccData);

            // 如果CC帧中的应答方编号校验通过，则增加一个有效的连接。
            theConnection.IsHandShaken = true;
            this.Context.IncreaseValidConnection();

            // 更新序号管理器的发送序号与确认序号。
            this.Context.SeqNoManager.GetAndUpdateSendSeq();
            this.Context.SeqNoManager.UpdateAckSeq(theFrame.SequenceNo);

            // 将CC帧中的AU2提交到MASL。
            this.Context.Observer.OnAleUserDataArrival(ccData.UserData);
        }

        #endregion

        #region "Private methods"

        #endregion

        #region "Public methods"
        #endregion

    }
}
