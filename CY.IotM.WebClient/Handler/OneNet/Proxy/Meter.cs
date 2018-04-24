using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneNETDataReceiver.Proxy
{
    public class Meter 
    {
        /// <summary>
        /// oneNet 系统中的DeviceID
        /// </summary>
        public string DeviceID { get; set; }
        /// <summary>
        /// 燃气表在关系数据库中的索引号
        /// </summary>
        public Int64 MeterID { get; set; }
        /// <summary>
        /// 燃气表地址
        /// </summary>
        public string Mac { get; set; }
        /// <summary>
        /// 通信密钥：用于加密物联网表和后台的通信数据，由{color:red}0~9和A~F{color}之间的16个字符组成表出厂时默认的密钥为：16个8
        /// </summary>
        public string Key { get; set; } = "8888888888888888";
        /// <summary>
        /// 旧密钥
        /// </summary>
        public string OldKey { get; set; }
        /// <summary>
        /// 00 气量表 01 金额表
        /// </summary>
        public string MeterType { get; set; }
    }

    class Msg
    {
        public List<_Data> data { get; set; } = new List<_Data>();

    }

    class _Data
    {
        public string val { get; set; }
        public int res_id { get; set; }
    }
}