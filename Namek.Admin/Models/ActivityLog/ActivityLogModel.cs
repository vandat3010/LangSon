using System;
using Namek.Admin.Extensions;
using Namek.Entity.RequestModel;

namespace Namek.Admin.Models.ActivityLog
{
    public class ActivityLogModel : BaseModel
    {
        public ActivityLogTypeModel ActivityLogTypeModel { get; set; }
        public UserModel UserModel { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IpAddress { get; set; }
        public string BeforeParams { get; set; }
        public string AfterParams { get; set; }
        public string RedirectUrl { get; set; }
    }
}