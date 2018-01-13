using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using CY.IotM.Common;
using CY.IotM.Common.Tool;

namespace CY.IotM.WebHandler
{
    public class BaseHandler : IHttpHandler, IRequiresSessionState
    {
        protected bool isLoginOn = false;
        protected CompanyOperator loginOperator;
        void GetLoginer(HttpContext context)
        {
            if (context.Session["LoginCompanyOperator"] == null)
            {   //查询数据中心记录的登录者信息
               
                WCFServiceProxy<ILoginerManage> proxy =null;
                try
                {
                    proxy = new WCFServiceProxy<ILoginerManage>();
                    string webCookie = context.Session.SessionID.ToString();
                    CompanyOperator dCLoginer = null;
                    dCLoginer = proxy.getChannel.GetLoginerByMd5Cookie(Md5.GetMd5(webCookie));
                    if (dCLoginer != null)
                    {
                        dCLoginer.Pwd = string.Empty;
                        context.Session["LoginCompanyOperator"] = dCLoginer;
                    }
                    else
                    {
                        if (context.Request.Form["NO_COOKIE_SessionId"] != null && context.Request.Form["NO_COOKIE_SessionId"].ToString() != string.Empty)
                        {
                            webCookie = context.Request.Form["NO_COOKIE_SessionId"].ToString();
                            dCLoginer = proxy.getChannel.GetLoginerByMd5Cookie(webCookie);
                            if (dCLoginer != null)
                            {
                                dCLoginer.Pwd = string.Empty;
                                context.Session["LoginCompanyOperator"] = dCLoginer;
                            }
                        }
                    }
                }
                catch
                { }
                finally
                {
                    if (proxy != null)
                        proxy.CloseChannel();
                }
               
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
                loginOperator = (CompanyOperator)context.Session["LoginCompanyOperator"];
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