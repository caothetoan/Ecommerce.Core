using System;
using System.Collections.Generic;
using Vnit.WebFramework.Models;

namespace Vnit.WebFramework.Models.News
{
    public class NewsItemModel: RootModel
    {
        public NewsItemModel()
        {
            NewsItemCategory = new List<NewsItemCategoryModel>();
            Locales= new List<NewsItemLocalizedModel>();
        }


        #region propertises
        /// <summary>
        /// Gets or sets the language identifier
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the news title
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the short text
        /// </summary>
        public string Short { get; set; }

        /// <summary>
        /// Gets or sets the full text
        /// </summary>
        public string Full { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the news item is published
        /// </summary>
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public int Version { get; set; }
        /// <summary>
        /// Gets or sets the news item start date and time
        /// </summary>
        public DateTime? StartDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the news item end date and time
        /// </summary>
        public DateTime? EndDateUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the news post comments are allowed 
        /// </summary>
        public bool AllowComments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is limited/restricted to certain stores
        /// </summary>
        public bool LimitedToStores { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
        public int Pageview { get; set; }

        #endregion


        #region relation
        /// <summary>
        /// Danh sách mapping tin tức và danh mục
        /// </summary>
        public IList<NewsItemCategoryModel> NewsItemCategory { get; set; }
        /// <summary>
        /// Danh sách mapping tin tức và từ khóa
        /// </summary>
        public List<NewsItemTagModel> NewsArticleTags { get; set; }

        public IList<NewsItemLocalizedModel> Locales { get; set; }

        /// <summary>
        /// Danh sách id từ khóa
        /// </summary>
        public int[] TagIds { get; set; }

        //[Required]
        public int[] newsCategoryIds { get; set; }

        /// <summary>
        /// Friendly URL
        /// </summary>
        public string SeName { get; set; }

        /// <summary>
        /// Ảnh thumbnail
        /// </summary>
        public string Thumbnail { get; set; }

       
        public string StartDateStr { get; set; }

        #endregion
    }
}