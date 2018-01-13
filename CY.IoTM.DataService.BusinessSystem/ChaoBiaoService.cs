using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IoTM.Common.Business;
using CY.IoTM.MongoDataHelper;
using System.Data;
using System.Data.SqlClient;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
namespace CY.IoTM.DataService.Business
{
    /// <summary>
    /// 抄表历史数据
    /// </summary>
    public class ChaoBiaoService : IChaoBiao
    {
        /// <summary>
        /// 每日抄表数据
        /// </summary>

        public List<View_UserMeterDayFirstHistory> GetModelLists(string where)
        {
            //CommandType a=new CommandType() ;
            string sql = "select *from View_UserMeterDayFirstHistory where ";
            sql = sql + where;
            //string strConnect = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.SchuleConnection, CommandType.Text, sql);
            List<View_UserMeterDayFirstHistory> listOTB_SYS_ModuleList = new List<View_UserMeterDayFirstHistory>();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    View_UserMeterDayFirstHistory model = new View_UserMeterDayFirstHistory();
                    if (ds.Tables[0].Rows[i]["CompanyID"] != null && ds.Tables[0].Rows[i]["CompanyID"].ToString() != "")
                    {
                        model.CompanyID = ds.Tables[0].Rows[i]["CompanyID"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["UserID"] != null && ds.Tables[0].Rows[i]["UserID"].ToString() != "")
                    {
                        model.UserID = ds.Tables[0].Rows[i]["UserID"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["UserName"] != null && ds.Tables[0].Rows[i]["UserName"].ToString() != "")
                    {
                        model.UserName = ds.Tables[0].Rows[i]["UserName"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["State"] != null && ds.Tables[0].Rows[i]["State"].ToString() != "")
                    {
                        model.State = Convert.ToChar(ds.Tables[0].Rows[i]["State"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["Address"] != null && ds.Tables[0].Rows[i]["Address"].ToString() != "")
                    {
                        model.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["Street"] != null && ds.Tables[0].Rows[i]["Street"].ToString() != "")
                    {
                        model.Street = ds.Tables[0].Rows[i]["Street"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["Community"] != null && ds.Tables[0].Rows[i]["Community"].ToString() != "")
                    {
                        model.Community = ds.Tables[0].Rows[i]["Community"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["Door"] != null && ds.Tables[0].Rows[i]["Door"].ToString() != "")
                    {
                        model.Door = ds.Tables[0].Rows[i]["Door"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["MeterNo"] != null && ds.Tables[0].Rows[i]["MeterNo"].ToString() != "")
                    {
                        model.MeterNo = ds.Tables[0].Rows[i]["MeterNo"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["MeterType"] != null && ds.Tables[0].Rows[i]["MeterType"].ToString() != "")
                    {
                        model.MeterType = ds.Tables[0].Rows[i]["MeterType"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["ValveState"] != null && ds.Tables[0].Rows[i]["ValveState"].ToString() != "")
                    {
                        model.ValveState = Convert.ToChar(ds.Tables[0].Rows[i]["ValveState"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["LastTotal"] != null && ds.Tables[0].Rows[i]["LastTotal"].ToString() != "")
                    {
                        model.LastTotal = decimal.Parse(ds.Tables[0].Rows[i]["LastTotal"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["TotalAmount"] != null && ds.Tables[0].Rows[i]["TotalAmount"].ToString() != "")
                    {
                        model.TotalAmount = decimal.Parse(ds.Tables[0].Rows[i]["TotalAmount"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["RemainingAmount"] != null && ds.Tables[0].Rows[i]["RemainingAmount"].ToString() != "")
                    {
                        model.RemainingAmount = decimal.Parse(ds.Tables[0].Rows[i]["RemainingAmount"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["InstallDate"] != null && ds.Tables[0].Rows[i]["InstallDate"].ToString() != "")
                    {
                        model.InstallDate = DateTime.Parse(ds.Tables[0].Rows[i]["InstallDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["ReadDate"] != null && ds.Tables[0].Rows[i]["ReadDate"].ToString() != "")
                    {
                        model.ReadDate = DateTime.Parse(ds.Tables[0].Rows[i]["ReadDate"].ToString());
                    }
                    listOTB_SYS_ModuleList.Add(model);
                }
                return listOTB_SYS_ModuleList;
            }
            else
            {
                return null;
            }
        }


        public List<View_UserMeterHistory> GetModelList(string where)
        {
            //CommandType a=new CommandType() ;
            string sql = "select *from View_UserMeterHistory where ";
            sql = sql + where;
            //string strConnect = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.SchuleConnection, CommandType.Text, sql);
            List<View_UserMeterHistory> listOTB_SYS_ModuleList = new List<View_UserMeterHistory>();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    View_UserMeterHistory model = new View_UserMeterHistory();
                    if (ds.Tables[0].Rows[i]["CompanyID"] != null && ds.Tables[0].Rows[i]["CompanyID"].ToString() != "")
                    {
                        model.CompanyID = ds.Tables[0].Rows[i]["CompanyID"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["UserID"] != null && ds.Tables[0].Rows[i]["UserID"].ToString() != "")
                    {
                        model.UserID = ds.Tables[0].Rows[i]["UserID"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["UserName"] != null && ds.Tables[0].Rows[i]["UserName"].ToString() != "")
                    {
                        model.UserName = ds.Tables[0].Rows[i]["UserName"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["State"] != null && ds.Tables[0].Rows[i]["State"].ToString() != "")
                    {
                        model.State = Convert.ToChar(ds.Tables[0].Rows[i]["State"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["Address"] != null && ds.Tables[0].Rows[i]["Address"].ToString() != "")
                    {
                        model.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["Street"] != null && ds.Tables[0].Rows[i]["Street"].ToString() != "")
                    {
                        model.Street = ds.Tables[0].Rows[i]["Street"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["Community"] != null && ds.Tables[0].Rows[i]["Community"].ToString() != "")
                    {
                        model.Community = ds.Tables[0].Rows[i]["Community"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["Door"] != null && ds.Tables[0].Rows[i]["Door"].ToString() != "")
                    {
                        model.Door = ds.Tables[0].Rows[i]["Door"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["MeterNo"] != null && ds.Tables[0].Rows[i]["MeterNo"].ToString() != "")
                    {
                        model.MeterNo = ds.Tables[0].Rows[i]["MeterNo"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["MeterType"] != null && ds.Tables[0].Rows[i]["MeterType"].ToString() != "")
                    {
                        model.MeterType = ds.Tables[0].Rows[i]["MeterType"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["ValveState"] != null && ds.Tables[0].Rows[i]["ValveState"].ToString() != "")
                    {
                        model.ValveState = Convert.ToChar(ds.Tables[0].Rows[i]["ValveState"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["LastTotal"] != null && ds.Tables[0].Rows[i]["LastTotal"].ToString() != "")
                    {
                        model.LastTotal = decimal.Parse(ds.Tables[0].Rows[i]["LastTotal"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["TotalAmount"] != null && ds.Tables[0].Rows[i]["TotalAmount"].ToString() != "")
                    {
                        model.TotalAmount = decimal.Parse(ds.Tables[0].Rows[i]["TotalAmount"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["RemainingAmount"] != null && ds.Tables[0].Rows[i]["RemainingAmount"].ToString() != "")
                    {
                        model.RemainingAmount = decimal.Parse(ds.Tables[0].Rows[i]["RemainingAmount"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["InstallDate"] != null && ds.Tables[0].Rows[i]["InstallDate"].ToString() != "")
                    {
                        model.InstallDate = DateTime.Parse(ds.Tables[0].Rows[i]["InstallDate"].ToString());
                    }
                    if (ds.Tables[0].Rows[i]["ReadDate"] != null && ds.Tables[0].Rows[i]["ReadDate"].ToString() != "")
                    {
                        model.ReadDate = DateTime.Parse(ds.Tables[0].Rows[i]["ReadDate"].ToString());
                    }
                    listOTB_SYS_ModuleList.Add(model);
                }
                return listOTB_SYS_ModuleList;
            }
            else
            {
                return null;
            }
        }



        public View_UserMeterHistory GetMonthLastRecord(string userId, string companyId,string month)
        {
            View_UserMeterHistory record = null;

            string thisMonth = month + "-01";
            string nextMonth = Convert.ToDateTime(thisMonth).AddMonths(1).ToString("yyyy-MM-dd");

            string sql = "select top 1 * from View_UserMeterHistory  where UserID='" + userId + "' and CompanyID='" + companyId + "' and ReadDate >'" 
                + thisMonth + "' and ReadDate<'" + nextMonth + "' order by ReadDate desc ";
           
            
            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.SchuleConnection, CommandType.Text, sql);
            List<View_UserMeterHistory> list = new List<View_UserMeterHistory>();

            if (ds.Tables[0].Rows.Count > 0)
            {

                record = new View_UserMeterHistory();
                record.CompanyID = ds.Tables[0].Rows[0]["CompanyID"].ToString();
                record.UserID = ds.Tables[0].Rows[0]["UserID"].ToString();
                record.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                record.State = Convert.ToChar(ds.Tables[0].Rows[0]["State"].ToString());
                record.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                record.Street = ds.Tables[0].Rows[0]["Street"].ToString();
                record.Community = ds.Tables[0].Rows[0]["Community"].ToString();
                record.Door = ds.Tables[0].Rows[0]["Door"].ToString();
                record.MeterNo = ds.Tables[0].Rows[0]["MeterNo"].ToString();
                record.MeterType = ds.Tables[0].Rows[0]["MeterType"].ToString();
                record.ValveState = Convert.ToChar(ds.Tables[0].Rows[0]["ValveState"].ToString());
                record.LastTotal = decimal.Parse(ds.Tables[0].Rows[0]["LastTotal"].ToString());
                record.TotalAmount = decimal.Parse(ds.Tables[0].Rows[0]["TotalAmount"].ToString());
                record.RemainingAmount = decimal.Parse(ds.Tables[0].Rows[0]["RemainingAmount"].ToString());
                record.InstallDate = DateTime.Parse(ds.Tables[0].Rows[0]["InstallDate"].ToString());
                record.ReadDate = DateTime.Parse(ds.Tables[0].Rows[0]["ReadDate"].ToString());
               
            }
            return record;
        }


    }

    
}
