using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.EntityProperties;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.EntityProperties
{
    public class EntityPropertyService : BaseEntityService<EntityProperty>, IEntityPropertyService
    {
        public EntityPropertyService(IDataRepository<EntityProperty> dataRepository) : base(dataRepository)
        {
        }
    }
}