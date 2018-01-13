using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CY.IoTM.Common.ADSystem
{
    [DataContract]
    public class ADPublisMeter
    {
        [DataMember]
        public long AP_ID { get; set; }
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string MeterNo { get; set; }
        [DataMember]
        public string CompanyID { get; set; }

    }
}