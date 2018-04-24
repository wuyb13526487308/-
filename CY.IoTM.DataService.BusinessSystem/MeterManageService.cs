using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.Business;
using CY.IoTM.MongoDataHelper;
using CY.IoTM.Common;

namespace CY.IoTM.DataService.Business
{
    public class MeterManageService:IMeterManage
    {
        public Message Add(IoT_Meter info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                Table<IoT_Meter> tbl = dd.GetTable<IoT_Meter>();
                var count = dd.GetTable<IoT_Meter>().Where(p => p.CompanyID == info.CompanyID && p.MeterNo == info.MeterNo).Count();
                if (count > 0)
                {
                    m = new Message()
                    {
                        Result = false,
                        TxtMessage = "表号不能重复!"
                    };
                    return m;
                }
                info.Direction = info.Direction == null ? "左" : info.Direction;
                info.InstallDate = info.InstallDate == null ? DateTime.Now : info.InstallDate;
                info.MKeyVer = info.MKeyVer == null ? 0 : info.MKeyVer;
                info.PriceCheck = info.PriceCheck == null ? '0' : info.PriceCheck;
                info.SettlementDay = info.SettlementDay == null ? 28 : info.SettlementDay;
                info.SettlementMonth = info.SettlementMonth == null ? 12 : info.SettlementMonth;
                info.SettlementType = info.SettlementType == null ? "0" : info.SettlementType;
                info.RemainingAmount = info.RemainingAmount == null ? 0 : info.RemainingAmount;
                info.TotalTopUp = info.TotalTopUp == null ? 0 : info.TotalTopUp;
                info.TotalAmount = info.TotalAmount == null ? 0 : info.TotalAmount;
                info.ValveState = info.ValveState == null ? '0' : info.ValveState;
                info.UploadCycle = info.UploadCycle == null ? getUploadCycle(info.CompanyID) : info.UploadCycle;
                info.MKey = info.MKey == null ? getKey(info.CompanyID) : info.MKey;

                info.IsUsed = info.IsUsed == null ? false : info.IsUsed;
                info.Price1 = info.Price1 == null ? 0 : info.Price1;
                info.Price2 = info.Price2 == null ? 0 : info.Price2;
                info.Price3 = info.Price3 == null ? 0 : info.Price3;
                info.Price4 = info.Price4 == null ? 0 : info.Price4;
                info.Price5 = info.Price5 == null ? 0 : info.Price5;
                info.Gas1 = info.Gas1 == null ? 0 : info.Gas1;
                info.Gas2 = info.Gas2 == null ? 0 : info.Gas2;
                info.Gas3 = info.Gas3 == null ? 0 : info.Gas3;
                info.Gas4 = info.Gas4 == null ? 0 : info.Gas4;
                info.Ladder = info.Ladder == null ? 3 : info.Ladder;

                info.MeterState = info.MeterState == null ? '4' : info.MeterState;

                if (info.MeterNo.Length > 14)
                    info.MeterNo = info.MeterNo.Substring(0, 14);
                else if (info.MeterNo.Length < 14)
                    info.MeterNo = info.MeterNo.PadLeft(14, '0');

                // 调用新增方法
                tbl.InsertOnSubmit(info);
                // 更新操作
                dd.SubmitChanges();

                IoT_Meter dbinfo = dd.GetTable<IoT_Meter>().Where(p =>
                 p.CompanyID == info.CompanyID && p.MeterNo == info.MeterNo).SingleOrDefault();

                new TaskManageDA().InsertMeter(dbinfo);//同时插入数据到mongoDB中

                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_Meter>(dbinfo)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增表具失败！" + e.Message
                };
            }
            return m;
        }
        /// <summary>
        /// 获取定义的key
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        private string getKey(string companyID)
        {
            IoT_SystemPar par = new SystemParManageService().getSystemPar(companyID);
            byte[] keys = { 0x88, 0x88, 0x88, 0x88, 0x88, 0x88, 0x88, 0x88 };

            if ((bool)par.AutoKey)
            {
                Random random = new Random(DateTime.Now.Millisecond);
                random.NextBytes(keys);
            }
            else
            {
                if (par.MKey != null || par.MKey.Length == 16)
                {
                    try
                    {
                        byte[] tmp = MyDataConvert.StrToToHexByte(par.MKey);
                        for (int i = 0; i < 8; i++)
                            keys[i] = tmp[i];
                    }
                    catch
                    {
                        goto END;
                    }
                }
            }
            END:
            StringBuilder sb = new StringBuilder();
            for(int i=0;i<8;i++)
                sb.Append (string.Format ("{0:X2}",keys[i]));
            return sb.ToString ();
        }
        /// <summary>
        /// 获取上传周期参数
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        private string getUploadCycle(string companyID)
        {
            //TODO:在此编码从系统配置的参数中获取
            return "01012359";
        }

        public Message Edit(IoT_Meter info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_Meter dbinfo = dd.GetTable<IoT_Meter>().Where(p =>
                  p.CompanyID == info.CompanyID && p.MeterNo == info.MeterNo).SingleOrDefault();
                if (dbinfo.MKeyVer == 0)
                { 
                    //重新设置密钥

                }

                //ConvertHelper.Copy<IoT_Meter>(dbinfo, info);
                dbinfo.MeterState = info.MeterState;
                // 更新操作
                dd.SubmitChanges();

                new TaskManageDA().UpdateMeter(info);//同时更新数据到mongoDB中
                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_Meter>(dbinfo)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑表具失败！" + e.Message
                };
            }
            return m;
        }

        public Message Delete(IoT_Meter info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                // 获得上下文对象中的表信息
                Table<IoT_Meter> tbl = dd.GetTable<IoT_Meter>();

                var s = tbl.Where(p => p.CompanyID == info.CompanyID && p.MeterNo == info.MeterNo).Single();
                tbl.DeleteOnSubmit(s as IoT_Meter);


                // 更新操作
                dd.SubmitChanges();
                new TaskManageDA().DeleteMeter(info.MeterNo);
                m = new Message()
                {
                    Result = true,
                    TxtMessage = "删除表具成功！"
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "删除表具失败！" + e.Message
                };
            }
            return m;
        }


        public IoT_Meter GetMeterByNo(string meterNo)
        {
            return QueryMeter(meterNo);
        }

        public static  IoT_Meter QueryMeter(string meterNo)
        {
            // 定义执行结果
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_Meter dbinfo = dd.GetTable<IoT_Meter>().Where(p =>
                 p.MeterNo == meterNo).SingleOrDefault();
                if (dbinfo != null)
                    return dbinfo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
#if DEBUG
                System.Diagnostics.Debug.Assert(false, e.Message);
#endif
            }
            return null;
        }




    }
}
