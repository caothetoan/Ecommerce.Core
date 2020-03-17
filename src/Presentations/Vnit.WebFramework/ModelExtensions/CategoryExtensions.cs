using System;
using Vnit.ApplicationCore.Constants;
using Vnit.ApplicationCore.Entities.Catalog;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services.SEO;
using Vnit.WebFramework.Models.Catalogs;

namespace Vnit.WebFramework.ModelExtensions
{
    public static class CategoryExtensions
    {
        public static CategoryModel ToModel(this Category category)
        {
            var model = category.Map<CategoryModel>();
            
            model.SeName = string.Format("/{0}/{1}", RouteConstants.Category, category.GetSeName());
            return model;
        }

        public static Category ToEntity(this CategoryModel model)
        {
            Category category = model.Map<Category>();
            category.CreatedOnUtc = category.UpdatedOnUtc = DateTime.Now;

            return category;
        }

        public static Category ToEntity(this CategoryPostModel model)
        {
            Category category = model.Map<Category>();
            category.CreatedOnUtc = category.UpdatedOnUtc = DateTime.Now;

            return category;
        }
    }
}
