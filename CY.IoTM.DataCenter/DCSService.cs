using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using CY.IoTM.Service.Common;
using CY.IoTM.Common.Classes;
using CY.IoTM.DataTransmitHelper.Factory;
using CY.IoTM.Common.Log;

namespace CY.IoTM.DataCenter
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,
        ConcurrencyMode = ConcurrencyMode.Single)]
    public class DCSService : IDCSService
    {
        /// <summary>
        /// 服务器端（数据中心）回调数据采集服务器的接口
        /// </summary>
        IDCSClient _iDCSClient;

        public IDCSClient getIDCSClient
        {
            get { return _iDCSClient; }
        }
        /// <summary>
        /// 服务器ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 数据采集服务器名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 数据采集服务器IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 数据采集服务器端口号
        /// </summary>
        public string Port { get; set; }

        #region IDCSService 成员

        public void Register(string dscID, string dscName, string dscIP, string dscPort)
        {
            this.ID = dscID;
            this.Name = dscName;
            this.IP = dscIP;
            this.Port = dscPort;
            _iDCSClient = OperationContext.Current.GetCallbackChannel<IDCSClient>();
            DCSRegister.getInstance().CheckDCSService();
            DCSService dcs = DCSRegister.getInstance().getDcsSevice(dscID);
            if (dcs != null)
            {
                if (this.Equals(dcs))
                {
                    Console.WriteLine("本次注册的对象已存在");
                }
                else
                {
                    Console.WriteLine("本次注册的对象不同。");
                }
            }
            DCSRegister.getInstance().AddDCS(this);
        }

        public void Heart(string dscID)
        {
            Console.Write("DC心跳检查{0} - {1}\r", dscID, DateTime.Now);
        }
        public bool Check()
        {
            try
            {
                _iDCSClient.Test();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void UnRegister(string dscID)
        {
            string tmp = DCSRegister.getInstance().ReMoveDcsService(dscID);
            if (tmp != "")
                Console.WriteLine("移除服务器{0}注册信息失败;", dscID);

        }
        #endregion


        #region  向数据中心  数据队列中添加数据
        /// <summary>
        /// 向数据中心  数据队列中添加数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="e"></param>
        public void InsertDataRecord(DataType type,DataArge e)
        {
            if (e.Data != null)
            {
                try
                {
                    switch (type)
                    {
                        case DataType.ReadData:
                            DataRecordQueueFactory.getInstance().AddWork((ReadDataInfo)e.Data);
                            Log.getInstance().Write(MsgType.Information, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}数据中心接收到{type}类型的数据，表号:{((ReadDataInfo)e.Data)._MeterNo} 数据采集时间：{((ReadDataInfo)e.Data)._ReadDate} ST1:{((ReadDataInfo)e.Data)._ST1} ST2:{((ReadDataInfo)e.Data)._ST2}");
                            break;
                        case DataType.WarningData:
                            DataRecordQueueFactory.getInstance().AddWork((WarningInfo)e.Data);
                            Log.getInstance().Write(MsgType.Information, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}数据中心接收到{type}类型的数据，表号:{((WarningInfo)e.Data).meterNo} 数据采集时间：{((WarningInfo)e.Data).readDate} ST1:{((WarningInfo)e.Data).st1} ST2:{((WarningInfo)e.Data).st2}");

                            break;
                        default:
                            break;
                    }
                }
                catch (Exception er)
                {
                    //异常记录
                    Log.getInstance().Write(er, MsgType.Error);
                }
            }
        }
        #endregion  向数据中心  数据队列中添加数据
    }

    public class DCSInfo
    {
        private DCSService _dcsService;

        /// <summary>
        /// 获取或设置数据采集服务器和数据中心双向通讯接口对象。
        /// </summary>
        public DCSService DcsService
        {
            get { return _dcsService; }
            set { _dcsService = value; }
        }
        /// <summary>
        /// 获取或设置数据采集服务器是否已成功注册
        /// </summary>
        public bool IsRegister { get; set; }
        private string id;
        /// <summary>
        /// 服务器ID
        /// </summary>
        public string ID { get { return id; } }
        /// <summary>
        /// 数据采集服务器名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 数据采集服务器IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 数据采集服务器端口号
        /// </summary>
        public string Port { get; set; }

        public DCSInfo(string dcsID)
        {
            this.id = dcsID;
        }
    }
}
