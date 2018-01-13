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
    /// PricingHandler 的摘要说明
    /// </summary>
    public class PricingHandler : BaseHandler
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
            IoT_Pricing Info;
            WCFServiceProxy<IPricingManage> proxy=null;
            try
            {
                switch (AjaxType)
                {
                    case "QUERY":

                        CommonSearch<IoT_Pricing> InfoSearch = new CommonSearch<IoT_Pricing>();
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
                        SearchCondition sCondition = new SearchCondition() { TBName = "IoT_Pricing", TFieldKey = "RegisterDate", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "RegisterDate DESC", TWhere = Where };
                        List<IoT_Pricing> list = InfoSearch.GetList(ref sCondition, context);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<IoT_Pricing>(list, sCondition.TTotalCount)
                        };
                        break;
                    case "QUERYUSER":

                        CommonSearch<View_PricingMeter> InfoSearch_User = new CommonSearch<View_PricingMeter>();
                        Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        if (context.Request.Form["ID"] != null && context.Request.Form["ID"].ToString().Trim() != string.Empty)
                        {
                            Where += " AND ID=" + context.Request.Form["ID"].ToString().Trim();
                        }

                        sCondition = new SearchCondition() { TBName = "View_PricingMeter", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "UserID DESC", TWhere = Where };
                        List<View_PricingMeter> list_User = InfoSearch_User.GetList(ref sCondition, context);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_PricingMeter>(list_User, sCondition.TTotalCount)
                        };
                        break;

                    case "ADD":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "tjgl_tj"))
                        {

                            Info = new CommonModelFactory<IoT_Pricing>().GetModelFromContext(context);
                            Info.CompanyID = base.loginOperator.CompanyID;
                            Info.Oper = base.loginOperator.Name;
                            Info.RegisterDate = DateTime.Now;

                            proxy = new WCFServiceProxy<IPricingManage>();

                            if (context.Request.Form["Scope"] != null && context.Request.Form["Scope"].ToString().Trim() != string.Empty)
                            {

                                if (context.Request.Form["Scope"].ToString().Trim() == "所有用户")
                                {

                                    jsonMessage = proxy.getChannel.AddPricingAll(Info);
                                }
                                else if (context.Request.Form["Scope"].ToString().Trim() == "选择用户")
                                {
                                    List<IoT_PricingMeter> alarmMeter = new List<IoT_PricingMeter>();
                                    if (context.Request.Form["strNo"] != null && context.Request.Form["strNo"].ToString().Trim() != string.Empty)
                                    {
                                        string strNo = context.Request.Form["strNo"];

                                        string[] arrNo = strNo.Split(',');

                                        for (int i = 0; i < arrNo.Length; i++)
                                        {
                                            IoT_PricingMeter meter = new IoT_PricingMeter();
                                            meter.MeterNo = arrNo[i];
                                            alarmMeter.Add(meter);
                                        }
                                    }
                                    jsonMessage = proxy.getChannel.Add(Info, alarmMeter);
                                }
                                //选择区域用户
                                else
                                {
                                    if (context.Request.Form["strArea"] != null && context.Request.Form["strArea"].ToString().Trim() != string.Empty)
                                    {
                                        string strNo = context.Request.Form["strArea"];
                                        string[] arrNo = strNo.Split(',');
                                        jsonMessage = proxy.getChannel.AddPricingArea(Info, arrNo.ToList());

                                    }
                                }
                            }
                        }
                        break;
                    case "EDIT":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "tjgl_tj"))
                        {
                            Info = new CommonModelFactory<IoT_Pricing>().GetModelFromContext(context);
                            proxy = new WCFServiceProxy<IPricingManage>();
                            jsonMessage = proxy.getChannel.Edit(Info);
                        }
                        break;
                    case "DELETE":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, ""))
                        {
                            Info = new CommonModelFactory<IoT_Pricing>().GetModelFromContext(context);
                            proxy = new WCFServiceProxy<IPricingManage>();
                            jsonMessage = proxy.getChannel.Delete(Info);

                        }
                        break;

                    case "UNDO":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "tjgl_tj"))
                        {
                            Info = new CommonModelFactory<IoT_Pricing>().GetModelFromContext(context);
                            proxy = new WCFServiceProxy<IPricingManage>();
                            jsonMessage = proxy.getChannel.UnSetParamter(Info);
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
            catch (Exception  ex){
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