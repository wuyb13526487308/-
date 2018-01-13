using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CY.IotM.Common;
using CY.IotM.Common.Tool;


namespace CY.IotM.WebClient
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LoginCompanyOperator"] == null)
                {                        //向数据中心记录登录信息
                    try
                    {
                        WCFServiceProxy<ILoginerManage> proxy = new WCFServiceProxy<ILoginerManage>();
                        string webCookie = Session.SessionID.ToString();
                        CompanyOperator dCLoginer = proxy.getChannel.GetLoginerByMd5Cookie(Md5.GetMd5(webCookie));
                        if (dCLoginer != null)
                        {
                            dCLoginer.Pwd = string.Empty;
                            Session["LoginCompanyOperator"] = dCLoginer;
                        }
                        else
                        {
                            Response.Redirect("Login.aspx", true);
                        }
                    }
                    catch 
                    {
                      
                    }
                   
                }
            }
        }
    }
}