/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 产品名称：BJMT-UR ATS
//
// 创 建 人：zhh_217
// 创建日期：2013-7-19 8:55:35 
// 邮    箱：zhh_217@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司 2009，保留所有权利
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace BJMT.RsspII4net.Controls
{
    /// <summary>
    /// 设备名称解析器
    /// </summary>
    public interface IRsspNodeNameResolver
    {
        /// <summary>
        /// 将设备ID转换为设备名称
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <returns>设备名称</returns>
        string Convert(uint deviceId);
    }
}
