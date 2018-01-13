using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CY.IoTM.OlbCommon;
using CY.IoTM.OlbCommon.Tool;
using CY.IoTM.OlbService;


namespace CY.IoTM.OnLineBusiness.Handler
{

    public class LogonManage : BaseHandler
    {
        public override void DoLoginedHandlerWork(HttpContext context)
        {
            DoNoLoginHandlerWork(context);
        }
        public override void DoNoLoginHandlerWork(HttpContext context)
        {
            Message jsonMessage = new Message();
            //获取操作类型AType:ADD,EDIT,DELETE,QUERY

            string AjaxType = context.Request.QueryString["AType"] == null ? string.Empty : context.Request.QueryString["AType"].ToString().ToUpper();
            switch (AjaxType)
            {
                
                case "USERLOGIN":
                    jsonMessage = UserLogin(context);
                    break;
                case "USERLOGINOUT":
                    if (context.Session["LoginCompanyOperator"] != null)
                    {
                        context.Session.Remove("LoginCompanyOperator");
                    }
                    string webCookie = context.Session.SessionID.ToString();
                    jsonMessage = LoginerManageService.GetInstance().UnLRegisterClientByMd5Cookie(Md5.GetMd5(webCookie));
                    break;
                case "USERREGISTER":
                    try
                    {
                        string name = "", pwd = "";
                        if (context.Request.Form["Name"] != null && context.Request.Form["Name"].ToString().Trim() != string.Empty)
                        {
                            name = context.Request.Form["Name"].ToString().Trim();
                        }
                        if (context.Request.Form["Password"] != null && context.Request.Form["Password"].ToString().Trim() != string.Empty)
                        {
                            pwd = context.Request.Form["Password"].ToString().Trim();
                        }
                        if (name != "" && pwd != "") 
                        {
                            Olb_User user = new Olb_User();
                            user.Account = name;
                            user.PassWord = Md5.GetMd5(pwd);
                            jsonMessage = UserManageService.GetInstance().Add(user);
                            if (jsonMessage.Result) {
                                Olb_User Loginer = UserManageService.GetInstance().GetUserByAccount(name);
                                webCookie = context.Session.SessionID.ToString();
                                LoginerManageService.GetInstance().UserLogin(Md5.GetMd5(webCookie), Loginer.Account);

                                Loginer.PassWord = string.Empty;
                                context.Session["LoginCompanyOperator"] = Loginer;
                            
                            }
                        }
                    }
                    catch (Exception e) { }
                    break;
                case "CHECKCODE":
                    string code = "";
                    if (context.Request.Form["Code"] != null && context.Request.Form["Code"].ToString().Trim() != string.Empty)
                    {
                        code = context.Request.Form["Code"].ToString().Trim();
                    }
                    string validCode = context.Session["CheckCode"] as String;  //获取系统生成的验证码  
                    if (!string.IsNullOrEmpty(validCode) && !string.IsNullOrEmpty(code))
                    {
                        if (code.ToLower() == validCode.ToLower())
                        {
                            jsonMessage = new Message()
                            {
                                Result = true,
                                TxtMessage = "ok"
                            };
                        }
                        else
                        {
                            jsonMessage = new Message()
                            {
                                Result = false,
                                TxtMessage = "验证码错误"
                            };
                        }
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
            context.Response.Write(JSon.TToJson<Message>(jsonMessage));
        }
        #region 用户登陆
        private Message UserLogin(HttpContext context)
        {
            Message jMessage = new Message();
            string LoginID = string.Empty;
            string LoginPsw = string.Empty;

            if (context.Request.Form["Name"] != null && context.Request.Form["Name"].ToString().Trim() != string.Empty)
            {
                LoginID = context.Request.Form["Name"].ToString().Trim();
            }
            if (context.Request.Form["Password"] != null)
            {
                LoginPsw = context.Request.Form["Password"].ToString().Trim();
            }
            if (LoginID == string.Empty)
            {
                jMessage = new Message()
                {
                    Result = false,
                    TxtMessage = "登录账号格式不正确。"
                };
            }
            else
            {

                Olb_User Loginer = UserManageService.GetInstance().GetUserByAccount(LoginID);

                if (Loginer != null && Loginer.Account != string.Empty)
                {
                    if (Loginer.PassWord == Md5.GetMd5(LoginPsw))
                    {
                        jMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = "登录成功。"
                        };
                        string webCookie = context.Session.SessionID.ToString();
                        LoginerManageService.GetInstance().UserLogin(Md5.GetMd5(webCookie), Loginer.Account);

                        Loginer.PassWord = string.Empty;
                        context.Session["LoginCompanyOperator"] = Loginer;
                    }
                    else
                    {
                        jMessage = new Message()
                        {
                            Result = false,
                            TxtMessage = "密码错误。"
                        };
                    }
                }
                else
                {
                    jMessage = new Message()
                    {
                        Result = false,
                        TxtMessage = "登录账号不存在。"
                    };
                }
            }
            return jMessage;
        }
        #endregion
    }
}