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
using NPOI.HSSF.UserModel;

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
                proxy = new WCFServiceProxy<IUserManage>();
                if (AjaxType == "UPLOAD")
                {
                    if (context.Request.Files != null && context.Request.Files.Count == 1)
                    {
                        HttpPostedFile postedFile = context.Request.Files[0];

                        DataTable dt = null;
                        Message m;
                        List<IoT_UserTemp> list = new List<IoT_UserTemp>();

                        NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook(postedFile.InputStream);

                        int sheetCount = book.NumberOfSheets;
                        for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
                        {
                            NPOI.SS.UserModel.ISheet sheet = book.GetSheetAt(sheetIndex);
                            if (sheet == null) continue;

                            NPOI.SS.UserModel.IRow row = sheet.GetRow(0);
                            if (row == null) continue;
                            int firstCellNum = row.FirstCellNum;
                            int lastCellNum = row.LastCellNum;
                            if (firstCellNum == lastCellNum) continue;

                            dt = new DataTable(sheet.SheetName);
                            for (int i = firstCellNum; i < lastCellNum; i++)
                            {
                                dt.Columns.Add(row.GetCell(i).StringCellValue, typeof(string));
                            }

                            for (int i = 1; i <= sheet.LastRowNum; i++)
                            {
                                DataRow newRow = dt.NewRow();
                                for (int j = firstCellNum; j < lastCellNum; j++)
                                {
                                    NPOI.SS.UserModel.ICell cell = sheet.GetRow(i).GetCell(j);
                                    if(cell == null)
                                    {
                                        newRow[j] = "";
                                    }
                                    else
                                    {
                                        switch (cell.CellType)
                                        {
                                            case NPOI.SS.UserModel.CellType.Blank:
                                            case NPOI.SS.UserModel.CellType.Unknown:
                                                newRow[j] = "";
                                                break;
                                            case NPOI.SS.UserModel.CellType.Numeric:
                                                if (HSSFDateUtil.IsCellDateFormatted(cell))
                                                {
                                                    newRow[j] = cell.DateCellValue.ToString("yyyy-MM-dd");
                                                }
                                                else
                                                {
                                                    newRow[j] = cell.NumericCellValue;
                                                }
                                                break;
                                            case NPOI.SS.UserModel.CellType.String:
                                                newRow[j] = cell.StringCellValue;
                                                break;
                                            case NPOI.SS.UserModel.CellType.Formula:
                                                newRow[j] = cell.CellFormula;
                                                break;
                                            case NPOI.SS.UserModel.CellType.Boolean:
                                                newRow[j] = cell.BooleanCellValue;
                                                break;
                                            case NPOI.SS.UserModel.CellType.Error:
                                                newRow[j] = "";
                                                break;
                                            default:
                                                newRow[j] = "";
                                                break;
                                        }
                                    }
                                }
                                if (lastCellNum < 8) continue;
                                decimal meterNum = decimal.TryParse(newRow[3].ToString(), out meterNum) ? meterNum : 0;

                                IoT_UserTemp gas = new IoT_UserTemp()
                                {
                                    UserName = newRow[0].ToString(),
                                    UserID = newRow[1].ToString(),
                                    MeterNo = newRow[2].ToString(),
                                    MeterNum = meterNum,
                                    Street = newRow[4].ToString(),
                                    Community = newRow[5].ToString(),
                                    Door = newRow[6].ToString(),
                                    Address = newRow[7].ToString(),
                                    CompanyID = loginOperator.CompanyID
                                };

                                /*
                                                                     Direction = newRow[8].ToString(),
                                    InstallType = newRow[9].ToString(),
                                    Phone = newRow[10].ToString(),
                                    UserType = newRow[11].ToString() =="0"?"0":"1",
                                    InstallDate = newRow[12].ToString(),                                 */

                                if(lastCellNum >= 9)
                                {
                                    gas.Direction = newRow[8].ToString();
                                }
                                if (lastCellNum >= 10)
                                {
                                    gas.InstallType = newRow[9].ToString();
                                }
                                if (lastCellNum >= 11)
                                {
                                    gas.Phone = newRow[10].ToString();
                                }
                                if (lastCellNum >= 12)
                                {
                                    gas.UserType = newRow[11].ToString();
                                }

                                if (lastCellNum >= 13)
                                {
                                    try
                                    {
                                        gas.InstallDate = Convert.ToDateTime(newRow[12].ToString()).ToString("yyyy-MM-dd");
                                    }
                                    catch { }
                                }

                                m = proxy.getChannel.AddTemp(gas);
                                if (!m.Result)
                                {
                                    list.Add(gas);
                                }
                            }                            
                        }
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = Newtonsoft.Json.JsonConvert.SerializeObject(list)
                        };
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
                proxy.CloseChannel();
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





























 