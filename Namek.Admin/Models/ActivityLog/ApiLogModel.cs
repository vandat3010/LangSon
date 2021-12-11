using System;
using Namek.Entity.RequestModel;
using Namek.Library.Entity.Logging;

namespace Namek.Admin.Models.ActivityLog
{
    public class ApiLogModel : BaseModel
    {
        public ActivityLogModel ActivityLogModel { get; set; }
        public LogLevel LogLevel { get; set; }
        public UserModel UserModel { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string IpAddress { get; set; }
        public string ShortMessage { get; set; }
        public string FullMessage { get; set; }
        public string PageUrl { get; set; }
        public string RefererUrl { get; set; }
        public bool? Success { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string JobId { get; set; }
        public bool? IsRequeued { get; set; }
        public int? ServiceId { get; set; }

        public bool IsDisabled
        {
            get
            {
                if (string.IsNullOrEmpty(JobId))
                    return true;
                if (IsRequeued == null)
                {
                    return Success ?? false;
                }
                return true;
            }
        }
    }
}