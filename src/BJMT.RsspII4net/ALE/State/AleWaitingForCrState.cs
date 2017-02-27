/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-12 19:36:10 
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

namespace BJMT.RsspII4net.ALE.State
{
    /// <summary>
    /// 表示ALE被动方正在等待ConnectionRequest帧。
    /// </summary>
    class AleWaitingForCrState : AleState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public AleWaitingForCrState(IAleStateContext context)
            :base(context)
        {

        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"

        public override void HandleConnectionRequestFrame(AleTunnel theConnection, AleFrame theFrame)
        {
            var crData = theFrame.UserData as AleConnectionRequest;

            // 停止握手计时器。
            theConnection.StopHandshakeTimer();

            // 复位序号。
            this.Context.SeqNoManager.Initlialize();

            // 检查CR协议帧。
            this.CheckCrFrame(crData, theConnection);

            // 更新服务类型。
            this.Context.RsspEP.ServiceType = crData.ServiceType;

            // 将CR帧中的AU1提交到MASL（主要更新RandomB）。                    
            this.Context.Observer.OnAleUserDataArrival(crData.UserData);

            // 发送CC帧。
            this.SendConnectionConfirmFrame(theConnection);

            // 更新序号管理器的发送序号与确认序号。
            this.Context.SeqNoManager.GetAndUpdateSendSeq();
            this.Context.SeqNoManager.UpdateAckSeq(theFrame.SequenceNo);

            if (!this.Context.ContainsTunnel(theConnection))
            {
                // 接收到一个新的TCP连接。
                var args = new TcpConnectedEventArgs(theConnection.ID,
                    this.Context.RsspEP.LocalID, theConnection.LocalEndPoint,
                    this.Context.RsspEP.RemoteID, theConnection.RemoteEndPoint);
                this.Context.TunnelEventNotifier.NotifyTcpConnected(args);

                // 增加有效的连接个数。
                theConnection.IsHandShaken = true;
                this.Context.IncreaseValidConnection();

                // 保存TCP连接
                this.Context.AddConnection(theConnection);
            }
        }
        #endregion

        #region "Private methods"

        #endregion

        #region "Public methods"
        #endregion

    }
}
