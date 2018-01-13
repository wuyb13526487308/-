using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 写结算日
    /// </summary>
    public class DataItem_A011 : DataItem
    {
        /// <summary>
        /// 结算日
        /// </summary>
        public int JieSuanDay
        {
            get
            {
                return Convert.ToUInt16(string.Format("{0:X2}", this.dataUnits[3]));
            }
            set
            {
                this.dataUnits[3] = BCD.I2B(value);
            }
        }

        public int JieSuanMonth
        {
            get
            {
                return Convert.ToUInt16(string.Format("{0:X2}", this.dataUnits[4]));
            }
            set
            {
                this.dataUnits[4] = BCD.I2B(value);
            }
        }

        public DataItem_A011(byte ser ,int JieSuanDay,int jieSuanMonth=1) 
        {
            this.dataUnits = new byte[5];
            this.dataUnits[0] = 0xA0;
            this.dataUnits[1] = 0x11;
            this.dataUnits[2] = ser;     
            this.JieSuanDay = JieSuanDay;
            this.JieSuanMonth = jieSuanMonth;
            
        }



        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_A011(byte[] data):base(data)
        {
        }
    }

    public class DataItem_A011_Answer : DataItem_Answer
    {
        public DataItem_A011_Answer(byte ser) : base(ser) { }

        protected override void Init()
        {
            base.Init();
            this.dataUnits[0] = 0xA0;
            this.dataUnits[1] = 0x11;
        }
    }
}
