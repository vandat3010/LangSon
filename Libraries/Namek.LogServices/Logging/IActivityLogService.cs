using Namek.Library.Entity.Logging;
using Namek.Library.Enums;
using Namek.Library.Services;

namespace Namek.LogServices.Logging
{
    public interface IActivityLogService : IBaseEntityService<ActivityLog>
    {
        void Insert(ActivityLog activityLog, ActivityLogTypeEnum activityLogTypeEnum);
    }
}