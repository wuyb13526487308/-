using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IotM.WebHandler;
using CY.IoTM.Common.Business;

namespace CY.IotM.WebClient.IotM
{
    public partial class PreViewImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            HttpContext context = Context;
            if (context.Session["LoginCompanyOperator"] != null)
            {

                CompanyOperator Loginer = (CompanyOperator)context.Session["LoginCompanyOperator"];


                WCFServiceProxy<IAdInfoManage> proxy = new WCFServiceProxy<IAdInfoManage>();


                int id = int.Parse(context.Request.QueryString["FileId"]);


                IoT_AdInfo adInfo = proxy.getChannel.GetAdInfoData(id);


                byte[] images = adInfo.FileData.ToArray();
                MemoryStream ms = new MemoryStream(images);
              
                context.Response.ClearContent();
                context.Response.ContentType = "image/jpeg";
                context.Response.BinaryWrite(ms.ToArray());//这里的Write改成BinaryWrite即可
                context.Response.End();


            }




        }
    }
}