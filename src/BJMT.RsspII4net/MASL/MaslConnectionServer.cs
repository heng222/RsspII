/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-16 8:59:05 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.Config;
using BJMT.RsspII4net.Infrastructure.Services;
using BJMT.RsspII4net.MASL.State;

namespace BJMT.RsspII4net.MASL
{
    class MaslConnectionServer : MaslConnection
    {
        #region "Filed"
        #endregion

        #region "Constructor"
        /// <summary>
        /// 创建一个适用于被动方的MASL Connection。
        /// </summary>
        public MaslConnectionServer(RsspEndPoint rsspEP,
            IMaslConnectionObserver observer,
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
        protected override MaslState GetInitialState()
        {
            return new MaslWaitingforAu1State(this);
        }
        #endregion

        #region "Private methods"
        #endregion

        #region "Public methods"
        #endregion

    }
}
