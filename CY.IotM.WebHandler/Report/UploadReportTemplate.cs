using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CY.IotM.Common;
using System.IO;
using RX.Version.FileManage;
using RX.Gas.ReportLib;
using System.Data;
using CY.Gas.Report.Common;
using CY.IotM.Common.Tool;

namespace CY.IotM.WebHandler.Report
{
    /// <summary>
    /// 上传报表模板处理入口
    /// </summary>
    public class UploadReportTemplate : BaseHandler
    {
        int success = 0;
        int failed = 0;
        List<string> successName = new List<string>();
        List<string> failedReasons = new List<string>();
        public override void DoLoginedHandlerWork(HttpContext context)
        {
            Message jsonMessage;
            bool isUpdate = false;
            //检查用户有没有更新报表权限，
            if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "gxbbmb"))
            {
                isUpdate = context.Request.Form["isUpdate"] == null ? false : bool.Parse(context.Request.Form["isUpdate"].ToString());
            }
            string strUploadPath = string.Format("{0}\\ReportTemplate\\{1}\\{2}\\{3}\\",
                context.Server.MapPath("~"), base.loginOperator.CompanyID, base.loginOperator.OperID, System.DateTime.Now.ToString("yyyyMMdd"));
            jsonMessage = new Message() { Result = false, TxtMessage = "权限验证失败，可能原因:\n1、数据中心通讯失败。\n2、系统管理员未与您分配对应操作权限。" };
            string fileName = string.Empty;
            //创建路径记录上传者账号信息
            if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "drbbmb"))
            {
                if (context.Request.Files != null && context.Request.Files.Count == 1)
                {
                    if (!Directory.Exists(strUploadPath))
                    {
                        Directory.CreateDirectory(strUploadPath);
                    }
                    HttpPostedFile postedFile = context.Request.Files[0];
                    fileName = strUploadPath + System.Web.HttpUtility.UrlDecode(Path.GetFileName(postedFile.FileName));
                    if (fileName != "" && Path.GetExtension(fileName) == ".pkf")
                    {
                        try
                        {
                            postedFile.SaveAs(fileName);
                            FilePacket pft = new FilePacket(fileName);
                            pft.OpenPacket(strUploadPath);
                            pft = null;
                            ImportReport(strUploadPath, isUpdate);
                            string ShowInfo = string.Empty;
                            ShowInfo += string.Format("成功{0}个。<br>", success);
                            int i = 0;
                            foreach (string tmp in successName)
                            {
                                i++;
                                ShowInfo += string.Format("{0}、【{1}】", i, tmp);
                            }
                            if (failed > 0)
                            {
                                ShowInfo += string.Format("<br>失败{0}个。<br>", failed);
                                i = 0;
                                foreach (string tmp in failedReasons)
                                {
                                    i++;
                                    ShowInfo += string.Format("{0}、【{1}】", i, tmp);
                                }
                            }
                            jsonMessage = new Message() { Result = true, TxtMessage = ShowInfo };
                        }
                        catch (Exception ex)
                        {
                            jsonMessage = new Message() { Result = false, TxtMessage = ex.Message };
                            if (File.Exists(fileName))
                            {
                                File.Delete(fileName);
                            }
                        }
                    }
                }
            }
            context.Response.Write(JSon.TToJson<Message>(jsonMessage));
        }
        //此处使用递归，进行遍历导入报表。
        private void ImportReport(string dPath, bool isUpdate)
        {
            // 遍历所有的文件和目录
            string[] fileList = System.IO.Directory.GetFileSystemEntries(dPath);
            foreach (string file in fileList)
            {
                if (!System.IO.Directory.Exists(file))
                {
                    if (Path.GetExtension(file) == ".rpt")
                    {
                        ReportDataSourceDs ds = new ReportDataSourceDs();
                        ds.ReadXml(file, XmlReadMode.Auto);
                        File.Delete(file);
                        Reports r = new Reports();
                        int rid = 0;
                        if (isUpdate)
                        {
                            rid = r.ImportReport(ds, new ImportConditioin() { bUpateReportTemplate = true, bUpdateDataSource = true });
                        }
                        else
                        {
                            rid = r.ImportReport(ds);
                        }
                        if (rid > 0)
                        {
                            BaseReport b = new BaseReport(rid);
                            successName.Add(b.ReportName);
                            success++;
                        }
                        else
                        {
                            failedReasons.Add(r.ErrorMessage);
                            failed++;
                        }
                    }
                }
                else
                {
                    ImportReport(file, isUpdate);
                }
            }
        }
    }
}
