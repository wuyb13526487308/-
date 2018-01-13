using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

namespace CY.IoTM.OlbCommon.Tool
{
    public static class ConvertHelper
    {

        /// <summary>
        /// 从DataTable转换为List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> GetList<T>(DataTable table)
        {
            List<T> list = new List<T>();
            T t = default(T);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in table.Rows)
            {
                t = Activator.CreateInstance<T>();
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (table.Columns.Contains(tempName))
                    {
                        object value = row[tempName];
                        if (value != DBNull.Value)
                        {
                            try
                            {
                                pro.SetValue(t, SwitchPropertyValue(pro.PropertyType, value), null);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                continue;
                            }
                        }
                    }
                }
                list.Add(t);
            }
            return list;
        }
        /// <summary>
        /// 从object转换为linqToSql对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object SwitchPropertyValue(Type type, object value)
        {
            bool isNullable = type.Name == "Nullable`1";
            if (isNullable)
            {
                type = type.GetGenericArguments()[0];
            }
            if (value == null)
            {
                return null;
            }
            if (type.Name == "Int16")
            {
                if (value.ToString() == string.Empty)
                {
                    return null;
                }
                return Convert.ToInt16(value);
            }
            if (type.Name == "Decimal")
            {
                if (value.ToString() == string.Empty)
                {
                    return null;
                }
                return Convert.ToDecimal(value);
            }
            if (type.Name == "Int32")
            {
                if (value.ToString() == string.Empty)
                {
                    return null;
                }
                return Convert.ToInt32(value);
            }
            if (type.Name == "Int64")
            {
                if (value.ToString() == string.Empty)
                {
                    return null;
                }
                return Convert.ToInt64(value);
            }
            if (type.Name == "Double")
            {
                if (value.ToString() == string.Empty)
                {
                    return null;
                }
                return Convert.ToDouble(value);
            }
            if (type.Name == "String")
            {
                return value.ToString().Trim();
            }
            if (type.Name == "Boolean")
            {
                if (value.ToString() == string.Empty)
                {
                    return null;
                }
                return Convert.ToBoolean(value);
            }
            if (type.Name == "DateTime")
            {
                if (value.ToString() == string.Empty)
                {
                    return null;
                }
                return Convert.ToDateTime(value);
            }
            if (type.Name == "Guid")
            {
                if (value.ToString() == string.Empty)
                {
                    return null;
                }
                return new Guid(value.ToString());
            }
            if (type.Name == "Char")
            {
                if (value.ToString() == string.Empty)
                {
                    return null;
                }
                return Convert.ToChar(value.ToString());
            }
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        /// <summary>
        /// 将newT的值赋给oldT:新值不为空才覆盖旧值
        /// </summary>
        /// <param name="oldT"></param>
        /// <param name="newT"></param>
        /// <returns></returns>
        public static void Copy<T>(T oldT, T newT)
        {
            PropertyInfo[] propertypes = oldT.GetType().GetProperties();
            string tempName = string.Empty;
            foreach (PropertyInfo pro in propertypes)
            {
                tempName = pro.Name;
                object value = SwitchPropertyValue(pro.PropertyType, pro.GetValue(newT, null));
                object oldValue = SwitchPropertyValue(pro.PropertyType, pro.GetValue(oldT, null));
                if (value != null && value != DBNull.Value&&(oldValue==null || value.ToString () != oldValue.ToString()))
                    pro.SetValue(oldT, value, null);
            }
        }

    }
}
