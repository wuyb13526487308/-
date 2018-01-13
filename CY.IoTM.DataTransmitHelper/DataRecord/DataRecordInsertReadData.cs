using CY.IoTM.Common.Business;
using CY.IoTM.Common.Classes;
using CY.IoTM.Common.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.DataTransmitHelper.DataRecord
{
    [Export(typeof(IReadDataQueue))]
    public class DataRecordInsertReadData : BaseDataRecordQueue
    {
        DataRecordInsertReadData()
            : base()
        {
            this.serviceNo = 1;
            this.serviceName = "插入表上传的流量数据";
        }
        protected override void DoWork(WorkQueue<ReadDataInfo>.EnqueueEventArgs e)
        {
            if (e == null) return;
            IoT_MeterDataHistory meterData=new IoT_MeterDataHistory()
            {
                ID=e.Item._ID,
                MeterID=e.Item._MeterID,
                MeterNo=e.Item._MeterNo,
                Ser=e.Item._Ser,
                ST1=e.Item._ST1,
                ST2=e.Item._ST2,
                Gas= e.Item._Gas,
                LastTotal=e.Item._LastTotal, 
                ReadDate=e.Item._ReadDate, 
                RemainingAmount=e.Item._RemainingAmount
            };
         
             string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
             //Linq to SQL 上下文对象
             DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
             try
             {
                 //向表IoT_MeterDataHistory中添加记录
                 Table<IoT_MeterDataHistory> tbl = dd.GetTable<IoT_MeterDataHistory>();
                 // 调用新增方法
                 tbl.InsertOnSubmit(meterData);
                 // 更新操作
                 dd.SubmitChanges();


                 //更新表IoT_Meter信息
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
                 //向表IoT_DayReadMeter中添加抄表数据记录
                 int iCount = dd.GetTable<IoT_DayReadMeter>().Where(p =>
                     ((DateTime)p.ReadDate).Year == ((DateTime)meterData.ReadDate).Year && ((DateTime)p.ReadDate).Month == ((DateTime)meterData.ReadDate).Month && ((DateTime)p.ReadDate).Day == ((DateTime)meterData.ReadDate).Day
                     && p.MeterNo == meterData.MeterNo).Count();
                 if (iCount == 0)
                 {
                     IoT_DayReadMeter dayReadMeter = new IoT_DayReadMeter();
                     dayReadMeter.Gas = meterData.Gas;  
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
             catch (Exception er)
             {
                 //异常记录
                 Log.getInstance().Write(er,MsgType.Error);
             }
        }
    }
}
