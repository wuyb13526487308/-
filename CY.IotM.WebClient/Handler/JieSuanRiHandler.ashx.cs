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
    /// JieSuanRiHandler 的摘要说明
    /// </summary>
    public class JieSuanRiHandler : BaseHandler
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
            IoT_SetSettlementDay Info;
            WCFServiceProxy<ISettlement> proxy = null;
            try
            {
                switch (AjaxType)
                {
                        //查询
                    case "QUERY":
                        CommonSearch<View_SettlementDayMeterView> InfoSearch = new CommonSearch<View_SettlementDayMeterView>();
                        string Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";

                        if (context.Request.Form["Date1"] != null && context.Request.Form["Date1"].ToString().Trim() != string.Empty)
                        {
                            Where += " AND convert(char(10),RegisterDate,120)='" + context.Request.Form["Date1"].ToString() + "'"; ;
                        }
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        SearchCondition sCondition = new SearchCondition() { TBName = "View_SettlementDayMeterView", TFieldKey = "DayID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "RegisterDate DESC", TWhere = Where };
                        List<View_SettlementDayMeterView> list = InfoSearch.GetList(ref sCondition, context);

                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_SettlementDayMeterView>(list, sCondition.TTotalCount)
                        };
                        break;
                        //查询失败用户
                    case "QUERYUSERFILE":
                        CommonSearch<View_SettlementDayMeter> InfoSearch_UserFILE = new CommonSearch<View_SettlementDayMeter>();
                        Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "'  AND State='3'";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        if (context.Request.Form["DayID"] != null && context.Request.Form["DayID"].ToString().Trim() != string.Empty)
                        {
                            Where += " AND MeterID=" + context.Request.Form["DayID"].ToString().Trim();
                        }
                        sCondition = new SearchCondition() { TBName = "View_SettlementDayMeter", TFieldKey = "MeterID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "MeterID DESC", TWhere = Where };
                        List<View_SettlementDayMeter> list_UserFILE = InfoSearch_UserFILE.GetList(ref sCondition, context);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_SettlementDayMeter>(list_UserFILE, sCondition.TTotalCount)
                        };
                        break;
                        //查询所有设定结算日的用户
                    case "QUERYUSER":
                        CommonSearch<View_SettlementDayMeter> InfoSearch_User = new CommonSearch<View_SettlementDayMeter>();
                        Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        if (context.Request.Form["MeterID"] != null && context.Request.Form["MeterID"].ToString().Trim() != string.Empty)
                        {
                            Where += " AND MeterID=" + context.Request.Form["MeterID"].ToString().Trim();
                        }

                        sCondition = new SearchCondition() { TBName = "View_SettlementDayMeter", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "UserID DESC", TWhere = Where };
                        List<View_SettlementDayMeter> list_User = InfoSearch_User.GetList(ref sCondition, context);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_SettlementDayMeter>(list_User, sCondition.TTotalCount)
                        };
                        break;
                        //新增结算日
                    case "ADD":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "szbjcs"))
                        {
                            Info = new CommonModelFactory<IoT_SetSettlementDay>().GetModelFromContext(context);
                            Info.CompanyID = base.loginOperator.CompanyID;
                            Info.Oper = base.loginOperator.Name;
                            Info.RegisterDate = DateTime.Now;
                            List<IoT_SettlementDayMeter> alarmMeter = new List<IoT_SettlementDayMeter>();
                            string Total = string.IsNullOrEmpty(context.Request.Form["Total"]) == true ? "" : context.Request.Form["Total"].ToString();
                            string Context = string.IsNullOrEmpty(context.Request.Form["Context"]) == true ? "" : context.Request.Form["Context"].ToString();
                            string SettlementDay = (string.IsNullOrEmpty(context.Request.Form["strDay"]) == true ? "0" : context.Request.Form["strDay"].ToString());
                            string SettlementMonth = (string.IsNullOrEmpty(context.Request.Form["strMonth"]) == true ? "0" : context.Request.Form["strMonth"].ToString());
                            Info.CompanyID = base.loginOperator.CompanyID;
                            Info.Context = Context;
                            //Info.ReportType = ReportType;
                            Info.RegisterDate = DateTime.Now;
                            Info.SettlementDay = int.Parse(SettlementDay);
                            Info.State = '0';
                            Info.TaskID = "";//目前还不知道任务ID的取值方法
                            Info.Total = int.Parse(Total);
                            Info.SettlementMonth = int.Parse(SettlementMonth);
                            proxy = new WCFServiceProxy<ISettlement>();
                            if (!string.IsNullOrEmpty(context.Request.Form["hidType"]) && (context.Request.Form["hidType"].ToString() != ""))
                            {
                                //判断所选用户类型
                                alarmMeter = new List<IoT_SettlementDayMeter>();
                                if (context.Request.Form["hidType"].ToString() == "1")
                                {
                                    Info.Scope = "所有用户";
                                    //jsonMessage = proxy.getChannel.AddSetAlarmAll(Info);
                                }
                                else if (context.Request.Form["hidType"].ToString() == "2")
                                {
                                    Info.Scope = "选择用户";
                                }
                                else
                                {
                                    //Info.Scope = context.Request.Form["hidType"].ToString();
                                    string strNo = context.Request.Form["communityStr"] == null ? "" : context.Request.Form["communityStr"];
                                    string[] arrNo = strNo.Split(',');
                                    Info.Scope = context.Request.Form["hidType"].ToString();
                                    //jsonMessage = proxy.getChannel.AddSetAlarmArea(Info, arrNo.ToList());
                                }
                                if (context.Request.Form["strNo"] != null && context.Request.Form["strNo"].ToString().Trim() != string.Empty)
                                {
                                    string strNo = context.Request.Form["strNo"];
                                    string[] arrNo = strNo.Split(',');
                                    for (int i = 0; i < arrNo.Length; i++)
                                    {
                                        IoT_SettlementDayMeter meter = new IoT_SettlementDayMeter();
                                        meter.MeterNo = arrNo[i];
                                        alarmMeter.Add(meter);
                                    }
                                }
                                jsonMessage = proxy.getChannel.Add(Info, alarmMeter);
                            }
                        }
                        break;
                        //撤销
                    case "REVOKE":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "RVJSR"))
                        {
                            proxy = new WCFServiceProxy<ISettlement>();
                            string ID = string.Empty;
                            if (context.Request["ID"] != null && context.Request["ID"].ToString().Trim() != string.Empty)
                            {
                                ID = context.Request["ID"].ToString() == "" ? "0" : context.Request["ID"].ToString();
                            }
                            jsonMessage = proxy.getChannel.revoke(ID, loginOperator.CompanyID);
                        }
                        break;
                    default:
                        jsonMessage = new Message()
                        {
                            Result = false,
                            TxtMessage = "操作未定义。"
                        };
                        break;
                        //修改
                    //case "EDIT":
                    //    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "szbjcs"))
                    //    {
                    //        Info = new CommonModelFactory<IoT_SetSettlementDay>().GetModelFromContext(context);
                    //        proxy = new WCFServiceProxy<ISettlement>();
                    //        jsonMessage = proxy.getChannel.Edit(Info);
                    //    }
                    //    break;
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