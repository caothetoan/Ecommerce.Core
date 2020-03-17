using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Widgets;
using Vnit.ApplicationCore.Services;

namespace Vnit.Services.Widgets
{
    public class WidgetService : BaseEntityService<Widget>, IWidgetService
    {
        public WidgetService(IDataRepository<Widget> dataRepository) : base(dataRepository)
        {
        }
    }
}
