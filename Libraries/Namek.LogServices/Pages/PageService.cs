using Namek.Library.Data;
using Namek.Library.Entity.Pages;
using Namek.Library.Services;

namespace Namek.LogServices.Pages
{
    public class PageService : BaseEntityService<Page>, IPageService
    {
        public PageService(IDataRepository<Page> dataRepository) : base(dataRepository) { }
    }
}