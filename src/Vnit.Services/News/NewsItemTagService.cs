using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.News;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.News
{
    public class NewsItemTagService : BaseEntityService<NewsItemTag>, INewsItemTagService
    {
        public NewsItemTagService(IDataRepository<NewsItemTag> dataRepository) : base(dataRepository)
        {
        }


    }
}
