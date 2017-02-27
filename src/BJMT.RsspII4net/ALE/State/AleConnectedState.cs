/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-12 19:36:49 
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
    /// 表示ALE主动方或被动方处于连接状态。
    /// </summary>
    class AleConnectedState : AleState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        public AleConnectedState(IAleStateContext context)
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
            // TCP连接成功后发送CR。
            this.SendConnectionRequestFrame(theConnection);

            // 通知观察器TCP连接已建立。
            var args = new TcpConnectedEventArgs(theConnection.ID, 
                this.Context.RsspEP.LocalID, theConnection.LocalEndPoint,
                this.Context.RsspEP.RemoteID, theConnection.RemoteEndPoint);
            this.Context.TunnelEventNotifier.NotifyTcpConnected(args);

            // 启动超时检测计时器。
            theConnection.StartHandshakeTimer();
        }

        public override void HandleConnectionRequestFrame(AleTunnel theConnection, Frames.AleFrame theFrame)
        {
            var crData = theFrame.UserData as AleConnectionRequest;

            // 停止超时检测计时器。
            theConnection.StopHandshakeTimer();

            // 检查CR协议帧。
            this.CheckCrFrame(crData, theConnection);

            // 检查AU1的正确性。
            this.Context.AuMsgBuilder.CheckAu1Packet(crData.UserData);

            // 发送CC帧。
            this.SendConnectionConfirmFrame(theConnection);

            if (!this.Context.ContainsTunnel(theConnection))
            {
                // 事件通知：接收到一个新的TCP连接。
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

        public override void HandleConnectionConfirmFrame(AleTunnel theConnection, Frames.AleFrame theFrame)
        {
            // 停止超时检测计时器。
            theConnection.StopHandshakeTimer();

            // 检查CC帧。
            var ccData = theFrame.UserData as AleConnectionConfirm;
            this.CheckCcFrame(ccData);

            // 如果CC帧中的应答方编号校验通过，则增加一个有效的连接。
            theConnection.IsHandShaken = true;
            this.Context.IncreaseValidConnection();
        }

        public override void HandleDataTransmissionFrame(AleTunnel theConnection, Frames.AleFrame theFrame)
        {
            var aleData = theFrame.UserData as AleDataTransmission;
            this.Context.Observer.OnAleUserDataArrival(aleData.UserData);
        }

        public override void SendUserData(byte[] maslPacket)
        {
            var aleData = new AleDataTransmission(maslPacket);
            var seqNo = (ushort)this.Context.SeqNoManager.GetAndUpdateSendSeq();
            var appType = this.Context.RsspEP.ApplicatinType;
            var dataFrame = new AleFrame(appType, seqNo, false, aleData);

            // send
            if (this.Context.RsspEP.ServiceType == ServiceType.A)
            {
                this.Context.Tunnels.ToList().ForEach(connection =>
                {
                    if (connection.IsActive)
                    {
                        dataFrame.IsNormal = connection.IsNormal;
                        var bytes = dataFrame.GetBytes();
                        connection.Send(bytes);
                    }
                });
            }
            else
            {
                dataFrame.IsNormal = false;
                var bytes = dataFrame.GetBytes();
                this.Context.Tunnels.ToList().ForEach(connection =>
                {
                    connection.Send(bytes);
                });
            }
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion
    }
}
