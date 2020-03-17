using Vnit.Api.ViewModels.News;
using Vnit.ApplicationCore.Entities.News;

namespace Vnit.Api.Extensions
{
    public static class NewsItemTagExtensions
    {
        public static NewsItemTagModel ToModel(this NewsItemTag a)
        {
            return new NewsItemTagModel()
            {
                Id = a.Id,
                NewsItemId = a.NewsItemId,
                TagId = a.TagId,
                DisplayOrder = a.DisplayOrder,
                Tag = a.Tag.ToModel()
            };
        }

        public static NewsItemTag ToEntity(this  NewsItemTagModel a)
        {
            return new NewsItemTag()
            {
                Id = a.Id,
                NewsItemId = a.NewsItemId,
                TagId = a.TagId,
                DisplayOrder = a.DisplayOrder,
                Tag = a.Tag.ToEntity()
            };
        }
    }
}