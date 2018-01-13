using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 写价格表
    /// </summary>
    public class DataItem_A010 : DataItem
    {

        //数据标识DI，序号SER，阶梯气价控制标志(CT)，价格1，用量1，价格2，用量2，价格3，用量3，价格4，用量4，价格5，启用日期
        public CT CT
        {
            get
            {
                return new CT(this.dataUnits[3]);
            }
            set
            {
                this.dataUnits[3] = value.GetByte();
            }
        }

        public decimal Price1
        {
            get
            {
                return MyDataConvert.TwoBCDToDecimal(this.dataUnits, 4);
            }
            set
            {
                MyDataConvert.DecimalToTwoBDC(value, this.dataUnits, 4);
            }
        }

        public decimal UseGas1
        {
            get { return MyDataConvert.ThreeBCDToDecimal(this.dataUnits, 6); }
            set
            {
                MyDataConvert.DecimalToThreeBDC(value, this.dataUnits, 6);
            }
        }


        public decimal Price2
        {
            get
            {
                return MyDataConvert.TwoBCDToDecimal(this.dataUnits, 9);
            }
            set
            {
                MyDataConvert.DecimalToTwoBDC(value, this.dataUnits, 9);
            }
        }

        public decimal UseGas2
        {
            get
            {
                return MyDataConvert.ThreeBCDToDecimal(this.dataUnits, 11);
            }
            set
            {
                MyDataConvert.DecimalToThreeBDC(value, this.dataUnits, 11);
            }
        }


        public decimal Price3
        {
            get
            {
                return MyDataConvert.TwoBCDToDecimal(this.dataUnits, 14);
            }
            set
            {
                MyDataConvert.DecimalToTwoBDC(value, this.dataUnits, 14);
            }
        }

        public decimal UseGas3
        {
            get
            {
                return MyDataConvert.ThreeBCDToDecimal(this.dataUnits, 16);
            }
            set
            {
                MyDataConvert.DecimalToThreeBDC(value, this.dataUnits, 16);
            }
        }


        public decimal Price4
        {
            get
            {
                return MyDataConvert.TwoBCDToDecimal(this.dataUnits, 19);
            }
            set
            {
                MyDataConvert.DecimalToTwoBDC(value, this.dataUnits, 19);
            }
        }

        public decimal UseGas4
        {
            get
            {
                return MyDataConvert.ThreeBCDToDecimal(this.dataUnits, 21);
            }
            set
            {
                MyDataConvert.DecimalToThreeBDC(value, this.dataUnits, 21);
            }
        }


        public decimal Price5
        {
            get
            {
                return MyDataConvert.TwoBCDToDecimal(this.dataUnits, 24);
            }
            set
            {
                MyDataConvert.DecimalToTwoBDC(value, this.dataUnits, 24);
            }
        }

        public DateTime  StartDate {
            get
            {
                return Convert.ToDateTime(string.Format ("20{0}-{1}-{2}", 
                    string.Format("{0:X2}", this.dataUnits[28]).PadLeft (2,'0'), 
                   (string.Format("{0:X2}", this.dataUnits[27])), 
                   (string.Format("{0:X2}", this.dataUnits[26]))));
            }
            set
            {
                this.dataUnits[26] = BCD.I2B (value.Day);
                this.dataUnits[27] = BCD.I2B(value.Month);
                this.dataUnits[28] = BCD.S2B(value.Year.ToString().Substring(2,2));
            }
        }


        /// <summary>
        ///主站请求
        /// </summary>
        public DataItem_A010(byte ser, CT CT, DateTime startDate, decimal price1 = 0, int useGas1 = 0
            , decimal price2 = 0, int useGas2 = 0
             , decimal price3 = 0, int useGas3 = 0
             , decimal price4 = 0, int useGas4 = 0
             , decimal price5=0
            ) 
        {
            this.dataUnits = new byte[0x1D];
            this.dataUnits[0] = 0xA0;
            this.dataUnits[1] = 0x10;
            /*数据标识DI，序号SER，阶梯气价控制标志(CT)，价格1，用量1，价格2，用量2，价格3，用量3，价格4，用量4，价格5，启用日期*/
            this.dataUnits[2] = ser;

            this.CT = CT;
            this.Price1 = price1;
            this.UseGas1 = useGas1;

            this.Price2 = price2;
            this.UseGas2 = useGas2;

            this.Price3 = price3;
            this.UseGas3 = useGas3;

            this.Price4 = price4;
            this.UseGas4 = useGas4;

            this.Price5 = price5;
            this.StartDate = startDate;
        }



        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_A010(byte[] data):base(data)
        {
        }       
    }

    public class DataItem_A010_Answer : DataItem
    {
        public DataItem_A010_Answer(byte ser, ST1 st1)
        {
            this.dataUnits = new byte[5];
            this.dataUnits[0] = 0xA0;
            this.dataUnits[1] = 0x10;
            /*数据标识DI，序号SER，阶梯气价控制标志(CT)，价格1，用量1，价格2，用量2，价格3，用量3，价格4，用量4，价格5，启用日期*/
            this.dataUnits[2] = ser;
            this.ST1 = st1;
        }

        public DataItem_A010_Answer(byte[] buffer):base(buffer)
        {            
        }

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
    }
}
