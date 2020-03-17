using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Entities.EntityProperties
{
    public class EntityProperty : BaseEntity
    {
        public int EntityId { get; set; }

        public string EntityName { get; set; }

        public string PropertyName { get; set; }

        public string Value { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ExpiredDate { get; set; }
    }
}
