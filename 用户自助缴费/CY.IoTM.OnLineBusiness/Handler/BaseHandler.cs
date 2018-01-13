using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using CY.IoTM.OlbCommon;
using CY.IoTM.OlbCommon.Tool;
using CY.IoTM.OlbService;

namespace CY.IoTM.OnLineBusiness
{
    public class BaseHandler : IHttpHandler, IRequiresSessionState
    {
        protected bool isLoginOn = false;
        protected Olb_User loginOperator;
        void GetLoginer(HttpContext context)
        {
            if (context.Session["LoginCompanyOperator"] == null)
            {   
                try
                {
                    string webCookie = context.Session.SessionID.ToString();
                    Olb_User dCLoginer = null;
                    dCLoginer =LoginerManageService.GetInstance().GetLoginerByMd5Cookie(Md5.GetMd5(webCookie));
                    if (dCLoginer != null)
                    {
                        dCLoginer.PassWord = string.Empty;
                        context.Session["LoginCompanyOperator"] = dCLoginer;
                    }
                    else
                    {
                        if (context.Request.Form["NO_COOKIE_SessionId"] != null && context.Request.Form["NO_COOKIE_SessionId"].ToString() != string.Empty)
                        {
                            webCookie = context.Request.Form["NO_COOKIE_SessionId"].ToString();
                            dCLoginer = LoginerManageService.GetInstance().GetLoginerByMd5Cookie(webCookie);
                            if (dCLoginer != null)
                            {
                                dCLoginer.PassWord = string.Empty;
                                context.Session["LoginCompanyOperator"] = dCLoginer;
                            }
                        }
                    }
                }
                catch
                { }
           
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Session["LoginCompanyOperator"] == null)
            {
                GetLoginer(context);
            }
            if (context.Session["LoginCompanyOperator"] != null)
            {
                isLoginOn = true;
                loginOperator = (Olb_User)context.Session["LoginCompanyOperator"];
                DoLoginedHandlerWork(context);
            }
            else
            {
                isLoginOn = false;
                loginOperator = null;
                DoNoLoginHandlerWork(context);
            }

        }

        public virtual void DoLoginedHandlerWork(HttpContext context)
        {

        }
        public virtual void DoNoLoginHandlerWork(HttpContext context)
        {
            context.Response.Write(JSon.TToJson<Message>(new Message() { Result = false, TxtMessage = "用户未登陆或登陆已超时。" }));
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

 
}