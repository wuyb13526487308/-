using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 换表指令
    /// </summary>
    public class DataItem_C107 : DataItem
    {
        public DataItem_C107(byte ser,decimal ljMoney,decimal syMoney,decimal ljGas)
        {
            this.dataUnits = new byte[15];
            this.dataUnits[0] = 0xC1;
            this.dataUnits[1] = 0x07;
            /*数据标识DI，序号SER，当前累计用量（金额），当前剩余用量（气量/金额），本阶梯周期已使用累计用量（气量）*/
            this.dataUnits[2] = ser;
            this.LJMoney = ljMoney;
            this.SYMoney = syMoney;
            this.LJGas = ljGas;

        }
        public DataItem_C107(byte[] data)
            : base(data)
        {
        }

        /// <summary>
        /// 当前累计用量（金额）
        /// </summary>
        public decimal LJMoney
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
        /// <summary>
        /// 当前剩余金额
        /// </summary>
        public decimal SYMoney
        {
            get
            {
                return MyDataConvert.FourBCDToDecimal(this.dataUnits, 7);
            }
            set
            {
                MyDataConvert.DecimalToFourBCD(value, this.dataUnits, 7);
            }
        }

        /// <summary>
        /// 本阶梯周期已使用累计用量
        /// </summary>
        public decimal LJGas
        {
            get
            {
                return MyDataConvert.FourBCDToDecimal(this.dataUnits, 11);
            }
            set
            {
                MyDataConvert.DecimalToFourBCD(value, this.dataUnits, 11);
            }
        }
    }

    public class DataItem_C107_Answer : DataItem_Answer
    {
        public DataItem_C107_Answer(byte ser) : base(ser) { }
        protected override void Init()
        {
            base.Init();
            this.dataUnits[0] = 0xC1;
            this.dataUnits[1] = 0x07;
        }
    }
}
