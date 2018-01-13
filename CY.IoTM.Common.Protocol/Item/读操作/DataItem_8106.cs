using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{

    /// <summary>
    /// 读密钥版本号
    /// </summary>
    public class DataItem_8106:DataItem
    {
        /// <summary>
         /// 密钥版本号VER
        /// </summary>
         public byte KeyVer 
        {
            get
            {
                return this.dataUnits[2];
            }
            set
            {
                this.dataUnits[2] = value;
            }        
        }

        /// <summary>
        ///主站请求
        /// </summary>
        public DataItem_8106(byte ser,byte keyVer) 
        {
            this.dataUnits = new byte[0x04];

            this.dataUnits[0] = 0x81;
            this.dataUnits[1] = 0x06;
            this.dataUnits[2] = ser;

            KeyVer = keyVer;
        }

        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_8106(byte[] data):base(data)
        {

        }
    }
}
