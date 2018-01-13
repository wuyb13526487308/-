using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace CY.IoTM.OlbCommon.Tool
{
    public static class JSon
    {
        /// <summary>
        /// List转成json 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonName"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ListToJson<T>(IList<T> list, int total) where T : new()
        {
            string jsonName = typeof(T).Name;
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"total\":" + total + ",");
            Json.Append("\"rows\":[");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Json.Append(TToJson<T>(list[i]));
                    if (i < list.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }

        /// <summary>
        /// T转成json 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonName"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string TToJson<T>(T Model) where T : new()
        {
            //return JsToJson.SerializeToJsonString(Model);
            if (Model == null)
            {
                Model = new T();
            }
            string jsonName = typeof(T).Name;
            StringBuilder Json = new StringBuilder();
            T obj = Activator.CreateInstance<T>();
            PropertyInfo[] pi = obj.GetType().GetProperties();
            Json.Append("{");
            for (int j = 0; j < pi.Length; j++)
            {
                object aObj = pi[j].GetValue(Model, null);
                if (aObj == null || aObj.ToString() == "NaN" || aObj.ToString() == "非数字")
                    aObj = string.Empty;
                Type type = aObj.GetType();
                try
                {
                    Json.Append("\"" + pi[j].Name + "\":" + StringFormat(aObj.ToString(), type));
                    if (j < pi.Length - 1)
                    {
                        Json.Append(",");
                    }
                }
                catch (OutOfMemoryException)
                {
                    Json.Remove(0, Json.Length);
                    Json = null;
                    GC.Collect();
                    return string.Empty;
                }

            }
            Json.Append("}");
            return Json.ToString();
        }

        /// <summary>
        /// 格式化字符型、日期型、布尔型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(Char))
            {
                str = (str == "\0" || str.Trim() == "") ? "\"\"" : "\"" + String2Json(str) + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            return str;
        }
        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string String2Json(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }
    }
}
