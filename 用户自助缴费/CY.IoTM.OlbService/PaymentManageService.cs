using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IoTM.OlbCommon;
using System.Data;
using System.Data.Linq;
using CY.IoTM.OlbCommon.Tool;


namespace CY.IoTM.OlbService
{
    /// <summary>
    /// 充值缴费管理
    /// </summary>
    public class PaymentManageService : IPaymentManage
    {


        private static PaymentManageService instance = null;
        public static PaymentManageService GetInstance()
        {
            if (instance == null)
            {
                instance = new PaymentManageService();
            }
            return instance;
        }

        string configName = "";

        private PaymentManageService()
        {
            configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"]; 
        }



        public Message AddPaymentRecord(Olb_PaymentRecord info) {


            Message m;
            try
            {
                DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
                Table<Olb_PaymentRecord> tbl = dd.GetTable<Olb_PaymentRecord>();

                string guid = Guid.NewGuid().ToString();
                info.ID = guid;

                tbl.InsertOnSubmit(info);
                dd.SubmitChanges();

                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<Olb_PaymentRecord>(info)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "添加缴费记录失败！" + e.Message
                };
            }
            return m;

        }

        public List<Olb_PaymentRecord> GetPaymentRecord(DateTime startTime, DateTime endTime, string account)
        {

            List<Olb_PaymentRecord> list;
            try
            {

                DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
                Table<Olb_PaymentRecord> tbl = dd.GetTable<Olb_PaymentRecord>();
                var s = tbl.Where(p => p.Account == account && p.PayTime >= startTime && p.PayTime <= endTime).ToList();
                list = s;
              
            }
            catch (Exception e)
            {
                list = null;
            }            
            return list;
        }


	}

}