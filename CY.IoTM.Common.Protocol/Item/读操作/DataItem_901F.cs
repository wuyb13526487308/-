using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{

    /// <summary>
    /// 读计量数据
    /// </summary>
    public class DataItem_901F_ANSWER:DataItem
    {
        public DataItem_901F_ANSWER(byte ser, DateTime readDate, decimal ljGas, decimal ljMoney, decimal syMoney, decimal lastLJGas, ST1 st1, ST2 st2) 
        {
            /*数据标识DI，序号SER，实时时间，当前累计用量（气量），当前累计用量（金额），当前剩余气量（气量/金额），结算日累计用量（气量/金额），状态ST1、ST2*/
            this.dataUnits = new byte[0x1d];

            this.dataUnits[0] = 0x90;
            this.dataUnits[1] = 0x1f;
            this.dataUnits[2] = ser;
            this.ReadDate = readDate;
            this.LJGas = ljGas;
            this.LJMoney = ljMoney;
            this.SYMoney = syMoney;
            this.LastLJGas = lastLJGas;
            this.ST1 = st1;
            this.ST2 = st2;
        }

        public DataItem_901F_ANSWER(byte[] data):base(data)
        {
        }
        /// <summary>
        /// 获取或设置抄表时间
        /// </summary>
        public DateTime ReadDate
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

        /// <summary>
        /// 获取或设置累计用气量
        /// </summary>
        public decimal LJGas
        {
            get
            {
                return MyDataConvert.FourBCDToDecimal(this.dataUnits, 10);
            }
            set
            {
                MyDataConvert.DecimalToFourBCD(value, this.dataUnits, 10);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal LJMoney
        {
            get
            {
                return MyDataConvert.FourBCDToDecimal(this.dataUnits, 14);

            }
            set
            {
                MyDataConvert.DecimalToFourBCD(value, this.dataUnits, 14);

            }
        }

        public decimal SYMoney
        {
            get
            {
                return MyDataConvert.FourBCDToDecimal(this.dataUnits, 18);

            }
            set
            {
                MyDataConvert.DecimalToFourBCD(value, this.dataUnits, 18);

            }
        }

        public decimal LastLJGas
        {
            get
            {
                return MyDataConvert.FourBCDToDecimal(this.dataUnits, 22);

            }
            set
            {
                MyDataConvert.DecimalToFourBCD(value, this.dataUnits, 22);
            }
        }

        public ST1 ST1
        {
            get
            {
                return new ST1() { ST_0 = this.dataUnits[27], ST_1 = this.dataUnits[26] };
            }
            set
            {
                this.dataUnits[31] = value.ST_0;
                this.dataUnits[32] = value.ST_1;
            }
        }

        public ST2 ST2
        {
            get
            {
                return new ST2() { ST = this.dataUnits[28] };
            }
            set
            {
                this.dataUnits[29] = value.ST;
            }
        }       
    }

    public class DataItem_901F_ASK : DataItem
    {
        public DataItem_901F_ASK(byte[] buffer)
            : base(buffer)
        {
        }
        public DataItem_901F_ASK(byte ser)
        {
            Init();
            this.dataUnits[2] = ser;
        }

        protected virtual void Init()
        {
            this.dataUnits = new byte[3];
            this.dataUnits[0] = 0x90;
            this.dataUnits[1] = 0x1F;
        }
    }
}
