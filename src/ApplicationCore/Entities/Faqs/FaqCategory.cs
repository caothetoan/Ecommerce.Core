using System;
using System.Collections.Generic;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Entities.Faqs
{
    public class FaqCategory : BaseEntity, ITracking
    {
        #region propertises
        public string Name { get; set; }
        public int Sequence { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }

        public int? ParentId { get; set; }
        #endregion

        #region relationship

        public ICollection<FAQ> Faqs { get; set; }

        #endregion
    }
}
