using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CY.IoTM.Common.Item
{
    public class DataItem_Answer : DataItem
    {
        public DataItem_Answer(byte[] buffer)
            : base(buffer)
        {
        }
        public DataItem_Answer(byte ser)
        {
            Init();
            this.dataUnits[2] = ser;
        }
        public DataItem_Answer(byte ser, IdentityCode identityCode)
        {
            Init();
            this.dataUnits[2] = ser;
            this.dataUnits[0] = (byte)(((ushort)identityCode) >> 8);
            this.dataUnits[1] = (byte)(((ushort)identityCode)&0xff);
        }

        protected virtual void Init()
        {
            this.dataUnits = new byte[3];
        }
    }
}
