/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-29 13:52:09 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.Events;

namespace BJMT.RsspII4net.Infrastructure.Services
{
    interface IAleTunnelEventNotifier
    {
        void NotifyTcpConnecting(TcpConnectingEventArgs args);
        void NotifyTcpConnected(TcpConnectedEventArgs args);
        void NotifyTcpConnectFailure(TcpConnectFailedEventArgs args);
        void NotifyTcpDisconnected(TcpDisconnectedEventArgs args);
    }
}
