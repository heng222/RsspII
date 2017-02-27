/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-29 13:23:15 
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
    /// 一个事件参数类，用于描述用户数据接收事件。
    /// </summary>
    public class UserDataIncomingEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="pkg">收到的Package。</param>
        public UserDataIncomingEventArgs(IncomingPackage pkg)
        {
            this.Package = pkg;
        }

        /// <summary>
        /// 获取收到的包。
        /// </summary>
        public IncomingPackage Package { get; private set; }
    }
}
