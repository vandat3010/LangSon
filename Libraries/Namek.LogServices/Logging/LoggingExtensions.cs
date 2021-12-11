﻿using System;
using System.Threading;
using Namek.Library.Entity.Logging;
using Namek.Library.Entity.Users;

namespace Namek.LogServices.Logging
{
    public static class LoggingExtensions
    {
        public static void Debug(this ILogService logger, string message, Exception exception = null, User User = null)
        {
            FilteredLog(logger, LogLevel.Debug, message, exception, User);
        }

        public static void Information(this ILogService logger, string message, Exception exception = null,
            User User = null)
        {
            FilteredLog(logger, LogLevel.Information, message, exception, User);
        }

        public static void Warning(this ILogService logger, string message, Exception exception = null,
            User User = null)
        {
            FilteredLog(logger, LogLevel.Warning, message, exception, User);
        }

        public static void Error(this ILogService logger, string message, Exception exception = null, User User = null)
        {
            FilteredLog(logger, LogLevel.Error, message, exception, User);
        }

        public static void Fatal(this ILogService logger, string message, Exception exception = null, User User = null)
        {
            FilteredLog(logger, LogLevel.Fatal, message, exception, User);
        }

        private static void FilteredLog(ILogService logger, LogLevel level, string message, Exception exception = null,
            User User = null)
        {
            //don't log thread abort exception
            if (exception is ThreadAbortException)
                return;

            if (logger.IsEnabled(level))
            {
                var fullMessage = exception == null ? string.Empty : exception.ToString();
                logger.InsertLog(level, message, fullMessage, User);
            }
        }
    }
}