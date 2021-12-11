using System;
using Namek.Library.Data;
using Namek.Library.Entity.Logging;
using Namek.Library.Entity.Users;
using Namek.Library.Helpers;
using Namek.Library.Services;

namespace Namek.LogServices.Logging
{
    public class LogService : BaseEntityService<Log>, ILogService
    {
        //private readonly CommonSettings _commonSettings;

        public LogService(IDataRepository<Log> dataRepository) : base(dataRepository) { }

        /// <summary>
        ///     Determines whether a log level is enabled
        /// </summary>
        /// <param name="level">Log level</param>
        /// <returns>Result</returns>
        public virtual bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return false;
                default:
                    return true;
            }
        }

        /// <summary>
        ///     Inserts a log item
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="shortMessage">The short message</param>
        /// <param name="fullMessage">The full message</param>
        /// <param name="customer">The customer to associate log record with</param>
        /// <returns>A log item</returns>
        public virtual Log InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "",
            User customer = null)
        {
            //check ignore word/phrase list?
            if (IgnoreLog(shortMessage) || IgnoreLog(fullMessage))
                return null;

            var log = new Log
            {
                LogLevel = logLevel,
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                IpAddress = WebHelper.GetClientIpAddress(),
                User = customer,
                //PageUrl = WebHelper.GetThisPageUrl(true),
                // ReferrerUrl = WebHelper.GetUrlReferrer(),
                CreatedOnUtc = DateTime.UtcNow
            };

            Repository.Insert(log);

            return log;
        }

        /// <summary>
        ///     Gets a value indicating whether this message should not be logged
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Result</returns>
        protected virtual bool IgnoreLog(string message)
        {
            //if (!_commonSettings.IgnoreLogWordlist.Any())
            //    return false;

            //if (String.IsNullOrWhiteSpace(message))
            //    return false;

            //return _commonSettings
            //    .IgnoreLogWordlist
            //    .Any(x => message.IndexOf(x, StringComparison.InvariantCultureIgnoreCase) >= 0);

            return false;
        }
    }
}