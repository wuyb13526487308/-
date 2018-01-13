using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IotM.WebHandler;
using CY.IoTM.Common.Business;


namespace CY.IotM.WebClient.Handler
{
    /// <summary>
    /// AlarmInfoHandler 的摘要说明
    /// </summary>
    public class AlarmInfoHandler : BaseHandler
    {

        public override void DoLoginedHandlerWork(HttpContext context)
        {
            Message jsonMessage;
            jsonMessage = new Message()
            {
                Result = false,
                TxtMessage = "权限验证失败，可能原因:\n1、数据中心通讯失败。\n2、系统管理员未与您分配对应操作权限。"
            };
            //获取操作类型AType:ADD,EDIT,DELETE
            string AjaxType = context.Request.QueryString["AType"] == null ? string.Empty : context.Request.QueryString["AType"].ToString().ToUpper();
            //View_AlarmInfo Info;
            //WCFServiceProxy<IAlarmInfo> proxy=null;
            try
            {
                switch (AjaxType)
                {

                    case "QUERY":

                    CommonSearch<View_AlarmInfo> InfoSearch = new CommonSearch<View_AlarmInfo>();
                    string Where = "1=1 ";
                    Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";

                    if(context.Request.Form["AlermItem"] != null && context.Request.Form["AlermItem"] != "")
                    {
                        Where += $" and AlarmValue ='{ context.Request.Form["AlermItem"]}'";
                    }
                    if (context.Request.Form["Date1"] != null && context.Request.Form["Date1"].ToString().Trim() != string.Empty)
                    {
                        Where += " AND ReportDate>='" + context.Request.Form["Date1"].ToString() + "'"; ;
                    }
                    if (context.Request.Form["Date2"] != null && context.Request.Form["Date2"].ToString().Trim() != string.Empty)
                    {
                        Where += " AND ReportDate<='" + context.Request.Form["Date2"].ToString() + "'"; ;
                    }

                    if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                    {
                        Where += context.Request.Form["TWhere"].ToString();
                    }
                    SearchCondition sCondition = new SearchCondition() { TBName = "View_AlarmInfo", TFieldKey = "ReportDate", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "ReportDate DESC", TWhere = Where };
                    List<View_AlarmInfo> list = InfoSearch.GetList(ref sCondition, context);
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<View_AlarmInfo>(list, sCondition.TTotalCount)
                    };
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
            catch (Exception  ex){
                jsonMessage = new Message()
                {
                    Result = false,
                    TxtMessage = ex.Message
                };
            }
            finally
            {
                //if (proxy != null)
                //    proxy.CloseChannel();
            }       
            context.Response.Write(JSon.TToJson<Message>(jsonMessage));
        }
    }
}