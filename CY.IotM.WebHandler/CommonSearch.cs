using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CY.IotM.Common;
using CY.IotM.Common.Tool;


namespace CY.IotM.WebHandler
{
    public class CommonSearch<T> where T : class,new()
    {

        public List<T> GetList(ref SearchCondition sCondition, HttpContext context)
        {
            List<T> list = new List<T>();
            WCFServiceProxy<ICommonSearch<T>> proxy = null;
            try
            {
                proxy = new WCFServiceProxy<ICommonSearch<T>>();
                // 设置分页参数:解析context   
                int Page = 1;
                int Rows = 100;
                string Sort = context.Request.Form["sort"] == null ? string.Empty : context.Request.Form["sort"].ToString();
                string Order = context.Request.Form["order"] == null ? string.Empty : context.Request.Form["order"].ToString();
                int.TryParse(context.Request.Form["page"] == null ? Page.ToString() : context.Request.Form["page"].ToString(), out Page);
                int.TryParse(context.Request.Form["rows"] == null ? Rows.ToString() : context.Request.Form["rows"].ToString(), out Rows);
                if (sCondition.TPageSize != 9999)
                {
                    sCondition.TPageSize = Rows;
                }
                sCondition.TPageCurrent = Page;
                if (Sort != string.Empty)
                {
                    sCondition.TFieldOrder = Sort == string.Empty ? string.Empty : Sort + " " + Order;
                }
                list = proxy.getChannel.getListBySearchCondition(ref sCondition);
            }
            catch
            { }
            finally
            {
                if (proxy != null)
                    proxy.CloseChannel();
            }
            return list;
        }
        public T GetFirstTModel(ref SearchCondition sCondition)
        {
            List<T> list = new List<T>();
            WCFServiceProxy<ICommonSearch<T>> proxy = null;
            try
            {
                proxy = new WCFServiceProxy<ICommonSearch<T>>();
                list = proxy.getChannel.getListBySearchCondition(ref sCondition);
            }
            catch
            { }
            finally
            {
                if (proxy != null)
                    proxy.CloseChannel();
            }
            return list.Count > 0 ? list[0] : null;
        }

    }
}