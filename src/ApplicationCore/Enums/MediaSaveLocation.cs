using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Vnit.ApplicationCore.Enums
{
    public enum MediaSaveLocation
    {
        [Description("Database")]
        Database = 0,
        [Description("FileSystem")]
        FileSystem = 1
    }
}
