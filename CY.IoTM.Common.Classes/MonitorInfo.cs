using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
 

namespace CY.IoTM.Common.Classes
{
    [DataContract]
    /// <summary>
    /// 数据采集端监视概况
    /// </summary> 
    public class MonitorInfo
    {
        /// <summary>
        /// 表链接数量
        /// </summary>
        [DataMember]
        public int LinkCount { get; set; }

        /// <summary>
        /// 当前连接表mac地址集合
        /// </summary>
        [DataMember]
        public List<string> LinkMeterMacList { get; set; }

        /// <summary>
        /// CPU占用率
        /// </summary>
        [DataMember]
        public float Cpu { get; set; }

        /// <summary>
        /// 内存
        /// </summary>
        [DataMember]
        public float Memory { get; set; }
    }

    /// <summary>
    /// 数据采集服务器基本信息
    /// </summary>
    [DataContract]
    public class CJDInfo
    {
        [DataMember]
        /// <summary>
        /// 数据采集服务器编号
        /// </summary>
        public string ID { get; set; }

        [DataMember]
        /// <summary>
        /// 数据采集服务器名称
        /// </summary>
        public string Name { get; set; }

        [DataMember]
        /// <summary>
        /// 数据采集服务器IP
        /// </summary>
        public string IP { get; set; }

        [DataMember]
        /// <summary>
        /// 数据采集服务器端口号
        /// </summary>
        public string Port { get; set; }
    }
}
