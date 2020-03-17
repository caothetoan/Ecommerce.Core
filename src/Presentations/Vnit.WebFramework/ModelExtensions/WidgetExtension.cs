using Vnit.ApplicationCore.Entities.Widgets;
using Vnit.ApplicationCore.Helpers;
using Vnit.WebFramework.Models.Widgets;

namespace Vnit.WebFramework.ModelExtensions
{
    public static class WidgetExtension
    {
        public static WidgetModel ToModel(this Widget widget)
        {
            return widget.Map<WidgetModel>();
        }

        public static Widget ToEntity(this WidgetModel widget)
        {
            return widget.Map<Widget>();
        }
    }
}
