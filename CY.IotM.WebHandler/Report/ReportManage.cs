using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using RX.Gas.ReportLib;
using System.IO;

namespace CY.IotM.WebHandler.Report
{
    public class ReportManage : BaseHandler
    {
        public override void DoLoginedHandlerWork(HttpContext context)
        {
            Message jsonMessage;
            //获取操作类型AType:ADD,EDIT,DELETE,QUERY
            string AjaxType = context.Request.QueryString["AType"] == null ? string.Empty : context.Request.QueryString["AType"].ToString().ToUpper();
            ReportTemplate Info;
            WCFServiceProxy<IOperRightManage> proxy;
            jsonMessage = new Message()
            {
                Result = false,
                TxtMessage = "权限验证失败，可能原因:\n1、数据中心通讯失败。\n2、系统管理员未与您分配对应操作权限。"
            };
            switch (AjaxType)
            {
                case "QUERY":
                    string Where = "1=1 ";
                    if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                    {
                        Where += "AND " + context.Request.Form["TWhere"].ToString();
                    }
                    CommonSearch<ReportTemplate> userInfoSearch = new CommonSearch<ReportTemplate>();
                    SearchCondition sCondition = new SearchCondition() { TBName = "ReportTemplate", TFieldShow="RID,ReportName", TFieldKey = "RID,ReportName", TTotalCount = -1, TPageCurrent = 1, TPageCount = 1, TFieldOrder = "RID ASC", TWhere = Where };
                    List<ReportTemplate> list = userInfoSearch.GetList(ref sCondition, context);
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<ReportTemplate>(list, sCondition.TTotalCount)
                    };
                    break;
                case "EDITNAME":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "xgbbmc"))
                    {
                        Info = new CommonModelFactory<ReportTemplate>().GetModelFromContext(context);

                        proxy = new WCFServiceProxy<IOperRightManage>();
                        jsonMessage = proxy.getChannel.EditReportName(Info);

                    }
                    break;
                case "EXPORT":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "dcbbmb"))
                    {

                        try
                        {
                            int rID = -1;
                            int.TryParse(context.Request.QueryString["RID"].ToString(), out rID);
                            string rName = context.Request.QueryString["RName"] == null ? "报表模板" : context.Request.QueryString["RName"].ToString();
                            string strSaveDictory = string.Format("{0}\\ReportTemplateExport\\{1}\\{2}\\{3}\\",
    context.Server.MapPath("~"), base.loginOperator.CompanyID, base.loginOperator.OperID, System.DateTime.Now.ToString("yyyyMMdd"));
                            string strSavePath = string.Format("{0}\\ReportTemplateExport\\{1}\\{2}\\{3}\\{4}.rpt",
    context.Server.MapPath("~"), base.loginOperator.CompanyID, base.loginOperator.OperID, System.DateTime.Now.ToString("yyyyMMdd"), rName);
                            string strDownloadUrl = string.Format("{0}/ReportTemplateExport/{1}/{2}/{3}/{4}.rpt",
     "..", base.loginOperator.CompanyID, base.loginOperator.OperID, System.DateTime.Now.ToString("yyyyMMdd"), rName);
                            ReportDataSourceDs ds = new Reports().ReadReport(rID);
                            if (!Directory.Exists(strSaveDictory))
                            {
                                Directory.CreateDirectory(strSaveDictory);
                            }
                            ds.WriteXml(strSavePath);
                            jsonMessage = new Message() { Result = true, TxtMessage = strDownloadUrl };

                            //以字符流的形式下载文件
                            FileStream fs = new FileStream(strSavePath, FileMode.Open);
                            byte[] bytes = new byte[(int)fs.Length];
                            fs.Read(bytes, 0, bytes.Length);
                            fs.Close();
                            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
                            context.Response.ContentType = "application/octet-stream";
                            //通知浏览器下载文件而不是打开
                            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(rName) + ".rpt");
                            context.Response.BinaryWrite(bytes);
                            context.Response.Flush();
                            context.Response.End();
                        }
                        catch (Exception e)
                        {
                            jsonMessage = new Message() { Result = false, TxtMessage = e.Message };
                        }

                    }
                    break;
                case "DELETE":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "scbbmb"))
                    {
                        Info = new CommonModelFactory<ReportTemplate>().GetModelFromContext(context);
                        Reports r = new Reports();
                        string dResult = r.DeleteReportTemplate(Info.RID);
                        jsonMessage = new Message()
                        {
                            Result = dResult == string.Empty ? true : false,
                            TxtMessage = dResult == string.Empty ? "删除成功" : dResult
                        };

                    }
                    break;

               case "QUERYCOMPANYREPORT":
                     Where = "1=1 ";
                    if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                    {
                        Where += "AND " + context.Request.Form["TWhere"].ToString();
                    }
                    CommonSearch<CompanyReport>  reportInfoSearch = new CommonSearch<CompanyReport>();
                    sCondition = new SearchCondition() { TBName = "View_CompanyReport", TFieldKey = "CompanyID,MenuCode", TTotalCount = -1, TPageCurrent = 1, TPageCount = 1, TFieldOrder = "RID ASC", TWhere = Where };

                    List<CompanyReport> reportlist = reportInfoSearch.GetList(ref sCondition, context);
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<CompanyReport>(reportlist, sCondition.TTotalCount)
                    };
                    break;

                default:
                    jsonMessage = new Message()
                    {
                        Result = false,
                        TxtMessage = "操作未定义。"
                    };
                    break;
            }
            context.Response.Write(JSon.TToJson<Message>(jsonMessage));
        }
    }
}
