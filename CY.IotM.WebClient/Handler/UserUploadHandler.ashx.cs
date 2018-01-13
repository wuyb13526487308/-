using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

using System.Data;
using System.Data.OleDb;
using System.IO;

using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IotM.WebHandler;
using CY.IoTM.Common.Business;



namespace CY.IotM.WebClient.Handler
{
    /// <summary>
    /// UserCategoryHandler 的摘要说明
    /// </summary>
    public class UserUploadHandler :  BaseHandler
    {

        public override void DoLoginedHandlerWork(HttpContext context)
        {
            Message jsonMessage;
            jsonMessage = new Message()
            {
                Result = false,
                TxtMessage = "权限验证失败，可能原因:\n1、数据中心通讯失败。\n2、系统管理员未与您分配对应操作权限。"
            };
            //获取操作类型AType:ADD,EDIT,DELETE
            string AjaxType = context.Request.QueryString["AType"] == null ? string.Empty : context.Request.QueryString["AType"].ToString().ToUpper();


            WCFServiceProxy<IUserManage> proxy = null;
            try
            {

                if (AjaxType == "UPLOAD")
                {

                  
                    string strUploadPath = string.Format("{0}\\UserUpload\\{1}\\{2}\\{3}\\",

                    context.Server.MapPath("~"), loginOperator.CompanyID, loginOperator.OperID, System.DateTime.Now.ToString("yyyyMMdd"));
                    string fileName = string.Empty;
                    if (context.Request.Files != null && context.Request.Files.Count == 1)
                    {
                        if (!Directory.Exists(strUploadPath))
                        {
                            Directory.CreateDirectory(strUploadPath);
                        }
                        HttpPostedFile postedFile = context.Request.Files[0];

                        if (postedFile.ContentLength < 2000000)
                        {
                            fileName = strUploadPath + System.Web.HttpUtility.UrlDecode(Path.GetFileName(postedFile.FileName));
                            string extension = Path.GetExtension(fileName).ToLower();
                            if (fileName != "" && (extension == ".xls" || extension == ".xlsx" ))
                            {
                                try
                                {
                                    postedFile.SaveAs(fileName);

                                    DataTable dt= ExcelToDataTable(fileName,"sheet1");

                                    proxy = new WCFServiceProxy<IUserManage>();

                                    Message m;Boolean IsRepeat=false;

                                    //清除临时表
                                    proxy.getChannel.DeleteUserTemp();

                                    foreach (DataRow dr in dt.Rows){


                                        Int64 meterNoInt;
                                        bool isMeterNoInt = Int64.TryParse(dr[2].ToString(), out meterNoInt);
                                        if (!isMeterNoInt) { break; }

                                        decimal meterNum = decimal.TryParse(dr[3].ToString(), out meterNum) ? meterNum : 0;
                                        
                                        IoT_UserTemp gas = new IoT_UserTemp()
                                        {
                                              UserName=dr[0].ToString(),
                                              UserID=dr[1].ToString(),
                                              MeterNo=dr[2].ToString(),
                                              MeterNum = meterNum,
                                              Street = dr[4].ToString(),
                                              Community = dr[5].ToString(),
                                              Door = dr[6].ToString(),
                                              Address=dr[7].ToString(),
                                              CompanyID = loginOperator.CompanyID
                                        };

                                        m = proxy.getChannel.AddTemp(gas);
                                        if (m.Result)
                                        {
                                            IsRepeat = true;
                                        }
                                    }

                                    jsonMessage = new Message() { Result = true, TxtMessage = "导入成功" };

                                    if (IsRepeat) {

                                        jsonMessage = new Message() { Result = true, TxtMessage = "导入成功,已自动剔除表号重复数据" };
                                    }


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
                            else
                            {
                                jsonMessage = new Message() { Result = false, TxtMessage = "文件格式不正确" };
                            }
                        }
                        else
                        {
                            jsonMessage = new Message() { Result = false, TxtMessage = "文件超过2M" };
                        }
                    }
                }

               
            }
            catch (Exception  ex){
                jsonMessage = new Message()
                {
                    Result = false,
                    TxtMessage = ex.Message
                };
            }
            finally
            {
               
            }       
            context.Response.Write(JSon.TToJson<Message>(jsonMessage));
        }






        public  DataTable ExcelToDataTable(string strExcelFileName, string strSheetName)
        {



            //源的定义
            //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + strExcelFileName + ";" + "Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";
            ////此连接只能操作Excel2007之前(.xls)文件

            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + strExcelFileName + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            //此连接可以操作.xls与.xlsx文件 (支持Excel2003 和 Excel2007 的连接字符串)
            //备注： "HDR=yes;"是说Excel文件的第一行是列名而不是数据，"HDR=No;"正好与前面的相反。
            //      "IMEX=1 "如果列中的数据类型不一致，使用"IMEX=1"可必免数据类型冲突。 



            //Sql语句
            //string strExcel = string.Format("select * from [{0}$]", strSheetName); 这是一种方法
            string strExcel = "select * from   [sheet1$]";

            //定义存放的数据表
            DataSet ds = new DataSet();

            //连接数据源
            OleDbConnection conn = new OleDbConnection(strConn);

            conn.Open();

            //适配到数据源
            OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, strConn);
            adapter.Fill(ds, strSheetName);

            conn.Close();

            return ds.Tables[strSheetName];
        }

        
       
        
    }
}





























 