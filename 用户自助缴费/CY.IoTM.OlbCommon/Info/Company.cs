using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CY.IoTM.OlbCommon
{
    [DataContract]
    public class Company
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        [DataMember]
        public string Id { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

    }
}
