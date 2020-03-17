using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.News;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.News
{
    public class NewsCategoryService : EntityService<NewsCategory>, INewsCategoryService
    {
        public NewsCategoryService(IDataRepository<NewsCategory> dataRepository) : base(dataRepository)
        {
        }



    }
}
