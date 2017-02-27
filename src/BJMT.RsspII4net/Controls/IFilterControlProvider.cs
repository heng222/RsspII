/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 产品名称：BJMT-UR ATS
//
// 创 建 人：zhh_217
// 创建日期：2016-2-19 8:55:35 
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
    /// 一个接口，可以用于提供过滤器控件。
    /// </summary>
    public interface IFilterControlProvider
    {
        /// <summary>
        /// 获取过滤器的名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 获取过滤控件。
        /// </summary>
        System.Windows.Forms.Control View { get; }
        

        /// <summary>
        /// 过滤指定的字节流。
        /// </summary>
        /// <param name="bytes">将要过滤的字节流。</param>
        /// <returns>true表示过滤指定的用户数据（显示），false表示不过滤(不显示)。</returns>
        bool Filter(IEnumerable<byte> bytes);
    }
}
