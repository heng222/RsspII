/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-5 13:08:33 
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
    /// TCP事件基类
    /// </summary>
    public abstract class TcpEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        public TcpEventArgs(string connectionID, 
            uint localID, IPEndPoint localEP,
            uint remoteID, IPEndPoint remoteEP)
        {
            this.ConnectionID = connectionID;

            this.LocalID = localID;
            this.LocalEndPoint = localEP;
            this.RemoteID = remoteID;
            this.RemoteEndPoint = remoteEP;
        }

        /// <summary>
        /// 获取TCP连接的唯一标识符。
        /// </summary>
        public string ConnectionID { get; private set; }

        /// <summary>
        /// 获取本地ID。
        /// </summary>
        public uint LocalID { get; private set; }
        /// <summary>
        /// 获取本地终结点。
        /// </summary>
        public IPEndPoint LocalEndPoint { get; private set; }

        /// <summary>
        /// 获取对方ID。
        /// </summary>
        public uint RemoteID { get; private set; }
        /// <summary>
        /// 获取对方终结点。
        /// </summary>
        public IPEndPoint RemoteEndPoint { get; private set; }

    }
}
