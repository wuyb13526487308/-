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

namespace CY.IotM.WebClient
{
    public partial class DownFrame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext context = Context;
            if (context.Session["LoginCompanyOperator"] != null)
            {

                CompanyOperator Loginer = (CompanyOperator)context.Session["LoginCompanyOperator"];
                //文件路径
                string physicNewFilePath = string.Format("{0}\\UserUpload\\",
                     context.Server.MapPath("~"));

                if (Directory.Exists(physicNewFilePath))
                {
                    string rarName = physicNewFilePath + "物联网表用户导入模板" + ".xlsx";
                    //如果文件存在
                    if (File.Exists(rarName))
                    {

                        System.IO.FileInfo file = new System.IO.FileInfo(rarName);
                        Response.Clear();
                        Response.Charset = "GB2312";
                        Response.ContentEncoding = System.Text.Encoding.UTF8;
                        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                        Response.AddHeader("Content-Disposition", "attachment; filename=物联网表用户导入模板.xlsx");
                        // 添加头信息，指定文件大小，让浏览器能够显示下载进度
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        // 指定返回的是一个不能被客户端读取的流，必须被下载
                        Response.ContentType = "application/ms-excel";
                        // 把文件流发送到客户端
                        Response.WriteFile(file.FullName);
                        // 停止页面的执行
                        //Response.End();     
                        HttpContext.Current.ApplicationInstance.CompleteRequest();

                    }

                }


            }


        }
    }
}