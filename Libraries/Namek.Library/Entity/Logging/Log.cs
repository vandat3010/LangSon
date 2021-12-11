using System;
using System.ComponentModel.DataAnnotations.Schema;
using Namek.Library.Data;
using Namek.Library.Entity.Users;

namespace Namek.Library.Entity.Logging
{
    /// <summary>
    ///     Represents a log record
    /// </summary>
    public class Log : BaseEntity
    {
        /// <summary>
        ///     Gets or sets the log level identifier
        /// </summary>
        public int LogLevelId { get; set; }

        /// <summary>
        ///     Service Id
        /// </summary>
        public int? ServiceId { get; set; }

        /// <summary>
        ///     Gets or sets ActivityLog identifier
        /// </summary>
        public int? ActivityLogId { get; set; }

        /// <summary>
        ///     Gets or sets the short message
        /// </summary>
        public string ShortMessage { get; set; }

        /// <summary>
        ///     Gets or sets the full exception
        /// </summary>
        public string FullMessage { get; set; }

        /// <summary>
        ///     Gets or sets the IP address
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        ///     Gets or sets the User identifier
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        ///     Gets or sets the page URL
        /// </summary>
        public string PageUrl { get; set; }

        /// <summary>
        ///     Gets or sets the referrer URL
        /// </summary>
        public string ReferrerUrl { get; set; }

        /// <summary>
        ///     Gets or sets the short message
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        ///     Gets or sets the full exception
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        ///     Gets or sets status success or failure
        /// </summary>
        public bool? Success { get; set; }

        /// <summary>
        ///     Gets or sets status success or failure
        /// </summary>
        public int? RetryTimes { get; set; }

        /// <summary>
        ///     Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime? RetryOnUtc { get; set; }

        /// <summary>
        ///     Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        ///     Gets or sets the date and time of instance change
        /// </summary>
        public DateTime? ModifiedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the Hangfire job Id
        /// </summary>
        public string JobId { get; set; }

        /// <summary>
        /// Gets or sets the IsRequeue field.
        /// </summary>
        public bool? IsRequeued { get; set; }

        /// <summary>
        ///     Gets or sets the log level
        /// </summary>
        [NotMapped]
        public LogLevel LogLevel
        {
            get => (LogLevel)LogLevelId;
            set => LogLevelId = (int)value;
        }

        /// <summary>
        ///     Gets or sets the User
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        ///     Gets or sets the ActivityLog
        /// </summary>
        public virtual ActivityLog ActivityLog { get; set; }
    }
}