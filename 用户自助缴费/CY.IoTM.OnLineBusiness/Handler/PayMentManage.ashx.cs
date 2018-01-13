using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CY.IoTM.OlbCommon;
using CY.IoTM.OlbCommon.Tool;
using CY.IoTM.OlbService;

namespace CY.IoTM.OnLineBusiness.Handler
{
    /// <summary>
    /// PayMentManage 的摘要说明
    /// </summary>
    public class PayMentManage : BaseHandler
    {


        public override void DoLoginedHandlerWork(HttpContext context)
        {
            Message jsonMessage;
            jsonMessage = new Message()
            {
                Result = false,
                TxtMessage = "权限验证失败，可能原因:\n1、数据中心通讯失败。\n2、系统管理员未与您分配对应操作权限。"
            };
            string AjaxType = context.Request.QueryString["AType"] == null ? string.Empty : context.Request.QueryString["AType"].ToString().ToUpper();

            try
            {
                switch (AjaxType)
                {
                    case "GETPAYMENT":
                    string startDate="", endDate = "";
                    if (context.Request.Form["Date1"] != null && context.Request.Form["Date1"].ToString().Trim() != string.Empty)
                    {
                        startDate = context.Request.Form["Date1"].ToString().Trim(); 
                    }
                    if (context.Request.Form["Date2"] != null && context.Request.Form["Date2"].ToString().Trim() != string.Empty)
                    {
                        endDate = context.Request.Form["Date2"].ToString().Trim();
                        endDate += " 23:59";
                    }

                    DateTime startTime = Convert.ToDateTime(startDate);
                    DateTime endTime = Convert.ToDateTime(endDate);

                    List<Olb_PaymentRecord> list= PaymentManageService.GetInstance().GetPaymentRecord(startTime, endTime,base.loginOperator.Account);
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<Olb_PaymentRecord>(list, list.Count)
                    };
                    break;

                    case "GETGASBILL":
                    string companyId = "", userId = "",month="";
                    if (context.Request.Form["month"] != null && context.Request.Form["month"].ToString().Trim() != string.Empty)
                    {
                        month = context.Request.Form["month"].ToString().Trim();
                    }
                    if (context.Request.Form["companyId"] != null && context.Request.Form["companyId"].ToString().Trim() != string.Empty)
                    {
                        companyId = context.Request.Form["companyId"].ToString().Trim();
                    }
                    if (context.Request.Form["userId"] != null && context.Request.Form["userId"].ToString().Trim() != string.Empty)
                    {
                        userId = context.Request.Form["userId"].ToString().Trim();
                    }

                    List<GasFeeBill> listBill =new List<GasFeeBill>();
                    GasFeeBill bill = IotMService.GetInstance().GetGasUserBill(userId, companyId, month);
                    if (bill == null)
                    {
                        jsonMessage = new Message()
                        {
                            Result = false,
                            TxtMessage = "本月还未产生账单"
                        };
                    }
                    else 
                    {
                        listBill.Add(bill);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<GasFeeBill>(listBill, listBill.Count)
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

            }
            context.Response.Write(JSon.TToJson<Message>(jsonMessage));
        }


    }
}