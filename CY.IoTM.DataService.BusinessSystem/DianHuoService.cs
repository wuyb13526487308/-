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
    /// 点火服务执行类
    /// </summary>
    public class DianHuoService : IDianHuo
    {

        /// <summary>
        /// 执行点火操作，更新表点火状态，并更新表信息为点火指定参数同时提交点火任务给通信层
        /// </summary>
        /// <param name="meters"></param>
        /// <returns></returns>
        public string Do(List<IoT_Meter> meters)
        {
            MeterManageService _mms = new MeterManageService();
            DianhuoDA dhDA = new DianhuoDA();
            TaskManageDA tmd = new TaskManageDA();

            foreach (IoT_Meter m in meters)
            {
                if (m.MeterState.ToString() == "5")
                {
                    IoT_Meter _oldMeter = MeterManageService.QueryMeter(m.MeterNo);
                    if (_oldMeter == null)
                        return string.Format("点火失败，表{0}信息不存在！", m.MeterNo);

                    if (_oldMeter.MeterState == '0')
                    {
                        return string.Format("表{0}已点火完成，不能重复操作。", m.MeterNo);
                    }
                    Meter _meter = tmd.QueryMeter(m.MeterNo.Trim());
                    tmd.InsertMeter(m);
                    IoT_SetAlarm alarmPar = new IoT_SetAlarm();
                    alarmPar.SwitchTag = "000000000       ";
                    alarmPar.Par1 = 30;
                    alarmPar.Par2 = 10;
                    alarmPar.Par3 = 30;
                    alarmPar.Par4 = "";
                    alarmPar.Par5 = 10;
                    alarmPar.Par6 = 30;
                    alarmPar.Par7 = 30;
                    alarmPar.Par8 = 30;
                    alarmPar.Par9 = "00";
                    new SetAlarmService().UpdateMeterAlarmPar(m.MeterNo.Trim(), alarmPar);

                    //点火状态,需要修改表的通讯密钥
                    if (_mms.Edit(m).Result == false)
                        return string.Format("点火失败，原因：登记表{0}点火信息失败。", m.MeterNo);
                    string result = dhDA.SubmitDianHuoASK(_oldMeter);
                    new UserManageService().UpadteUserStatus("2", m.UserID);//用户表状态置为 撤销点火
                    if (result != "")
                    {
                        _mms.Edit(_oldMeter);
                    }
                }
            }
            return "";
        }



        public string Undo(IoT_Meter meter)
        {
            DianhuoDA dhDA = new DianhuoDA();

            string result = dhDA.UnDoDianHuo(meter);
            if (result != "")
                return "撤销失败，原因：" + result;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_Meter dbinfo = dd.GetTable<IoT_Meter>().Where(p =>
                 p.MeterNo == meter.MeterNo).SingleOrDefault();

                if (dbinfo != null)
                {
                    dbinfo.MeterState = '4';//撤销点火状态，退回到已安装状态
                    // 更新操作
                    dd.SubmitChanges();
                    new UserManageService().UpadteUserStatus("1", meter.UserID);//用户表状态退回到1 等待点火
                }

                return "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
#if DEBUG
                Debug.Assert(false, e.Message);
#endif
                return e.Message;
            }
        }

        public IoT_Meter QueryMeter(string meterNo)
        {
            return MeterManageService.QueryMeter(meterNo);
        }


        public Message DianHuo(List<String> meterNoList, string meterType, Int64 priceId, string companyId, DateTime enableDate, List<String> lstUserID, string EnableMeterOper)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                List<IoT_Meter> dianHuoMeter = new List<IoT_Meter>();

                //获取价格信息
                IoT_PricePar priceInfo = dd.GetTable<IoT_PricePar>().Where(p =>
                   p.CompanyID == companyId && p.ID == priceId).SingleOrDefault();
                //if (priceInfo==null)
                //{
                //     return new Message()
                //    {
                //        Result = false,
                //        TxtMessage = "该表具选择的价格不存在，请加载页面重新选择！"
                //    };
                //}
                foreach (string UserID in lstUserID)
                {
                    IoT_User dbinfo = dd.GetTable<IoT_User>().Where(p => p.UserID == UserID).SingleOrDefault();
                    if (dbinfo != null && dbinfo.State == '2')
                    {
                        return new Message()
                    {
                        Result = false,
                        TxtMessage = "用户" +dbinfo.UserName + "已发送点火申请，请勿重复发送！"
                    };
                    }
                }
                foreach (string meterNo in meterNoList)
                {

                    IoT_Meter dbinfo = dd.GetTable<IoT_Meter>().Where(p => p.MeterNo == meterNo).SingleOrDefault();
                    if (priceInfo != null)
                    {
                        dbinfo.Price1 = priceInfo.Price1 == null ? 0 : priceInfo.Price1;
                        dbinfo.Gas1 = priceInfo.Gas1 == null ? 0 : priceInfo.Gas1;
                        dbinfo.Price2 = priceInfo.Price2 == null ? 0 : priceInfo.Price2;
                        dbinfo.Gas2 = priceInfo.Gas2 == null ? 0 : priceInfo.Gas2;
                        dbinfo.Price3 = priceInfo.Price3 == null ? 0 : priceInfo.Price3;
                        dbinfo.Gas3 = priceInfo.Gas3 == null ? 0 : priceInfo.Gas3;
                        dbinfo.Price4 = priceInfo.Price4 == null ? 0 : priceInfo.Price4;
                        dbinfo.Gas4 = priceInfo.Gas4 == null ? 0 : priceInfo.Gas4;
                        dbinfo.Price5 = priceInfo.Price5 == null ? 0 : priceInfo.Price5;
                        dbinfo.IsUsed = priceInfo.IsUsed == null ? false : priceInfo.IsUsed;
                        dbinfo.Ladder = priceInfo.Ladder == null ? 3 : priceInfo.Ladder;
                        dbinfo.SettlementDay = priceInfo.SettlementDay;
                        dbinfo.SettlementMonth = priceInfo.SettlementMonth;
                        dbinfo.SettlementType = priceInfo.SettlementType;
                    }
                    dbinfo.EnableMeterOper = EnableMeterOper;

                    dbinfo.PriceID = (int)priceId;
                    dbinfo.MeterType = meterType;
                    dbinfo.EnableMeterDate = enableDate;
                    dbinfo.MeterState = '5';
                    dianHuoMeter.Add(dbinfo);

                }

                // 更新操作
                dd.SubmitChanges();

                string result = Do(dianHuoMeter);
                if (result == "")
                {
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = "点火成功"
                    };
                }
                else
                {
                    m = new Message()
                    {
                        Result = false,
                        TxtMessage = result
                    };

                }
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "" + e.Message
                };
            }
            return m;
        }

    }
}
