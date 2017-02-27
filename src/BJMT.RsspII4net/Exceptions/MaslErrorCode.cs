/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-12-8 15:37:56 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.Exceptions
{
    /// <summary>
    /// Masl错误码定义。
    /// </summary>
    enum MaslErrorCode : byte
    {
        /// <summary>
        /// 无错误，即正常释放。
        /// </summary>
        NormalRelease = 0,

        /// <summary>
        /// 参数无效
        /// </summary>
        ParameterInvalid =3,

        /// <summary>
        /// Mac无效。
        /// </summary>
        MacInvalid = 4,

        /// <summary>
        /// 序列完整性错误。
        /// 在连接已经建立的情况下收到验证信息（AU1/AU2/AU3/AR）
        /// </summary>
        SequenceIntegrityFailure = 5,

        /// <summary>
        /// 方向标志错误。
        /// </summary>
        DirectionFlagFailure = 6,

        /// <summary>
        /// 连接建立超时
        /// </summary>
        ConnectionTimeout = 7,

        /// <summary>
        /// 错误的Sa PDU 区
        /// </summary>
        SaPduFieldInvalid = 8,

        /// <summary>
        /// 错误的Sa PDU 序列
        /// </summary>
        SaPduSeqInvalid = 9,

        /// <summary>
        /// Sa PDU长度错误
        /// </summary>
        SaPduLengthError = 10,

        /// <summary>
        /// 表示没有合适的可供选择的原因代码。
        /// </summary>
        NotDefined = 127,
    }
}
