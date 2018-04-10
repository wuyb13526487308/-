using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
 

namespace CY.IoTM.Common.Log
{
    public class Log : ILog
    {
        private static Log _log;
        private static readonly Object _object = new object();
        //日志对象的缓存队列
        private Queue<Msg> msgs;
        //private Queue<DPLogMsg> _dpmsgs;
        /// <summary>
        /// 设备发送或接收数据历史缓存队列
        /// </summary>
        private Queue<DTULogMsg> _dtuMsgs;
        private Queue<MonitorLogMsg> _monitorMsgs;
        private Queue<MeterLogMsg> _meterMsgs;
        private Queue<OneMeterDataLogMsg> _oneMeterDataLogMsg;

        private List<LogStreamWriter> _dpLogStreamList;
        private List<LogStreamWriter> _dtuLogStreamList;
        private List<LogStreamWriter> _monitorinfoLogStreamList;
        private List<LogStreamWriter> _meterLogStreamList;
        private List<LogStreamWriter> _oneMeterDataLogStreamList;

        private string path;
        //日志写入线程的控制标记
        private bool state;
        /// <summary>
        /// 记录系统日志文件生命周期的时间标记
        /// </summary>
        private static DateTime SystemTimeSign;
        //日志文件写入流对象
        private StreamWriter writer;

        private Log()
        {
            //设置日志的路径
            state = true;
            path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //path = System.Configuration.ConfigurationManager.AppSettings["SystemPath"] == null ? AppDomain.CurrentDomain.BaseDirectory.ToString() : System.Configuration.ConfigurationManager.AppSettings["SystemPath"].ToString() + "\\";
            //this._dpmsgs = new Queue<DPLogMsg>();
            this.msgs = new Queue<Msg>();
            this._dtuMsgs = new Queue<DTULogMsg>();
            this._monitorMsgs = new Queue<MonitorLogMsg>();
            this._meterMsgs = new Queue<MeterLogMsg>();
            this._oneMeterDataLogMsg = new Queue<OneMeterDataLogMsg>();



            this._dpLogStreamList = new List<LogStreamWriter>();
            this._dtuLogStreamList = new List<LogStreamWriter>();
            this._monitorinfoLogStreamList = new List<LogStreamWriter>();
            this._meterLogStreamList = new List<LogStreamWriter>();
            this._oneMeterDataLogStreamList = new List<LogStreamWriter>();

            Thread thread = new Thread(work);
            thread.IsBackground = true;
            thread.Start();
        }
        public static Log getInstance()
        {
            if (_log == null)
            {
                lock (_object)
                {
                    if (_log == null)
                    {
                        _log = new Log();
                    }
                }
            }
            return _log;
        }
        /// <summary>
        /// 该方法在系统退出时调用，否则将导致日志记录失败
        /// </summary>
        public void Close()
        {
            this.state = false;
        }

        private void work()
        {
            while (state)
            {
                if (System.AppDomain.CurrentDomain.IsFinalizingForUnload()) break;
                ////判断队列中是否存在待写入的日志
                if (_monitorMsgs.Count > 0)
                {
                    MonitorLogMsg msg = _monitorMsgs.Dequeue();
                    WriteHisMonitorInfoLogFile(msg);

                }
                if (_meterMsgs.Count > 0)
                {
                    MeterLogMsg msg = _meterMsgs.Dequeue();
                    WriteMeterLogFile(msg);
                }
                if (msgs.Count > 0)
                {
                    Msg msg = null;
                    lock (msgs)
                    {
                        msg = msgs.Dequeue();
                    }
                    if (msg != null)
                    {
                        FileWrite(msg);
                    }
                }
                if (_dtuMsgs.Count > 0)
                {
                    DTULogMsg _msg = _dtuMsgs.Dequeue();
                    WriteDTULogFile(_msg);
                }

                if (_oneMeterDataLogMsg.Count > 0)
                {
                    OneMeterDataLogMsg _msg = _oneMeterDataLogMsg.Dequeue();
                    WriteOneMeterDataLogFile(_msg);
                }
                if (!(msgs.Count > 0 || _dtuMsgs.Count > 0 || _monitorMsgs.Count > 0 || _meterMsgs.Count > 0 || _oneMeterDataLogMsg.Count > 0))
                {
                    Thread.Sleep(100);
                }
            }
            //关闭所有缓存日志文件
            foreach (LogStreamWriter sm in _dpLogStreamList)
                sm.Close();
            foreach (LogStreamWriter sm in _dtuLogStreamList)
                sm.Close();
            foreach (LogStreamWriter sm in _monitorinfoLogStreamList)
                sm.Close();
            foreach (LogStreamWriter sm in _meterLogStreamList)
                sm.Close();
            foreach (LogStreamWriter sm in _oneMeterDataLogStreamList)
                sm.Close();   
            //关闭系统日志
            FileClose();
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path"></param>
        private void CreatePath(string path)
        {
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
        }
        /// <summary>
        /// 将系统信息写入日志文件
        /// </summary>
        /// <param name="msg"></param>
        private void FileWrite(Msg msg)
        {
            try
            {
                if (writer == null)
                {
                    FileOpen();
                }

                //判断文件到期标志，如果当前文件到期则关闭当前文件创建新的日志文件
                if (DateTime.Now >= SystemTimeSign)
                {
                    FileClose();
                    FileOpen();
                }
                writer.Write(msg.Datetime);
                writer.Write('\t');
                writer.Write(msg.Type);
                writer.Write('\t');
                writer.WriteLine(msg.Text);
                writer.Flush();

            }
            catch (Exception e)
            {
                Console.Out.Write(e);
            }
        }

        /// <summary>
        /// 打开系统日志文件
        /// </summary>
        private void FileOpen()
        {
            DateTime now = DateTime.Now;
            CreatePath(path + string.Format("\\Log\\{0:yyyyMMdd}\\", now));
            writer = new StreamWriter(path + string.Format("\\Log\\{0:yyyyMMdd}\\{1}", now, "Log.log"), true, Encoding.UTF8);
        }

        //关闭打开的日志文件
        private void FileClose()
        {
            if (writer != null)
            {
                writer.Flush();
                writer.Close();
                writer.Dispose();
                writer = null;
            }
        }

        #region 采集服务器收发命令 存储到日志文件
        //写终端（DTU）设备日志文件
        private bool WriteDTULogFile(DTULogMsg msg)
        {
            LogStreamWriter sm = null;// this._dpLogStreamList.Where(p => p.DtuID == msg.DTUID && p.DataPointID == msg.DataPointID).Single();
            if (this._dtuLogStreamList.Count > 0)
            {
                List<LogStreamWriter> list = this._dtuLogStreamList.Where(p => p.DtuID == msg.DTUID).ToList();
                if (list.Count == 1)
                    sm = list[0];
            }
            if (sm == null)
            {
                //记录该采集点的对象不存在，创建该对象。
                sm = CreateDTULogFile(msg.DTUID);
                this._dtuLogStreamList.Add(sm);
            }
            if (DateTime.Now >= sm.TimeSign)
            {
                sm.Close();
                this._dtuLogStreamList.Remove(sm);
                //创建新日志文件
                sm = CreateDTULogFile(msg.DTUID);
                this._dtuLogStreamList.Add(sm);
            }
            //记录日志
            try
            {
                sm.WriteLine(msg.Text);
                sm.Flush();
                return true;
            }
            catch
            {
                sm.Close();
                //如果写流失败
                this._dtuLogStreamList.Remove(sm);
                return false;
            }
        }
        private LogStreamWriter CreateDTULogFile(string dtuID)
        {
            DateTime now = DateTime.Now;
            CreatePath(path + string.Format("\\Log\\{0:yyyyMMdd}\\", now));
            return new LogStreamWriter(dtuID, "", path + string.Format("\\Log\\{0:yyyyMMdd}\\ZD-{1}.txt", now, dtuID));
        }
        #endregion 采集服务器收发命令 存储到日志文件

        #region 将采集服务器监测信息存储到日志文件
        /// <summary>
        /// 将采集服务器监测信息存储到文件
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool WriteHisMonitorInfoLogFile(MonitorLogMsg msg)
        {
            LogStreamWriter sm = null;// this._dpLogStreamList.Where(p => p.DtuID == msg.DTUID && p.DataPointID == msg.DataPointID).Single();
            if (this._monitorinfoLogStreamList.Count > 0)
            {
                List<LogStreamWriter> list = this._monitorinfoLogStreamList.Where(p => p.DtuID == msg.cjdID).ToList();
                if (list.Count == 1)
                    sm = list[0];
            }
            if (sm == null)
            {
                //记录该监视信息对象不存在，创建该对象。
                sm = CreateHisMonitorInfo(msg.cjdID);
                this._monitorinfoLogStreamList.Add(sm);
            }
            if (DateTime.Now >= sm.TimeSign)
            {
                sm.Close();
                this._monitorinfoLogStreamList.Remove(sm);
                //创建新日志文件
                sm = CreateHisMonitorInfo(msg.cjdID);
                this._monitorinfoLogStreamList.Add(sm);
            }
            //记录日志
            try
            {
                sm.WriteLine(msg.text);
                sm.Flush();
                return true;
            }
            catch
            {
                sm.Close();
                //如果写流失败
                this._monitorinfoLogStreamList.Remove(sm);
                return false;
            }
        }

        private int _fileSpace = 1;
        public int FileSpace
        {
            get { return _fileSpace; }
            set { _fileSpace = value; }
        }
        /// <summary>
        /// 创建 监测信息日志
        /// </summary>
        /// <param name="cjdID"></param>
        /// <returns></returns>
        private LogStreamWriter CreateHisMonitorInfo(string cjdID)
        {
            DateTime now = DateTime.Now;
            CreatePath(path + string.Format("\\Data\\{0:yyyyMMdd}\\", now));
            return new LogStreamWriter(cjdID, path + string.Format("\\Data\\{0:yyyyMMdd}\\{1}_{0:yyyyMMddHHmm}.txt", now, cjdID), _fileSpace);
        }
        #endregion

        #region 将表上传到下线耗时信息存储到日志文件
        /// <summary>
        /// 将采集服务器监测信息存储到文件
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool WriteMeterLogFile(MeterLogMsg msg)
        {
            LogStreamWriter sm = null;// this._dpLogStreamList.Where(p => p.DtuID == msg.DTUID && p.DataPointID == msg.DataPointID).Single();
            if (this._meterLogStreamList.Count > 0)
            {
                List<LogStreamWriter> list = this._meterLogStreamList.ToList();
                if (list.Count == 1)
                    sm = list[0];
            }
            if (sm == null)
            {
                //记录该监视信息对象不存在，创建该对象。
                sm = CreateMeterLog();
                this._meterLogStreamList.Add(sm);
            }
            if (DateTime.Now >= sm.TimeSign)
            {
                sm.Close();
                this._meterLogStreamList.Remove(sm);
                //创建新日志文件
                sm = CreateMeterLog();
                this._meterLogStreamList.Add(sm);
            }
            //记录日志
            try
            {
                sm.WriteLine(msg.text);
                sm.Flush();
                return true;
            }
            catch
            {
                sm.Close();
                //如果写流失败
                this._meterLogStreamList.Remove(sm);
                return false;
            }
        }
        /// <summary>
        /// 创建 监测信息日志
        /// </summary>
        /// <param name="cjdID"></param>
        /// <returns></returns>
        private LogStreamWriter CreateMeterLog()
        {
            DateTime now = DateTime.Now;
            CreatePath(path + string.Format("\\MeterData\\{0:yyyyMMdd}\\", now));
            return new LogStreamWriter(path + string.Format("\\MeterData\\{0:yyyyMMdd}\\{0:yyyyMMddHH}.txt", now));
        }
        #endregion

        #region 将单表信息存储到日志文件
        /// <summary>
        /// 将单表信息存储到日志文件
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool WriteOneMeterDataLogFile(OneMeterDataLogMsg msg)
        {
            LogStreamWriter sm = null;// this._dpLogStreamList.Where(p => p.DtuID == msg.DTUID && p.DataPointID == msg.DataPointID).Single();
            if (this._oneMeterDataLogStreamList.Count > 0)
            {
                List<LogStreamWriter> list = this._oneMeterDataLogStreamList.ToList();
                if (list.Count == 1)
                    sm = list[0];
            }
            if (sm == null)
            {
                //记录该监视信息对象不存在，创建该对象。
                sm = CreateOneMeterDataLog(msg.mac);
                this._oneMeterDataLogStreamList.Add(sm);
            }
            else
            {
                sm.Close();
                this._oneMeterDataLogStreamList.Remove(sm);
                //创建新日志文件
                sm = CreateOneMeterDataLog(msg.mac);
                this._oneMeterDataLogStreamList.Add(sm);
            }
            //记录日志
            try
            {
                sm.WriteLine(msg.text);
                sm.Flush();
                return true;
            }
            catch
            {
                sm.Close();
                //如果写流失败
                this._oneMeterDataLogStreamList.Remove(sm);
                return false;
            }
        }
        /// <summary>
        /// 创建 监测信息日志
        /// </summary>
        /// <param name="cjdID"></param>
        /// <returns></returns>
        private LogStreamWriter CreateOneMeterDataLog(string mac)
        {
            DateTime now = DateTime.Now;
            CreatePath(path + string.Format("\\Log\\OneMeterData\\{0:yyyyMMdd}\\", now));
            return new LogStreamWriter(path + string.Format("\\Log\\OneMeterData\\{0:yyyyMMdd}\\{1}.txt", now, mac), mac);
        }
        #endregion




        /// <summary>
        /// 记录系统日志
        /// </summary>
        /// <param name="type"></param>
        /// <param name="logInfo"></param>
        public void Write(MsgType type, string logInfo)
        {
            Write(logInfo, type);
        }

        /// <summary>
        /// 写DTU设备收发的数据
        /// </summary>
        /// <param name="msg"></param>
        public void Write(DTULogMsg msg)
        {
            lock (_object)
            {
                _dtuMsgs.Enqueue(msg);
            }
        }
        /// <summary>
        /// 将采集服务器历史监测信息  存储到文件
        /// </summary>
        /// <param name="msg"></param>
        public void Write(MonitorLogMsg msg)
        {
            lock (_object)
            {
                _monitorMsgs.Enqueue(msg);
            }
        }
        /// <summary>
        /// 将表上下线信息存储到文件
        /// </summary>
        /// <param name="msg"></param>
        public void Write(MeterLogMsg msg)
        {
            lock (_object)
            {
                _meterMsgs.Enqueue(msg);
            }
        }

        /// <summary>
        /// 将单表信息存储到文件
        /// </summary>
        /// <param name="msg"></param>
        public void Write(OneMeterDataLogMsg msg)
        {
            lock (_object)
            {
                _oneMeterDataLogMsg.Enqueue(msg);
            }
        }

        /// <summary>
        /// 写入新日志，根据指定的日志对象Msg
        /// </summary>
        /// <param name="msg">日志内容对象</param>
        public void Write(Msg msg)
        {
            if (msg != null)
            {
                lock (msgs)
                {
                    msgs.Enqueue(msg);
                }
            }
        }

        /// <summary>
        /// 写入新日志，根据指定的日志内容和信息类型，采用当前时间为日志时间写入新日志
        /// </summary>
        /// <param name="text">日志内容</param>
        /// <param name="type">信息类型</param>
        public void Write(string text, MsgType type)
        {
            Write(new Msg(text, type));
        }

        /// <summary>
        /// 写入新日志，根据指定的日志时间、日志内容和信息类型写入新日志
        /// </summary>
        /// <param name="dt">日志时间</param>
        /// <param name="text">日志内容</param>
        /// <param name="type">信息类型</param>
        public void Write(DateTime dt, string text, MsgType type)
        {
            Write(new Msg(dt, text, type));
        }

        /// <summary>
        /// 写入新日志，根据指定的异常类和信息类型写入新日志
        /// </summary>
        /// <param name="e">异常对象</param>
        /// <param name="type">信息类型</param>
        public void Write(Exception e, MsgType type)
        {
            //
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Message ---");
            sb.AppendLine(e.Message);
            sb.AppendLine("HelpLink ---");
            sb.AppendLine(e.HelpLink);
            sb.AppendLine("Source ---");
            sb.AppendLine(e.Source);
            sb.AppendLine("StackTrace ---");
            sb.AppendLine(e.StackTrace);
            //sb.AppendLine("TargetSite ---");
            //sb.AppendLine(e.StackTrace);
            Write(new Msg(sb.ToString(), type));
        }

        /// <summary>
        /// 读取指定设备的采集点的日志数据
        /// </summary>
        /// <param name="dtuID"></param>
        /// <param name="dataPointID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public LogCollection ReadDPLog(string zdID, string mac, DateTime date, int pageNum, int pageSize, ReadLogDataType lType)
        {
            LogCollection str = new LogCollection() { Rows = 0, ListTxtMessage = new List<TxtMessage>() };
            string basePath = path.Substring(0, path.LastIndexOf("\\"));
            string fileName = string.Empty;

            if (lType == ReadLogDataType.DTU)
            {
                fileName = basePath + string.Format("\\Log\\{0:yyyyMMdd}\\ZD-{1}.txt", date, zdID);
            }
            else if (lType == ReadLogDataType.System)
            {
                fileName = basePath + string.Format("\\Log\\{0:yyyyMMdd}\\Log.log", date);
            }
            else if (lType == ReadLogDataType.OneMeterData)
            {
                // return new LogStreamWriter(path + string.Format("\\Log\\OneMeterData\\{0:yyyyMMdd}\\{1:yyyyMMddHH}.txt", now, mac));
                fileName = basePath + string.Format("\\Log\\OneMeterData\\{0:yyyyMMdd}\\{1}.txt", date, mac);
            }
            if (File.Exists(fileName))
            {
                FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                // 创建一个数据流读入器，和打开的文件关联 
                StreamReader srMyfile = new StreamReader(fs);

                try
                {

                    // 把文件指针重新定位到文件的开始 
                    srMyfile.BaseStream.Seek(0, SeekOrigin.Begin);
                    // 打印文件文本内容 
                    int sIndex = (pageNum - 1) * pageSize;
                    int eIndex = (pageNum) * pageSize;
                    int sCount = 0;
                    string s1;
                    while ((s1 = srMyfile.ReadLine()) != null)
                    {
                        if (s1 != "")
                        {
                            if (sCount >= sIndex && sCount < eIndex)
                            {
                                str.ListTxtMessage.Add(new TxtMessage() { Message = s1 });
                            }
                            sCount++;
                        }
                    }
                    str.Rows = sCount;
                }
                catch { }
                finally
                {
                    srMyfile.Close();
                    fs.Close();
                }
            }
            else
            {
                TxtMessage t = new TxtMessage();
                if (lType == ReadLogDataType.DTU)
                {
                    t.Message = string.Format("设备：{0}，{1}日调度记录不存在。", zdID, date.ToString("yyyyMMdd"));

                }
                else if (lType == ReadLogDataType.System)
                {
                    t.Message = string.Format("{0}月系统信息日志记录不存在。", date.ToString("yyyyMM"));

                }
                else if (lType == ReadLogDataType.OneMeterData)
                {
                    t.Message = string.Format("表：{0},{1}日志记录不存在。", mac, date.ToString("yyyyMMdd"));

                }
                str.Rows = 1;
                str.ListTxtMessage.Add(t);

            }
            return str;
        }
    }

    internal class LogStreamWriter : StreamWriter
    {
        string dtuID;

        public string DtuID
        {
            get { return dtuID; }
            set { dtuID = value; }
        }
        string mac;

        public string MAC
        {
            get { return mac; }
            set { mac = value; }
        }



        string dataPointID;

        public string DataPointID
        {
            get { return dataPointID; }
            set { dataPointID = value; }
        }

        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        private DateTime timeSign;
        /// <summary>
        /// 获取标记文件写入的有效时间
        /// </summary>
        public DateTime TimeSign
        {
            get { return this.timeSign; }
        }

        public LogStreamWriter(string dtuID, string dataPointID, string path)
            : base(path, true, Encoding.UTF8)
        {
            this.dtuID = dtuID;
            this.dataPointID = dataPointID;
            this.path = path;
            DateTime now = DateTime.Now;
            this.timeSign = new DateTime(now.Year, now.Month, now.Day);
            this.timeSign = TimeSign.AddDays(1);
        }
        public LogStreamWriter(string dtuID, string path, int fileSpace)
            : base(path, true, Encoding.UTF8)
        {
            this.dtuID = dtuID;
            this.path = path;
            DateTime now = DateTime.Now;
            this.timeSign = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 1);
            this.timeSign = TimeSign.AddMinutes(fileSpace);
        }

        public LogStreamWriter(string path)
            : base(path, true, Encoding.UTF8)
        {

            this.path = path;
            DateTime now = DateTime.Now;
            this.timeSign = new DateTime(now.Year, now.Month, now.Day, now.Hour, 1, 1);
            this.timeSign = TimeSign.AddHours(1);
        }
        public LogStreamWriter(string path, string mac)
            : base(path, true, Encoding.UTF8)
        {

            this.path = path;
            this.mac = mac;
            DateTime now = DateTime.Now;
            this.timeSign = new DateTime(now.Year, now.Month, now.Day);
            this.timeSign = TimeSign.AddDays(1);
        }

        public override void Close()
        {
            try
            {
                base.Close();
            }
            catch { }
        }
    }
}
