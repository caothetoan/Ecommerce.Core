
using Vnit.ApplicationCore.Entities.News;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.News
{
    public interface INewsItemService : IBaseEntityService<NewsItem>
    {
        void Add(NewsItem newsItem, int[] newsCategoryIds);

        void AttachNewsItemToCategory(int newsItemId, int categoryId);

        void AttachNewsItemToCategory(NewsItem newsItem, NewsCategory category);
    }
}
