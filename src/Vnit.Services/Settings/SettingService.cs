using System;
using System.Collections.Generic;
using System.Linq;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Settings;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Services.Settings
{
    public  class SettingService : BaseEntityService<Setting>, ISettingService
    {
        public SettingService(IDataRepository<Setting> dataRepository)
            : base(dataRepository)
        {
        }

        public Setting Get<T>(string keyName) where T : ISettings
        {
            var groupName = typeof(T).Name;
            var settings = Repository.Get(x => x.Name == keyName);
            if (!string.IsNullOrEmpty(groupName))
                settings = settings.Where(x => x.GroupName == groupName);

            return settings.FirstOrDefault();
        }

        public void Save<T>(string keyName, string keyValue) where T : ISettings
        {
            var groupName = typeof(T).Name;

            //check if setting exist
            var setting = Get<T>(keyName);
            if (setting == null)
            {
                setting = new Setting()
                {
                    GroupName = groupName,
                    Name = keyName,
                    Value = keyValue
                };
                Repository.Insert(setting);
            }
            else
            {
                setting.Value = keyValue;
                Repository.Update(setting);
            }
        }

        public void Save<T>(T settings) where T : ISettings
        {
            //each setting group will have some properties. We'll loop through these using reflection
            var propertyFields = typeof(T).GetProperties();
            foreach (var property in propertyFields)
            {
                var propertyName = property.Name;
                var valueObj = property.GetValue(settings);
                var value = valueObj == null ? "" : valueObj.ToString();
                //save the property
                Save<T>(propertyName, value);
            }
        }

        public T GetSettings<T>() where T : ISettings
        {
            //create a new settings object
            var settingsObj = Activator.CreateInstance<T>();

            FurnishInstance(settingsObj);

            return settingsObj;
        }

        public void LoadSettings<T>(T settingsObject) where T : ISettings
        {
            FurnishInstance(settingsObject);
        }

        private void FurnishInstance<T>(T settingsInstance) where T : ISettings
        {
            var settingInstanceType = settingsInstance.GetType();
            //each setting group will have some properties. We'll loop through these using reflection
            var propertyFields = settingInstanceType.GetProperties();

            foreach (var property in propertyFields)
            {
                var propertyName = property.Name;

                //retrive the value of setting from db
                var savedSettingEntity =
                    Get(x => x.Name == propertyName && x.GroupName == settingInstanceType.Name, null).FirstOrDefault();

                if (savedSettingEntity != null)
                    //set the property
                    property.SetValue(settingsInstance, PropertyTypeConverter.CastPropertyValue(property, savedSettingEntity.Value));
            }
        }

        public T GetSettings<T>(List<Setting> settings) where T : ISettings
        {
            if (settings == null)
                return default(T);
            //create a new settings object
            var settingsInstance = Activator.CreateInstance<T>();

            var settingInstanceType = settingsInstance.GetType();
            //each setting group will have some properties. We'll loop through these using reflection
            var propertyFields = settingInstanceType.GetProperties();

            foreach (var property in propertyFields)
            {
                var propertyName = property.Name;

                //retrive the value of setting from db
                var savedSettingEntity =
                    settings.Find(x => x.Name == propertyName && x.GroupName == settingInstanceType.Name);

                if (savedSettingEntity != null)
                    //set the property
                    property.SetValue(settingsInstance, PropertyTypeConverter.CastPropertyValue(property, savedSettingEntity.Value));
            }
            return settingsInstance;
        }
    }
}
