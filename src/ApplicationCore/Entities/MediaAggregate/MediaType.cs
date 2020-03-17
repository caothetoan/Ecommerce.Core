using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Vnit.ApplicationCore.Entities.MediaAggregate
{
    public enum MediaType
    {
        [Description("Header")]
        Image,
        [Description("Gallery")]
        Gallery,
        [Description("Video")]
        Video,
        [Description("Zip")]
        Zip,
        [Description("Pdf")]
        Pdf,
        [Description("Powerpoint")]
        Powerpoint,
        [Description("Word")]
        Word
    }

}
