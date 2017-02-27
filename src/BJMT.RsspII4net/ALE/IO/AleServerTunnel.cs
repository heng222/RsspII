/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-22 15:43:23 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Net.Sockets;

namespace BJMT.RsspII4net.ALE
{
    class AleServerTunnel : AleTunnel
    {
        #region "Filed"
        private IAleServerTunnelObserver _observer;
        private bool _waitingforConnectionRequest = false;
        #endregion

        #region "Constructor"
        /// <summary>
        /// 构造一个接收请求的连接对象。
        /// </summary>
        public AleServerTunnel(TcpClient client, IAleServerTunnelObserver observer, bool waitforCR)
            : base(client, observer)
        {
            _observer = observer;
            _waitingforConnectionRequest = waitforCR;
        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        protected override void OnOpen()
        {
            this.BeginReceive();

            if (_waitingforConnectionRequest)
            {
                this.StartHandshakeTimer();
            }
        }

        protected override void OnReceiveCallbackException(Exception ex)
        {
            try
            {
                this.Close();
            }
            catch (System.Exception ex1)
            {
                LogUtility.Error(ex1.ToString());
            }
        }

        protected override void OnHandshakeTimeout()
        {
            try
            {
                LogUtility.Error(string.Format("规定时间里没有收到CR帧，关闭Socket。LEP = {0}, REP={1}.",
                    this.LocalEndPoint, this.RemoteEndPoint));

                this.Close();
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }

        public override void Disconnect()
        {
            try
            {
                this.Close();
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex.ToString());
            }
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
