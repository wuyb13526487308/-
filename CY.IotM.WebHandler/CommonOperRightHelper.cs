using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using System.Text;


namespace CY.IotM.WebHandler
{
    public static class CommonOperRightHelper
    {

        public static bool CheckMenuCode(CompanyOperator info, string menuCode)
        {
            bool result = false;
            WCFServiceProxy<IOperRightManage> proxy = null;
            try
            {
                proxy = new WCFServiceProxy<IOperRightManage>();
                result = proxy.getChannel.CheckMenuCode(info, menuCode);
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (proxy != null)
                    proxy.CloseChannel();
            }
            return result;
        }
        public static string GetHidMenuCode(CompanyOperator info)
        {
            string result = string.Empty;
            WCFServiceProxy<IOperRightManage> proxy = null;
            try
            {
                proxy = new WCFServiceProxy<IOperRightManage>();
                result = proxy.getChannel.LoadHiddenMenuCode(info);
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (proxy != null)
                    proxy.CloseChannel();
            }
            return result;
        }
        public static string MenuListToJson(CompanyOperator info)
        {
            List<DefineMenu> list = new List<DefineMenu>();
            WCFServiceProxy<IOperRightManage> proxy = null;
            try
            {
                proxy = new WCFServiceProxy<IOperRightManage>();
                list = proxy.getChannel.LoadDefineMenuByLoginOper(info, false);
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (proxy != null)
                    proxy.CloseChannel();
            }
            List<DefineMenu> tmp = new List<DefineMenu>();
            List<DefineMenu> tmp1 = new List<DefineMenu>();
            StringBuilder Json = new StringBuilder();
            int id = 0;
            if (list != null && list.Count > 0)
            {
                var vList = list.Where(p => p.Type == "00");
                if (vList != null)
                    tmp = vList.OrderBy(p => p.OrderNum).ToList();
                else
                    tmp = null;
                if (tmp != null)
                {
                    //动态生成菜单：只解析两级菜单
                    Json.Append("[");
                    for (int i = 0; i < tmp.Count; i++)
                    {

                        Json.Append("{");
                        Json.Append("\"id\":" + (id++));
                        Json.Append(",");
                        Json.Append("\"text\":\"" + tmp[i].Name + "\"");
                        //if (tmp[i].UrlClass != string.Empty)
                        //{
                        Json.Append(",");
                        Json.Append("\"attributes\":{\"url\":\"" + tmp[i].UrlClass + "\"");
                        Json.Append(",");
                        Json.Append("\"type\":\"" + tmp[i].Type + "\"");
                        Json.Append(",");
                        Json.Append("\"menucode\":\"" + tmp[i].MenuCode + "\"");
                        Json.Append("}");
                        //}
                        var vvList = list.Where(p => (p.Type == "01" || p.Type == "03") && p.FatherCode == tmp[i].MenuCode);
                        if (vvList != null)
                            tmp1 = vvList.OrderBy(p => p.OrderNum).ToList();
                        else
                        {
                            tmp1 = null;
                        }
                        if (tmp1 != null && tmp1.Count > 0)
                        {
                            Json.Append(",");
                            Json.Append("\"children\":[");
                            for (int j = 0; j < tmp1.Count; j++)
                            {

                                Json.Append("{");
                                Json.Append("\"id\":" + (id++));
                                Json.Append(",");
                                Json.Append("\"text\":\"" + tmp1[j].Name + "\"");
                                //if (tmp1[j].UrlClass != string.Empty)
                                //{
                                Json.Append(",");
                                Json.Append("\"attributes\":{\"url\":\"" + tmp1[j].UrlClass + "\"");
                                Json.Append(",");
                                Json.Append("\"type\":\"" + tmp1[j].Type + "\"");
                                Json.Append(",");
                                Json.Append("\"menucode\":\"" + tmp1[j].MenuCode + "\"");
                                Json.Append("}");

                                //}
                                Json.Append("}");
                                if (j != tmp1.Count - 1)
                                {
                                    Json.Append(",");
                                }
                            }
                            Json.Append("]");
                        }
                        Json.Append("}");
                        if (i != tmp.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("]");
                }

            }
            return Json.ToString();
        }
        public static string CompanyMenuListToJson(string companyID)
        {
            List<DefineMenu> list = new List<DefineMenu>();
            WCFServiceProxy<IOperRightManage> proxy = null;
            try
            {
                proxy = new WCFServiceProxy<IOperRightManage>();
                list = proxy.getChannel.LoadCompanyDefineMenu(companyID);
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (proxy != null)
                    proxy.CloseChannel();
            }
            List<DefineMenu> tmp = new List<DefineMenu>();
            List<DefineMenu> tmp1 = new List<DefineMenu>();
            List<DefineMenu> tmp2 = new List<DefineMenu>();
            StringBuilder Json = new StringBuilder();
            int id = 0;
            if (list != null && list.Count > 0)
            {
                var vList = list.Where(p => p.Type == "00");
                if (vList != null)
                    tmp = vList.OrderBy(p => p.OrderNum).ToList();
                else
                    tmp = null;
                if (tmp != null)
                {
                    //动态生成菜单：只解析两级菜单,按钮
                    Json.Append("[");
                    for (int i = 0; i < tmp.Count; i++)
                    {

                        Json.Append("{");
                        Json.Append("\"id\":" + (id++));
                        Json.Append(",");
                        Json.Append("\"text\":\"" + tmp[i].Name + "\"");

                        Json.Append(",");
                        Json.Append("\"attributes\":{\"url\":\"" + tmp[i].UrlClass + "\"");
                        Json.Append(",");
                        Json.Append("\"type\":\"" + tmp[i].Type + "\"");
                        Json.Append(",");
                        Json.Append("\"menucode\":\"" + tmp[i].MenuCode + "\"");
                        Json.Append("}");

                        var vvList = list.Where(p => (p.Type == "01" || p.Type == "03") && p.FatherCode == tmp[i].MenuCode);
                        if (vvList != null)
                            tmp1 = vvList.OrderBy(p => p.OrderNum).ToList();
                        else
                        {
                            tmp1 = null;
                        }
                        if (tmp1 != null && tmp1.Count > 0)
                        {
                            Json.Append(",");
                            Json.Append("\"children\":[");
                            for (int j = 0; j < tmp1.Count; j++)
                            {

                                Json.Append("{");
                                Json.Append("\"id\":" + (id++));
                                Json.Append(",");
                                Json.Append("\"text\":\"" + tmp1[j].Name + "\"");

                                Json.Append(",");
                                Json.Append("\"attributes\":{\"url\":\"" + tmp1[j].UrlClass + "\"");
                                Json.Append(",");
                                Json.Append("\"type\":\"" + tmp1[j].Type + "\"");
                                Json.Append(",");
                                Json.Append("\"menucode\":\"" + tmp1[j].MenuCode + "\"");
                                Json.Append("}");

                                var vvvList = list.Where(p => p.Type == "02" && p.FatherCode == tmp1[j].MenuCode);
                                if (vvvList != null)
                                    tmp2 = vvvList.OrderBy(p => p.OrderNum).ToList();
                                else
                                {
                                    tmp2 = null;
                                }
                                if (tmp2 != null && tmp2.Count > 0)
                                {
                                    Json.Append(",");
                                    Json.Append("\"children\":[");
                                    for (int k = 0; k < tmp2.Count; k++)
                                    {

                                        Json.Append("{");
                                        Json.Append("\"id\":" + (id++));
                                        Json.Append(",");
                                        Json.Append("\"text\":\"" + tmp2[k].Name + "\"");

                                        Json.Append(",");
                                        Json.Append("\"attributes\":{\"url\":\"" + tmp2[k].UrlClass + "\"");
                                        Json.Append(",");
                                        Json.Append("\"type\":\"" + tmp2[k].Type + "\"");
                                        Json.Append(",");
                                        Json.Append("\"menucode\":\"" + tmp2[k].MenuCode + "\"");
                                        Json.Append("}");
                                        Json.Append("}");
                                        if (k != tmp2.Count - 1)
                                        {
                                            Json.Append(",");
                                        }
                                    }
                                    Json.Append("]");
                                }
                                Json.Append("}");
                                if (j != tmp1.Count - 1)
                                {
                                    Json.Append(",");
                                }
                            }
                            Json.Append("]");
                        }
                        Json.Append("}");
                        if (i != tmp.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("]");
                }

            }
            return Json.ToString();
        }
        public static string GetCompanyRightMenuCode(string companyID, string rightCode)
        {
            string result = string.Empty;
            WCFServiceProxy<IOperRightManage> proxy = null;
            try
            {
                proxy = new WCFServiceProxy<IOperRightManage>();
                result = proxy.getChannel.LoadCompanyDefineRightMenu(companyID, rightCode);
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (proxy != null)
                    proxy.CloseChannel();
            }
            return result;
        }



        public static string GetMenuListToJson(string companyID)
        {

            List<DefineMenu> list = new List<DefineMenu>();
            WCFServiceProxy<IOperRightManage> proxy = null;
            try
            {
                proxy = new WCFServiceProxy<IOperRightManage>();
                list = proxy.getChannel.LoadCompanyDefineMenu(companyID);
                list = list.Where(p => p.Type == "00" || p.Type == "01" || p.Type == "03").ToList();
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (proxy != null)
                    proxy.CloseChannel();
            }
            StringBuilder Json = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                foreach (DefineMenu p in list)
                {

                    Json.Append(",");
                    Json.Append(p.MenuCode);
                }
            }
            Json.Append(",");
            return Json.ToString();

        }



        public static string GetLeftMenu(CompanyOperator info)
        {

            List<DefineMenu> list = new List<DefineMenu>();
            WCFServiceProxy<IOperRightManage> proxy = null;
            try
            {
                proxy = new WCFServiceProxy<IOperRightManage>();
                list = proxy.getChannel.LoadDefineMenuByLoginOper(info, false);
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (proxy != null)
                    proxy.CloseChannel();
            }

            List<DefineMenu> tmp = new List<DefineMenu>();
            List<DefineMenu> tmp1 = new List<DefineMenu>();


            var vList = list.Where(p => p.Type == "00");
            if (vList != null)
                tmp = vList.OrderBy(p => p.OrderNum).ToList();
            else
                tmp = null;


            StringBuilder menuApp = new StringBuilder();

            menuApp.Append("[");
            foreach (DefineMenu node in tmp)
            {

                if (menuApp.Length > 1) menuApp.Append(",");
                menuApp.Append("{");
                menuApp.Append("No:'" + node.MenuCode + "'");
                menuApp.Append(",Name:'" + node.Name + "'");
                menuApp.Append(",Img:'" + node.ImageUrl + "'");
                menuApp.Append(",Url:'" + node.UrlClass + "'");
                menuApp.Append(",Children:[");


                var vvList = list.Where(p => (p.Type == "01" || p.Type == "03") && p.FatherCode == node.MenuCode);
                if (vvList != null)
                    tmp1 = vvList.OrderBy(p => p.OrderNum).ToList();
                else
                {
                    tmp1 = null;
                }


                if (tmp1.Count > 0)
                {
                    string childrenMenu = "";
                    foreach (DefineMenu cNode in tmp1)
                    {
                        if (childrenMenu.Length > 0) childrenMenu += ",";
                        childrenMenu += "{";
                        childrenMenu += "No:'" + cNode.MenuCode + "'";
                        childrenMenu += ",Name:'" + cNode.Name + "'";
                        childrenMenu += ",Img:'" + cNode.ImageUrl + "'";
                        childrenMenu += ",Url:'" + cNode.UrlClass + "'";
                        childrenMenu += ",Children:[]";
                        childrenMenu += "}";

                    }
                    menuApp.Append(childrenMenu);
                }
                menuApp.Append("]}");

            }
            menuApp.Append("]");
            return menuApp.ToString();
        }


    }
}