using System;
using System.Collections.Generic;
using System.Text;
using Vnit.ApplicationCore.Entities.Localization;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Entities.News
{
    /// <summary>
    /// Represents a news item
    /// </summary>
    public partial class NewsItem : BaseEntity, ISlugSupported, IStoreMappingSupported, IHasLocalizedProperty<NewsItem>
    {
        private ICollection<NewsComment> _newsComments;
        //private ICollection<NewsItemCategory> _newsItemCategories;

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

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime? UpdatedOnUtc { get; set; }

        public int Pageview { get; set; }

        public string Thumbnail { get; set; }

        public int Version { get; set; }
        #region relation

        /// <summary>
        /// Gets or sets the news comments
        /// </summary>
        public virtual ICollection<NewsComment> NewsComments
        {
            get => _newsComments ?? (_newsComments = new List<NewsComment>());
            protected set => _newsComments = value;
        }

        public virtual ICollection<NewsItemCategory> NewsItemCategories { get; set; }
        //{
        //    get => _newsItemCategories ?? (_newsItemCategories = new List<NewsItemCategory>());
        //    protected set => _newsItemCategories = value;
        //}

        public virtual ICollection<NewsItemTag> NewsItemTags { get; set; }

        /// <summary>
        /// Gets or sets the language
        /// </summary>
        public virtual Language Language { get; set; } 
        #endregion

    }
}
