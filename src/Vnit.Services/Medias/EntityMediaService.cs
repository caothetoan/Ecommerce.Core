using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.MediaAggregate;

namespace Vnit.ApplicationCore.Services.Medias
{
    public class EntityMediaService : BaseEntityService<EntityMedia>, IEntityMediaService
    {

        public EntityMediaService(IDataRepository<EntityMedia> dataRepository) : base(dataRepository)
        {
        }
        
    }
}
