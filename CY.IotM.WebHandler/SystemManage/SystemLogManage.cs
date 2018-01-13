using CY.IotM.Common;
using CY.IotM.Common.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CY.IotM.WebHandler.SystemManage
{
    public class SystemLogManage : BaseHandler
    {
        public override void DoLoginedHandlerWork(HttpContext context)
        {
            Message jsonMessage;
            //获取操作类型AType:ADD,EDIT,DELETE,QUERY
            string AjaxType = context.Request.QueryString["AType"] == null ? string.Empty : context.Request.QueryString["AType"].ToString().ToUpper();
            SystemLog Info;
            jsonMessage = new Message()
            {
                Result = false,
                TxtMessage = "权限验证失败，可能原因:\n1、数据中心通讯失败。\n2、系统管理员未与您分配对应操作权限。"
            };
            WCFServiceProxy<ISystemLogManage> proxy = null;
            try
            {
                switch (AjaxType)
                {
                    case "QUERY":
                        string Where = "1=1 ";
                        if (base.loginOperator.CompanyID.ToUpper() != "ZZCY")
                            Where += "  AND CompanyID='" + base.loginOperator.CompanyID + "'";
                        if (context.Request.Form["LogType"] != null && context.Request.Form["LogType"].ToString().Trim() != string.Empty)
                        {
                            Where += " AND LogType=" + context.Request.Form["LogType"].ToString();
                        }
                        if (context.Request.Form["Date1"] != null && context.Request.Form["Date1"].ToString().Trim() != string.Empty)
                        {
                            Where += " AND LogTime>='" + context.Request.Form["Date1"].ToString() + "'"; ;
                        }
                        if (context.Request.Form["Date2"] != null && context.Request.Form["Date2"].ToString().Trim() != string.Empty)
                        {
                            Where += " AND LogTime<='" + context.Request.Form["Date2"].ToString() + "'"; ;
                        }
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += "  AND " + context.Request.Form["TWhere"].ToString();
                        }
                        CommonSearch<SystemLog> userInfoSearch = new CommonSearch<SystemLog>();
                        SearchCondition sCondition = new SearchCondition() { TBName = "S_SystemLog", TFieldKey = "LogID", TTotalCount = -1, TPageCurrent = 1, TPageCount = 1, TFieldOrder = "LogID DESC", TWhere = Where };
                        List<SystemLog> list = userInfoSearch.GetList(ref sCondition, context);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<SystemLog>(list, sCondition.TTotalCount)
                        };
                        break;
                    case "ADD":
                        proxy = new WCFServiceProxy<ISystemLogManage>();
                        Info = new CommonModelFactory<SystemLog>().GetModelFromContext(context);
                        Info.CompanyID = base.loginOperator.CompanyID;
                        Info.OperID = base.loginOperator.OperID;
                        Info.OperName = base.loginOperator.Name;
                        Info.LogTime = System.DateTime.Now;
                        ClientHelper clientHelper = new ClientHelper();
                        Info.LoginIP = clientHelper.ClientIP;
                        Info.LoginBrowser = clientHelper.clientBrowser;
                        Info.LoginSystem = clientHelper.ClientSystem;
                        jsonMessage = proxy.getChannel.AddSystemLog(Info);
                        break;
                    default:
                        jsonMessage = new Message()
                        {
                            Result = false,
                            TxtMessage = "操作未定义。"
                        };
                        break;
                }
            }
            catch
            { }
            finally
            {
                if (proxy != null)
                    proxy.CloseChannel();
            }
            context.Response.Write(JSon.TToJson<Message>(jsonMessage));
        }
    }
}

