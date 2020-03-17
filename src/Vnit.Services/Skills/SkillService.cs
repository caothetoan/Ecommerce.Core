using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Skills;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.Skills
{
    public class SkillService : BaseEntityService<Skill>, ISkillService
    {
        public SkillService(IDataRepository<Skill> dataRepository) : base(dataRepository)
        {
        }
    }
}
