/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-8 9:53:20 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.MASL.Frames
{
    /// <summary>
    /// Masl帧方向定义。
    /// </summary>
    enum MaslFrameDirection
    {
        /// <summary>
        /// 发起方 -> 应答方
        /// </summary>
        Client2Server = 0,

        /// <summary>
        /// 应答方 -> 发起方
        /// </summary>
        Server2Client = 1,
    }
}
