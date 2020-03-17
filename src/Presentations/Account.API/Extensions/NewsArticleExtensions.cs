using System;
using System.Linq;
using System.Web;
using Vnit.Api.ViewModels.News;
using Vnit.ApplicationCore.Constants;
using Vnit.ApplicationCore.Entities.News;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services.Localization;
using Vnit.Services.SEO;

namespace Vnit.Api.Extensions
{
    public static class NewsItemExtensions
    {
        public static NewsItemModel ToModel(this NewsItem newsItem)
        {
            return new NewsItemModel()
            {
                Id = newsItem.Id,
                Name = newsItem.Name,
                Short = newsItem.Short.GetSubString(ConstantKey.SummaryMaxLength),
                Full = HttpUtility.HtmlDecode(newsItem.Full),
                Published = newsItem.Published,

                CreatedOnUtc = DateTime.Now,

                StartDateUtc = newsItem.StartDateUtc,
                EndDateUtc = newsItem.EndDateUtc,
                Pageview = newsItem.Pageview,

                Version = newsItem.Version,
                DisplayOrder = newsItem.DisplayOrder,
                MetaTitle = newsItem.MetaTitle,
                MetaDescription = newsItem.MetaDescription,
                MetaKeywords = newsItem.MetaKeywords,
                Thumbnail = string.Format("{0}", newsItem.Thumbnail),

                NewsArticleTags = newsItem.NewsItemTags?.Select(x => x.ToModel()).ToList(),
                NewsItemCategory = newsItem.NewsItemCategories?.Select(x => new NewsItemCategoryModel()
                {
                    NewsCategoryId = x.NewsCategoryId,
                    NewsItemId = x.NewsItemId,
                    Id = x.Id,
                    NewsCategory = x.NewsCategory?.ToModel()
                }).ToList()
            };

        }
        public static NewsItemModel ToModel(this NewsItem NewsItem, int languageId)
        {
            var model = NewsItem.ToModel();
            model.LanguageId = languageId;
            model.SeName = string.Format("/{0}/{1}", RouteConstants.NewsItemDetail,
                NewsItem.GetSlug());//languageId

            if (languageId <= 0)
            {
                return model;
            }

            var localizedProperties = NewsItem.GetLocalizedPropertys(languageId);
            var localePropertyName = localizedProperties.FirstOrDefault(x => x.LocaleKey == "Name");
            if (localePropertyName != null)
                model.Name = localePropertyName?.LocaleValue;

            var localePropertyDescription = localizedProperties.FirstOrDefault(x => x.LocaleKey == "summary");
            if (localePropertyDescription != null)
                model.Short = localePropertyDescription?.LocaleValue;

            var localePropertyBody = localizedProperties.FirstOrDefault(x => x.LocaleKey == "content");
            if (localePropertyBody != null)
                model.Full = localePropertyBody?.LocaleValue;
            return model;
        }

        public static void ToModel(this NewsItem newsItem, NewsItemModel entityModel)
        {
            if (entityModel.LanguageId <= 0)
            {
                return;
            }
            var localizedProperties = newsItem.GetLocalizedPropertys(entityModel.LanguageId);
            var localePropertyName = localizedProperties.FirstOrDefault(x => x.LocaleKey == "Name");
            if (localePropertyName != null)
                entityModel.Name = localePropertyName?.LocaleValue;

            var localePropertyDescription = localizedProperties.FirstOrDefault(x => x.LocaleKey == "summary");
            if (localePropertyDescription != null)
                entityModel.Short = localePropertyDescription?.LocaleValue;

            var localePropertyBody = localizedProperties.FirstOrDefault(x => x.LocaleKey == "content");
            if (localePropertyBody != null)
                entityModel.Full = localePropertyBody?.LocaleValue;
        }

        public static NewsItem ToEntity(this NewsItemModel model)
        {
            return model.Map<NewsItem>();
        }


    }
}