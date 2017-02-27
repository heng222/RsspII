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

namespace BJMT.RsspII4net.Exceptions
{
    /// <summary>
    /// 密钥缺失异常。
    /// Missing authentication key。
    /// </summary>
    class AuthenticationKeyMissingException : MaslException
    {
        private const byte SubCode = 2;

        /// <summary>
        /// 初始化 AuthenticationKeyMissingException 类的新实例。
        /// </summary>
        public AuthenticationKeyMissingException()
            : base(MaslErrorCode.ParameterInvalid, SubCode, "Missing authentication key。")
        {
        }

        /// <summary>
        /// 使用指定的错误消息初始化 AuthenticationKeyMissingException 类的新实例。
        /// </summary>
        /// <param name="message">描述错误的消息。</param>
        public AuthenticationKeyMissingException(string message)
            : base(MaslErrorCode.ParameterInvalid, SubCode, message)
        {

        }

        /// <summary>
        /// 用序列化数据初始化 AuthenticationKeyMissingException 类的新实例。
        /// </summary>
        /// <param name="info">System.Runtime.Serialization.SerializationInfo，它存有有关所引发异常的序列化的对象数据。</param>
        /// <param name="context">System.Runtime.Serialization.StreamingContext，它包含有关源或目标的上下文信息。</param>
        [SecuritySafeCritical]
        protected AuthenticationKeyMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 AuthenticationKeyMissingException 类的新实例。
        /// </summary>
        /// <param name="message">解释异常原因的错误消息。</param>
        /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）。</param>
        public AuthenticationKeyMissingException(string message, Exception innerException)
            : base(MaslErrorCode.ParameterInvalid, SubCode, message, innerException)
        {

        }
    }
}
