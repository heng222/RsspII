/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-13 8:40:16 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.Infrastructure.Services;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.ALE.State
{
    interface IAleStateContext
    {
        AleState CurrentState { get; set; }

        bool Connected { get; }

        IAleConnectionObserver Observer { get; }
        RsspEndPoint RsspEP { get; }

        IAuMessageBuilder AuMsgBuilder { get; }

        IAleTunnelEventNotifier TunnelEventNotifier { get; }
        SeqNoManager SeqNoManager { get; }
        IEnumerable<AleTunnel> Tunnels { get; }

        bool ContainsTunnel(AleTunnel tunnel);
        void AddConnection(AleTunnel item);
        void IncreaseValidConnection();
        void DescreaseValidConnection();
        void RemoveCloseConnection(AleTunnel theConnection);
    }
}
