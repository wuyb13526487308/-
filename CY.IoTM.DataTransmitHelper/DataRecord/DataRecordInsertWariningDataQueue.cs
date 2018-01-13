using CY.IoTM.Common.Business;
using CY.IoTM.Common.Classes;
using CY.IoTM.Common.Log;
using System;
using System.ComponentModel.Composition;
using System.Data.Linq;

namespace CY.IoTM.DataTransmitHelper.DataRecord
{
    [Export(typeof(IWarningDataQueue))]
    public class DataRecordInsertWariningDataQueue :IWarningDataQueue
    {
        private bool _isRunning = false;
        /// <summary>
        /// 服务队列编号
        /// </summary>
        protected int serviceNo = 0;
        /// <summary>
        /// 服务队列名称
        /// </summary>
        protected string serviceName = string.Empty;
        private WorkQueue<WarningInfo> Queue = new WorkQueue<WarningInfo>();

        DataRecordInsertWariningDataQueue(){
            this.serviceNo = 2;
            this.serviceName = "插入表报警信息"; 
        } 

        /// <summary>
        /// 获取服务队列编号
        /// </summary>
        public int getServiceNo
        {
            get { return this.serviceNo; }
        }
        /// <summary>
        /// 获取服务队列名称
        /// </summary>
        public string getServiceName
        {
            get { return this.serviceName; }
        }

        /// <summary>
        ///获取队列
        /// </summary>  
        public WorkQueue<WarningInfo> getWorkQueue
        {
            get { return this.Queue; }
        }
        /// <summary>
        /// 向队列中添加对象
        /// </summary>
        /// <param name="item"></param>
        public void workQueue_AddWork(WarningInfo item)
        {
            Log.getInstance().Write(MsgType.Information,$"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}添加数据到报警处理队列,this.Queue.Count:{this.Queue.QueueCount()}，表号:{item.meterNo} 数据采集时间：{item.readDate} ST1:{item.st1} ST2:{item.st2}");

            this.Queue.EnqueueItem(item);
        }
        /// <summary>
        /// 服务启动
        /// </summary>
        public void Start()
        {
            if (!_isRunning)
            {
                //Console.WriteLine(string.Format("{0}-启动【{1}】服务", System.DateTime.Now, this.serviceName));
                Log.getInstance().Write(MsgType.Information,string.Format("{0}-启动【{1}】服务", System.DateTime.Now, this.serviceName));
                _isRunning = true;
                Queue.UserWork += new UserWorkEventHandler<WarningInfo>(workQueue_UserWork);
            }
            else
            {
                Log.getInstance().Write(MsgType.Information, string.Format("【{0}】服务正在运行中。不能重复进行启动操作", this.serviceName));
          
                //Console.WriteLine(string.Format("【{0}】服务正在运行中。不能重复进行启动操作", this.serviceName));
            }
        }
        /// <summary>
        /// 服务停止
        /// </summary>
        public void Stop()
        {
            if (_isRunning)
            {
                //Console.WriteLine(string.Format("{0}-停止【{1}】服务", System.DateTime.Now, this.serviceName));
                Log.getInstance().Write(MsgType.Information, string.Format("{0}-停止【{1}】服务", System.DateTime.Now, this.serviceName));
                _isRunning = false;
                Queue.UserWork -= new UserWorkEventHandler<WarningInfo>(workQueue_UserWork);
            }
            else
            {
                Log.getInstance().Write(MsgType.Information,string.Format("【{0}】服务已经停止。不能重复进行停止操作", this.serviceName));
                //Console.WriteLine(string.Format("【{0}】服务已经停止。不能重复进行停止操作", this.serviceName));
            }
        }
        private void workQueue_UserWork(object sender, WorkQueue<WarningInfo>.EnqueueEventArgs e)
        {
            try
            {
                DoWork(e);
            }
            catch (Exception ex)
            {
                 Log.getInstance().Write(ex, MsgType.Error);
            }

        }
        WorkQueue<WarningInfo>.EnqueueEventArgs _old;

        protected virtual void DoWork(WorkQueue<WarningInfo>.EnqueueEventArgs e)
        {
            if (e == null) return;
            //临时解决重复数据
            if (_old != null && _old.Item.meterNo == e.Item.meterNo && _old.Item.readDate == e.Item.readDate)
            {
                _old = e;
                return;
            }
            _old = e;

            Log.getInstance().Write(MsgType.Information, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}数据中心处理报警数据，表号:{e.Item.meterNo} 数据采集时间：{e.Item.readDate} ST1:{e.Item.st1} ST2:{e.Item.st2}");

            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                Table<IoT_AlarmInfo> tbl = dd.GetTable<IoT_AlarmInfo>();
                IoT_AlarmInfo alarminfo = new IoT_AlarmInfo();
                alarminfo.MeterID =e.Item.MeterID;
                alarminfo.MeterNo = e.Item.meterNo;
                alarminfo.ReportDate = e.Item.readDate;
                if (e.Item.st1.Substring(0, 2) == "11")
                {
                    alarminfo.Item = "阀门异常";
                    alarminfo.AlarmValue = "报警";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }
                string str = "";

                if (e.Item.st1.Substring(2, 1) == "1" || e.Item.st1.Substring(3, 1) == "1")
                {
                    str += e.Item.st1.Substring(2, 1) == "1" ? "电池1级欠压" : "电池2级欠压";
                }

                if (e.Item.st1.Substring(4, 1) == "1")
                {
                    str += "锂电池欠压";
                }


                if (e.Item.st1.Substring(5, 1) == "1")
                {
                    str += " 外电源异常";
                }

                if (e.Item.st1.Substring(6, 1) == "1")
                {
                    str += " 干电池异常";
                }
                if (e.Item.st1.Substring(7, 1) == "1")
                {
                    str += " 锂电池异常";
                }

                if (str != "")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = e.Item.MeterID;
                    alarminfo.MeterNo = e.Item.meterNo;
                    alarminfo.ReportDate = e.Item.readDate;
                    alarminfo.Item = str;
                    alarminfo.AlarmValue = "电源状态";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }

                if (e.Item.st1.Substring(9, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = e.Item.MeterID;
                    alarminfo.MeterNo = e.Item.meterNo;
                    alarminfo.ReportDate = e.Item.readDate;

                    alarminfo.Item = "无外电（外电和干电池）";
                    alarminfo.AlarmValue = "报警";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }
                if (e.Item.st1.Substring(10, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = e.Item.MeterID;
                    alarminfo.MeterNo = e.Item.meterNo;
                    alarminfo.ReportDate = e.Item.readDate;

                    alarminfo.Item = "欠压（干电池）";
                    alarminfo.AlarmValue = "报警";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }
                if (e.Item.st1.Substring(11, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = e.Item.MeterID;
                    alarminfo.MeterNo = e.Item.meterNo;
                    alarminfo.ReportDate = e.Item.readDate;

                    alarminfo.Item = "操作错误/磁干扰";
                    alarminfo.AlarmValue = "报警";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }

                if (e.Item.st1.Substring(12, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = e.Item.MeterID;
                    alarminfo.MeterNo = e.Item.meterNo;
                    alarminfo.ReportDate = e.Item.readDate;

                    alarminfo.Item = "余额不足/气量用尽";
                    alarminfo.AlarmValue = "报警";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }
                if (e.Item.st1.Substring(13, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = e.Item.MeterID;
                    alarminfo.MeterNo = e.Item.meterNo;
                    alarminfo.ReportDate = e.Item.readDate;

                    alarminfo.Item = "人工控制关阀";
                    alarminfo.AlarmValue = "报警";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }
                if (e.Item.st1.Substring(14, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = e.Item.MeterID;
                    alarminfo.MeterNo = e.Item.meterNo;
                    alarminfo.ReportDate = e.Item.readDate;

                    alarminfo.Item = "燃气表故障";
                    alarminfo.AlarmValue = "报警";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }
                if (e.Item.st1.Substring(15, 1) == "1")
                {
                    alarminfo = new IoT_AlarmInfo();
                    alarminfo.MeterID = e.Item.MeterID;
                    alarminfo.MeterNo = e.Item.meterNo;
                    alarminfo.ReportDate = e.Item.readDate;

                    alarminfo.Item = "长期未与服务器通讯报警";
                    alarminfo.AlarmValue = "报警";
                    // 调用新增方法
                    tbl.InsertOnSubmit(alarminfo);
                }
                string[] items = { "移动报警 / 地震震感器动作切断报警", "长期未使用切断报警", "LCD屏故障报警", "持续流量超时切断报警", "异常微小流量切断报警", "异常大流量切断报警", "流量过载切断报警", "燃气漏气切断报警" };
                for (int i = 0; i < 8; i++)
                {
                    if (e.Item.st2.Substring(i, 1) == "1")
                    {
                        alarminfo = new IoT_AlarmInfo();
                        alarminfo.MeterID = e.Item.MeterID;
                        alarminfo.MeterNo = e.Item.meterNo;
                        alarminfo.ReportDate = e.Item.readDate;

                        alarminfo.Item = items[i];
                        alarminfo.AlarmValue = "报警";
                        // 调用新增方法
                        tbl.InsertOnSubmit(alarminfo);
                    }
                }
                // 更新操作
                dd.SubmitChanges();

            }
            catch (Exception er)
            {                 
                Log.getInstance().Write(er,MsgType.Error);
            }
        }
    }
}
