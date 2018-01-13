using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CY.IoTM.OlbCommon
{
     [DataContract]
    public class GasUser
    {
        [DataMember]
        public string UserID {get;set;}
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string CompanyID { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public decimal Balance { get; set; }
        [DataMember]
        public string MeterNo { get; set; }
        [DataMember]
        public int MeterType { get; set; }
    }
}
