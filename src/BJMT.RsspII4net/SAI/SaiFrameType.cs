/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-18 15:24:13 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.SAI
{
    /// <summary>
    /// SAI层的帧类型定义。
    /// </summary>
    enum SaiFrameType : byte
    {
        /// <summary>
        /// 时钟偏移开始报文
        /// </summary>
        TTS_OffsetStart = 1,
        /// <summary>
        /// 时钟偏移Answer1报文
        /// </summary>
        TTS_OffsetAnswer1 = 2,
        /// <summary>
        /// 时钟偏移Answer2报文
        /// </summary>
        TTS_OffsetAnswer2 = 3,
        /// <summary>
        /// 时钟偏移估算报文
        /// </summary>
        TTS_OffsetEstimate = 4,
        /// <summary>
        /// 时钟偏移结束报文
        /// </summary>
        TTS_OffsetEnd = 5,
        /// <summary>
        /// 使用TTS保护的应用消息。
        /// </summary>
        TTS_AppData = 6,


        /// <summary>
        /// EC起始消息
        /// </summary>
        EC_Start = 0x81,
        /// <summary>
        /// 使用EC保护的应用消息。
        /// </summary>
        EC_AppData = 0x86,
        /// <summary>
        /// 使用EC保护并请求应答的应用消息。
        /// </summary>
        EC_AppDataAskForAck = 0x87,
        /// <summary>
        /// 使用EC保护并含有应答确认的应用消息。
        /// </summary>
        EC_AppDataAcknowlegment = 0x88,
    }
}
