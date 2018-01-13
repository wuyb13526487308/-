using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CY.IotM.Common
{
    [DataContract]
    [Serializable]
    public class CompanyReport
    {

        [DataMember]
        public string MenuCode { get; set; }
        [DataMember]
        public string CompanyID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string RID { get; set; }

    }
}
