using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.Log;

namespace BJMT.RsspII4net.ITest.Utilities
{
    /// <summary>
    /// 日志实用工具类
    /// </summary>
    static class LogUtility
    {
        private static readonly ILog _log = LogManager.GetLogger("RSSP");


        public static void Debug(string format, params object[] args)
        {
            _log.DebugFormat(format, args);
        }

        public static void Info(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
        }

        public static void Warning(object info)
        {
            _log.Warn(info);
        }

        public static void Warning(object info, Exception ex)
        {
            _log.Warn(info, ex);
        }

        public static void Error(object info)
        {
            _log.Error(info);
        }

        public static void Error(object info, Exception ex)
        {
            _log.Error(info, ex);
        }
    }
}
