/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-29 13:17:32 
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
    /// 一个事件参数类，用于描述TCP终结点正在监听事件的数据。
    /// </summary>
    public class TcpEndPointListeningEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="localID">本地节点ID。</param>
        /// <param name="endPoint">正在监听的终结点。</param>
        public TcpEndPointListeningEventArgs(uint localID, IPEndPoint endPoint)
        {
            this.LocalID = localID;
            this.EndPoint = endPoint;
        }

        /// <summary>
        /// 本地节点编号。
        /// </summary>
        public uint LocalID { get; private set; }

        /// <summary>
        /// 获取当前正在监听的终结点。
        /// </summary>
        public IPEndPoint EndPoint { get; private set; } 
    }
}
