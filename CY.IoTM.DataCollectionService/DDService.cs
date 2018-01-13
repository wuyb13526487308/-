using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using CY.IoTM.Common;
using CY.IoTM.Service.Common;
using CY.IoTM.Channel;
using CY.IoTM.Common.Classes;
using CY.IoTM.Common.Log;
using CY.IoTM.DataTransmitHelper.Factory;

namespace CY.IoTM.DataCollectionService
{
    /// <summary>
    /// 调度服务器类
    /// </summary>
    [DataContract()]
    public class DDService : IDCSClient
    {
        private static DDService _ddService;
        private static readonly object _object = new object();
        /// <summary>
        /// 系统是否已初始化成功，true 成功
        /// </summary>
        private bool isInit = false;

        /// <summary>
        /// 表示是否向数据中心注册成功，true 注册成功 false 注册失败
        /// </summary>
        private bool isRegister = false;

        /// <summary>
        /// 获取服务器是否向数据中心注册成功,true 注册成功 false 注册失败
        /// </summary>
        [DataMember]
        public bool IsRegister
        {
            get { return isRegister; }
            set { }
        }

        private string serviceNo;
        /// <summary>
        /// 获取服务器编号
        /// </summary>
        [DataMember]
        public string DASServiceNo
        {
            get
            {
                return serviceNo;
            }
            set { this.serviceNo = value; }

        }

        private string serviceName;
        /// <summary>
        /// 获取服务名称
        /// </summary>
        [DataMember]
        public string DASServiceName
        {
            get
            {
                return this.serviceName;
            }
            set { this.serviceName = value; }

        }
        /// <summary>
        /// 采集服务器IP
        /// </summary>
        private string serviceIP;
        /// <summary>
        /// 采集服务器端口号
        /// </summary>
        private string servicePort;
        /// <summary>
        /// 数据中心服务接口对象
        /// </summary>
        WCFServiceProxy<IDCSService> _iDCSServiceProxy;

        private Thread myThread;
        private bool isChecked = true;
        private InstanceContext contxt;

        /// <summary>
        /// 构造调度服务器对象
        /// </summary>
        private DDService()
        {
            //读取服务器基本配置信息
            this.serviceNo = System.Configuration.ConfigurationManager.AppSettings["CJServiceNo"]; //"0001";            
            this.serviceName = System.Configuration.ConfigurationManager.AppSettings["serviceName"];// "数据采集服务器";
            this.serviceIP = System.Configuration.ConfigurationManager.AppSettings["CJServiceIP"];//IP
            this.servicePort = System.Configuration.ConfigurationManager.AppSettings["CJServicePort"];//Port
            
            //向数据中心注册服务器
            contxt = new InstanceContext(this);
            _iDCSServiceProxy = new WCFServiceProxy<IDCSService>(contxt);
          


            this.isRegister = Register();
            this.myThread = new Thread(Heart);
            this.myThread.IsBackground = true;
            this.myThread.Start();
        }
        private bool Register()
        {
            try
            {
                _iDCSServiceProxy.getChannel.Register(this.serviceNo, this.serviceName, this.serviceIP, this.servicePort);
                Log.getInstance().Write(MsgType.Information, string.Format("服务器：{0}-{1} 注册成功", serviceNo, serviceName));
                return true;
            }
            catch (Exception e)
            {
                _iDCSServiceProxy = new WCFServiceProxy<IDCSService>(contxt);
                Log.getInstance().Write(MsgType.Error, "注册服务失败,原因：" + e.Message);
                return false;
            }
        }
        private void Heart()
        {
            //调度服务到数据中心的注册维护。
            while (isChecked)
            {
                Thread.Sleep(1000 * 5);
                if (this.isRegister)
                {
                    try
                    {
                       
                        this._iDCSServiceProxy.getChannel.Heart(this.serviceNo);
                    }
                    catch
                    {
                        isRegister = false;
                        contxt = new InstanceContext(this);
                        if (_iDCSServiceProxy != null)
                        {
                            try
                            {
                                if (_iDCSServiceProxy.State != System.ServiceModel.CommunicationState.Faulted)
                                {
                                    _iDCSServiceProxy.Close();
                                }
                            }
                            catch
                            {
                                _iDCSServiceProxy.Abort();
                            }
                        }
                        _iDCSServiceProxy = new WCFServiceProxy<IDCSService>(contxt);
                    }
                }
                else
                {
                    this.isRegister = Register();
                }
            }
        }

        /// <summary>
        /// 获取调度服务器对象
        /// </summary>
        /// <returns></returns>
        public static DDService getInstance()
        {
            if (_ddService == null)
            {
                lock (_object)
                {
                    if (_ddService == null)
                        _ddService = new DDService();
                }
            }
            return _ddService;
        }

        /// <summary>
        /// 注销服务
        /// </summary>
        public void UnRegister()
        {
            if (_iDCSServiceProxy != null)
            {
                try
                {
                    _iDCSServiceProxy.getChannel.UnRegister(serviceNo);
                    Log.getInstance().Write(MsgType.Information, "注销服务成功");
                }
                catch (Exception e)
                {
                    Log.getInstance().Write(MsgType.Error, "注销服务失败,原因：" + e.Message);
                }
            }
        }

        #region IDCSClient 成员

        public void Test()
        {
            //数据中心心跳检测
            Console.Write("DD心跳检查{0}-{1}\r", this.serviceNo, DateTime.Now);
        }

        #endregion   

        #region 获取监视信息
        CSystemInfo systeminfo = new CSystemInfo();
        /// <summary>
        /// 获取监视情况
        /// </summary>
        /// <returns></returns>
        public DataArge GetMonitorInfo()
        {
            //表连接个数
            int linkCount = 0;
            ICreateDataChannel[] dataChannel = DataChannelFactoryService.getInstance().DataChannelFactory;            
            for (int i = 0; i < dataChannel.Length; i++)
            {
                linkCount += dataChannel[i].GetConectionCount();
            } 
            const int GB_DIV = 1024 * 1024 * 1024;
            float _tmp = 0;//内存G
            MonitorInfo monitorinfo = new MonitorInfo();
            if (systeminfo != null)
            {
                //使用物理内存
                _tmp = (systeminfo.PhysicalMemory - systeminfo.MemoryAvailable) / (float)GB_DIV;
                monitorinfo.LinkCount = linkCount;
                monitorinfo.Cpu = float.Parse(systeminfo.CpuLoad.ToString("#0.00"));
                monitorinfo.Memory = float.Parse(_tmp.ToString("#0.00"));
            }
            else
            {
                monitorinfo.LinkCount = 0;
                monitorinfo.Cpu = 0;
                monitorinfo.Memory = 0;
            }
            return new DataArge(DataType.MonitorData,monitorinfo);
        }
        #endregion 获取监视信息    
        
        #region 向业务服务器  数据中心队列中添加数据
        /// <summary>
        /// 向业务服务器  数据中心队列中添加数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="e"></param>
        public void InsertDataRecord(DataArge e)
        {
            if (_iDCSServiceProxy != null)
            {
                try
                {
                    //向业务服务器 数据队列中添加数据
                    if(e.DataType == DataType.ReadData)
                        Log.getInstance().Write(MsgType.Information, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}上报采集记录到数据中心,DataType:{e.DataType} MeterNo:{((ReadDataInfo)e.Data)._MeterNo} ReadDate:{((ReadDataInfo)e.Data)._ReadDate}");
                    if(e.DataType == DataType.WarningData)
                        Log.getInstance().Write(MsgType.Information, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}上报采集记录到数据中心,DataType:{e.DataType} MeterNo:{((WarningInfo)e.Data).meterNo} ReadDate:{((WarningInfo)e.Data).readDate} st1:{((WarningInfo)e.Data).st1}  st2:{((WarningInfo)e.Data).st2}");

                    _iDCSServiceProxy.getChannel.InsertDataRecord(e.DataType, e);
                }
                catch(Exception er)
                {
                    //异常记录
                    Log.getInstance().Write(er,MsgType.Error);
                }
            }
        }
        #endregion 向业务服务器  数据中心队列中添加数据

        #region 获取日志文件
        /// <summary>
        /// 获取日志文件
        /// </summary>
        /// <param name="cjdID"></param>
        /// <param name="mac"></param>
        /// <param name="date"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="lType"></param>
        /// <returns></returns>
        public LogCollection getDCSLog(string cjdID, string mac, DateTime date, int pageNum, int pageSize, ReadLogDataType lType)
        {
            return Log.getInstance().ReadDPLog(cjdID, mac, date, pageNum, pageSize, lType);
        }
        #endregion  获取日志文件
    }
}
