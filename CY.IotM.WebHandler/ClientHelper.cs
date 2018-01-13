using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace CY.IotM.WebHandler
{
    public class ClientHelper
    {
        string _clientAgent = string.Empty;
        string _clientBrowser = string.Empty;
        public string clientBrowser
        {
            get { return _clientBrowser; }
        }
        string _clientIP = string.Empty;
        public string ClientIP
        {
            get { return _clientIP; }
        }
        string _clientSystem = string.Empty;

        public string ClientSystem
        {
            get { return _clientSystem; }
        }
        bool _isMobile = false;
        public ClientHelper()
        {
            this._clientAgent = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"] == null ?
                string.Empty : HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
            this._clientIP = getIP();
            this._isMobile = IsMobile();
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            this._clientSystem =string.Format( "{0}（{1}）",this._isMobile?"移动设备":"电脑",textInfo.ToTitleCase(getSystem()));
            this._clientBrowser = textInfo.ToTitleCase(getBrowser());
        }
        private string getBrowser()
        {
            string agent = this._clientAgent.ToLower();
            Regex regStr_ie = new Regex(@"msie [\d]+");
            Regex regStr_ff = new Regex(@"firefox\/[\d]+");
            Regex regStr_opera = new Regex(@"opr\/[\d]+");
            Regex regStr_chrome = new Regex(@"chrome\/[\d]+");
            Regex regStr_saf = new Regex(@"safari\/[\d]+");

            //IE
            if (agent.IndexOf("msie") > -1)
            {
                return regStr_ie.Match(agent).ToString();
            }
            //firefox
            if (agent.IndexOf("firefox") > -1)
            {
                return regStr_ff.Match(agent).ToString();
            }
            //Opr
            if (agent.IndexOf("opr") > -1)
            {
                return regStr_opera.Match(agent).ToString();
            }
            //Chrome
            if (agent.IndexOf("chrome") > -1)
            {
                return regStr_chrome.Match(agent).ToString();
            }          
            //Safari
            if (agent.IndexOf("safari") > -1 && agent.IndexOf("chrome") < 0)
            {
                return regStr_saf.Match(agent).ToString();
            }
            if (this._isMobile)
            {
                return "其他wap";
            }
            return "其他";
        }
        //获取客户端IP地址
        private string getIP()
        {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            if (null == result || result == String.Empty)
            {
                return "0.0.0.0";
            }
            return result;
        }

        private bool IsMobile()
        {
            bool isMoblie = false;
            string[] mobileAgents = { "iphone", "android", "phone", "mobile", "wap", "netfront", 
                                        "java", "opera mobi", "opera mini", "ucweb", "windows ce", 
                                        "symbian", "series", "webos", "sony", "blackberry",
                                        "dopod", "nokia", "samsung", "palmsource", "xda", 
                                        "pieplus", "meizu", "midp", "cldc", "motorola", 
                                        "foma", "docomo", "up.browser", "up.link", 
                                        "blazer", "helio", "hosin", "huawei", "novarra",
                                        "coolpad", "webos", "techfaith", "palmsource", 
                                        "alcatel", "amoi", "ktouch", "nexian", "ericsson", 
                                        "philips", "sagem", "wellcom", "bunjalloo", "maui",
                                        "smartphone", "iemobile", "spice", "bird", "zte-", 
                                        "longcos", "pantech", "gionee", "portalmmm", "jig browser", 
                                        "hiptop", "benq", "haier", "^lct", "320x320", "240x320", 
                                        "176x220", "w3c ", "acs-", "alav", "alca", "amoi", "audi",
                                        "avan", "benq", "bird", "blac", "blaz", "brew", "cell", 
                                        "cldc", "cmd-", "dang", "doco", "eric", "hipt", "inno", 
                                        "ipaq", "java", "jigs", "kddi", "keji", "leno", "lg-c", 
                                        "lg-d", "lg-g", "lge-", "maui", "maxo", "midp", "mits", 
                                        "mmef", "mobi", "mot-", "moto", "mwbp", "nec-", "newt", 
                                        "noki", "oper", "palm", "pana", "pant", "phil", "play", 
                                        "port", "prox", "qwap", "sage", "sams", "sany", "sch-", 
                                        "sec-", "send", "seri", "sgh-", "shar", "sie-", "siem", 
                                        "smal", "smar", "sony", "sph-", "symb", "t-mo", "teli", "tim-",
                                        "tosh", "tsm-", "upg1", "upsi", "vk-v", "voda", "wap-", "wapa", 
                                        "wapi", "wapp", "wapr", "webc", "winw", "winw", "xda", "xda-", "Googlebot-Mobile" };

            if (!string.IsNullOrEmpty(this._clientAgent))
            {
                for (int i = 0; i < mobileAgents.Length; i++)
                {
                    if (this._clientAgent.ToLower().IndexOf(mobileAgents[i]) > -1)
                    {
                        isMoblie = true;
                        break;
                    }
                }
            }
            return isMoblie;
        }
        //获取操作系统版本号
        private string getSystem()
        {
            string Agent = this._clientAgent.ToLower();
            if (Agent.IndexOf("android") > -1)
            {
                return "Android";
            }
            else if (Agent.IndexOf("mac os") > -1)
            {
                return "Mac";
            }
            else if (this._isMobile)
            {
                return "其他平台";
            }
            else if (Agent.IndexOf("nt 4.0") > -1)
            {
                return "Windows NT ";
            }
            else if (Agent.IndexOf("nt 5.0") > -1)
            {
                return "Windows 2000";
            }
            else if (Agent.IndexOf("nt 5.1") > -1)
            {
                return "Windows XP";
            }
            else if (Agent.IndexOf("nt 5.2") > -1)
            {
                return "Windows 2003";
            }
            else if (Agent.IndexOf("nt 6.0") > -1)
            {
                return "Windows Vista/2008";
            }
            else if (Agent.IndexOf("nt 6.1") > -1)
            {
                return "Windows 7/2008 R2";
            }
            else if (Agent.IndexOf("nt 6.2") > -1)
            {
                return "Windows 8/2012/Phone 8";
            }
            else if (Agent.IndexOf("nt 6.3") > -1)
            {
                return "Windows 8.1/2012 R2";
            }
            else if (Agent.IndexOf("windowsce") > -1)
            {
                return "Windows CE";
            }
            else if (Agent.IndexOf("nt") > -1)
            {
                return "Windows NT ";
            }
            else if (Agent.IndexOf("9x") > -1)
            {
                return "Windows ME";
            }
            else if (Agent.IndexOf("98") > -1)
            {
                return "Windows 98";
            }
            else if (Agent.IndexOf("95") > -1)
            {
                return "Windows 95";
            }
            else if (Agent.IndexOf("win32") > -1)
            {
                return "Win32";
            }
            else if (Agent.IndexOf("linux") > -1)
            {
                return "Linux";
            }
            else if (Agent.IndexOf("sunos") > -1)
            {
                return "SunOS";
            }
            else if (Agent.IndexOf("linux") > -1)
            {
                return "Linux";
            }
            else if (Agent.IndexOf("windows") > -1)
            {
                return "Windows";
            }
            return "其他";
        }

    }
}
