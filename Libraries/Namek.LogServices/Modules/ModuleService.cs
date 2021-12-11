using Namek.Library.Data;
using Namek.Library.Entity.Modules;
using Namek.Library.Services;

namespace Namek.LogServices.Modules
{
    public class ModuleService : BaseEntityService<Module>, IModuleService
    {
        public ModuleService(IDataRepository<Module> dataRepository) : base(dataRepository) { }
    }
}