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
    /// GasUserManage 的摘要说明
    /// </summary>
    public class GasUserManage : BaseHandler
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

                    case "GETGASUSERLIST":
                        List<GasUser> userlist = GasUserManageService.GetInstance().GetGasUserList(base.loginOperator.Account);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<GasUser>(userlist, userlist.Count)
                        };
                        break;
                    case "GETCOMPANY":
                        List<Company> list = IotMService.GetInstance().GetCompanyList();
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<Company>(list, list.Count)
                        };
                        break;
                    case "GETGASUSER":
                        string companyId = "", userId = "";
                        if (context.Request.Form["companyId"] != null && context.Request.Form["companyId"].ToString().Trim() != string.Empty)
                        {
                            companyId = context.Request.Form["companyId"].ToString().Trim();
                        }
                        if (context.Request.Form["userId"] != null && context.Request.Form["userId"].ToString().Trim() != string.Empty)
                        {
                            userId = context.Request.Form["userId"].ToString().Trim();
                        }
                        if (companyId != "" && userId != "")
                        {
                            GasUser gasUser = IotMService.GetInstance().GetGasUserByUserId(userId, companyId);
                            jsonMessage = new Message()
                            {
                                Result = true,
                                TxtMessage = JSon.TToJson<GasUser>(gasUser)
                            };
                        }
                        break;
                    case "ADDGASUSER":
                        companyId = ""; userId = "";
                        if (context.Request.Form["companyId"] != null && context.Request.Form["companyId"].ToString().Trim() != string.Empty)
                        {
                            companyId = context.Request.Form["companyId"].ToString().Trim();
                        }
                        if (context.Request.Form["userId"] != null && context.Request.Form["userId"].ToString().Trim() != string.Empty)
                        {
                            userId = context.Request.Form["userId"].ToString().Trim();
                        }
                        if (companyId != "" && userId != "")
                        {
                            jsonMessage = GasUserManageService.GetInstance().AddGasUser(base.loginOperator.Account, userId, companyId);
                        }
                        break;
                    case "DELETEGASUSER":
                        companyId = ""; userId = "";
                        if (context.Request.Form["Company"] != null && context.Request.Form["Company"].ToString().Trim() != string.Empty)
                        {
                            companyId = context.Request.Form["Company"].ToString().Trim();
                        }
                        if (context.Request.Form["UserID"] != null && context.Request.Form["UserID"].ToString().Trim() != string.Empty)
                        {
                            userId = context.Request.Form["UserID"].ToString().Trim();
                        }
                        if (companyId != "" && userId != "")
                        {
                            jsonMessage = GasUserManageService.GetInstance().RemoveGasUser(base.loginOperator.Account, userId, companyId);
                        }
                        break;
                    case "CHARGE":
                        companyId = ""; userId = "";decimal money=0;
                        if (context.Request.Form["companyId"] != null && context.Request.Form["companyId"].ToString().Trim() != string.Empty)
                        {
                            companyId = context.Request.Form["companyId"].ToString().Trim();
                        }
                        if (context.Request.Form["userId"] != null && context.Request.Form["userId"].ToString().Trim() != string.Empty)
                        {
                            userId = context.Request.Form["userId"].ToString().Trim();
                        }
                        if (context.Request.Form["money"] != null && context.Request.Form["money"].ToString().Trim() != string.Empty)
                        {
                            money = decimal.Parse(context.Request.Form["money"].ToString().Trim());
                        }
                        if (companyId == "" || userId == "" || money == 0) { break; }
                        string result = IotMService.GetInstance().Charge( userId, companyId,money);
                        if (result == "") 
                        {

                            GasUser gasUser = IotMService.GetInstance().GetGasUserByUserId(userId, companyId);
                            Olb_PaymentRecord payMent=new Olb_PaymentRecord(){
                                 Account=base.loginOperator.Account,
                                 GasUserID=userId,
                                 PayMoney=money,
                                 PayTime=DateTime.Now,
                                 Balance = gasUser.Balance,
                                 GasUserAddress=gasUser.Address,
                                 GasUserName=gasUser.UserName  
                            };
                            PaymentManageService.GetInstance().AddPaymentRecord(payMent);
                            jsonMessage = new Message() { Result = true, TxtMessage = "充值成功。" };
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