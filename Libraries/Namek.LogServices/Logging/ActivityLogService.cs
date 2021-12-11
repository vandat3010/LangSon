using Namek.Library.Data;
using Namek.Library.Entity.Logging;
using Namek.Library.Enums;
using Namek.Library.Helpers;
using Namek.Library.Services;

namespace Namek.LogServices.Logging
{
    public class ActivityLogService : BaseEntityService<ActivityLog>, IActivityLogService
    {
        private readonly IActivityLogTypeService _activityLogTypeService;

        public ActivityLogService(IDataRepository<ActivityLog> dataRepository,
            IActivityLogTypeService activityLogTypeService) : base(dataRepository)
        {
            _activityLogTypeService = activityLogTypeService;
        }

        public void Insert(ActivityLog activityLog, ActivityLogTypeEnum activityLogTypeEnum)
        {
            var activityLogTypeName = activityLogTypeEnum.GetDisplayName();
            var activityLogType = _activityLogTypeService.FirstOrDefault(n => n.SystemKeyword == activityLogTypeName);
            if (activityLogType == null)
            {
                activityLogType = new ActivityLogType
                {
                    SystemKeyword = activityLogTypeEnum.GetDisplayName(),
                    Enabled = true,
                    Name = activityLogTypeEnum.GetDescription()
                };
                _activityLogTypeService.Insert(activityLogType);
            }
            activityLog.ActivityLogTypeId = activityLogType.Id;
            Repository.Insert(activityLog);
        }
    }
}