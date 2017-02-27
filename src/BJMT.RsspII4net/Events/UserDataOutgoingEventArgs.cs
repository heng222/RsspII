/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-30 8:58:25 
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
    /// 一个事件参数类，用于描述用户数据发送事件。
    /// </summary>
    public class UserDataOutgoingEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="pkg">收到的Package。</param>
        public UserDataOutgoingEventArgs(OutgoingPackage pkg)
        {
            this.Package = pkg;
        }

        /// <summary>
        /// 获取将要发送的包。
        /// </summary>
        public OutgoingPackage Package { get; private set; }

    }
}
