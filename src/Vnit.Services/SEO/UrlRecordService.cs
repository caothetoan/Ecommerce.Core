using System;
using System.Collections.Generic;
using System.Linq;
using Vnit.ApplicationCore.Constants;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities;
using Vnit.ApplicationCore.Entities.Seo;
using Vnit.ApplicationCore.Entities.Settings;
using Vnit.ApplicationCore.Interfaces;
using Vnit.ApplicationCore.Services;
using Vnit.ApplicationCore.Services.Caching;

namespace Vnit.Services.SEO
{
    public class UrlRecordService : BaseEntityService<UrlRecord>, IUrlRecordService
    {
        private readonly ICacheService _cacheManager;
        private readonly LocalizationSettings _localizationSettings;

        public UrlRecordService(IDataRepository<UrlRecord> dataRepository, ICacheService cacheManager, LocalizationSettings localizationSettings) : base(dataRepository)
        {
            _cacheManager = cacheManager;
            _localizationSettings = localizationSettings;
        }

        public UrlRecord GetUrlRecord<T>(T entity, string typeName = "", bool isUpdate = true) where T : IPermalinkSupported
        {
            //first check if the entity already has a permalink
            if (string.IsNullOrWhiteSpace(typeName))
                typeName = entity.GetType().Name;
            var urlRecord = FirstOrDefault(x => x.EntityName == typeName && x.EntityId == entity.Id);
            if (urlRecord == null)
            {
                urlRecord = new UrlRecord()
                {
                    EntityName = typeName,
                    EntityId = entity.Id,
                    Slug = SeoExtensions.GetSeName(entity.Name),
                    IsActive = true
                };
                //save it
                Insert(urlRecord);
            }
            else
            {
                if (isUpdate)
                {
                    urlRecord.Slug = SeoExtensions.GetSeName(entity.Name);
                    urlRecord.IsActive = true;

                    Update(urlRecord);
                }
            }
            return urlRecord;
        }

        /// <summary>
        /// Gets all cached URL records
        /// </summary>
        /// <returns>cached URL records</returns>
        protected virtual IList<UrlRecord> GetAllUrlRecordsCached()
        {
            //cache
            string key = string.Format(ConstantKey.URLRECORD_ALL_KEY);
            return _cacheManager.Get(key, () =>
            {
                //we use no tracking here for performance optimization
                //anyway records are loaded only for read-only operations
                var query = from ur in Repository.TableNoTracking
                            select ur;
                var urlRecords = query.ToList();
                var list = new List<UrlRecord>();
                foreach (var ur in urlRecords)
                {
                    var urlRecordForCaching = Map(ur);
                    list.Add(urlRecordForCaching);
                }
                return list;
            });
        }

        private UrlRecord Map(UrlRecord ur)
        {
            return ur;
        }


        /// <summary>
        /// Deletes an URL record
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        public virtual void DeleteUrlRecord(UrlRecord urlRecord)
        {
            if (urlRecord == null)
                throw new ArgumentNullException("urlRecord");

            Repository.Delete(urlRecord);

            //cache
            _cacheManager.RemoveByPattern(ConstantKey.URLRECORD_PATTERN_KEY);
        }

        /// <summary>
        /// Deletes an URL records
        /// </summary>
        /// <param name="urlRecords">URL records</param>
        public virtual void DeleteUrlRecords(IList<UrlRecord> urlRecords)
        {
            if (urlRecords == null)
                throw new ArgumentNullException("urlRecords");

            Repository.Delete(urlRecords);

            //cache
            _cacheManager.RemoveByPattern(ConstantKey.URLRECORD_PATTERN_KEY);
        }

        /// <summary>
        /// Gets an URL records
        /// </summary>
        /// <param name="urlRecordIds">URL record identifiers</param>
        /// <returns>URL record</returns>
        public virtual IList<UrlRecord> GetUrlRecordsByIds(int[] urlRecordIds)
        {
            var query = Repository.Table;

            return query.Where(p => urlRecordIds.Contains(p.Id)).ToList();
        }

        /// <summary>
        /// Gets an URL record
        /// </summary>
        /// <param name="urlRecordId">URL record identifier</param>
        /// <returns>URL record</returns>
        public virtual UrlRecord GetUrlRecordById(int urlRecordId)
        {
            if (urlRecordId == 0)
                return null;

            return Repository.Get(urlRecordId);
        }

        /// <summary>
        /// Inserts an URL record
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        public virtual void InsertUrlRecord(UrlRecord urlRecord)
        {
            if (urlRecord == null)
                throw new ArgumentNullException("urlRecord");

            Repository.Insert(urlRecord);

            //cache
            _cacheManager.RemoveByPattern(ConstantKey.URLRECORD_PATTERN_KEY);
        }

        /// <summary>
        /// Updates the URL record
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        public virtual void UpdateUrlRecord(UrlRecord urlRecord)
        {
            if (urlRecord == null)
                throw new ArgumentNullException("urlRecord");

            Repository.Update(urlRecord);

            //cache
            _cacheManager.RemoveByPattern(ConstantKey.URLRECORD_PATTERN_KEY);
        }

        /// <summary>
        /// Find URL record
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>Found URL record</returns>
        public virtual UrlRecord GetBySlug(string slug)
        {
            if (String.IsNullOrEmpty(slug))
                return null;

            var query = from ur in Repository.Table
                        where ur.Slug == slug
                        //first, try to find an active record
                        orderby ur.IsActive descending, ur.Id
                        select ur;
            var urlRecord = query.FirstOrDefault();
            return urlRecord;
        }

        /// <summary>
        /// Find URL record (cached version).
        /// This method works absolutely the same way as "GetBySlug" one but caches the results.
        /// Hence, it's used only for performance optimization in public store
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <returns>Found URL record</returns>
        public virtual UrlRecord GetBySlugCached(string slug)
        {
            if (String.IsNullOrEmpty(slug))
                return null;

            if (_localizationSettings.LoadAllUrlRecordsOnStartup)
            {
                //load all records (we know they are cached)
                var source = GetAllUrlRecordsCached();
                var query = from ur in source
                            where ur.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase)
                            //first, try to find an active record
                            orderby ur.IsActive descending, ur.Id
                            select ur;
                var urlRecordForCaching = query.FirstOrDefault();
                return urlRecordForCaching;
            }

            //gradual loading
            string key = string.Format(ConstantKey.URLRECORD_BY_SLUG_KEY, slug);
            return _cacheManager.Get(key, () =>
            {
                var urlRecord = GetBySlug(slug);
                if (urlRecord == null)
                    return null;

                //var urlRecordForCaching = Map(urlRecord);
                return urlRecord;
            });
        }

        /// <summary>
        /// Gets all URL records
        /// </summary>
        /// <param name="slug">Slug</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>URL records</returns>
        public virtual IPagedList<UrlRecord> GetAllUrlRecords(string slug = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = Repository.Table;
            if (!String.IsNullOrWhiteSpace(slug))
                query = query.Where(ur => ur.Slug.Contains(slug));
            query = query.OrderBy(ur => ur.Slug);

            var urlRecords = new PagedList<UrlRecord>(query, pageIndex, pageSize);
            return urlRecords;
        }

        /// <summary>
        /// Find slug
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="entityName">Entity name</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Found slug</returns>
        public virtual string GetActiveSlug(int entityId, string entityName, int languageId)
        {
            // remove where by languageId 20180104
            if (_localizationSettings.LoadAllUrlRecordsOnStartup)
            {
                string key = string.Format(ConstantKey.URLRECORD_ACTIVE_BY_ID_NAME_LANGUAGE_KEY, entityId, entityName, languageId);
                return _cacheManager.Get(key, () =>
                {
                    //load all records (we know they are cached)
                    var source = GetAllUrlRecordsCached();
                    var query = from ur in source
                                where ur.EntityId == entityId &&
                                      ur.EntityName == entityName &&
                                      //ur.LanguageId == languageId &&
                                      ur.IsActive
                                orderby ur.Id descending
                                select ur.Slug;
                    var slug = query.FirstOrDefault();
                    //little hack here. nulls aren't cacheable so set it to ""
                    if (slug == null)
                        slug = "";
                    return slug;
                });
            }
            else
            {
                //gradual loading
                string key = string.Format(ConstantKey.URLRECORD_ACTIVE_BY_ID_NAME_LANGUAGE_KEY, entityId, entityName, languageId);
                return _cacheManager.Get(key, () =>
                {
                    var source = Repository.Table;
                    var query = from ur in source
                                where ur.EntityId == entityId &&
                                      ur.EntityName == entityName &&
                                      //ur.LanguageId == languageId &&
                                      ur.IsActive
                                orderby ur.Id descending
                                select ur.Slug;
                    var slug = query.FirstOrDefault();
                    //little hack here. nulls aren't cacheable so set it to ""
                    if (slug == null)
                        slug = "";
                    return slug;
                });
            }
        }
        /// <summary>
        /// Save slug
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="slug">Slug</param>
        /// <param name="languageId">Language ID</param>
        public virtual void SaveSlug<T>(T entity, string slug, int languageId) where T : BaseEntity, ISlugSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int entityId = entity.Id;
            string entityName = typeof(T).Name;

            var query = from ur in Repository.Table
                        where ur.EntityId == entityId &&
                              ur.EntityName == entityName &&
                              ur.LanguageId == languageId
                        orderby ur.Id descending
                        select ur;
            var allUrlRecords = query.ToList();
            var activeUrlRecord = allUrlRecords.FirstOrDefault(x => x.IsActive);

            if (activeUrlRecord == null && !string.IsNullOrWhiteSpace(slug))
            {
                //find in non-active records with the specified slug
                var nonActiveRecordWithSpecifiedSlug = allUrlRecords
                    .FirstOrDefault(x => x.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase) && !x.IsActive);
                if (nonActiveRecordWithSpecifiedSlug != null)
                {
                    //mark non-active record as active
                    nonActiveRecordWithSpecifiedSlug.IsActive = true;
                    UpdateUrlRecord(nonActiveRecordWithSpecifiedSlug);
                }
                else
                {
                    //new record
                    var urlRecord = new UrlRecord
                    {
                        EntityId = entityId,
                        EntityName = entityName,
                        Slug = slug,
                        LanguageId = languageId,
                        IsActive = true,
                    };
                    InsertUrlRecord(urlRecord);
                }
            }

            if (activeUrlRecord != null && string.IsNullOrWhiteSpace(slug))
            {
                //disable the previous active URL record
                activeUrlRecord.IsActive = false;
                UpdateUrlRecord(activeUrlRecord);
            }

            if (activeUrlRecord != null && !string.IsNullOrWhiteSpace(slug))
            {
                //it should not be the same slug as in active URL record
                if (!activeUrlRecord.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase))
                {
                    //find in non-active records with the specified slug
                    var nonActiveRecordWithSpecifiedSlug = allUrlRecords
                        .FirstOrDefault(x => x.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase) && !x.IsActive);
                    if (nonActiveRecordWithSpecifiedSlug != null)
                    {
                        //mark non-active record as active
                        nonActiveRecordWithSpecifiedSlug.IsActive = true;
                        UpdateUrlRecord(nonActiveRecordWithSpecifiedSlug);

                        //disable the previous active URL record
                        activeUrlRecord.IsActive = false;
                        UpdateUrlRecord(activeUrlRecord);
                    }
                    else
                    {
                        //insert new record
                        //we do not update the existing record because we should track all previously entered slugs
                        //to ensure that URLs will work fine
                        var urlRecord = new UrlRecord
                        {
                            EntityId = entityId,
                            EntityName = entityName,
                            Slug = slug,
                            LanguageId = languageId,
                            IsActive = true,
                        };
                        InsertUrlRecord(urlRecord);

                        //disable the previous active URL record
                        activeUrlRecord.IsActive = false;
                        UpdateUrlRecord(activeUrlRecord);
                    }

                }
            }
        }
    }
}
