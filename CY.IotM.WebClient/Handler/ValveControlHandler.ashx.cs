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
    /// ValveControlHandler 的摘要说明
    /// </summary>
    public class ValveControlHandler : BaseHandler
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

            View_ValveControl Info;
            WCFServiceProxy<IValveControl> proxy = null; 
            WCFServiceProxy<IMeterManage> proxyMeter = null;



            try
            {
                switch (AjaxType)
                {


                    case "QUERY":

                    CommonSearch<View_ValveControl> InfoSearch = new CommonSearch<View_ValveControl>();
                    string Where = "1=1 ";
                    Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                    if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                    {
                        Where += context.Request.Form["TWhere"].ToString();
                    }
                    SearchCondition sCondition = new SearchCondition() { TBName = "View_ValveControl", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "RegisterDate Desc", TWhere = Where };
                    List<View_ValveControl> list = InfoSearch.GetList(ref sCondition, context);
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<View_ValveControl>(list, sCondition.TTotalCount)
                    };
                    break;



                    //开阀      
                    case "KAIFA":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "fmcz_kf"))
                    {

                        proxyMeter = new WCFServiceProxy<IMeterManage>();
                        proxy = new WCFServiceProxy<IValveControl>();

                        string reason = "";
                        if (context.Request.Form["Reason"] != null && context.Request.Form["Reason"].ToString().Trim() != string.Empty)
                        {
                            reason = context.Request.Form["Reason"].ToString().Trim();
                        }

                        if (context.Request.Form["strNo"] != null && context.Request.Form["strNo"].ToString().Trim() != string.Empty)
                        {
                            string strNo = context.Request.Form["strNo"];

                            string[] arrNo = strNo.Split(',');

                            for (int i = 0; i < arrNo.Length; i++)
                            {
                                IoT_Meter meter= proxyMeter.getChannel.GetMeterByNo(arrNo[i]);
                                proxy.getChannel.TurnOn(meter, reason,loginOperator.Name);

                            }
                        }
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = "操作成功"
                        };
                    }
                    break;

                    //关阀
                    case "GUANFA":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "fmcz_gf"))
                    {
                        proxyMeter = new WCFServiceProxy<IMeterManage>();
                        proxy = new WCFServiceProxy<IValveControl>();

                        string reason = "";
                        if (context.Request.Form["Reason"] != null && context.Request.Form["Reason"].ToString().Trim() != string.Empty)
                        {
                            reason = context.Request.Form["Reason"].ToString().Trim();
                        }

                        if (context.Request.Form["strNo"] != null && context.Request.Form["strNo"].ToString().Trim() != string.Empty)
                        {
                            string strNo = context.Request.Form["strNo"];

                            string[] arrNo = strNo.Split(',');

                            for (int i = 0; i < arrNo.Length; i++)
                            {
                                IoT_Meter meter = proxyMeter.getChannel.GetMeterByNo(arrNo[i]);
                                proxy.getChannel.TurnOff(meter, reason, loginOperator.Name);

                            }
                        }
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = "操作成功"
                        };
                    }
                    break;


                    case "UNDO":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "fmcz_cx"))
                    {
                        Info = new CommonModelFactory<View_ValveControl>().GetModelFromContext(context);
                        proxy = new WCFServiceProxy<IValveControl>();
                        proxy.getChannel.Undo(Info.TaskID,Info.Context);

                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = "操作成功"
                        };
                    
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