using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CY.IoTM.OlbCommon
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public bool Result
        {
            get;
            set;
        }
        [DataMember]
        public string TxtMessage
        {
            get;
            set;
        }
    }
}
