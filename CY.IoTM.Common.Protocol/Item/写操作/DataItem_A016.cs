using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 写表底数
    /// </summary>
    public class DataItem_A016:DataItem
    {
        /// <summary>
        /// 表底数
        /// </summary>
        public decimal MeterNum
        {
            get
            {
                return MyDataConvert.FourBCDToDecimal(this.dataUnits, 3);

            }
            set
            {
                MyDataConvert.DecimalToFourBCD(value, this.dataUnits, 3);
            }
        }


        public DataItem_A016(byte ser,int gas) 
        {
            this.dataUnits = new byte[7];
            this.dataUnits[0] = 0xA0;
            this.dataUnits[1] = 0x16;
            this.dataUnits[2] = ser;
            this.MeterNum = gas;
        }

        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_A016(byte[] data):base(data)
        {
        }
    }

    public class DataItem_A016_ASK : DataItem_Answer
    {
        public ST1 ST1
        {
            get
            {
                return new ST1() { ST_0 = this.dataUnits[3], ST_1 = this.dataUnits[4] };
            }
            set
            {
                this.dataUnits[3] = value.ST_0;
                this.dataUnits[4] = value.ST_1;
            }
        }

        public ST2 ST2
        {
            get
            {
                return new ST2() { ST = this.dataUnits[5] };
            }
            set
            {
                this.dataUnits[5] = value.ST;
            }
        }       
        public DataItem_A016_ASK(byte ser, ST1 st1, ST2 st2):base(ser)
        {
            this.ST1 = st1;
            this.ST2 = st2;
        }
        protected override void Init()
        {
            this.dataUnits = new byte[6];
            this.dataUnits[0] = 0xA0;
            this.dataUnits[1] = 0x16;
        }
    }
}
