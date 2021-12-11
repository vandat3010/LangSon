using Automation.Library.Data;
using Automation.Library.Entity.Modules;
using Automation.Library.Services;

namespace Automation.LogServices.Agency
{
    public class AgencyService : BaseEntityService<Module>, IAgencyService
    {
        public AgencyService(IDataRepository<Module> dataRepository) : base(dataRepository) { }
    }
}