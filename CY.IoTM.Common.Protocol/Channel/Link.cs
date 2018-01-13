using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IoTM.Channel;
using System.Configuration; 

namespace CY.IoTM.Common
{
    /// <summary>
    /// 通道物理层对象构建基类，用于创建数据通道物理层实现。
    /// </summary>
    public abstract class Link
    {
        /// <summary>
        /// 物理层连接表的MAC
        /// </summary>
        protected string _MAC;

        ///// <summary>
        ///// 指示设备物理连接通道是否可用 true表示物理连接可用
        ///// </summary>
        //public bool IsLonin { get; protected set; }

        ///// <summary>
        ///// 物理层通道连接
        ///// </summary>
        //protected virtual void Login()
        //{
        //}

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public virtual bool Send(byte[] buffer)
        {
            return true;
        }

        /// <summary>   
        /// 关闭物理数据通道   
        /// </summary>   
        public virtual void Close()
        { }

        /// <summary>
        /// 
        /// </summary>
        public List<IDataChannel> DataChannelList { get; set; }

        /// <summary>
        /// 记录通道接收的数据到日志系统
        /// </summary>
        /// <param name="buffer"></param>
        protected void RecordRevDataToLog(string meterAddr,byte[] buffer,string socketHandle ="")
        {
            string serviceNo = ConfigurationManager.AppSettings["CJServiceNo"];
            //Console.WriteLine(string.Format("接收到主站发送的请求帧：{0}\r", MyDataConvert.BytesToHexStr(buffer))); 
            Log.Log.getInstance().Write(new Log.DTULogMsg(serviceNo, meterAddr, Log.DUTDataType.R, buffer));
            Log.Log.getInstance().Write(new Log.OneMeterDataLogMsg(meterAddr, string.Format ("接收[{0}]-> ",socketHandle) + MyDataConvert.BytesToHexStr(buffer)));
            //Console.WriteLine(string.Format("接收到主站发送的请求帧：{0}\r", MyDataConvert.BytesToHexStr(buffer)));
        }
        

        /// <summary>
        /// 记录发送到通道的数据到日志系统
        /// </summary>
        /// <param name="buffer"></param>
        protected void RecordSendDataToLog(string meterAddr,byte[] buffer, string socketHandle)
        {
            string serviceNo = System.Configuration.ConfigurationManager.AppSettings["CJServiceNo"];
            Log.Log.getInstance().Write(new Log.DTULogMsg(serviceNo, meterAddr, Log.DUTDataType.S, buffer));
            Log.Log.getInstance().Write(new Log.OneMeterDataLogMsg(meterAddr, string.Format("发送[{0}]-> ", socketHandle) + MyDataConvert.BytesToHexStr(buffer)));

        }


    }
}
