using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vnit.ApplicationCore.Constants;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Localization;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Services;
using Vnit.ApplicationCore.Services.Caching;

namespace Vnit.Services.Languages
{
    public class LanguageService : BaseEntityService<Language>, ILanguageService
    {
        private readonly ICacheService _cacheService;

        public LanguageService(IDataRepository<Language> dataRepository, ICacheService cacheService) : base(dataRepository)
        {
            _cacheService = cacheService;
        }
        public LanguageService(IDataRepository<Language> dataRepository) : base(dataRepository)
        {
        }
        /// <summary>
        /// Gets all languages
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Languages</returns>
        public virtual IList<Language> GetAllLanguages(bool showHidden = false)
        {
            string key = string.Format(ConstantKey.LANGUAGES_ALL_KEY, showHidden);
            var languages = _cacheService.Get(key, () =>
            {
                Expression<Func<Language, bool>> where = x => true;

                if (!showHidden)
                    where = ExpressionHelpers.CombineAnd<Language>(where, l => l.Published);

                var query = Repository.Get(where);
                if (!showHidden)
                    query = query.Where(l => l.Published);
                query = query.OrderBy(l => l.DisplayOrder).ThenBy(l => l.Id);
                return query.ToList();
            });

            return languages;
        }

    }
}
