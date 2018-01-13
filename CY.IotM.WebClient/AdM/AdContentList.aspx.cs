using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CY.IotM.WebClient.AdM
{
    public partial class AdContentList : System.Web.UI.Page
    {
        public string stateShowStr = "";
        public string CompanyID = "";
        public long ACID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Request.QueryString["AC_ID"].ToString() == "")
            {
                Response.Write("<script>alert('AC_ID参数为空!');</script>'");
                return;
            }
            else {
                AC_ID.Value = Request.QueryString["AC_ID"].ToString();
                State.Value = Request.QueryString["State"].ToString();
                ACID = long.Parse(Request.QueryString["AC_ID"].ToString());
                CompanyID = Request.QueryString["CompanyID"].ToString();
            }
            if (State.Value == "0") {
                stateShowStr = "草稿";
            }
            else if (State.Value == "1") {
                stateShowStr = "可发布";
            }
            else if (State.Value == "2")
            { stateShowStr = "已发布"; }
            else { stateShowStr = "未知"; }
        }
    }
}