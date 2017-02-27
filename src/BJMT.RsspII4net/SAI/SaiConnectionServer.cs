/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-16 10:33:28 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using BJMT.RsspII4net.ALE;
using BJMT.RsspII4net.ALE.Frames;
using BJMT.RsspII4net.Infrastructure.Services;

namespace BJMT.RsspII4net.SAI
{
    class SaiConnectionServer : SaiConnection
    {
        #region "Filed"
        #endregion

        #region "Constructor"

        /// <summary>
        /// 创建一个适用于被动方的SAI Connection。
        /// </summary>
        public SaiConnectionServer(RsspEndPoint rsspEP,
            ISaiConnectionObserver observer,
            IAleTunnelEventNotifier tunnelEventNotifier)
            : base(rsspEP, observer, tunnelEventNotifier)
        {
        }
        #endregion

        #region "Properties"
        #endregion

        #region "Virtual methods"
        #endregion

        #region "Override methods"
        protected override SaiState GetInitialState(DefenseStrategy strategy)
        {
            return new SaiInvalidState(this);
        }

        protected override DefenseStrategy GetDefenseStrategy()
        {
            return null;
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"

        public void HandleAleConnectionRequestFrame(AleServerTunnel connection, AleFrame requestFrame)
        {
            _maslConnection.HandleAleConnectionRequestFrame(connection, requestFrame);
        }


        public void AddAleServerTunnel(AleServerTunnel tunnel)
        {
            _maslConnection.AddAleServerTunnel(tunnel);
        }
        #endregion

    }
}
