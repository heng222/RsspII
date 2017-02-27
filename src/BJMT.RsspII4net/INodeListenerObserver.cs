/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-13 14:25:25 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace BJMT.RsspII4net
{
    interface INodeListenerObserver
    {
        void OnEndPointListening(TcpListener listener);
        void OnEndPointListenFailed(TcpListener listener, string message);

        void OnAcceptTcpClient(TcpClient tcpClient);
    }
}
