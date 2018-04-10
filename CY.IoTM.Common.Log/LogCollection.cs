using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CY.IoTM.Common.Log
{
    [DataContract]
    public class LogCollection
    {
        /// <summary>
        /// 总行数
        /// </summary>
        [DataMember]
        public int Rows { get; set; }
        /// <summary>
        /// 信息集合
        /// </summary>
        [DataMember]
        public List<TxtMessage> ListTxtMessage { get; set; }
    }
    [DataContract]
    public class TxtMessage
    {
        /// <summary>
        /// 文本信息
        /// </summary>
        [DataMember]
        public string Message { get; set; }
    }
}
