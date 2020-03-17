using System.Collections.Generic;
using Vnit.ApplicationCore.Entities.Localization;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.Languages
{
    public interface ILanguageService : IBaseEntityService<Language>
    {
        IList<Language> GetAllLanguages(bool showHidden = false);
    }
}
