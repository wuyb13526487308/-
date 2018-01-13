using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CY.IotM.WebHandler;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.Business;

namespace CY.IotM.WebClient
{
    /// <summary>
    /// OlbService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class OlbService : System.Web.Services.WebService
    {
        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public String GetCompanyList()
        {
            List<OlbCompany> listOlbCompany = null;
            WCFServiceProxy<ICommonSearch<CompanyInfo>> proxy = null;
            try
            {
                proxy = new WCFServiceProxy<ICommonSearch<CompanyInfo>>();
              
                SearchCondition sCondition = new SearchCondition()
                {
                    TBName = "S_CompanyInfo",
                    TFieldKey = "CompanyID",
                    TPageSize = 9999,
                    TPageCurrent = 1,
                    TFieldOrder = "CompanyName ASC"
                };
                List<CompanyInfo> list = proxy.getChannel.getListBySearchCondition(ref sCondition);
                listOlbCompany = new List<OlbCompany>();
                foreach (CompanyInfo c in list)
                {
                    listOlbCompany.Add(new OlbCompany() { Id=c.CompanyID, Name=c.CompanyName});
                }
            }
            catch (Exception ex) {  }
            finally {
                if (proxy != null)
                    proxy.CloseChannel();
            }
            return  JsToJson.SerializeToJsonString(listOlbCompany);
        }


        /// <summary>
        /// 根据户号获取用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [WebMethod]
        public String GetGasUserByUserId(string userId,string companyId)
        {

            OlbGasUser userInfo = null;
            WCFServiceProxy<ICommonSearch<View_UserMeter>> proxy = null;
            try
            {

                proxy = new WCFServiceProxy<ICommonSearch<View_UserMeter>>();
            
                SearchCondition sCondition = new SearchCondition()
                {
                    TBName = "View_UserMeter",
                    TFieldKey = "UserID",
                    TPageSize = 1,
                    TPageCurrent = 1,
                    TFieldOrder = "UserID ASC",
                    TWhere = " 1=1 AND CompanyID='" + companyId + "'  AND UserID='" + userId+"' "
                };
                List<View_UserMeter> list = proxy.getChannel.getListBySearchCondition(ref sCondition);

                if (list.Count > 0) {
                    userInfo = new OlbGasUser();
                    userInfo.UserID = list[0].UserID;
                    userInfo.UserName = list[0].UserName;
                    userInfo.Address = list[0].Address;
                    userInfo.CompanyID = list[0].CompanyID;
                    userInfo.Balance = (decimal)list[0].RemainingAmount;
                    userInfo.MeterNo = list[0].MeterNo;
                    userInfo.MeterType =int.Parse(list[0].MeterType);
                }

            }
            catch (Exception ex) { }
            finally
            {
                if (proxy != null)
                    proxy.CloseChannel();
            }
            return JsToJson.SerializeToJsonString(userInfo);
        }




        /// <summary>
        /// 根据户号和月份获取用气账单（查询月最后一条减上个月最后一条）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [WebMethod]
        public String GetGasUserBill(string userId, string companyId, string month)
        {

            OlbGasFeeBill gasFeeBill = null;
            WCFServiceProxy<IChaoBiao> proxy = null;
            try
            {

                proxy = new WCFServiceProxy<IChaoBiao>();
                View_UserMeterHistory thisRecord = proxy.getChannel.GetMonthLastRecord(userId,companyId,month);

                string thisMonth = month + "-01";
                string lastMonth = Convert.ToDateTime(thisMonth).AddMonths(-1).ToString("yyyy-MM");

                View_UserMeterHistory lastRecord = proxy.getChannel.GetMonthLastRecord(userId, companyId, lastMonth);

                if (thisRecord != null && lastRecord != null) {
                    gasFeeBill = new OlbGasFeeBill();
                    gasFeeBill.ChaoBiaoTime = (DateTime)thisRecord.ReadDate;
                    gasFeeBill.UserID = thisRecord.UserID;
                    gasFeeBill.UserName = thisRecord.UserName;
                    gasFeeBill.LastNum = (decimal)lastRecord.TotalAmount;
                    gasFeeBill.ThisNum = (decimal)thisRecord.TotalAmount;
                    gasFeeBill.GasNum = (decimal)thisRecord.TotalAmount - (decimal)lastRecord.TotalAmount;
                    gasFeeBill.GasFee = (decimal)lastRecord.RemainingAmount - (decimal)thisRecord.RemainingAmount;
                }
            }
            catch (Exception ex) { }
            finally
            {
                if (proxy != null)
                    proxy.CloseChannel();
            }
            return JsToJson.SerializeToJsonString(gasFeeBill);

        }




        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [WebMethod]
        public String Charge(string userId, string companyId,decimal money) {

            string result = "";
            WCFServiceProxy<IUserManage> proxyUser = new WCFServiceProxy<IUserManage>();
            WCFServiceProxy<IMeterTopUp> _valveProxy = new WCFServiceProxy<IMeterTopUp>();
            try
            {
                string meterNo = proxyUser.getChannel.GetUserMeterByUserId(userId, companyId);
                result = _valveProxy.getChannel.Topup(meterNo.Trim(), money, TopUpType.接口, "", "",new IoT_MeterTopUp());
            }
            catch (Exception e) { }
            finally {
                proxyUser.CloseChannel();
                _valveProxy.CloseChannel();
            }
            return result;
        
        }





    }
}
