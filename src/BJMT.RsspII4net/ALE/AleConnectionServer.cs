/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-16 8:18:55 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using BJMT.RsspII4net.ALE.State;
using BJMT.RsspII4net.Infrastructure.Services;

namespace BJMT.RsspII4net.ALE
{
    class AleConnectionServer : AleConnection, IAleServerTunnelObserver
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        
        /// <summary>
        /// 创建一个适用于被动方的ALE Connection。
        /// </summary>
        public AleConnectionServer(RsspEndPoint rsspEP,
            IAuMessageBuilder auMsgProvider,
            IAleConnectionObserver observer,
            IAleTunnelEventNotifier tunnelEventNotifier)
            : base(rsspEP, auMsgProvider, observer, tunnelEventNotifier)
        {

        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"

        protected override AleState GetInitialState()
        {
            return new AleWaitingForCrState(this);            
        }

        protected override void HandleTunnelDisconnected(AleTunnel theConnection, string reason)
        {
            try
            {
                // 服务器端，移除并关闭此连接。
                this.RemoveCloseConnection(theConnection);
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

        #region "IAleServerTunnel 接口实现"
        #endregion
    }
}
