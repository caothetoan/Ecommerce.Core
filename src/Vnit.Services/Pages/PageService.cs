using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Pages;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.Pages
{
    public class PageService : EntityService<Page>, IPageService
    {
        public PageService(IDataRepository<Page> dataRepository) : base(dataRepository)
        {
        }
    }
}
