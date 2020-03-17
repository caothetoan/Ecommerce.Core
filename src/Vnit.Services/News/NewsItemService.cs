using System;
using System.Linq;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Catalog;
using Vnit.ApplicationCore.Entities.News;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.News
{
    public class NewsItemService : EntityService<NewsItem>, INewsItemService
    {
        private readonly IDataRepository<NewsItemCategory> _newsItemCategoryRepository;

        public NewsItemService(IDataRepository<NewsItem> dataRepository, IDataRepository<NewsItemCategory> newsItemCategoryRepository) : base(dataRepository)
        {
            _newsItemCategoryRepository = newsItemCategoryRepository;
        }

        public void Add(NewsItem newsItem, int[] newsCategoryIds)
        {
            base.Insert(newsItem);


            if (newsCategoryIds != null && newsCategoryIds.Length > 0)
            {

                foreach (int categoryId in newsCategoryIds)
                {
                    AttachNewsItemToCategory(newsItem.Id, categoryId);
                }
            }
        }
        public void AttachNewsItemToCategory(int newsItemId, int categoryId)
        {
            if (newsItemId == 0)
            {
                throw new Exception("Can't attach newsItem with media with Id '0'");
            }
            //insert newsItem picture only if it doesn't exist
            var insertRequired =
                !_newsItemCategoryRepository.Get(x => x.NewsItemId == newsItemId && x.NewsCategoryId == categoryId).Any();

            if (!insertRequired) return;


            var entityMedia = new NewsItemCategory()
            {
                NewsItemId = newsItemId,
                NewsCategoryId = categoryId,
                DisplayOrder = 0
            };

            _newsItemCategoryRepository.Insert(entityMedia);
        }

        public void AttachNewsItemToCategory(NewsItem newsItem, NewsCategory category)
        {
            //if (category.Id == 0)
            //{
            //    _categoryRepository.Insert(category);
            //}
            AttachNewsItemToCategory(newsItem.Id, category.Id);
        }

    }
}
