using System;
using System.Threading.Tasks;
using Vnit.ApplicationCore.Entities;
using Vnit.ApplicationCore.Entities.Seo;
using Vnit.ApplicationCore.Interfaces;
using Vnit.ApplicationCore.Management;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.SEO
{
    public static class PermalinkExtensions
    {
        /// <summary>
        /// Gets the permalink for this entity. Automatically creates new if it doesn't exist
        /// </summary>
        /// <param name="permalinkSupportedInstance"></param>
        /// <param name="typeName"></param>
        /// <param name="isUpdate"></param>
        /// <returns></returns>
        public static UrlRecord GetUrlRecord(this IPermalinkSupported permalinkSupportedInstance, string typeName = "", bool isUpdate = true)
        {
            //resolve permalink service
            var permalinkService = EngineContext.Current.Resolve<IUrlRecordService>();
            return permalinkService.GetUrlRecord(permalinkSupportedInstance, typeName, isUpdate);
        }

        public static T GetBySeName<T>(this IBaseEntityService<T> entityService, string seName) where T : BaseEntity
        {
            //resolve permalink service
            var permalinkService = EngineContext.Current.Resolve<IUrlRecordService>();
            var entityname = typeof(T).Name;
            var permalink = permalinkService.FirstOrDefault(x => x.EntityName == entityname && x.Slug == seName && x.IsActive);
            if (permalink == null)
                return default(T);

            var entityId = permalink.EntityId;
            return entityService.Get(entityId);
        }
        public static async Task<T> GetBySeNameAsync<T>(this IBaseEntityService<T> entityService, string seName) where T : BaseEntity
        {
            //resolve permalink service
            var permalinkService = EngineContext.Current.Resolve<IUrlRecordService>();
            var entityname = typeof(T).Name;
            var permalink = await permalinkService.FirstOrDefaultAsync(x => x.EntityName == entityname && x.Slug == seName && x.IsActive);
            if (permalink == null)
                return default(T);

            var entityId = permalink.EntityId;
            return await entityService.GetAsync(entityId);
        }
        public static string GetSlug<T>(this T entity)
            where T : BaseEntity, ISlugSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            string entityName = typeof(T).Name;
            return GetSlug(entity.Id, entityName);
        }
        public static string GetSlug(int entityId, string entityName)
        {
            //resolve permalink service
            var permalinkService = EngineContext.Current.Resolve<IUrlRecordService>();
            var permalink = permalinkService.FirstOrDefault(
                x => x.EntityId == entityId 
                && x.EntityName == entityName && x.IsActive);
            if (permalink == null)
                return null;

            return permalink.Slug;            
        }
    }
}
