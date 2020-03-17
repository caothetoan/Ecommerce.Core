using System;
using Vnit.Api.ViewModels.Catalogs;
using Vnit.ApplicationCore.Constants;
using Vnit.ApplicationCore.Entities.Catalog;
using Vnit.ApplicationCore.Helpers;
using Vnit.Services.SEO;

namespace Vnit.Api.Extensions
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
            category.CreatedOnUtc = DateTime.Now;
            category.UpdatedOnUtc = DateTime.Now;

            return category;
        }

        public static Category ToEntity(this CategoryPostModel model)
        {
            Category category = model.Map<Category>();
            category.CreatedOnUtc = DateTime.Now;
            category.UpdatedOnUtc = DateTime.Now;

            return category;
        }
    }
}
