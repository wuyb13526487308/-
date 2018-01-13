using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IotM.WebHandler;
using CY.IoTM.Common.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CY.IotM.WebClient.Handler
{
    /// <summary>
    /// CZDuiLieHandler 的摘要说明
    /// </summary>
    public class CZDuiLieHandler : BaseHandler
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
            WCFServiceProxy<ISetUploadCycle> proxy = null;
            try
            {
                switch (AjaxType)
                {
                    case "QUERY":
                        CommonSearch<View_ChongZhi> InfoSearch = new CommonSearch<View_ChongZhi>();
                        string Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' and State=0";
                        if (context.Request.Form["DateS"] != null && context.Request.Form["DateS"].ToString().Trim() != string.Empty)
                        {
                            Where += " AND convert(char(10),TopUpDate,120)>='" + context.Request.Form["DateS"].ToString() + "'"; ;
                        }
                        if (context.Request.Form["DateE"] != null && context.Request.Form["DateE"].ToString().Trim() != string.Empty)
                        {
                            Where += " AND convert(char(10),TopUpDate,120)<='" + context.Request.Form["DateE"].ToString() + "'"; ;
                        }
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        SearchCondition sCondition = new SearchCondition() { TBName = "View_ChongZhi", TFieldKey = "AID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "TopUpDate DESC", TWhere = Where };
                        List<View_ChongZhi> list = InfoSearch.GetList(ref sCondition, context);

                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_ChongZhi>(list, sCondition.TTotalCount)
                        };
                        break;
                    case "REVIEW":

                        Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "'";
                        if (context.Request["ID"] != null && context.Request["ID"].ToString().Trim() != string.Empty)
                        {
                            Where += " AND AID=" + context.Request["ID"].ToString(); ;
                        }
                        CommonSearch<View_ChongZhi> InfoSearch1 = new CommonSearch<View_ChongZhi>();
                        SearchCondition sCondition1 = new SearchCondition() { TBName = "View_ChongZhi", TFieldKey = "AID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "TopUpDate DESC", TWhere = Where };
                        List<View_ChongZhi> list1 = InfoSearch1.GetList(ref sCondition1, context);

                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_ChongZhi>(list1, sCondition1.TTotalCount)
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
            catch (Exception ex)
            {
                jsonMessage = new Message()
                {
                    Result = false,
                    TxtMessage = ex.Message
                };
            }
            finally
            {
                if (proxy != null)
                    proxy.CloseChannel();
            }
            context.Response.Write(JSon.TToJson<Message>(jsonMessage));
        }
    }
}