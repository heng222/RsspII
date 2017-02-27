/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-12 19:38:33 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Diagnostics;
using System.Linq;
using BJMT.RsspII4net.ALE.Frames;

namespace BJMT.RsspII4net.ALE.State
{
    abstract class AleState
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        protected AleState(IAleStateContext context)
        {
            LogUtility.Info(string.Format("{0}：ALE层新状态= {1}", context.RsspEP.ID, this.GetType().Name));

            this.Context = context;
        }
        #endregion

        #region "Properties"
        protected IAleStateContext Context { get; private set; }
        #endregion

        #region "Virtual methods"
        public virtual void HandleTcpConnected(AleClientTunnel theConnection)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }

        public virtual void HandleConnectionRequestFrame(AleTunnel theConnection, AleFrame theFrame)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }

        public virtual void HandleConnectionConfirmFrame(AleTunnel theConnection, AleFrame theFrame)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }

        public virtual void HandleDiFrame(AleTunnel theConnection, AleFrame theFrame)
        {
            var diData = theFrame.UserData as AleDisconnect;
            this.Context.Observer.OnAleUserDataArrival(diData.UserData);

            theConnection.Disconnect();
        }


        public virtual void HandleDataTransmissionFrame(AleTunnel theConnection, AleFrame theFrame)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }


        public virtual void SendUserData(byte[] maslPacket)
        {
            LogUtility.Error(string.Format("{0}: {1}.{2} not implement!",
                this.Context.RsspEP.ID, this.GetType().Name,
                new StackFrame(0).GetMethod().Name.Split('.').Last()));
        }


        public virtual void Disconnect(byte[] diSaPDU)
        {
            var seqNo = (ushort)this.Context.SeqNoManager.GetAndUpdateSendSeq();
            var appType = this.Context.RsspEP.ApplicatinType;
            var aleData = new AleDisconnect(diSaPDU);

            var dataFrame = new AleFrame(appType, seqNo, false, aleData);
            var bytes = dataFrame.GetBytes();

            var tcpLinks = this.Context.Tunnels.ToList();
            tcpLinks.ForEach(connection =>
            {
                connection.Send(bytes);
                connection.Disconnect();
            });
        }
        #endregion

        #region "Override methods"
        #endregion

        #region "Private/protected methods"

        protected void CheckCrFrame(AleConnectionRequest crData, AleTunnel theTunnel)
        {
            if (crData.ServiceType == ServiceType.A)
            {
                throw new Exception(string.Format("收到CR帧，请求A类服务，不支持此类型，断开连接。"));
            }

            if (crData.ResponderID != this.Context.RsspEP.LocalID)
            {
                throw new Exception(string.Format("CR帧中的被动方编号（{0}）与本地编号（{1}）不一致，断开此连接。",
                    crData.ResponderID, this.Context.RsspEP.LocalID));
            }

            if (crData.InitiatorID == this.Context.RsspEP.LocalID)
            {
                throw new Exception(string.Format("CR帧中的主动方编号（{0}）与本地编号（{1}）相同，断开此连接。",
                    crData.InitiatorID, this.Context.RsspEP.LocalID));
            }

            if (!this.Context.RsspEP.IsClientAcceptable(crData.InitiatorID, theTunnel.RemoteEndPoint))
            {
                throw new Exception(string.Format("CR帧中的主动方编号（{0}）或终结点（{1}）不在可接受的范围内，断开此连接。",
                    crData.InitiatorID, theTunnel.RemoteEndPoint));
            }
        }

        protected void CheckCcFrame(AleConnectionConfirm ccData)
        {
            if (ccData.ServerID != this.Context.RsspEP.RemoteID)
            {
                throw new Exception(string.Format("CC帧中的应答方编号与期望的不一致，期望值={0}，实际值={1}",
                    this.Context.RsspEP.RemoteID, ccData.ServerID));
            }
        }

        protected void SendConnectionRequestFrame(AleClientTunnel theConnection)
        {
            var au1Packet = this.Context.AuMsgBuilder.BuildAu1Packet();

            var crData = new AleConnectionRequest(this.Context.RsspEP.LocalID, this.Context.RsspEP.RemoteID,
                this.Context.RsspEP.ServiceType, au1Packet);

            // 构建AleFrame。
            ushort seqNo = 0;
            var appType = this.Context.RsspEP.ApplicatinType;
            var crFrame = new AleFrame(appType, seqNo, theConnection.IsNormal, crData);

            // 发送。
            var bytes = crFrame.GetBytes();
            theConnection.Send(bytes);

            LogUtility.Info(string.Format("{0}: Send CR frame via tcp connection(LEP = {1}, REP={2}).",
                this.Context.RsspEP.ID, theConnection.LocalEndPoint, theConnection.RemoteEndPoint));
        }

        protected void SendConnectionConfirmFrame(AleTunnel theConnection)
        {
            var au2Packet = this.Context.AuMsgBuilder.BuildAu2Packet();

            var ccData = new AleConnectionConfirm(this.Context.RsspEP.LocalID, au2Packet);

            // 构建AleFrame。
            ushort seqNo = 0;
            var appType = this.Context.RsspEP.ApplicatinType;
            var ccFrame = new AleFrame(appType, seqNo, theConnection.IsNormal, ccData);

            // 发送。
            var bytes = ccFrame.GetBytes();
            theConnection.Send(bytes);

            LogUtility.Info(string.Format("{0}: Send CC frame via tcp connection(LEP = {1}, REP={2}).",
                this.Context.RsspEP.ID, theConnection.LocalEndPoint, theConnection.RemoteEndPoint));
        }
        #endregion

        #region "Public methods"
        #endregion

    }
}
