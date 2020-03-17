using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Vnit.ApplicationCore.Helpers
{
    public static class ModelMapping
    {
     
        public static T Map<T>(this object fromSource) where T : class, new()
        {
            
            if (fromSource == null) return default(T);

            var ret = new T();
            var sourceProps = fromSource.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destinationProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var desProp in destinationProps)
            {
                var sourceProp = sourceProps.FirstOrDefault(m => m.Name == desProp.Name);

                if (sourceProp != null)
                {
                    try
                    {
                        desProp.SetValue(ret, sourceProp.GetValue(fromSource, null), null);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(desProp.Name + desProp.PropertyType, ex);
                    }

                }
            }
            return ret;
        }

        public static T MapValue<T>(this object fromSource) where T : class, new()
        {

            if (fromSource == null) return default(T);

            FieldInfo[] fields = fromSource.GetType().GetFields(BindingFlags.Public |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Instance);

            var ret = new T();
            var destinationProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var desProp in destinationProps)
            {
                var sourceProp = fields.FirstOrDefault(m => m.Name == desProp.Name);

                if (sourceProp != null)
                {
                    try
                    {
                        desProp.SetValue(ret, sourceProp.GetValue(fromSource), null);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(desProp.Name + desProp.PropertyType, ex);
                    }

                }
            }
            return ret;
        }

        public static T ToEntity<T>(DataRow dr) where T : new()
        {
            var ret = new T();

            DataColumnCollection lstColumn = dr.Table.Columns;

            var propertyInfos = ret.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in propertyInfos)
            {
                if (lstColumn.Contains(propertyInfo.Name) && dr[propertyInfo.Name] != DBNull.Value)
                {
                    try
                    {
                        var propertyType = propertyInfo.PropertyType;
                        if (propertyType.IsGenericType &&
                            propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            propertyType = propertyType.GetGenericArguments()[0];
                        }
                        //if (lstColumn.Contains("DataJson") && (dr[propertyInfo.Name].ToString().Equals("0") || String.IsNullOrEmpty(dr[propertyInfo.Name].ToString())))
                        //{
                        //    dynamic data = JObject.Parse(dr["DataJson"].ToString());
                        //    propertyInfo.SetValue(ret,
                        //        data[propertyInfo.Name] != null
                        //            ? Convert.ChangeType(data[propertyInfo.Name], propertyType)
                        //            : Convert.ChangeType(dr[propertyInfo.Name], propertyType), null);
                        //}
                        //else
                        {
                            propertyInfo.SetValue(ret, Convert.ChangeType(dr[propertyInfo.Name], propertyType), null);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(propertyInfo.Name + propertyInfo.PropertyType, ex);
                    }
                }
            }

            return ret;
        }

        public static DataTable ToDataTable<TSource>(IList<TSource> data)
        {
            try
            {
                DataTable dataTable = new DataTable(typeof(TSource).Name);
                PropertyInfo[] props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in props)
                {
                    dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
                         prop.PropertyType);
                }

                foreach (TSource item in data)
                {
                    var values = new object[props.Length];
                    for (int i = 0; i < props.Length; i++)
                    {
                        values[i] = props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            return (from DataRow row in dt.Rows select GetItem<T>(row)).ToList();
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            DataColumnCollection lstColumn = dr.Table.Columns;
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (lstColumn.Contains(pro.Name) && dr[pro.Name] != DBNull.Value)
                {
                    try
                    {
                        var propertyType = pro.PropertyType;
                        if (propertyType.IsGenericType &&
                            propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            propertyType = propertyType.GetGenericArguments()[0];
                        }
                        pro.SetValue(obj, Convert.ChangeType(dr[pro.Name], propertyType), null);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(pro.Name + pro.PropertyType, ex);
                    }
                }
            }
            return obj;
        }

    }
}
