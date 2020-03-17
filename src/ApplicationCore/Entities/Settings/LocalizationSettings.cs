using System;
using System.Collections.Generic;
using System.Text;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Entities.Settings
{
    public class LocalizationSettings : ISettings
    {
        public bool LoadAllUrlRecordsOnStartup { get; set; }
        public bool LoadAllLocalizedPropertiesOnStartup { get; set; }
        public bool LoadAllLocaleRecordsOnStartup { get; set; }
    }
}
