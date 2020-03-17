using Vnit.ApplicationCore.Entities.Pages;
using Vnit.ApplicationCore.Constants;
using Vnit.Services.SEO;
using Vnit.WebFramework.Models.Pages;

namespace Vnit.WebFramework.ModelExtensions
{
    public static class PageExtensions
    {
        public static PageModel ToModel(this Page page)
        {
            var model = new PageModel
            {
                Name = page.Name,
                Description = page.Description,
                Body = page.Body,
                Active = page.Active,
                Icon = page.Icon,
                Id = page.Id,
                CreateDate = page.CreateDate,
                CreateBy = page.CreateBy,
                IsDeleted = page.IsDeleted,
                SeName = string.Format("/{0}/{1}", RouteConstants.Page, page.GetSeName())
            };
            return model;
        }

        public static Page ToEntity(this PageModel entity)
        {
            return new Page()
            {
                Name = entity.Name,
                Description = entity.Description,
                Body = entity.Body,
                Active = entity.Active,
                Icon = entity.Icon,
                Id = entity.Id,
                CreateDate = entity.CreateDate,
                CreateBy = entity.CreateBy,
                IsDeleted = entity.IsDeleted
            };
        }
    }
   
}
