using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vnit.ApplicationCore.Helpers;
using Vnit.Api.ViewModels;

namespace Vnit.Api.Extensions
{
    public static class SelectListItemExtension
    {
        public static IEnumerable<SelectListItem> GetSelectList<T>()
        {
            return (Enum.GetValues(typeof(T)).Cast<T>().Select(
                enu => new SelectListItem() { Text = enu.ToString(), Value = enu.To<int>().ToString() })).ToList();
        }
      

        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
               where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = e, Name = e.ToString() };
            return new SelectList(values, "Id", "Name", enumObj);
        }
        public static IEnumerable<EnumResponseModel> GetEnums<T>()
        {
            return (Enum.GetValues(typeof(T)).Cast<T>().Select(
                enu => new EnumResponseModel() { Name = enu.ToString(), Id = enu.To<int>(), Description = EnumExtensionMethods.GetEnumDescription<T>(enu.To<int>()) })).ToList();
        }
    }
}