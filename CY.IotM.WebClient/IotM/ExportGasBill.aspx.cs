using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IotM.WebHandler;
using CY.IoTM.Common.Business;
using Aspose.Cells;

namespace CY.IotM.WebClient.IotM
{
    public partial class ExportGasBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WCFServiceProxy<IMeterGasBill> proxy = null;
         
            try
            {

                CompanyOperator dCLoginer = null;
                if (this.Context.Session["LoginCompanyOperator"] != null)
                {
                    dCLoginer = (CompanyOperator)this.Context.Session["LoginCompanyOperator"];
                }
                else 
                {
                    return;
                }
               

                string month = Request.QueryString["Month"];
                string companyId = dCLoginer.CompanyID;



                Workbook workbook = new Workbook();

                workbook.Worksheets.Add("气量表结算数据");

                Worksheet sheet = (Worksheet)workbook.Worksheets[0];
              
                sheet.Cells["A1"].PutValue("户号");
                sheet.Cells["B1"].PutValue("户名");
            
                sheet.Cells["C1"].PutValue("地址");
                sheet.Cells["D1"].PutValue("表号");

                sheet.Cells["E1"].PutValue("结算月份");
                sheet.Cells["F1"].PutValue("上次表底");
                sheet.Cells["G1"].PutValue("本次表底");
                sheet.Cells["H1"].PutValue("用气量");
                sheet.Cells["I1"].PutValue("气费");
                sheet.Cells["J1"].PutValue("抄表时间");


                proxy = new WCFServiceProxy<IMeterGasBill>();
                List<View_MeterGasBill> listAll = proxy.getChannel.GetGasBillByMonth(month, companyId);



                int RowNo = 2;

                for (int i = 0; i < listAll.Count; i++)
                {
                    sheet.Cells["A" + RowNo].PutValue(listAll[i].UserID);

                    sheet.Cells["B" + RowNo].PutValue(listAll[i].UserName);

                    sheet.Cells["C" + RowNo].PutValue(listAll[i].Address);

                    sheet.Cells["D" + RowNo].PutValue(listAll[i].MeterNo);

                    sheet.Cells["E" + RowNo].PutValue(listAll[i].UseMonth);

                    sheet.Cells["F" + RowNo].PutValue(listAll[i].LastSum.ToString());
                    sheet.Cells["G" + RowNo].PutValue(listAll[i].ThisSum.ToString());
                    sheet.Cells["H" + RowNo].PutValue(listAll[i].UseGasSum.ToString());

                    sheet.Cells["I" + RowNo].PutValue(listAll[i].GasFee.ToString());
                    sheet.Cells["J" + RowNo].PutValue(listAll[i].ThisReadDate.ToString());

                    RowNo++;
                }


                String filename = string.Format("{0}{1}.xls", "气量表结算数据", month);

                Response.ContentType = "application/ms-excel;charset=utf-8";

                Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.GetEncoding("utf-8")));

                System.IO.MemoryStream memStream = workbook.SaveToStream();

                Response.BinaryWrite(memStream.ToArray());

                Response.End();
            }
            catch (Exception)
            {

            }
            finally
            {
                if (proxy != null)
                    proxy.CloseChannel();
            }
        }
    }
}