/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-8 9:09:59 
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
    enum MaslFrameType : byte
    {
        /// <summary>
        /// none.
        /// </summary>
        None = 0,
        /// <summary>
        /// First Authentication message.
        /// </summary>
        AU1 = 1,
        /// <summary>
        /// Second Authenticatioin message.
        /// </summary>
        AU2 = 2,
        /// <summary>
        /// Third Authentication message.
        /// </summary>
        AU3 = 3,
        /// <summary>
        /// Authtication Response message.
        /// </summary>
        AR = 9,

        /// <summary>
        /// Data Transimission message.
        /// </summary>
        DT = 5,

        /// <summary>
        /// Disconnect message.
        /// </summary>
        DI = 8,
    }
}
