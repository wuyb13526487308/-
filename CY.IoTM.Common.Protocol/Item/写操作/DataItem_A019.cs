using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 出厂启用
    /// </summary>
    public class DataItem_A019:DataItem
    {
        public DataItem_A019(byte ser) 
        {
            this.dataUnits = new byte[3];
            this.dataUnits[0] = 0xA0;
            this.dataUnits[1] = 0x19;
            this.dataUnits[2] = ser;
        }

        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_A019(byte[] data):base(data)
        {
        }
    }


}
