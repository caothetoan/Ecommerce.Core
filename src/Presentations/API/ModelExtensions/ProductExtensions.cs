using Vnit.ApplicationCore.Constants;
using Vnit.ApplicationCore.Entities.Catalog;
using Vnit.ApplicationCore.Helpers;
using Vnit.Services.SEO;
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

        public static Product ToEntity(this ProductModel model)
        {
            var entity = model.Map<Product>();
            entity.CreatedOnUtc = System.DateTime.Now;
            entity.Short = model.Short.SanitizeHtml();
            entity.Full = model.Full.SanitizeHtml();
            return entity;
        }
    }
}
