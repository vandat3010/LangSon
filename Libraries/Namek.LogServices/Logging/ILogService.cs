using Namek.Library.Entity.Logging;
using Namek.Library.Entity.Users;
using Namek.Library.Services;

namespace Namek.LogServices.Logging
{
    public interface ILogService : IBaseEntityService<Log>
    {
        /// <summary>
        ///     Determines whether a log level is enabled
        /// </summary>
        /// <param name="level">Log level</param>
        /// <returns>Result</returns>
        bool IsEnabled(LogLevel level);

        Log InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "", User customer = null);
    }
}