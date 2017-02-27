/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-8 9:02:49 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.ITest
{
    /// <summary>
    /// 设备类型。
    /// （有效值0~7，使用三个比特。）
    /// </summary>
    public enum EquipmentType : byte
    {
        /// <summary>
        /// 无效。
        /// </summary>
        None = 0,

        /// <summary>
        /// RBC
        /// </summary>
        RBC = 1,

        /// <summary>
        /// 车载系统
        /// </summary>
        VOBC = 2,

        /// <summary>
        /// 应答器
        /// </summary>
        Responder = 3,

        /// <summary>
        /// Key Management Centre 密钥管理中心。
        /// </summary>
        KMC = 5,

        /// <summary>
        /// 联锁。
        /// </summary>
        CBI = 6,
    }
}
