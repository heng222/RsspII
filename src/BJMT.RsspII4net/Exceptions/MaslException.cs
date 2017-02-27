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
using System.Security;
using System.Runtime.Serialization;

namespace BJMT.RsspII4net.Exceptions
{
    /// <summary>
    /// 消息鉴定安全层异常。
    /// </summary>
    [Serializable]
    class MaslException : Exception
    {
        /// <summary>
        /// 127表示没有合适的可供选择的原因代码或者子原因代码。
        /// </summary>
        public const byte UndefineMajorRason = (byte)MaslErrorCode.NotDefined;

        /// <summary>
        /// 127表示没有合适的可供选择的原因代码或者子原因代码。
        /// </summary>
        public const byte UndefineMinorReason = 127;

        /// <summary>
        /// 获取错误的主要原因。
        /// </summary>
        public MaslErrorCode MajorReason { get; protected set; }

        /// <summary>
        /// 获取错误的次要原因。 
        /// </summary>
        public byte MinorReason { get; protected set; }

        /// <summary>
        /// 初始化 MaslException 类的新实例。
        /// </summary>
        public MaslException(MaslErrorCode majorReason, byte minorReason = UndefineMinorReason)
        {
            this.MajorReason = majorReason;
            this.MinorReason = minorReason;
        }

        /// <summary>
        /// 使用指定的错误消息初始化 MaslException 类的新实例。
        /// </summary>
        /// <param name="majorReason">主要原因。</param>
        /// <param name="minorReason">次要原因。</param>
        /// <param name="message">描述错误的消息。</param>
        public MaslException(MaslErrorCode majorReason, byte minorReason, string message)
            :base(message)
        {
            this.MajorReason = majorReason;
            this.MinorReason = minorReason;
        }

        /// <summary>
        /// 用序列化数据初始化 MaslException 类的新实例。
        /// </summary>
        /// <param name="info">System.Runtime.Serialization.SerializationInfo，它存有有关所引发异常的序列化的对象数据。</param>
        /// <param name="context">System.Runtime.Serialization.StreamingContext，它包含有关源或目标的上下文信息。</param>
        [SecuritySafeCritical]
        protected MaslException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 MaslException 类的新实例。
        /// </summary>
        /// <param name="majorReason">主要原因。</param>
        /// <param name="minorReason">次要原因。</param>
        /// <param name="message">解释异常原因的错误消息。</param>
        /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）。</param>
        public MaslException(MaslErrorCode majorReason, byte minorReason, string message, Exception innerException)
            : base(message, innerException)
        {
            this.MajorReason = majorReason;
            this.MinorReason = minorReason;
        }
    }
}
