using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CY.IoTM.Common.ADSystem;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IotM.WebHandler;
using CY.IoTM.Common.Business;


namespace CY.IotM.WebClient.AdM
{
    public partial class AdMPlay : System.Web.UI.Page
    {
        public string imgArray = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            string CompanyID = Request.QueryString["CompanyID"] == null ? string.Empty : Request.QueryString["CompanyID"].ToString();
            string AC_ID = Request.QueryString["AC_ID"] == null ? string.Empty : Request.QueryString["AC_ID"].ToString();
            if (AC_ID == "" || CompanyID=="")
            {
                Response.Write("<script>alert('参数错误！AC_ID,CompanyID不能为空！')</script>");
                return;
            }

            ADItem Info = new ADItem();
            WCFServiceProxy<IADItemDAL> proxy = null;
            proxy = new WCFServiceProxy<IADItemDAL>();
            

            //提出显示图片ACID
            List<ADItem> listView = proxy.getChannel.getListShow(long.Parse(AC_ID));
            //把数据写到特定DIV中;
            for (int i = 0; i < listView.Count; i++)
            {
                imgArray += "<img src='ADMPlayShow.aspx?fileLen=" + listView[i].FileLength + "&StoreName=" + listView[i].StoreName + "&CompanyID=" + CompanyID + "'  title='" + listView[i].FileName + "' />";

            } 
        }
    }
}