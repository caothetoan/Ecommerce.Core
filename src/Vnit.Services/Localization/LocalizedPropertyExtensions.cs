using System.Collections.Generic;
using System.Linq;
using Vnit.ApplicationCore.Entities;
using Vnit.ApplicationCore.Entities.Localization;
using Vnit.ApplicationCore.Interfaces;
using Vnit.ApplicationCore.Management;

namespace Vnit.ApplicationCore.Services.Localization
{
    public static class LocalizedPropertyExtensions
    {
        /// <summary>
        /// Gets the properties of entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="langId"></param>
        /// <returns></returns>
        public static IList<LocalizedProperty> GetLocalizedPropertys<T>(this IHasLocalizedProperty<T> entity, int langId) where T : BaseEntity
        {
            if (entity == null)
                return null;
            var localizedPropertyService = EngineContext.Current.Resolve<ILocalizedPropertyService>();
            return localizedPropertyService.Get(
                x =>
            x.LocaleKeyGroup == typeof(T).Name && 
            x.EntityId == entity.Id &&
            x.LanguageId == langId).ToList();
        }

        /// <summary>
        /// Gets the property with specified name for current entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <param name="langId"></param>
        /// <returns></returns>
        public static LocalizedProperty GetLocalizedProperty<T>(this IHasLocalizedProperty<T> entity, string propertyName, int langId) where T : BaseEntity
        {
            var localizedPropertyService = EngineContext.Current.Resolve<ILocalizedPropertyService>();
            return
                localizedPropertyService.Get(
                    x => 
                    x.LocaleKeyGroup == typeof(T).Name && 
                    x.EntityId == entity.Id
                    && x.LanguageId == langId
                    && x.LocaleKey == propertyName,
                    null).FirstOrDefault();
        }

        /// <summary>
        /// Gets the property valueas stored
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <param name="langId"></param>
        /// <returns></returns>
        public static object GetLocalizedPropertyValue<T>(this IHasLocalizedProperty<T> entity, string propertyName, int langId) where T : BaseEntity
        {
            var localizedProperty = GetLocalizedProperty(entity, propertyName, langId);
            return localizedProperty?.LocaleValue;
        }

        ///// <summary>
        ///// Gets property value as the target type
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="entity"></param>
        ///// <param name="propertyName"></param>
        ///// <param name="defaultValue"></param>
        ///// <returns></returns>
        //public static T GetPropertyValueAs<T>(this IHasLocalizedProperty entity, string propertyName, T defaultValue = default(T))
        //{
        //    var localizedPropertyService = EngineContext.Current.Resolve<ILocalizedPropertyService>();
        //    var typeName = entity.GetType().BaseType?.Name;
        //    var entityProperty = localizedPropertyService.Get(
        //            x =>
        //            x.LocaleKeyGroup == typeName && 
        //            x.EntityId == entity.Id && 
        //            x.LocaleKey == propertyName,
        //            null).FirstOrDefault();

        //    if (entityProperty == null)
        //        return defaultValue;

        //    return JsonConvert.DeserializeAnonymousType(entityProperty.LocaleValue, defaultValue);
        //}
    }
}
