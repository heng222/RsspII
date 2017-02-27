/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-29 13:20:41 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.Events
{
    /// <summary>
    /// 一个事件参数类，用于描述节点中断事件的数据。
    /// </summary>
    public class NodeInterruptionEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="localID">本地节点ID</param>
        /// <param name="remoteID">远程节点ID</param>
        public NodeInterruptionEventArgs(uint localID, uint remoteID)
        {
            this.LocalID = localID;
            this.RemoteID = remoteID;
        }

        /// <summary>
        /// 获取本地节点ID。
        /// </summary>
        public uint LocalID { get; private set; }

        /// <summary>
        /// 获取远程节点ID。
        /// </summary>
        public uint RemoteID { get; private set; }

    }
}
