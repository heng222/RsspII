/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-29 13:12:13 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace BJMT.RsspII4net.Events
{
    /// <summary>
    /// 一个事件参数类，用于描述TCP连接建立事件的数据。
    /// </summary>
    public class TcpConnectedEventArgs : TcpEventArgs
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        public TcpConnectedEventArgs(string connectionID, 
            uint localID, IPEndPoint localEP,
            uint remoteID, IPEndPoint remoteEP)
            :base(connectionID, localID, localEP, remoteID, remoteEP)
        {
        }
    }
}
