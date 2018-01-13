using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Classes
{
    [DataContract]
    public class ReadDataInfo
    {
        [DataMember]
        public long _ID { get; set; }

        [DataMember]
        public System.Nullable<long> _MeterID { get; set; }

        [DataMember]
        public string _MeterNo { get; set; }

        [DataMember]
        public System.Nullable<int> _Ser { get; set; }

        [DataMember]
        public System.Nullable<decimal> _Gas { get; set; }

        [DataMember]
        public System.Nullable<System.DateTime> _ReadDate { get; set; }

        [DataMember]
        public System.Nullable<decimal> _RemainingAmount { get; set; }

        [DataMember]
        public System.Nullable<decimal> _LastTotal { get; set; }

        [DataMember]
        public string _ST1 { get; set; }

        [DataMember]
        public string _ST2 { get; set; }
    }
}
