using System.Collections.Generic;
using Vnit.ApplicationCore.Entities.Settings;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Services.Settings
{
    public  interface ISettingService : IBaseEntityService<Setting>
    {
        Setting Get<T>(string keyName) where T : ISettings;

        void Save<T>(string keyName, string keyValue) where T : ISettings;

        void Save<T>(T settings) where T : ISettings;

        T GetSettings<T>() where T : ISettings;

        void LoadSettings<T>(T settingsObject) where T : ISettings;

        T GetSettings<T>(List<Setting> settings) where T : ISettings;
    }
}
