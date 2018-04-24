using OneNETDataReceiver.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace CY.IotM.WebClient
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Param.token = $"{System.Configuration.ConfigurationManager.AppSettings["token"]}";
            Param.aeskey = $"{System.Configuration.ConfigurationManager.AppSettings["aeskey"]}";
            string jmMode = $"{System.Configuration.ConfigurationManager.AppSettings["IsJM"]}";
            Param.appkey = $"{System.Configuration.ConfigurationManager.AppSettings["appkey"]}";
            Param.url = $"{System.Configuration.ConfigurationManager.AppSettings["ApiUrl"]}";
            if (jmMode == "" || jmMode == "0")
            {
                Param.isJiaMi = false;//明文
            }
            else
            {
                Param.isJiaMi = true;
            }

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}