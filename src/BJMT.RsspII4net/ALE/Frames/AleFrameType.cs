/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-18 13:56:56 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.ALE.Frames
{
    /// <summary>
    /// ALE使用的所有包的类型定义。
    /// </summary>
    enum AleFrameType : byte
    {
        /// <summary>
        /// 连接请求
        /// </summary>
        ConnectionRequest = 0x01,
        /// <summary>
        /// 连接确认
        /// </summary>
        ConnectionConfirm = 0x02,

        /// <summary>
        /// 数据传输
        /// </summary>
        DataTransmission = 0x03,

        /// <summary>
        /// （连接）断开指示
        /// </summary>
        Disconnect = 0x04,

        /// <summary>
        /// 将数据通信切换到冗余链路
        /// </summary>
        SwitchN2R = 251,
        /// <summary>
        /// 将数据通信切换到正常链路
        /// </summary>
        SwitchR2N = 253,

        /// <summary>
        /// 非活动链路的生命信息。
        /// </summary>
        KANA = 254,
        /// <summary>
        /// 活动链路的生命信息
        /// </summary>
        KAA = 255,
    }
}
