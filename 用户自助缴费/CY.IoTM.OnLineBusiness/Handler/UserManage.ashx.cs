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
    /// UserManage 的摘要说明
    /// </summary>
    public class UserManage : BaseHandler
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
                    case "LOADLOGINER":
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.TToJson<Olb_User>(base.loginOperator)
                        };
                        break;
                    case "USEREDIT":
                        Olb_User user = base.loginOperator;
                        if (context.Request.Form["Name"] != null && context.Request.Form["Name"].ToString().Trim() != string.Empty)
                        {
                            user.Name = context.Request.Form["Name"].ToString().Trim();
                        }
                        if (context.Request.Form["IdentityCard"] != null && context.Request.Form["IdentityCard"].ToString().Trim() != string.Empty)
                        {
                            user.IdentityCard = context.Request.Form["IdentityCard"].ToString().Trim();
                        }
                        if (context.Request.Form["Address"] != null && context.Request.Form["Address"].ToString().Trim() != string.Empty)
                        {
                            user.Address = context.Request.Form["Address"].ToString().Trim();
                        }

                        jsonMessage = UserManageService.GetInstance().Edit(user);
                        break;

                    case "UPDATEPWD":
                        string oldPwd="", newPwd="", account="";
                        if (context.Request.Form["oldPwd"] != null && context.Request.Form["oldPwd"].ToString().Trim() != string.Empty)
                        {
                            oldPwd = context.Request.Form["oldPwd"].ToString().Trim();
                        }
                        if (context.Request.Form["newPwd"] != null && context.Request.Form["newPwd"].ToString().Trim() != string.Empty)
                        {
                            newPwd = context.Request.Form["newPwd"].ToString().Trim();
                        }
                        if (oldPwd != "" && newPwd != "" && account != "") {

                            account = base.loginOperator.Account;
                            oldPwd = Md5.GetMd5(oldPwd);
                            newPwd = Md5.GetMd5(newPwd);
                            jsonMessage = UserManageService.GetInstance().UpdatePwd(oldPwd, newPwd, account);
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