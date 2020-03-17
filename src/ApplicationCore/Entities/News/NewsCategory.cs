using System;
using System.Collections.Generic;
using System.Text;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Entities.News
{
    public class NewsCategory : BaseEntity, ITracking, ISlugSupported
    {
        #region propertises
        public string Name { get; set; }

        public string Short { get; set; }

        public int DisplayOrder { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }

        public int? ParentId { get; set; }
        #endregion
    }
}
