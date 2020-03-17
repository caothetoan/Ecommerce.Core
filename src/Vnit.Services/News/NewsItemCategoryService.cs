using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.News;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.News
{
    public class NewsItemCategoryService : BaseEntityService<NewsItemCategory>, INewsItemCategoryService
    {
        public NewsItemCategoryService(IDataRepository<NewsItemCategory> dataRepository) : base(dataRepository)
        {
        }
    }
}
