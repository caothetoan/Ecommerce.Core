﻿using System;
using System.Collections;
using System.Reflection;

namespace Vnit.ApplicationCore.Helpers
{
    public class PropertyTypeConverter
    {
        /// <summary>
        /// Casts a property value to appropriate type
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object CastPropertyValue(PropertyInfo property, string value)
        {
            if (property == null || string.IsNullOrEmpty(value))
                return null;
            //enumeration?
            if (property.PropertyType.IsEnum)
            {
                var enumType = property.PropertyType;
                if (Enum.IsDefined(enumType, value))
                    return Enum.Parse(enumType, value);
            }
            //boolean?
            if (property.PropertyType == typeof(bool))
                return ((IList) new[] {"1", "true", "on", "checked"}).Contains(value.ToLower());

            //uri?
            if (property.PropertyType == typeof(Uri))
                return new Uri(Convert.ToString(value));

            //fallback
            return Convert.ChangeType(value, property.PropertyType);
        }
    }
}
