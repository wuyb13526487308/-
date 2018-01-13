
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CY.IoTM.Common.ADSystem;
using CY.IoTM.MongoDataHelper;


namespace CY.IoTM.ADService
{
    /// <summary>
    /// 广告发布管理
    /// </summary>
    public class ADPublishManager : IADPublishManager
    {
        /// <summary>
        /// 发布广告
        /// </summary>
        /// <param name="ap_id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string ADPublish(long ap_id, PublishType type= PublishType.NewPublish)
        {
            M_ADService adservice = new M_ADService();
            string result = "";
            try
            {
                ADPublish adPublish = this.ReadADPublish(ap_id);
                result = adservice.PublishAD(adPublish, this.ReadPublishUser(ap_id), this.ReadADItems((long)adPublish.AC_ID), type);
            }
            catch(Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 撤销广告发布
        /// </summary>
        /// <param name="ap_id"></param>
        /// <returns></returns>
        public string UnADPublish(long ap_id)
        {

            return "";
        }

        /// <summary>
        /// 读取广告主题列表
        /// </summary>
        /// <param name="ac_id">广告主题ID</param>
        /// <returns></returns>
        public List<ADItem> ReadADItems(long ac_id)
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);

            try
            {
                Table<ADItem> tbl = dd.GetTable<ADItem>();
                return tbl.Where(p => p.AC_ID == ac_id).OrderBy(p=>p.OrderID).ToList();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 读取指定发布信息
        /// </summary>
        /// <param name="ap_id"></param>
        /// <returns></returns>
        public ADPublish ReadADPublish(long ap_id)
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);

            try
            {
                Table<ADPublish> tbl = dd.GetTable<ADPublish>();
                ADPublish adPublish = (from p in tbl where p.AP_ID == ap_id select p).Single();
                return adPublish;
            }
            catch{
                return null;
            }
        }

        /// <summary>
        /// 读取指定发布的发布用户列表
        /// </summary>
        /// <param name="ap_id"></param>
        /// <returns></returns>
        public List<ADPublisMeter> ReadPublishUser(long ap_id)
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);

            try
            {
                string sql = string.Format(@"SELECT ADPublishUser.AP_ID,ADPublishUser.UserID, IoT_Meter.MeterNo,ADPublishUser.CompanyID FROM ADPublishUser ,IoT_Meter 
WHERE ADPublishUser.UserID = IoT_Meter.UserID and ADPublishUser.CompanyID = IoT_Meter.CompanyID and ADPublishUser.AP_ID ={0}", ap_id);
                object[] param = new object[0];
                return dd.ExecuteQuery<ADPublisMeter>(sql, param).ToList();
            }
            catch {
                return null;
            }            
        }
    }
}
