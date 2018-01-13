using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.Business;
using System.Data;
using System.Data.SqlClient;
using CY.IoTM.MongoDataHelper;



namespace CY.IoTM.DataService.Business
{
    public class MeterGasBillService : IMeterGasBill
    {

        /// <summary>
        /// 气量表结算
        /// </summary>
        /// <param name="meterNo"></param>
        /// <param name="month"></param>
        /// <param name="settleDate"></param>
        /// <returns></returns>
        public Message SettleMeterGas(string meterNo, string month) 
        {
            Message m = new Message() { Result = true, TxtMessage = "操作成功。" };

            string sqlText = string.Empty;
            try
            {
                sqlText = "SP_SettleMeterGas";
                SqlParameter[] parms = new SqlParameter[] 
                { 
                    new SqlParameter("MeterNo", meterNo.Trim()),
                    new SqlParameter("UseMonth", month)
                    //new SqlParameter("SettlementDateTime", settleDate)
                };
                SQLHelper.ExecuteNonQuery(SQLHelper.SchuleConnection, CommandType.StoredProcedure, sqlText, parms);
            }
            catch (Exception e)
            {
                m = new Message() { Result = false, TxtMessage = "操作失败，【" + e.Message + "】" };
            }
            return m;

        }


        /// <summary>
        /// 根据价格类型查询气量表
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="priceId"></param>
        /// <returns></returns>
        public List<IoT_Meter> GetMeterByPriceId(string companyId, int priceId) 
        {

            List<IoT_Meter> meterList = new List<IoT_Meter>();
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                Table<IoT_Meter> tbl = dd.GetTable<IoT_Meter>();
                meterList = dd.GetTable<IoT_Meter>().Where(p => p.CompanyID == companyId && p.PriceID == priceId).ToList();
            }
            catch (Exception e)
            {
               
            }
            return meterList;
        }

        /// <summary>
        /// 获取结算时间
        /// </summary>
        /// <param name="meterNo"></param>
        /// <returns></returns>
        public DateTime GetMeterSettleDate(string meterNo)
        {
            DateTime settleDate = new DateTime();
            TaskManageDA MeterDA = new TaskManageDA();
            Meter meter=MeterDA.QueryMeter(meterNo);
            settleDate = Convert.ToDateTime(meter.SettlementDateTime);
            return settleDate;
        
        }



        /// <summary>
        /// 根据月份获取结算账单
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<View_MeterGasBill> GetGasBillByMonth(string month,string companyId) 
        {

            List<View_MeterGasBill> billList = new List<View_MeterGasBill>();
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                Table<View_MeterGasBill> tbl = dd.GetTable<View_MeterGasBill>();
                billList = dd.GetTable<View_MeterGasBill>().Where(p => p.CompanyID == companyId && p.UseMonth == month).ToList();
            }
            catch (Exception e)
            {

            }
            return billList;
        
        }




    }
}
