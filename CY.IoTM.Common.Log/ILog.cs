using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CY.IoTM.Common.Log
{
    /// <summary>
    /// 日志底层处理接口
    /// </summary>
    public interface ILog
    {
        void Write(MsgType type, string logInfo);
    }


    public enum LogDataType
    {
        /// <summary>
        /// 数据采集点下发的指令
        /// </summary>
        DataPointCMD,
        /// <summary>
        /// 数据采集点接收到的数据
        /// </summary>
        DataPointData,
        /// <summary>
        /// 系统错误信息
        /// </summary>
        SystemErrorInfo,
        /// <summary>
        /// 历史监控信息
        /// </summary>
        HisMonitorInfo,
    }

    public enum ReadLogDataType
    {
        /// <summary>
        /// 单个DTU日志
        /// </summary>
        DTU = 0,
        /// <summary>
        /// 单个采集点日志
        /// </summary>
        DataPoint = 1,
        /// <summary>
        /// 系统日志
        /// </summary>
        System = 2,
        /// <summary>
        /// 单表信息
        /// </summary>
        OneMeterData = 3,
    }


    public enum DUTDataType
    {
        /// <summary>
        /// 发送数据
        /// </summary>
        S = 0,
        /// <summary>
        /// 接收数据
        /// </summary>
        R = 1

    }

    public struct DTULogMsg
    {
        /// <summary>
        /// 采集终端ID
        /// </summary>
        public string DTUID;

        /// <summary>
        /// 天然气表地址
        /// </summary>
        public string MeterMac;
        /// <summary>
        /// 要记录的采集点数据类型
        /// </summary>
        public DUTDataType DataType;
        /// <summary>
        /// 要记录的采集点数据。
        /// </summary>
        private byte[] Data;

        public DateTime LogDateTime;
        private string getHex(byte[] buffer)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
                sb.Append(string.Format(" {0:X2}", buffer[i]));
            return sb.ToString();
        }
        /// <summary>
        /// 获取文本
        /// </summary>
        public string Text
        {
            get
            {
                return string.Format("{0:yyyy-MM-dd HH:mm:ss}-> {1}:\t {2}\r", this.LogDateTime, this.DataType, getHex(Data));
            }
        }

        public DTULogMsg(string dtuID, string meterMac, DUTDataType type, byte[] data)
        {

            this.DTUID = dtuID;
            this.MeterMac = meterMac;
            this.DataType = type;
            this.Data = data;
            this.LogDateTime = DateTime.Now;
        }

    }

    /// <summary>
    /// 监视信息
    /// </summary>
    public struct MonitorLogMsg
    {
        public string cjdID;
        public string text;

        public MonitorLogMsg(string cjdID, string text)
        {

            this.cjdID = cjdID;
            this.text = text;
        }

    }
    /// <summary>
    /// 表上线到下线耗时信息
    /// </summary>
    public struct MeterLogMsg
    {

        public string text;
        public MeterLogMsg(string text)
        {

            this.text = text;
        }
    }
    /// <summary>
    /// 单表信息
    /// </summary>
    public struct OneMeterDataLogMsg
    {
        public string mac;
        public string text;
        public OneMeterDataLogMsg(string mac, string text)
        {
            this.mac = mac;
            this.text = string.Format ("{0:yyyy-MM-dd HH:mm:ss.fff} -> {1}", DateTime.Now, text);
        }
    }

    /// <summary>
    /// 日志消息类型的枚举
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 指示未知信息类型的日志记录
        /// </summary>
        Unknown,

        /// <summary>
        /// 指示普通信息类型的日志记录
        /// </summary>
        Information,

        /// <summary>
        /// 指示警告信息类型的日志记录
        /// </summary>
        Warning,

        /// <summary>
        /// 指示错误信息类型的日志记录
        /// </summary>
        Error,

        /// <summary>
        /// 指示成功信息类型的日志记录
        /// </summary>
        Success

    }
}
