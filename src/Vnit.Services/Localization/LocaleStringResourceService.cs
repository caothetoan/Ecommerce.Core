using System;
using System.Collections.Generic;
using System.Linq;
using Vnit.ApplicationCore.Constants;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Localization;
using Vnit.ApplicationCore.Entities.Settings;
using Vnit.ApplicationCore.Interfaces;
using Vnit.ApplicationCore.Services.Caching;

namespace Vnit.ApplicationCore.Services.Localization
{
    public class LocaleStringResourceService : BaseEntityService<LocaleStringResource>, ILocaleStringResourceService
    {
        private readonly IWorkContext _workContext;

        private readonly ICacheService _cacheManager;

        private readonly LocalizationSettings _localizationSettings;

        public LocaleStringResourceService(IDataRepository<LocaleStringResource> dataRepository, IWorkContext workContext, ICacheService cacheManager, LocalizationSettings localizationSettings) : base(dataRepository)
        {
            _workContext = workContext;
            _cacheManager = cacheManager;
            _localizationSettings = localizationSettings;
        }

        /// <summary>
        /// Get ngôn ngữ theo entityId
        /// </summary>
        /// <param name="langId"></param>
        /// <returns></returns>
        public IList<LocaleStringResource> GetLocaleStringResources(int langId)
        {
            return Repository.Get(x =>          
            x.LanguageId == langId).ToList();
        }

        /// <summary>
        /// Gets all locale string resources by language identifier
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Locale string resources</returns>
        public virtual Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(int languageId)
        {
            string key = string.Format(ConstantKey.LOCALSTRINGRESOURCES_ALL_KEY, languageId);
            return _cacheManager.Get(key, () =>
            {
                //we use no tracking here for performance optimization
                //anyway records are loaded only for read-only operations
                var query = from l in Repository.TableNoTracking
                    orderby l.ResourceName
                    where l.LanguageId == languageId
                    select l;
                var locales = query.ToList();
                //format: <name, <id, value>>
                var dictionary = new Dictionary<string, KeyValuePair<int, string>>();
                foreach (var locale in locales)
                {
                    var resourceName = locale.ResourceName.ToLowerInvariant();
                    if (!dictionary.ContainsKey(resourceName))
                        dictionary.Add(resourceName, new KeyValuePair<int, string>(locale.Id, locale.ResourceValue));
                }
                return dictionary;
            });
        }

        /// <summary>
        /// Gets a resource string based on the specified ResourceKey property.
        /// </summary>
        /// <param name="resourceKey">A string representing a ResourceKey.</param>
        /// <returns>A string representing the requested resource string.</returns>
        public virtual string GetResource(string resourceKey)
        {
            if (_workContext.WorkingLanguage != null)
                return GetResource(resourceKey, _workContext.WorkingLanguage.Id);

            return "";
        }
        /// <summary>
        /// Gets a resource string based on the specified ResourceKey property.
        /// </summary>
        /// <param name="resourceKey">A string representing a ResourceKey.</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="logIfNotFound">A value indicating whether to log error if locale string resource is not found</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="returnEmptyIfNotFound">A value indicating whether an empty string will be returned if a resource is not found and default value is set to empty string</param>
        /// <returns>A string representing the requested resource string.</returns>
        public virtual string GetResource(string resourceKey, int languageId,
            bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false)
        {
            string result = string.Empty;
            if (resourceKey == null)
                resourceKey = string.Empty;
            resourceKey = resourceKey.Trim().ToLowerInvariant();
            if (_localizationSettings.LoadAllLocaleRecordsOnStartup)
            {
                //load all records (we know they are cached)
                var resources = GetAllResourceValues(languageId);
                if (resources.ContainsKey(resourceKey))
                {
                    result = resources[resourceKey].Value;
                }
            }
            else
            {
                //gradual loading
                string key = string.Format(ConstantKey.LOCALSTRINGRESOURCES_BY_RESOURCENAME_KEY, languageId, resourceKey);
                string lsr = _cacheManager.Get(key, () =>
                {
                    var query = from l in Repository.Table
                        where l.ResourceName == resourceKey
                              && l.LanguageId == languageId
                        select l.ResourceValue;
                    return query.FirstOrDefault();
                });

                if (lsr != null)
                    result = lsr;
            }
            if (String.IsNullOrEmpty(result))
            {
                //if (logIfNotFound)
                //    _logger.Warning(string.Format("Resource string ({0}) is not found. Language ID = {1}", resourceKey, languageId));

                if (!String.IsNullOrEmpty(defaultValue))
                {
                    result = defaultValue;
                }
                else
                {
                    if (!returnEmptyIfNotFound)
                        result = resourceKey;
                }
            }
            return result;
        }

    }
}
