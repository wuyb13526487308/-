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
    /// CZTongJiHandler 的摘要说明
    /// </summary>
    public class CZTongJiHandler : BaseHandler
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
            string Action = context.Request.QueryString["Action"] == null ? string.Empty : context.Request.QueryString["Action"].ToString().ToUpper();
            string AjaxType = context.Request.QueryString["AType"] == null ? string.Empty : context.Request.QueryString["AType"].ToString().ToUpper();
           
            try
            {
                switch (AjaxType)
                {
                        //查询
                    case "QUERY":
                        switch (Action)
                        {
                            case "0"://依据用户
                                string Where = string.Empty;
                                SearchCondition sCondition = new SearchCondition() { TBName = "IoT_User", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "UserID ASC", TWhere = Where };
                                CommonSearch<View_UserMeter> InfoSearchView = new CommonSearch<View_UserMeter>();
                                Where = "1=1 ";
                                Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                                if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                                {
                                    Where += context.Request.Form["TWhere"].ToString();
                                }
                                sCondition = new SearchCondition() { TBName = "View_UserMeter", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "UserID ASC", TWhere = Where };
                                List<View_UserMeter> listView = InfoSearchView.GetList(ref sCondition, context);
                                jsonMessage = new Message()
                                {
                                    Result = true,
                                    TxtMessage = JSon.ListToJson<View_UserMeter>(listView, sCondition.TTotalCount)
                                };
                                break;
                            case "1"://依据充值方式
                                Where = "1=1 ";
                                Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                                if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                                {
                                    Where += context.Request.Form["TWhere"].ToString();
                                } InfoSearchView = new CommonSearch<View_UserMeter>();
                                sCondition = new SearchCondition() { TBName = "View_UserMeter", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "UserID ASC", TWhere = Where };
                                List<View_UserMeter> listView1 = InfoSearchView.GetList(ref sCondition, context);
                                jsonMessage = new Message()
                                {
                                    Result = true,
                                    TxtMessage = JSon.ListToJson<View_UserMeter>(listView1, sCondition.TTotalCount)
                                };
                                break;
                            case "2"://依据操作员
                                Where = "1=1 ";
                                Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                                if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                                {
                                    Where += context.Request.Form["TWhere"].ToString();
                                }
                                InfoSearchView = new CommonSearch<View_UserMeter>();
                                sCondition = new SearchCondition() { TBName = "View_UserMeter", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "UserID ASC", TWhere = Where };
                                List<View_UserMeter> listView2 = InfoSearchView.GetList(ref sCondition, context);
                                jsonMessage = new Message()
                                {
                                    Result = true,
                                    TxtMessage = JSon.ListToJson<View_UserMeter>(listView2, sCondition.TTotalCount)
                                };
                                break;
                            case "USERCZHISTORY":
                                //拼接Where条件
                                Where = "1=1 ";
                                Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                                string Date1 = context.Request.QueryString["Date1"] == null ? string.Empty : context.Request.QueryString["Date1"].ToString().ToUpper();
                                string Date2 = context.Request.QueryString["Date2"] == null ? string.Empty : context.Request.QueryString["Date2"].ToString().ToUpper();
                                if (Date1 != "")
                                {
                                    Where += "AND TopUpDate>='" + Date1 + "' ";
                                }
                                if (Date2 != "")
                                {
                                    Where += "AND TopUpDate<='" + Date2 + "' ";
                                }
                                //实现查询
                                CommonSearch<View_ChongZhi> InfoSearchView_ChongZhi = new CommonSearch<View_ChongZhi>();
                                SearchCondition View_ChongZhi = new SearchCondition() { TBName = "View_ChongZhi", TFieldKey = "AID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "TopUpDate DESC", TWhere = Where };
                                List<View_ChongZhi> lstView_ChongZhi = InfoSearchView_ChongZhi.GetList(ref View_ChongZhi, context);
                                //将讯息转换为Json字符串
                                jsonMessage = new Message()
                                {
                                    Result = true,
                                    TxtMessage = JSon.ListToJson<View_ChongZhi>(lstView_ChongZhi, View_ChongZhi.TTotalCount)
                                };
                                break;
                            default:
                                jsonMessage = new Message()
                                {
                                    Result = false,
                                    TxtMessage = "没有此统计类型。"
                                };
                                break;
                        }
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
                //if (proxy != null)
                //    proxy.CloseChannel();
            }
            context.Response.Write(JSon.TToJson<Message>(jsonMessage));
        }
    }
}