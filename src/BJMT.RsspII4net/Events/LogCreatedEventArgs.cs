using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJMT.RsspII4net.Events
{
    /// <summary>
    /// 一个事件参数类，用于RSSP-II通信组件日志产生事件。
    /// </summary>
    public class LogCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// 获取一个值，用于表示是否为提示性日志。
        /// </summary>
        public bool IsInfo { get; private set; }

        /// <summary>
        /// 获取一个值，用于表示是否为警告性日志。
        /// </summary>
        public bool IsWarning { get; private set; }

        /// <summary>
        /// 获取一个值，用于表示是否为错误性日志。
        /// </summary>
        public bool IsError { get; private set; }

        /// <summary>
        /// 获取一个值，用于表示日志的描述性信息。
        /// </summary>
        public string Message { get; private set; }

        private LogCreatedEventArgs()
        {
        }

        /// <summary>
        /// 创建一个提示性日志事件参数对象。
        /// </summary>
        /// <param name="message">日志的描述性信息。</param>
        /// <returns>日志事件参数对象</returns>
        public static LogCreatedEventArgs CreateInfo(string message)
        {
            var args = new LogCreatedEventArgs();
            args.Message = message;
            args.IsInfo = true;
            return args;
        }

        /// <summary>
        /// 创建一个警告性日志事件参数对象。
        /// </summary>
        /// <param name="message">日志的描述性信息。</param>
        /// <returns>日志事件参数对象</returns>
        public static LogCreatedEventArgs CreateWarning(string message)
        {
            var args = new LogCreatedEventArgs();
            args.Message = message;
            args.IsWarning = true;
            return args;
        }

        /// <summary>
        /// 创建一个错误性日志事件参数对象。
        /// </summary>
        /// <param name="message">日志的描述性信息。</param>
        /// <returns>日志事件参数对象</returns>
        public static LogCreatedEventArgs CreateError(string message)
        {
            var args = new LogCreatedEventArgs();
            args.Message = message;
            args.IsError = true;
            return args;
        }
    }
}
