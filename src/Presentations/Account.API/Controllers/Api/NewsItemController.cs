using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Vnit.Api.Extensions;
using Vnit.Api.ViewModels;
using Vnit.Api.ViewModels.News;
using Vnit.ApplicationCore.Constants;
using Vnit.ApplicationCore.Entities.Localization;
using Vnit.ApplicationCore.Entities.News;
using Vnit.ApplicationCore.Enums;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services.Localization;
using Vnit.ApplicationCore.Services.News;
using Vnit.ApplicationCore.Services.SEO;

namespace Vnit.Api.Controllers.Api
{
    public class NewsItemController : BaseApiController
    {
        #region Members
        private readonly INewsItemService _newsItemService;
        private readonly INewsItemCategoryService _newsPubsService;
        private readonly ILocalizedPropertyService _localizedPropertyService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly INewsItemTagService _newsItemTagService;
        #endregion

        #region Ctors
        public NewsItemController(INewsItemService newsItemService, INewsItemCategoryService newsPubsService, ILocalizedPropertyService localizedPropertyService, IUrlRecordService urlRecordService, INewsItemTagService newsItemTagService)
        {
            _newsItemService = newsItemService;
            _newsPubsService = newsPubsService;
            _localizedPropertyService = localizedPropertyService;
            _urlRecordService = urlRecordService;
            _newsItemTagService = newsItemTagService;
        }
        #endregion

        #region Utilities
        [NonAction]
        protected virtual void InsertLocales(NewsItem newsItem, NewsItemModel newsItemModel)
        {
            if (newsItemModel.LanguageId <= 0)
            {
                return;
            }
            newsItemModel.Locales.Add(new NewsItemLocalizedModel()
            {
                LanguageId = newsItemModel.LanguageId,
                Name = newsItemModel.Name,

            });
            foreach (var localized in newsItemModel.Locales)
            {
                //ILocalizedPropertyService.SaveLocalizedValue(NewsItem,
                //    x => x.Name,
                //    localized.Name,
                //    localized.LanguageId);
                #region LocalizedProperty
                
                    _localizedPropertyService.InsertLocalizedProperty(new LocalizedProperty()
                    {
                        EntityId = newsItem.Id,
                        LanguageId = localized.LanguageId,
                        LocaleKeyGroup = "NewsItem",
                        LocaleKey = "title",
                        LocaleValue = newsItem.Name
                    });
                    _localizedPropertyService.InsertLocalizedProperty(new LocalizedProperty()
                    {
                        EntityId = newsItem.Id,
                        LanguageId = localized.LanguageId,
                        LocaleKeyGroup = "NewsItem",
                        LocaleKey = "summary",
                        LocaleValue = newsItem.Short
                    });
                    _localizedPropertyService.InsertLocalizedProperty(new LocalizedProperty()
                    {
                        EntityId = newsItem.Id,
                        LanguageId = localized.LanguageId,
                        LocaleKeyGroup = "NewsItem",
                        LocaleKey = "content",
                        LocaleValue = newsItem.Full
                    });                
                #endregion

                ////search engine name
                //var seName = NewsItem.ValidateSeName(localized.SeName, localized.Name, false);
                //_urlRecordService.SaveSlug(NewsItem, seName, localized.LanguageId);
            }
            VerboseReporter.ReportSuccess("Sửa ngôn ngữ tin tức thành công", "put");
        }

        [NonAction]
        protected virtual void UpdateLocales(NewsItem NewsItem, NewsItemModel entityModel)
        {
            if (entityModel.LanguageId <= 0)
            {
                return;
            }

            entityModel.Locales.Add(new NewsItemLocalizedModel()
            {
                LanguageId = entityModel.LanguageId,
                Name = entityModel.Name,

            });
            foreach (var localized in entityModel.Locales)
            {
                #region LocalizedProperty

                _localizedPropertyService.SaveLocalizedValue<NewsItem>(NewsItem, n => n.Name, NewsItem.Name, localized.LanguageId);

                _localizedPropertyService.SaveLocalizedValue<NewsItem>(NewsItem, n => n.Short, NewsItem.Short, localized.LanguageId);

                _localizedPropertyService.SaveLocalizedValue<NewsItem>(NewsItem, n => n.Full, NewsItem.Full, localized.LanguageId);

                #endregion

                //search engine name
                //var seName = NewsItem.ValidateSeName(localized.SeName, localized.Name, false);
                //_urlRecordService.SaveSlug(NewsItem, seName, localized.LanguageId);
            }

            VerboseReporter.ReportSuccess("Sửa ngôn ngữ tin tức thành công", "put");
        }

        [NonAction]
        protected virtual void GetLocales(NewsItem newsItem, NewsItemModel entityModel)
        {
            var localizedProperties = newsItem.GetLocalizedPropertys(entityModel.LanguageId);

            entityModel.LanguageId = entityModel.LanguageId;
            entityModel.Name = localizedProperties.FirstOrDefault(x => x.LocaleKey == "Name")?.LocaleValue;
            entityModel.Short = localizedProperties.FirstOrDefault(x => x.LocaleKey == "summary")?.LocaleValue;
            entityModel.Full = localizedProperties.FirstOrDefault(x => x.LocaleKey == "content")?.LocaleValue;

        }
        #endregion

       
        /// <summary>
        /// Lấy ds tin tức
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get([FromQuery] RootRequestModel requestModel)
        {
            #region predicate

            Expression<Func<NewsItem, bool>> where = x => true;
            DateTime dateFrom, dateTo;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = a => a.Name.Contains(requestModel.Name);
            if ((requestModel.DateFrom.IsNotNullOrEmpty()))
            {
                dateFrom = requestModel.DateFrom.ToDateTime();
                where = ExpressionHelpers.CombineAnd<NewsItem>(where, a => a.CreatedOnUtc >= dateFrom);
            }
            if ((requestModel.DateTo.IsNotNullOrEmpty()))
            {
                dateTo = requestModel.DateTo.ToDateTime().AddDays(1);
                where = ExpressionHelpers.CombineAnd<NewsItem>(where, a => a.CreatedOnUtc <= dateTo);
            }

           
            if (requestModel.CategoryId.HasValue)
            {
                where = ExpressionHelpers.CombineAnd<NewsItem>(where,
                    a => a.NewsItemCategories.Any(x => x.NewsCategoryId == requestModel.CategoryId.Value));
            }
            #endregion


            var newsItems = await _newsItemService.GetPagedListAsync(
                where,
                x => x.StartDateUtc,
                false,
                requestModel.Page - 1,
                requestModel.Count);

            return RespondSuccess(newsItems.Select(x => x.ToModel()), newsItems.TotalCount);
        }

        [Route("get/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] NewsItemRequestModel requestModel)
        {
            #region predicate


            Expression<Func<NewsItem, bool>> where = x => true;
           
            where = ExpressionHelpers.CombineAnd(where, x => x.Id == requestModel.Id);

            #endregion

            var article = await _newsItemService.FirstOrDefaultAsync(
                where,
                x => x.NewsItemCategories,
                x => x.NewsItemTags);
            if (article == null)
            {

                VerboseReporter.ReportError("Không tìm thấy tin tức này", "get");
                return RespondFailure();
            }

            var model = article.ToModel(requestModel.LanguageId);

            //GetLocales(article, model);

            return RespondSuccess(model);
        }

        [HttpGet]
        [Route("get/{seName}")]
        public async Task<IActionResult> GetBySeName(string seName)
        {
            var newsItem = await _newsItemService.GetBySeNameAsync(seName);
            if (newsItem == null)
                return RespondFailure();

            var model = newsItem.ToModel();

            return RespondSuccess(model);
        }

        [Route("post")]
        [HttpPost]
        public IActionResult Post(NewsItemModel entityModel)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            if (entityModel == null)
            {
                VerboseReporter.ReportError("Dữ liệu không hợp lệ.");
                return RespondFailure();
            }
            DateTime publishDt = entityModel.StartDateStr.ToDateTime();

            #region save NewsItem
            var article = new NewsItem
            {
                Name = entityModel.Name,
                Short = entityModel.Short.GetSubString( ConstantKey.SummaryMaxLength),
                Full = HttpUtility.HtmlDecode(entityModel.Full),
                Published = entityModel.Published,
               
                CreatedOnUtc = publishDt, // DateTime.Now,
               
                StartDateUtc = publishDt,
                EndDateUtc = entityModel.EndDateUtc,
              
                MetaTitle = entityModel.MetaTitle ?? entityModel.Name,
                MetaKeywords = entityModel.MetaKeywords ?? entityModel.Name,

            };

            article.MetaDescription = entityModel.MetaDescription ?? article.Short;

            //save it
            _newsItemService.Insert(article);

            VerboseReporter.ReportSuccess("Tạo tin tức thành công", "post");
            #endregion

            #region newsCategoryIds
            if (entityModel.newsCategoryIds != null && entityModel.newsCategoryIds.Length > 0)
            {
                article.NewsItemCategories = new List<NewsItemCategory>();

                foreach (int newsCategoryId in entityModel.newsCategoryIds)
                {
                    NewsItemCategory newsPubs = new NewsItemCategory()
                    {
                        NewsItemId = article.Id,
                        NewsCategoryId = newsCategoryId,
                    };
                    article.NewsItemCategories.Add(newsPubs);
                }
                _newsItemService.Update(article);
            }
            #endregion

            #region TagIds

            if (entityModel.TagIds != null && entityModel.TagIds.Length > 0)
            {
                article.NewsItemTags = new List<NewsItemTag>();
                foreach (var entityModelTagId in entityModel.TagIds)
                {
                    NewsItemTag newsItemTag = new NewsItemTag()
                    {
                        NewsItemId = article.Id,
                        TagId = entityModelTagId
                    };
                    article.NewsItemTags.Add(newsItemTag);
                }
                _newsItemService.Update(article);
            }

            #endregion


            InsertLocales(article, entityModel);


            return RespondSuccess(article);
        }

        [Route("put")]
        [HttpPut]
        public IActionResult Put(NewsItemModel entityModel)
        {
            #region validate
            //if (!ModelState.IsValid)
            //    return BadRequest();
            if (entityModel == null)
            {
                VerboseReporter.ReportError("Dữ liệu không hợp lệ.");
                return RespondFailure();
            }
            DateTime publishDt = entityModel.StartDateStr.ToDateTime();

            //get  
            var article = _newsItemService.Get(entityModel.Id,
                x => x.NewsItemCategories,
                x => x.NewsItemTags);
            if (article == null)
            {

                VerboseReporter.ReportError("Không tìm thấy tin này", "post");
                return RespondFailure();
            } 
            #endregion

            #region NewsItem
            article.Name = entityModel.Name;
            article.Short = entityModel.Short.GetSubString(ConstantKey.SummaryMaxLength);
           
            article.Full = HttpUtility.HtmlDecode(entityModel.Full);
           
            article.StartDateUtc = publishDt;
            article.EndDateUtc = entityModel.EndDateUtc ?? publishDt.AddDays(7);
            article.DisplayOrder = entityModel.DisplayOrder;
            article.Published = entityModel.Published;
            article.UpdatedOnUtc = DateTime.Now;
            article.Version = article.Version + 1;

            article.MetaTitle = entityModel.MetaTitle ?? article.Name;
            article.MetaDescription = entityModel.MetaDescription ?? article.Short;
            article.MetaKeywords = entityModel.MetaKeywords ?? article.Name;
            //save it
            _newsItemService.Update(article);

            VerboseReporter.ReportSuccess("Sửa tin tức thành công", "put");
            #endregion


            #region newsCategory
            if (entityModel.newsCategoryIds != null && entityModel.newsCategoryIds.Length > 0)
            {

                foreach (int newsCategoryId in entityModel.newsCategoryIds)
                {
                    // tim chuyen muc
                    var foundNewsCategory = article.NewsItemCategories.FirstOrDefault(x => x.NewsCategoryId == newsCategoryId);
                    // neu khong tim thay chuyen muc trong bang quan he thi them moi
                    if (foundNewsCategory == null)
                    {
                        NewsItemCategory newsItemCategory = new NewsItemCategory()
                        {
                            NewsItemId = article.Id,
                            NewsCategoryId = newsCategoryId,
                        };
                        _newsPubsService.Insert(newsItemCategory);
                    }
                    //    foreach (var newsPub in article.NewsPubs)
                    //    {
                    //        if (Array.IndexOf(entityModel.newsCategoryIds, newsPub.cat_id) == -1)
                    //        {
                    //            NewsPubs newsPubs = new NewsPubs()
                    //            {
                    //                article_id = article.id,
                    //                cat_id = newsCategoryId,
                    //            };
                    //            if (article.news_status != (int) NewsItemStatus.Unpublish)
                    //            {
                    //                newsPubs.approve_date = DateTime.Now;
                    //                newsPubs.approver = CurrentUser.full_name;
                    //            }
                    //            article.NewsPubs.Add(newsPubs);
                    //        }
                    //    }
                }
                //_NewsItemService.Update(article);
                // find list newsPubs by article_id
                var newsItemCategories = _newsPubsService.Get(x => x.NewsCategoryId == entityModel.Id).ToList();
                foreach (var newsItemCategory in newsItemCategories)
                {
                    // tim chuyen muc
                    var foundNewsCategory = entityModel.newsCategoryIds.FirstOrDefault(x => x == newsItemCategory.NewsCategoryId);
                    // neu khong tim thay chuyen muc trong mang danh muc gui len thi xoa di
                    if (foundNewsCategory == 0)
                    {
                        _newsPubsService.Delete(newsItemCategory);
                    }
                }

            }
            #endregion

            #region TagIds

            if (entityModel.TagIds != null && entityModel.TagIds.Length > 0)
            {
                foreach (var entityModelTagId in entityModel.TagIds)
                {
                    var foundNewsItemTag =
                        article.NewsItemTags.FirstOrDefault(x => x.TagId == entityModelTagId);
                    if (foundNewsItemTag == null)
                    {
                        NewsItemTag tag = new NewsItemTag()
                        {
                            TagId = entityModelTagId,
                            NewsItemId = article.Id
                        };
                        _newsItemTagService.Insert(tag);
                    }
                }

                var newsItemTags = _newsItemTagService.Get(x => x.NewsItemId == entityModel.Id).ToList();
                foreach (var newsItemTag in newsItemTags)
                {
                    var foundNewsItemTag = entityModel.TagIds.FirstOrDefault(x => x == newsItemTag.TagId);
                    if (foundNewsItemTag == 0)
                    {
                        _newsItemTagService.Delete(newsItemTag);
                    }
                }
            }
            else
            {
                var newsItemTags = _newsItemTagService.Get(x => x.NewsItemId == entityModel.Id).ToList();
                foreach (var newsItemTag in newsItemTags)
                {
                    _newsItemTagService.Delete(newsItemTag);
                }
            }

            #endregion

            #region SaveLocalizedValue
            UpdateLocales(article, entityModel);
            #endregion

            return RespondSuccess(article);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id < 0)
                return RespondFailure();
            var article = _newsItemService.FirstOrDefault(x => x.Id == id,
                x => x.NewsItemCategories,
                x => x.NewsItemTags);
            if (article == null)
                return RespondFailure();

            _newsItemService.Delete(article);

            _localizedPropertyService.Delete(x => x.EntityId == article.Id && x.LocaleKeyGroup == "NewsItem");

            VerboseReporter.ReportSuccess("Xóa tin tức thành công", "delete");
            return RespondSuccess();
        }

        [Route("updateStatus/{id:int}")]
        [HttpPut]
        public IActionResult UpdateStatus(int id)
        {
            if (id < 0)
                return RespondFailure();
            var article = _newsItemService.FirstOrDefault(x => x.Id == id);
            if (article == null)
                return RespondFailure();

            article.Published = !article.Published;
            _newsItemService.Update(article);

            VerboseReporter.ReportSuccess("Duyệt tin tức thành công", "updateStatus");
            return RespondSuccess();
        }

        //
        [HttpGet]
        public IActionResult GetNewsItemStatuses()
        {
            return RespondSuccess(SelectListItemExtension.GetEnums<NewsItemStatus>());
        }
       
    }
}