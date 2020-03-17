using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Widgets;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.Widgets
{
    public class GroupPageService : BaseEntityService<GroupPage>, IGroupPageService
    {
        public GroupPageService(IDataRepository<GroupPage> dataRepository) : base(dataRepository)
        {
        }
    }
}
