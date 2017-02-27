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
    /// 代表ALE层协议解析时引发的所有异常。
    /// </summary>
    [Serializable]
    class AleFrameParsingException : Exception
    {  
        /// <summary>
        /// 初始化 AleFrameParsingException 类的新实例。
        /// </summary>
        public AleFrameParsingException()
            : base("ALE层协议解析时引发异常。")
        {

        }

        /// <summary>
        /// 使用指定的错误消息初始化 AleFrameParsingException 类的新实例。
        /// </summary>
        /// <param name="message">描述错误的消息。</param>
        public AleFrameParsingException(string message)
            :base(message)
        {

        }

        /// <summary>
        /// 用序列化数据初始化 AleFrameParsingException 类的新实例。
        /// </summary>
        /// <param name="info">System.Runtime.Serialization.SerializationInfo，它存有有关所引发异常的序列化的对象数据。</param>
        /// <param name="context">System.Runtime.Serialization.StreamingContext，它包含有关源或目标的上下文信息。</param>
        [SecuritySafeCritical]
        protected AleFrameParsingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        /// <summary>
        /// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 AleFrameParsingException 类的新实例。
        /// </summary>
        /// <param name="message">解释异常原因的错误消息。</param>
        /// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）。</param>
        public AleFrameParsingException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
