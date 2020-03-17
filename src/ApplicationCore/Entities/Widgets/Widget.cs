using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Entities.Widgets
{
    public class Widget: BaseEntity
    {
        public string Name { get; set; }

        public bool Published { get; set; }

        public int DisplayOrder { get; set; }


        public DateTime CreatedOnUtc { get; set; }

        public DateTime? UpdatedOnUtc { get; set; }
    }
}
