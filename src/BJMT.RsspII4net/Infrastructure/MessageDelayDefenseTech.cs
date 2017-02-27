/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-6 14:05:06 
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
    /// RSSP消息延迟防御技术
    /// </summary>
    public enum MessageDelayDefenseTech
    {
        /// <summary>
        /// 无效值。
        /// </summary>
        None,

        /// <summary>
        /// ExcutionCycle 基于执行周期的防御技术。
        /// </summary>
        EC,

        /// <summary>
        /// TripleTimeStamp 三重时间戳防御技术。
        /// </summary>
        TTS,
    }
}
