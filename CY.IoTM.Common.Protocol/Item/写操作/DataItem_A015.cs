using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 写标准时间
    /// </summary>
    public class DataItem_A015 : DataItem
    {
        /// <summary>
        /// 实时时间
        /// </summary>
        public DateTime TimeNow
        {
            get
            {
                return MyDataConvert.SevenBCDToDateTime(this.dataUnits, 3);

            }
            set
            {
                MyDataConvert.DateTimeToSevenBCD(value, this.dataUnits, 3);
            }
        }
        public DataItem_A015(byte ser) 
        {
            this.dataUnits = new byte[10];
            this.dataUnits[0] = 0xa0;
            this.dataUnits[1] = 0x15;
            this.dataUnits[2] = ser;
            this.TimeNow = DateTime.Now;
        }

        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_A015(byte[] data):base(data)
        {
        }      
    }

    public class DataItem_A015_ASK : DataItem_Answer
    {
        public DataItem_A015_ASK(byte ser) : base(ser) { }
        protected override void Init()
        {
            base.Init();
            this.dataUnits[0] = 0xA0;
            this.dataUnits[1] = 0x15;
        }
    }
}
