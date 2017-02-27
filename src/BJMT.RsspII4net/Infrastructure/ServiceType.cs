/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-18 13:53:15 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net
{
    /// <summary>
    /// RSSP服务类型定义。
    /// </summary>
    public enum ServiceType : byte
    {
        /// <summary>
        /// A类服务
        /// </summary>
        A = 0x00,

        /// <summary>
        /// D类型服务
        /// </summary>
        D = 0x03,
    }
}
