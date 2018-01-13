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
    /// SetUpLoadDateHandler 的摘要说明
    /// </summary>
    public class SetUpLoadDateHandler : BaseHandler
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
            IoT_SetUploadCycle Info;
            WCFServiceProxy<ISetUploadCycle> proxy = null;
            try
            {
                switch (AjaxType)
                {
                    //查询上传周期资料
                    case "QUERY":
                        CommonSearch<View_UpLoadDateView> InfoSearch = new CommonSearch<View_UpLoadDateView>();
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
                        SearchCondition sCondition = new SearchCondition() { TBName = "View_UpLoadDateView", TFieldKey = "CycleID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "RegisterDate DESC", TWhere = Where };
                        List<View_UpLoadDateView> list = InfoSearch.GetList(ref sCondition, context);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_UpLoadDateView>(list, sCondition.TTotalCount)
                        };
                        break;
                        //查询所有设定上传周期失败的用户
                    case "QUERYUSERFILE":
                        CommonSearch<View_UpLoadDate> InfoSearch_UserFILE = new CommonSearch<View_UpLoadDate>();
                        Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "'  AND State='3'";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        if (context.Request.Form["MeterID"] != null && context.Request.Form["MeterID"].ToString().Trim() != string.Empty)
                        {
                            Where += " AND MeterID=" + context.Request.Form["MeterID"].ToString().Trim();
                        }

                        sCondition = new SearchCondition() { TBName = "View_UpLoadDate", TFieldKey = "MeterID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "MeterID DESC", TWhere = Where };
                        List<View_UpLoadDate> list_UserFILE = InfoSearch_UserFILE.GetList(ref sCondition, context);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_UpLoadDate>(list_UserFILE, sCondition.TTotalCount)
                        };
                        break;
                        //根据设定上传周期的用户
                    case "QUERYUSER":
                        CommonSearch<View_UpLoadDate> InfoSearch_User = new CommonSearch<View_UpLoadDate>();
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

                        sCondition = new SearchCondition() { TBName = "View_UpLoadDate", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "UserID DESC", TWhere = Where };
                        List<View_UpLoadDate> list_User = InfoSearch_User.GetList(ref sCondition, context);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_UpLoadDate>(list_User, sCondition.TTotalCount)
                        };
                        break;
                        //新增上传周期
                    case "ADD":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "szbjcs"))
                        {
                            Info = new CommonModelFactory<IoT_SetUploadCycle>().GetModelFromContext(context);
                            Info.CompanyID = base.loginOperator.CompanyID;
                            Info.Oper = base.loginOperator.Name;
                            Info.RegisterDate = DateTime.Now;
                            List<IoT_UploadCycleMeter> alarmMeter = new List<IoT_UploadCycleMeter>();
                            string Total = string.IsNullOrEmpty(context.Request.Form["Total"]) == true ? "" : context.Request.Form["Total"].ToString();
                            string ReportType = string.IsNullOrEmpty(context.Request.Form["ReportType"]) == true ? "" : context.Request.Form["ReportType"].ToString();
                            string par = (string.IsNullOrEmpty(context.Request.Form["strDay"]) == true ? "00" : context.Request.Form["strDay"].ToString()) + (string.IsNullOrEmpty(context.Request.Form["strMinu"]) == true ? "00" : context.Request.Form["strHour"].ToString()) + (string.IsNullOrEmpty(context.Request.Form["strHour"]) == true ? "00" : context.Request.Form["strMinu"].ToString());
                            string Context = string.IsNullOrEmpty(context.Request.Form["Context"]) == true ? "" : context.Request.Form["Context"].ToString();
                            Info.Context = Context;
                            Info.ReportType = ReportType;
                            Info.RegisterDate = DateTime.Now;
                            Info.Par = par;
                            Info.State = '0';
                            Info.TaskID = "";//目前还不知道任务ID的取值方法
                            proxy = new WCFServiceProxy<ISetUploadCycle>();
                            if (!string.IsNullOrEmpty(context.Request.Form["hidType"]) && (context.Request.Form["hidType"].ToString() != ""))
                            {
                                //设定选择用户的说明
                                if (context.Request.Form["hidType"].ToString() == "1")
                                {
                                    Info.Scope = "所有用户";
                                    jsonMessage = proxy.getChannel.AddSetAlarmAll(Info);
                                }
                                else if (context.Request.Form["hidType"].ToString() == "2")
                                {
                                    Info.Scope = "选择用户";
                                    alarmMeter = new List<IoT_UploadCycleMeter>();
                                    if (context.Request.Form["strNo"] != null && context.Request.Form["strNo"].ToString().Trim() != string.Empty)
                                    {
                                        string strNo = context.Request.Form["strNo"];
                                        string[] arrNo = strNo.Split(',');
                                        for (int i = 0; i < arrNo.Length; i++)
                                        {
                                            IoT_UploadCycleMeter meter = new IoT_UploadCycleMeter();
                                            meter.MeterNo = arrNo[i];
                                            alarmMeter.Add(meter);
                                        }
                                    }
                                    jsonMessage = proxy.getChannel.Add(Info, alarmMeter);
                                }
                                else
                                {
                                    string strNo = context.Request.Form["communityStr"] == null ? "" : context.Request.Form["communityStr"];
                                    string[] arrNo = strNo.Split(',');
                                    Info.Scope = context.Request.Form["hidType"].ToString();
                                    jsonMessage = proxy.getChannel.AddSetAlarmArea(Info, arrNo.ToList());
                                }
                            }
                        }
                        break;
                        //修改上传周期
                    //case "EDIT":
                    //    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "szbjcs"))
                    //    {
                    //        Info = new CommonModelFactory<IoT_SetUploadCycle>().GetModelFromContext(context);
                    //        proxy = new WCFServiceProxy<ISetUploadCycle>();
                    //        jsonMessage = proxy.getChannel.Edit(Info);
                    //    }
                    //    break;
                        //撤销上传周期的设定
                    case "REVOKE":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "SCZQREV"))
                        {
                            proxy = new WCFServiceProxy<ISetUploadCycle>();
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