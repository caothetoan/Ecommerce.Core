using Vnit.ApplicationCore.Constants;
using Vnit.ApplicationCore.Entities.Catalog;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services.SEO;
using Vnit.WebFramework.Models.Catalogs;

namespace Vnit.WebFramework.ModelExtensions
{
    public static class ProductExtensions
    {
        public static ProductModel ToModel(this Product page)
        {
            var model = page.Map<ProductModel>();
            
            model.SeName = string.Format("/{0}/{1}", RouteConstants.Product, page.GetSeName());
            return model;
        }

        public static Product ToEntity(this ProductModel entity)
        {
            return entity.Map<Product>();
        }
    }
}
