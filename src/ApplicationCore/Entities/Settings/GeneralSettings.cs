using System;
using System.Collections.Generic;
using System.Text;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Entities.Settings
{
    public class GeneralSettings : ISettings
    {
        /// <summary>
        /// The domain where public website has been installed
        /// </summary>
        public string Domain { get; set; }

        public string MediaSaveLocation { get; set; }


        /// <summary>
        /// The domain where api has been installed
        /// </summary>
        public string WebsiteApiUrl { get; set; }

        public string AutomationApiUrl { get; set; }

        /// <summary>
        /// The domain for which authentication cookie is issued. Keep this to a cross domain value (that begins with a .)
        /// </summary>
        public string ApplicationCookieDomain { get; set; }

        /// <summary>
        /// The domain where images are served. Default is same as ApplicationApiRoot
        /// </summary>
        public string ImageServerDomain { get; set; }

        /// <summary>
        /// The domain where videos are served. Default is same as ApplicationApiRoot
        /// </summary>
        public string VideoServerDomain { get; set; }

        /// <summary>
        /// Default timezone to be used for network
        /// </summary>
        public string DefaultTimeZoneId { get; set; }

        public int SettingVat { get; set; }
        public bool IsInstalled { get; set; }
    }
}
