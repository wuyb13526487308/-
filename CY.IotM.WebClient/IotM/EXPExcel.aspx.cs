using Aspose.Cells;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CY.IotM.WebClient.IotM
{
    public partial class EXPExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WCFServiceProxy<IChaoBiao> proxy = new WCFServiceProxy<IChaoBiao>();
            try
            {

                string TimeKind = Request.QueryString["Time"];
                string UserKind = Request.QueryString["User"];
                string CompanyID = Request.QueryString["CompanyID"];
                string Where = "1=1 and CompanyID='" + CompanyID + "'";
                if (UserKind != "*" && UserKind != "")
                {
                    Where += "AND charindex(','+CAST(Community AS NVARCHAR(10))+',',','+'" + UserKind + "'+',')>=1";
                }
                if (TimeKind != "*")
                {
                    Where += "and CONVERT(varchar(100), ReadDate, 23)='" + TimeKind + "'";
                }
                List<View_UserMeterHistory> listAll = proxy.getChannel.GetModelList(Where);
                List<View_UserMeterHistory> listEXP = new List<View_UserMeterHistory>();
                var newModel = (from n in listAll
                                orderby n.Community, n.ReadDate descending
                                select new { n.UserID }).Distinct().ToList();
                if (TimeKind == "*")
                {//如果汇出说有数据，执行以下（筛选出最新一笔的数据）
                    foreach (var item in newModel)
                    {
                        View_UserMeterHistory Model = new View_UserMeterHistory();
                        Model = (from n in listAll
                                 where n.UserID == item.UserID
                                 orderby n.ReadDate descending
                                 select n).FirstOrDefault();
                        listEXP.Add(Model);
                    }
                }
                else
                {//如果汇出指定日期的数据，执行以下（筛选出指定日期下的第一笔数据）
                    //foreach (var item in newModel)
                    //{
                    //View_UserMeterDayFirstHistory Models = new View_UserMeterDayFirstHistory();
                    List<View_UserMeterDayFirstHistory> list = proxy.getChannel.GetModelLists(Where);
                    //Model = (from n in list
                    //         where n.UserID == item.UserID
                    //         orderby n.ReadDate ascending
                    //         select n).FirstOrDefault();
                    foreach (View_UserMeterDayFirstHistory Model in list)
                    {
                        View_UserMeterHistory ModelAdd = new View_UserMeterHistory()
                        {
                            CompanyID = Model.CompanyID,
                            UserID = Model.UserID,
                            UserName = Model.UserName,
                            State = Model.State,
                            Address = Model.Address,
                            Street = Model.Street,
                            Community = Model.Community,
                            Door = Model.Door,
                            MeterNo = Model.MeterNo,
                            MeterType = Model.MeterType,
                            ValveState = Model.ValveState,
                            LastTotal = Model.LastTotal,
                            TotalAmount = Model.TotalAmount,
                            RemainingAmount = Model.RemainingAmount,
                            ReadDate = Model.ReadDate,
                            InstallDate = Model.InstallDate,
                            Gas = Model.Gas,
                        };
                        listEXP.Add(ModelAdd);
                    }
                  
                    //}
                }
                listAll = listEXP;
                Workbook workbook = new Workbook();

                workbook.Worksheets.Add("未下载数据");

                Worksheet sheet = (Worksheet)workbook.Worksheets[0];
                //Worksheet sheet1 = (Worksheet)workbook.Worksheets[1];


                sheet.Cells["A1"].PutValue("户号");
                sheet.Cells["B1"].PutValue("户名");
                sheet.Cells["C1"].PutValue("地址");
                sheet.Cells["D1"].PutValue("表号");
                sheet.Cells["E1"].PutValue("阀状态");
                sheet.Cells["F1"].PutValue("总用量");
                sheet.Cells["G1"].PutValue("余额(元)");
                sheet.Cells["H1"].PutValue("抄表时间");


                int RowNo = 2;

                for (int i = 0; i < listAll.Count; i++)
                {
                    sheet.Cells["A" + RowNo].PutValue(listAll[i].UserID);

                    sheet.Cells["B" + RowNo].PutValue(listAll[i].UserName);

                    sheet.Cells["C" + RowNo].PutValue(listAll[i].Address);

                    sheet.Cells["D" + RowNo].PutValue(listAll[i].MeterNo);
                    if (listAll[i].ValveState == '0')
                    {
                        sheet.Cells["E" + RowNo].PutValue("阀开");
                    }
                    else
                    {
                        sheet.Cells["E" + RowNo].PutValue("阀关");
                    }

                    sheet.Cells["F" + RowNo].PutValue(listAll[i].TotalAmount.ToString());
                    sheet.Cells["G" + RowNo].PutValue(listAll[i].RemainingAmount.ToString());
                    sheet.Cells["H" + RowNo].PutValue(listAll[i].ReadDate.ToString());

                    RowNo++;
                }


                String filename = string.Format("{0}{1}.xls", "抄表记录", Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"));

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