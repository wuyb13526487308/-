using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CY.IoTM.OlbCommon
{
    public class GasFeeBill
    {

        public string UserID { get; set; }
        public string UserName { get; set; }
        public DateTime ChaoBiaoTime { get; set; }
        public decimal LastNum { get; set; }
        public decimal ThisNum { get; set; }
        public decimal GasNum { get; set; }
        public decimal GasFee { get; set; }


    }
}
