using System;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Entities.Faqs
{
    public partial class FAQ : BaseEntity, ITracking, IPermalinkSupported, ISlugSupported, IMetaData
    {
        #region propertises
        /// <summary>
        /// Câu hỏi
        /// </summary>
        public string Name { get; set; }

        public string Short { get; set; }

        /// <summary>
        /// Trả lời
        /// </summary>
        public string answer { get; set; }


        public int faq_status { get; set; }

        public int Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? FaqCategoryId { get; set; }

        #region tracking
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        #endregion

        /// <summary>
        /// Lượt xem
        /// </summary>
        public int Pageview { get; set; }

        #region Meta tag

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public int? Sequence { get; set; }

        public int? Version { get; set; }
        #endregion
        #endregion

        #region relation

        public virtual FaqCategory FaqCategory { get; set; }

        #endregion
    }
}
