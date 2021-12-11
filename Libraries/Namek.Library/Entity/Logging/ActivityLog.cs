using System;
using Namek.Library.Data;
using Namek.Library.Entity.Users;
using Namek.Library.Enums;

namespace Namek.Library.Entity.Logging
{
    /// <summary>
    ///     Represents an activity log record
    /// </summary>
    [System.ComponentModel.DataAnnotations.Schema.Table("ActivityLog")]
    public class ActivityLog : BaseEntity
    {
        public ActivityLog()
        {
            LogType = 0;
        }
        /// <summary>
        ///     Gets or sets the activity log type identifier
        /// </summary>
        public int ActivityLogTypeId { get; set; }

        /// <summary>
        ///     Gets or sets the User identifier
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Gets or sets the activity comment
        /// </summary>
        public string Comment { get; set; }

        public string BeforeParams { get; set; }

        public string AfterParams { get; set; }

        public string RedirectUrl { get; set; }

        public byte LogType { get; set; }

        /// <summary>
        ///     Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        ///     Gets the activity log type
        /// </summary>
        public virtual ActivityLogType ActivityLogType { get; set; }

        /// <summary>
        ///     Gets the User
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        ///     Gets or sets the ip address
        /// </summary>
        public virtual string IpAddress { get; set; }
    }
}