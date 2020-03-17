using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vnit.Api.ViewModels.Widgets;
using Vnit.ApplicationCore.Entities.Widgets;
using Vnit.ApplicationCore.Helpers;

namespace Vnit.Api.Extensions
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
