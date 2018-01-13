using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 修正表数据   
    /// </summary>
    public class DataItem_C102 : DataItem
    {
        /*数据标识DI，序号SER，当前累计用量（金额），当前剩余用量（气量/金额），
         * 结算日期累计用量（气量）（未运行一个阶梯周期的表具，下发/修正安装时的累计用量即表计数轮数值）*/

        /// <summary>
        /// 修正表数据 构造函数
        /// </summary>
        /// <param name="ser">序号SER</param>
        /// <param name="currentLJMoney">当前累计金额</param>
        /// <param name="currentSYMoney">当前剩余金额</param>
        /// <param name="settlementDayLJGas">结算日期累计表底</param>
        public DataItem_C102(byte ser, decimal currentLJMoney, decimal currentSYMoney, decimal settlementDayLJGas)
        {
            this.dataUnits = new byte[15];
            this.dataUnits[0] = 0xC1;
            this.dataUnits[1] = 0x02;
            this.dataUnits[2] = ser;
            this.CurrentLJMoney = currentLJMoney;
            this.CurrentDaySYMoney = currentSYMoney;
            this.SettlementDayLJGas = settlementDayLJGas;

        }
        //表指令待定

        public DataItem_C102(byte[] data) : base(data) { }

        /// <summary>
        /// 当前累计金额
        /// </summary>
        public decimal CurrentLJMoney
        {
            get{
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
        public decimal CurrentDaySYMoney
        {
            get
            {
                return MyDataConvert.FourBCDToDecimal_Symbol(this.dataUnits, 7);
            }
            set
            {
                MyDataConvert.DecimalToFourBCD_Symbol(value, this.dataUnits, 7);
            }
        }
        /// <summary>
        /// 结算日期累计用量（表底）
        /// </summary>
        public decimal SettlementDayLJGas
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

    public class DataItem_C102_Answer : DataItem_Answer
    {
        public DataItem_C102_Answer(byte ser) : base(ser) { }
        protected override void Init()
        {
            base.Init();
            this.dataUnits[0] = 0xC1;
            this.dataUnits[1] = 0x02;
        }
    }
}
