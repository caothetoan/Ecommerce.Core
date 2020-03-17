using System.Collections.Generic;
using Vnit.ApplicationCore.Entities.Localization;

namespace Vnit.ApplicationCore.Services.Localization
{
    public interface ILocaleStringResourceService : IBaseEntityService<LocaleStringResource>
    {
        IList<LocaleStringResource> GetLocaleStringResources(int langId);

        string GetResource(string resourceKey);

        string GetResource(string resourceKey, int languageId,
            bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false);

        Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(int languageId);
    }
}
