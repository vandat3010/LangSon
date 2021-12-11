using Namek.Library.Data;
using Namek.Library.Entity.Logging;
using Namek.Library.Services;

namespace Namek.LogServices.Logging
{
    public class ActivityLogTypeService : BaseEntityService<ActivityLogType>, IActivityLogTypeService
    {
        public ActivityLogTypeService(IDataRepository<ActivityLogType> dataRepository) : base(dataRepository) { }
    }
}