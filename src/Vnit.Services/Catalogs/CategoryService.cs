using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Catalog;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.Catalogs
{
   
    public class CategoryService : EntityService<Category>, ICategoryService
    {
        public CategoryService(IDataRepository<Category> dataRepository) : base(dataRepository)
        {
        }
    }
}
