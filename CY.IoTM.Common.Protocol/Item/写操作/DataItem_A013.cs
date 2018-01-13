using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 写购入金额
    /// </summary>
    public class DataItem_A013 : DataItem
    {
        /// <summary>
        /// 本次购入序号
        /// </summary>
        public int BuyIndex
        {
            get
            {
                return BCD.S2B(string.Format("{0:X2}", this.dataUnits[3]));
            }
            set
            {
                this.dataUnits[3] = BCD.I2B(value);
            }
        }


        /// <summary>
        /// 本次购入金额
        /// </summary>
        public decimal BuyMoney
        {
            get
            {
                return MyDataConvert.FourBCDToDecimal(this.dataUnits, 4);

            }
            set
            {
                MyDataConvert.DecimalToFourBCD(value, this.dataUnits, 4);

            }
        }

        public DataItem_A013(byte ser,int buyIndex, decimal buyMoney) 
        {
            this.dataUnits = new byte[8];
            this.dataUnits[0] = 0xa0;
            this.dataUnits[1] = 0x13;
            this.dataUnits[2] = ser;
            this.BuyIndex = buyIndex;
            this.BuyMoney = buyMoney;
        }

        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_A013(byte[] data):base(data){ }
    }

    public class DataItem_A013_ASK : DataItem_Answer
    {
        public DataItem_A013_ASK(byte ser, int buyIndex, decimal buyMoney):base(ser)
        {
            this.BuyIndex = buyIndex;
            this.BuyMoney = buyMoney;
        }

        protected override void Init()
        {
            this.dataUnits = new byte[8];
            this.dataUnits[0] = 0xa0;
            this.dataUnits[1] = 0x13;
        }

        /// <summary>
        /// 本次购入序号
        /// </summary>
        public int BuyIndex
        {
            get
            {
                return BCD.S2B(string.Format("{0:X2}", this.dataUnits[3]));
            }
            set
            {
                this.dataUnits[3] = BCD.I2B(value);
            }
        }


        /// <summary>
        /// 本次购入金额
        /// </summary>
        public decimal BuyMoney
        {
            get
            {
                return MyDataConvert.FourBCDToDecimal(this.dataUnits, 4);

            }
            set
            {
                MyDataConvert.DecimalToFourBCD(value, this.dataUnits, 4);

            }
        }
    }
}
