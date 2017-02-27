using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BJMT.RsspII4net.Events;

namespace BJMT.RsspII4net
{
    static class LogUtility
    {
        public static event EventHandler<LogCreatedEventArgs> LogCreated;

        public static void Info(string message)
        {
            try
            {
                if (LogCreated != null)
                {
                    LogCreated(null, LogCreatedEventArgs.CreateInfo(message));
                }
            }
            catch (Exception)
            {
            }
        }

        public static void Warn(string message)
        {
            try
            {
                if (LogCreated != null)
                {
                    LogCreated(null, LogCreatedEventArgs.CreateWarning(message));
                }
            }
            catch (Exception)
            {
            }
        }

        public static void Error(string message)
        {
            try
            {
                if (LogCreated != null)
                {
                    LogCreated(null, LogCreatedEventArgs.CreateError(message));
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
