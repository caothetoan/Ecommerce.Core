
using System.Collections.Generic;
using Vnit.ApplicationCore.Entities.Catalog;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.Catalogs
{
    public interface IProductService : IBaseEntityService<Product>
    {
        void AttachProductToCategory(int productId, int categoryId);

        void AttachProductToCategory(Product product, Category category);

        void Add(Product product, int[] productCategoryIds);

        void UnassignProductToCategory(Product product, Category category);
        void UnassignProductToCategory(int productId, int categoryId);
        

        IList<Product> GetProductsByCategoryId(int categoryId);
    }
}
