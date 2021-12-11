using Namek.Library.Data;
using Namek.Library.Entity.PermissionActions;
using Namek.Library.Services;

namespace Namek.LogServices.PermissionActions
{
    public class PermissionActionService : BaseEntityService<PermissionAction>, IPermissionActionService
    {
        public PermissionActionService(IDataRepository<PermissionAction> dataRepository) : base(dataRepository) { }
    }
}