using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Linq;
using CY.IoTM.OlbCommon;
using CY.IoTM.OlbCommon.Tool;

namespace CY.IoTM.OlbService
{
    /// <summary>
    /// 订单管理
    /// </summary>
  public class ChargeOrderService:IChargeOrderManage  
  {


        private static ChargeOrderService instance = null;
        public static ChargeOrderService GetInstance()
        {
            if (instance == null)
            {
                instance = new ChargeOrderService();
            }
            return instance;
        }

        string configName = "";

        private ChargeOrderService()
        {
            configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"]; 
        }

      /// <summary>
      /// 添加订单
      /// </summary>
      /// <param name="info"></param>
      /// <returns></returns>
        public Message Add(Olb_ChargeOrder info) 
      {

          Message m;
          try
          {
              //需要为每个方法创建一个DataContext实例 原因在于DataContext的缓存机制
              DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
              Table<Olb_ChargeOrder> tbl = dd.GetTable<Olb_ChargeOrder>();

              //string guid = Guid.NewGuid().ToString();
              //info.ID = guid;

              tbl.InsertOnSubmit(info);
              dd.SubmitChanges();

              m = new Message()
              {
                  Result = true,
                  TxtMessage = JSon.TToJson<Olb_ChargeOrder>(info)
              };

          }
          catch (Exception e)
          {
              m = new Message()
              {
                  Result = false,
                  TxtMessage = "添加订单失败！" + e.Message
              };
          }
          return m;
      }
      /// <summary>
      /// 编辑订单
      /// </summary>
      /// <param name="info"></param>
      /// <returns></returns>
        public Message Edit(Olb_ChargeOrder info)
      {
          Message m;
          try
          {
              DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
              Olb_ChargeOrder dbinfo = dd.GetTable<Olb_ChargeOrder>().Where(p => p.ID == info.ID).SingleOrDefault();

            
              ConvertHelper.Copy<Olb_ChargeOrder>(dbinfo, info);

              dd.SubmitChanges();
              m = new Message()
              {
                  Result = true,
                  TxtMessage = JSon.TToJson<Olb_ChargeOrder>(dbinfo)
              };
          }
          catch (Exception e)
          {
              m = new Message()
              {
                  Result = false,
                  TxtMessage = "编辑订单失败！" + e.Message
              };
          }
          return m;
      }





        public Olb_ChargeOrder GetChargeOrderById(string id) {

            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            Olb_ChargeOrder dbinfo = dd.GetTable<Olb_ChargeOrder>().Where(p => p.ID == id).SingleOrDefault();

            return dbinfo;
        }

    
	}
}





