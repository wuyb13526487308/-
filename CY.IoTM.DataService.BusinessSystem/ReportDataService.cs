using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using CY.IoTM.Common.Business;
using CY.IoTM.MongoDataHelper;
using System.Diagnostics;
using CY.IoTM.Common.Log;
using CY.IoTM.DataCollectionService;
using CY.IoTM.Common.Classes;

namespace CY.IoTM.DataService.Business
{
    public class ReportDataService : IReportData
    {
        #region IReportData 成员
        /// <summary>
        /// 处理提交抄表数据
        /// </summary>
        /// <param name="meter"></param>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public SubmitResult Submit(Meter meter, SubmitData dataItem)
        {
            SubmitResult result = new SubmitResult();
            //如果表为金额表，则判断是否需要效验，如需要则添加校准任务
            if (meter.MeterType == ((byte)CY.IoTM.Common.Item.MeterType.金额表).ToString ().PadLeft(2,'0') && meter.MeterState =="0")
            {
                new Settlement().Calculate(meter, dataItem, result);
            }

            //上报数据
            DataPar par = new DataPar(meter, dataItem);
            //new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(DoWithData)).Start(par);
            DoWithData(par);
            
            return result;
        }

        #endregion



        class DataPar
        {
            Meter meter;

            public Meter Meter
            {
                get { return meter; }
                set { meter = value; }
            }
            SubmitData dataItem;

            public SubmitData DataItem
            {
                get { return dataItem; }
                set { dataItem = value; }
            }

            public DataPar(Meter meter, SubmitData dataItem)
            {
                this.meter = meter;
                this.dataItem = dataItem;
            }
        }

        /// <summary>
        /// 数据处理线程
        /// </summary>
        /// <param name="objectPar"></param>
        private void DoWithData(object objectPar)
        {
            DataPar par = objectPar as DataPar; 
            if (par == null) 
            {                  
                return; 
            }            
            try
            {
                ////保存数据到数据库
                //IoT_MeterDataHistory meterData = new IoT_MeterDataHistory();
                ////累计用量
                //meterData.Gas = par.DataItem.LJGas;
                ////
                //meterData.MeterID = par.Meter.MeterID;
                //meterData.MeterNo = par.Meter.Mac;
                //meterData.ReadDate = par.DataItem.ReadDate;
                //meterData.RemainingAmount = par.DataItem.SYMoney;
                //meterData.LastTotal = par.DataItem.LastLJGas;
                //meterData.ST1 = par.DataItem.ST1.ToString();
                //meterData.ST2 = par.DataItem.ST2.ToString();

                ReadDataInfo readdatainfo = new ReadDataInfo();
                readdatainfo._Gas = par.DataItem.LJGas;
                readdatainfo._MeterID = par.Meter.MeterID;
                readdatainfo._MeterNo = par.Meter.Mac;
                readdatainfo._ReadDate = par.DataItem.ReadDate;
                readdatainfo._RemainingAmount = par.DataItem.SYMoney;
                readdatainfo._LastTotal = par.DataItem.LastLJGas;
                readdatainfo._ST1 = par.DataItem.ST1.ToString();
                readdatainfo._ST2 = par.DataItem.ST2.ToString();


                WarningInfo warninginfo = new WarningInfo();
                warninginfo.MeterID = par.Meter.MeterID;
                warninginfo.meterNo = par.Meter.Mac;
                warninginfo.readDate = par.DataItem.ReadDate;
                warninginfo.st1 = par.DataItem.ST1.ToString(); 
                warninginfo.st2 = par.DataItem.ST2.ToString();
                //InsertReadData(meterData);
                // //分析报警信息
                //InsertWariningData(par.Meter.MeterID, par.Meter.Mac, par.DataItem.ReadDate, par.DataItem.ST1.ToString(), par.DataItem.ST2.ToString());

                ///向业务服务器 队列中添加对象
                Log.getInstance().Write(MsgType.Information, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}上报采集记录到数据中心,DataType:{DataType.ReadData} MeterNo:{readdatainfo._MeterNo} ReadDate:{readdatainfo._ReadDate}");
                Log.getInstance().Write(MsgType.Information, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}上报采集记录到数据中心,DataType:{DataType.WarningData} MeterNo:{readdatainfo._MeterNo} ReadDate:{readdatainfo._ReadDate} ST1:{par.DataItem.ST1.ToString()} ST2:{ warninginfo.st2}");
                DDService.getInstance().InsertDataRecord(new DataArge(DataType.ReadData, readdatainfo));
                DDService.getInstance().InsertDataRecord(new DataArge(DataType.WarningData, warninginfo));

            }
            catch (Exception er)
            {
                Log.getInstance().Write(MsgType.Information, "DoWithData:"+er.ToString());
            }
           }

        private void InsertReadData(IoT_MeterDataHistory meterData)
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                Table<IoT_MeterDataHistory> tbl = dd.GetTable<IoT_MeterDataHistory>();
                // 调用新增方法
                tbl.InsertOnSubmit(meterData);
                // 更新操作
                dd.SubmitChanges();

                IoT_Meter dbinfo = dd.GetTable<IoT_Meter>().Where(p =>
  p.MeterNo == meterData.MeterNo && p.ID == meterData.MeterID).SingleOrDefault();
                if (dbinfo != null)
                {
                    dbinfo.LastTotal = meterData.LastTotal;
                    dbinfo.ReadDate = meterData.ReadDate;
                    dbinfo.TotalAmount = meterData.Gas;
                    dbinfo.RemainingAmount = meterData.RemainingAmount;
                    dbinfo.ValveState = meterData.ST1.Substring(0, 2) == "00" ? '0' : '1';
                    dd.SubmitChanges();
                }

                int iCount = dd.GetTable<IoT_DayReadMeter>().Where(p =>
                    ((DateTime)p.ReadDate).Year == ((DateTime)meterData.ReadDate).Year && ((DateTime)p.ReadDate).Month == ((DateTime)meterData.ReadDate).Month && ((DateTime)p.ReadDate).Day == ((DateTime)meterData.ReadDate).Day
                    && p.MeterNo == meterData.MeterNo).Count();
                if (iCount == 0)
                {
                    IoT_DayReadMeter dayReadMeter = new IoT_DayReadMeter();
                    dayReadMeter.Gas = meterData.Gas;
                    //
                    dayReadMeter.MeterID = meterData.MeterID;
                    dayReadMeter.MeterNo = meterData.MeterNo;
                    dayReadMeter.ReadDate = meterData.ReadDate;
                    dayReadMeter.RemainingAmount = meterData.RemainingAmount;
                    dayReadMeter.LastTotal = meterData.LastTotal;
                    dayReadMeter.ST1 = meterData.ST1.ToString();
                    dayReadMeter.ST2 = meterData.ST2.ToString();
                    dd.GetTable<IoT_DayReadMeter>().InsertOnSubmit(dayReadMeter);
                    dd.SubmitChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void InsertWariningData(long MeterID,string meterNo,DateTime readDate,string st1, string st2)
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                Table<IoT_AlarmInfo> tbl = dd.GetTable<IoT_AlarmInfo>();
                IoT_AlarmInfo alarminfo = new IoT_AlarmInfo();
                alarminfo.MeterID = MeterID;
                alarminfo.MeterNo = meterNo;
                alarminfo.ReportDate = readDate;
                if(st1.Substring(0,2) =="11")
                {
                    alarminfo.Item = "阀门";
                    alarminfo.AlarmValue = "异常";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }

                if (st1.Substring(2, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = MeterID;
                    alarminfo.MeterNo = meterNo;
                    alarminfo.ReportDate = readDate;

                    alarminfo.Item = "电池电压";
                    alarminfo.AlarmValue = "欠压";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }

                if (st1.Substring(5, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = MeterID;
                    alarminfo.MeterNo = meterNo;
                    alarminfo.ReportDate = readDate;

                    alarminfo.Item = "电源状态";
                    alarminfo.AlarmValue = "异常";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }

                if (st1.Substring(6, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = MeterID;
                    alarminfo.MeterNo = meterNo;
                    alarminfo.ReportDate = readDate;

                    alarminfo.Item = "外电源状态";
                    alarminfo.AlarmValue = "异常";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }
                if (st1.Substring(7, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = MeterID;
                    alarminfo.MeterNo = meterNo;
                    alarminfo.ReportDate = readDate;

                    alarminfo.Item = "锂电池状态";
                    alarminfo.AlarmValue = "异常";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }
                if (st1.Substring(10, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = MeterID;
                    alarminfo.MeterNo = meterNo;
                    alarminfo.ReportDate = readDate;

                    alarminfo.Item = "欠压（干电池）";
                    alarminfo.AlarmValue = "欠压";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }
                if (st1.Substring(11, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = MeterID;
                    alarminfo.MeterNo = meterNo;
                    alarminfo.ReportDate = readDate;

                    alarminfo.Item = "操作错误/磁干扰";
                    alarminfo.AlarmValue = "异常";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }

                if (st1.Substring(12, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = MeterID;
                    alarminfo.MeterNo = meterNo;
                    alarminfo.ReportDate = readDate;

                    alarminfo.Item = "余额不足/气量用尽";
                    alarminfo.AlarmValue = "余额不足/气量用尽";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }
                if (st1.Substring(13, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = MeterID;
                    alarminfo.MeterNo = meterNo;
                    alarminfo.ReportDate = readDate;

                    alarminfo.Item = "内部错误关阀";
                    alarminfo.AlarmValue = "内部错误关阀";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                } 
                if (st1.Substring(15, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = MeterID;
                    alarminfo.MeterNo = meterNo;
                    alarminfo.ReportDate = readDate;

                    alarminfo.Item = "长期未与服务器通讯报警";
                    alarminfo.AlarmValue = "长期未与服务器通讯报警";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }
                string[] items = { "移动报警 / 地震震感器动作切断报警", "长期未使用切断报警", "燃气压力过低切断报警", "持续流量超时切断报警", "异常微小流量切断报警","异常大流量切断报警","流量过载切断报警","燃气漏气切断报警" };
                for (int i = 0; i < 8; i++)
                {
                    if (st2.Substring(i, 1) == "1")
                    {
                        alarminfo = new IoT_AlarmInfo();
                        alarminfo.MeterID = MeterID;
                        alarminfo.MeterNo = meterNo;
                        alarminfo.ReportDate = readDate;

                        alarminfo.Item = items[i];
                        alarminfo.AlarmValue = "报警";
                        // 调用新增方法
                        tbl.InsertOnSubmit(alarminfo);
                    }
                }
                // 更新操作
                dd.SubmitChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
