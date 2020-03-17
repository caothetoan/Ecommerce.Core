using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Entities.MediaAggregate
{
    public class Media : BaseEntity
    {
        public string Name { get; set; }

        public string SystemName { get; set; }

        public string Description { get; set; }

        public string AlternativeText { get; set; }

        public string LocalPath { get; set; }

        public string ThumbnailPath { get; set; }

        public string MimeType { get; set; }

        public byte[] Binary { get; set; }

        public MediaType MediaType { get; set; }

        public int UserId { get; set; }

        public DateTime DateCreated { get; set; }


        public bool? IsFeatured { get; set; }


        //public virtual IList<MediaTag> MediaTags { get; set; }

        //public virtual IList<EntityMedia> EntityMedias { get; set; }
    }
}
