using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities;
using Vnit.ApplicationCore.Interfaces;
using Vnit.ApplicationCore.Management;
using Vnit.ApplicationCore.Services;
using Vnit.Services.SEO;

namespace Vnit.Services
{
   
    public class EntityService<T> : BaseEntityService<T> where T : BaseEntity
    {
        public EntityService(IDataRepository<T> dataRepository) : base(dataRepository)
        {
        }

        public override void Insert(T entity, bool reloadNavigationProperties = false)
        {
            base.Insert(entity, reloadNavigationProperties);
            //insert permalink if its supported
            var supported = entity as IPermalinkSupported;
            if (supported != null)
            {
                supported.GetUrlRecord(typeof(T).Name);
            }
        }

        public override void Update(T entity)
        {
            /* We should never modify permalinks on updates, even if name has changed.
             * this is because of maintaining seo links, else old links may become 404. 
             * However update functionality can be provided by using webapi*/
            base.Update(entity);

            var supported = entity as IPermalinkSupported;
            if (supported != null)
            {
                supported.GetUrlRecord(typeof(T).Name);
            }
        }

        public override void Delete(T entity)
        {
            var permalinkService = EngineContext.Current.Resolve<IUrlRecordService>();
            //if it's permalink supported entity then we'll either disable or delete the permalink
            var supported = entity as IPermalinkSupported;
            if (supported != null)
            {
                var permalink = supported.GetUrlRecord(typeof(T).Name);

                //if the entity is soft deletable, we just disable the permalink
                var deletable = entity as ISoftDeletable;
                if (deletable != null)
                {
                    permalink.IsActive = false;
                    //update permalink
                    permalinkService.Update(permalink);
                }
                else
                {
                    //we can safely remove the permalink altogether
                    permalinkService.Delete(permalink);
                }
            }

            //call the base entity
            base.Delete(entity);
        }
    }
}
