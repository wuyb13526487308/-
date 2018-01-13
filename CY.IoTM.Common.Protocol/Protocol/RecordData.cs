using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CY.IoTM.Common
{
    public class RecordData
    {

        public String LoTMac  {get;set;}
        public ControlCode ControlCode { get; set; }
        public IdentityCode IdentityCode { get; set; }
        public DataItem Data { get; set; }
        public DateTime RevTime { get; set; }
        public RecordData()
        {

        }
    }


}
