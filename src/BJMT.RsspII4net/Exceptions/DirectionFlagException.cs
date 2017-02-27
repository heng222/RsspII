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
using System.Runtime.Serialization;
using System.Security;
using BJMT.RsspII4net.MASL.Frames;

namespace BJMT.RsspII4net.Exceptions
{
    /// <summary>
    /// 一个Masl层异常，表示方向标志异常。
    /// </summary>
    class DirectionFlagException : MaslException
    {
        private const MaslErrorCode Code = MaslErrorCode.MacInvalid;
        private const byte SubCode = 1;

        /// <summary>
        /// 初始化 DirectionFlagException 类的新实例。
        /// </summary>
        /// <param name="expectedDir">表示期望的方向。</param>
        public DirectionFlagException(MaslFrameDirection expectedDir)
            : base(Code, SubCode, "方向标志与期望的不一致。")
        {
            this.MinorReason = (expectedDir==MaslFrameDirection.Server2Client) ? (byte)1 : (byte)2;
        }

        /// <summary>
        /// 使用指定的错误消息初始化 DirectionFlagException 类的新实例。
        /// </summary>
        /// <param name="expectedDir">表示期望的方向。</param>
        /// <param name="message">描述错误的消息。</param>
        public DirectionFlagException(MaslFrameDirection expectedDir, string message)
            : base(Code, SubCode, message)
        {
            this.MinorReason = (expectedDir == MaslFrameDirection.Server2Client) ? (byte)1 : (byte)2;
        }

        /// <summary>
        /// 用序列化数据初始化 DirectionFlagException 类的新实例。
        /// </summary>
        /// <param name="info">System.Runtime.Serialization.SerializationInfo，它存有有关所引发异常的序列化的对象数据。</param>
        /// <param name="context">System.Runtime.Serialization.StreamingContext，它包含有关源或目标的上下文信息。</param>
        [SecuritySafeCritical]
        protected DirectionFlagException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 DirectionFlagException 类的新实例。
        /// </summary>
        /// <param name="expectedDir">表示期望的方向。</param>
        /// <param name="message">解释异常原因的错误消息。</param>
        /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）。</param>
        public DirectionFlagException(MaslFrameDirection expectedDir, string message, Exception innerException)
            : base(Code, SubCode, message, innerException)
        {
            this.MinorReason = (expectedDir == MaslFrameDirection.Server2Client) ? (byte)1 : (byte)2;
        }
    }
}
