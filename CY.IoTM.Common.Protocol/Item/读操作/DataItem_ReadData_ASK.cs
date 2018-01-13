using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CY.IoTM.Common.Item.读操作
{
    /// <summary>
    /// 读数据请求帧
    /// </summary>
    public class DataItem_ReadData_ASK:DataItem
    {
        public DataItem_ReadData_ASK(byte[] data)
            : base(data)
        {

        }

        public DataItem_ReadData_ASK(IdentityCode identityCode, byte ser)
        {
            this.dataUnits = new byte[3];
            this.dataUnits[0] = (byte)((UInt16)identityCode >> 8);
            this.dataUnits[1] = (byte)(((UInt16)identityCode) & 0xff);
            this.dataUnits[2] = ser;
        }
    }
}
