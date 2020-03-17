using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Menus;

namespace Vnit.ApplicationCore.Services.Menus
{
    public class MenuService : BaseEntityService<Menu>, IMenuService
    {
        public MenuService(IDataRepository<Menu> dataRepository) : base(dataRepository)
        {
        }
    }
}
