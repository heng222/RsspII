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
    /// 用户数据解析器接口。
    /// </summary>
    public interface IRsspUserDataResolver
    {
        /// <summary>
        /// 获取用户数据的描述信息。
        /// </summary>
        /// <param name="userData">用户数据。</param>
        /// <param name="isIncomingData">是否为输入性数据，true表示输入性数据，false表示输出性数据。</param>
        /// <returns>协议帧的描述性信息</returns>
        string GetDescription(IEnumerable<byte> userData, bool isIncomingData);

        /// <summary>
        /// 获取用户数据的自定义标签
        /// </summary>
        /// <param name="userData">用户数据。</param>
        /// <param name="isIncomingData">是否为输入性数据，true表示输入性数据，false表示输出性数据。</param>
        /// <returns>自定义标签</returns>
        string GetLabel(IEnumerable<byte> userData, bool isIncomingData);
    }
}
