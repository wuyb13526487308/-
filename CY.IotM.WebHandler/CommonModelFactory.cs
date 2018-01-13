using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using CY.IotM.Common.Tool;

namespace CY.IotM.WebHandler
{
    public class CommonModelFactory<T> where T : class,new()
    {
        public T GetModelFromContext(HttpContext context)
        {

            T t = new T();
            string tempName = string.Empty;
            PropertyInfo[] propertypes = t.GetType().GetProperties();
            foreach (PropertyInfo pro in propertypes)
            {
                tempName = pro.Name;
                if (context.Request.Form.AllKeys.Contains(tempName))
                {
                    object value = context.Request.Form[tempName];
                    if (value != DBNull.Value)
                        pro.SetValue(t, ConvertHelper.SwitchPropertyValue(pro.PropertyType, value), null);
                }
            }
            return t;
        }
    }
}