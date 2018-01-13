using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Classes
{
    [DataContract]
    public class WarningInfo
    {
        [DataMember]
        public long MeterID { get; set; }
        [DataMember]
        public string meterNo { get; set; }
        [DataMember]
        public DateTime readDate { get; set; }
        [DataMember]
        public string st1 { get; set; }
        [DataMember]
        public string st2 { get; set; }
    }
}
