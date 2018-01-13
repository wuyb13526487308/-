using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 历史计量数据1
    /// </summary>
    public class DataItem_D120: DataItem
    {
        /// <summary>
        ///主站请求
        /// </summary>
        public DataItem_D120(byte ser,decimal ljGas,decimal ljMoney) 
        {
            this.dataUnits = new byte[0x0b];
            Set单元标识符();
            this.dataUnits[2] = ser;
            /*数据标识DI，序号SER，1月结算日累计用量（气量），1月结算日累计用量（金额）*/
            this.LJGas = ljGas;
            this.LJMoney = LJMoney;
        }

        protected virtual void Set单元标识符()
        {
            this.dataUnits[0] = 0xD1;
            this.dataUnits[1] = 0x20;
        }

        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_D120(byte[] data):base(data)
        {
          
        }
        public DataItem_D120()
        {
            this.dataUnits = new byte[0x0b];
            Set单元标识符();
        }
        public decimal LJGas
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
        public decimal LJMoney
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
    }

    public class DataItem_ReadHistoryData : DataItem
    {
        public DataItem_ReadHistoryData(IdentityCode identityCode,byte ser)
        {
            this.dataUnits = new byte[3];
            this.dataUnits[0] = (byte)((UInt16)identityCode >> 8);
            this.dataUnits[1] = (byte)(((UInt16)identityCode) & 0xff);
            this.dataUnits[2] = ser;
        }

        public DataItem_ReadHistoryData(byte[] buffer) : base(buffer) { }
    }
}
