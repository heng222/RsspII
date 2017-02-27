/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：北京地铁15号线ATS项目
//
// 创 建 人：zhangheng
// 创建日期：04/24/2014 16:58:20 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司 2009-2015 保留所有权利
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace BJMT.RsspII4net.SAI.TTS
{
    /// <summary>
    /// TTS观察器
    /// </summary>
    internal interface ITripleTimestampObserver
    {
        /// <summary>
        /// 当发送时间戳的发生过零点时调用。
        /// </summary>
        /// <param name="latestTimestamp">最新的时间戳</param>
        /// <param name="lastTimestamp">上一次的时间戳</param>
        void OnTimestampZeroPassed(UInt32 latestTimestamp, UInt32 lastTimestamp);
    }
}
