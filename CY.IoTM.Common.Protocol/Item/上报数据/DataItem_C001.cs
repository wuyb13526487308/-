using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{

    /// <summary>
    /// 读计量数据
    /// </summary>
    [DataContract]
    public class DataItem_C001 : DataItem
    {

        //当前累计用量（气量），当前累计用量（金额），当前剩余气量（气量/金额） ，结算日累计用量（气量/金额），实时时间，状态ST1、ST2

        public DataItem_C001(byte[] data):base(data)
        { 
        }

        public DataItem_C001(byte ser,DateTime readDate,decimal ljGas,decimal ljMoney,decimal syMoney,decimal lastLJGas,ST1 st1,ST2 st2)
        {
            //数据标识DI（2），序号SER(1)，抄表时间(7)，当前累计用量（气量）(4)，当前累计用量（金额）(4)，当前剩余用量（气量/金额）(4)，最后一次结算日累计用量（气量/金额）(4)，结算日(1)，状态ST1(2)、ST2(1)
            this.dataUnits = new byte[0x1D];
            this.dataUnits[0] = 0xc0;
            this.dataUnits[1] = 0x01;
            this.dataUnits[2] = ser;
            this.ReadDate = readDate;
            this.LJGas = ljGas;
            this.LJMoney = ljMoney;
            this.SYMoney = syMoney;
            this.LastLJGas = lastLJGas;
            this.ST1 = st1;
            this.ST2 = st2;

        }
        /// <summary>
        /// 获取或设置抄表时间
        /// </summary>
        [DataMember]
        public DateTime ReadDate
        {
            get{
                //int year = Convert.ToInt32(string.Format ("{0:X2}{1:X2}",this.dataUnits[9],this.dataUnits[8]));
                //int month = Convert.ToInt32(string.Format("{0:X2}", this.dataUnits[7]));
                //int day = Convert.ToInt32(string.Format("{0:X2}", this.dataUnits[6]));
                //int hour = Convert.ToInt32(string.Format("{0:X2}", this.dataUnits[5]));
                //int minute = Convert.ToInt32(string.Format("{0:X2}", this.dataUnits[4]));
                //int second = Convert.ToInt32(string.Format("{0:X2}", this.dataUnits[3]));
                //return new DateTime(year, month, day, hour, minute, second);
                return MyDataConvert.SevenBCDToDateTime(this.dataUnits, 3);

            }
            set
            {
                //this.dataUnits[9] =(byte) Convert.ToInt32(value.Year.ToString().Substring(0, 2));//年高2位
                //this.dataUnits[8] = (byte)Convert.ToInt32(value.Year.ToString().Substring(2, 2));//年低2位
                //this.dataUnits[7] = (byte)Convert.ToInt32(value.Month.ToString().Substring(0, 2));//月份
                //this.dataUnits[6] = (byte)Convert.ToInt32(value.Day.ToString().Substring(0, 2));//日
                //this.dataUnits[5] = (byte)Convert.ToInt32(value.Hour.ToString().Substring(0, 2));//小时
                //this.dataUnits[4] = (byte)Convert.ToInt32(value.Minute.ToString().Substring(0, 2));//分钟
                //this.dataUnits[3] = (byte)Convert.ToInt32(value.Second.ToString().Substring(0, 2));//秒
                MyDataConvert.DateTimeToSevenBCD(value, this.dataUnits, 3);
            }
        }

        /// <summary>
        /// 获取或设置累计用气量
        /// </summary>
        [DataMember]
        public decimal LJGas
        {
            get
            {
                //StringBuilder sb = new StringBuilder();
                //sb.Append(string.Format("{0:X2}", this.dataUnits[13]));
                //sb.Append(string.Format("{0:X2}", this.dataUnits[12]));
                //sb.Append(string.Format("{0:X2}.", this.dataUnits[11]));
                //sb.Append(string.Format("{0:X2}", this.dataUnits[10]));
                //return Convert.ToDecimal(sb.ToString());
                return MyDataConvert.FourBCDToDecimal(this.dataUnits, 10);
            }
            set
            {
                //string strLJGas = Convert.ToInt64(value * 100).ToString().PadLeft(8, '0');
                //this.dataUnits[10] = (byte)Convert.ToInt32(strLJGas.Substring(6, 2));//
                //this.dataUnits[11] = (byte)Convert.ToInt32(strLJGas.Substring(4, 2));//
                //this.dataUnits[12] = (byte)Convert.ToInt32(strLJGas.Substring(2, 2));//
                //this.dataUnits[13] = (byte)Convert.ToInt32(strLJGas.Substring(0, 2));//
                MyDataConvert.DecimalToFourBCD(value, this.dataUnits, 10);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public decimal LJMoney
        {
            get
            {
                //StringBuilder sb = new StringBuilder();
                //sb.Append(string.Format("{0:X2}", this.dataUnits[17]));
                //sb.Append(string.Format("{0:X2}", this.dataUnits[16]));
                //sb.Append(string.Format("{0:X2}.", this.dataUnits[15]));
                //sb.Append(string.Format("{0:X2}", this.dataUnits[14]));
                //return Convert.ToDecimal(sb.ToString());
                return MyDataConvert.FourBCDToDecimal(this.dataUnits, 14);

            }
            set
            {
                //string strLJMoney = Convert.ToInt64(value * 100).ToString().PadLeft(8, '0');
                //this.dataUnits[14] = (byte)Convert.ToInt32(strLJMoney.Substring(6, 2));//
                //this.dataUnits[15] = (byte)Convert.ToInt32(strLJMoney.Substring(4, 2));//
                //this.dataUnits[16] = (byte)Convert.ToInt32(strLJMoney.Substring(2, 2));//
                //this.dataUnits[17] = (byte)Convert.ToInt32(strLJMoney.Substring(0, 2));//
                MyDataConvert.DecimalToFourBCD(value, this.dataUnits, 14);

            }
        }

        [DataMember]
        public decimal SYMoney
        {
            get
            {
                return MyDataConvert.FourBCDToDecimal_Symbol(this.dataUnits, 18);
            }
            set
            {
                MyDataConvert.DecimalToFourBCD_Symbol(value, this.dataUnits, 18);
            }
        }

        [DataMember]
        public decimal LastLJGas
        {
            get
            {
                //StringBuilder sb = new StringBuilder();
                //sb.Append(string.Format("{0:X2}", this.dataUnits[25]));
                //sb.Append(string.Format("{0:X2}", this.dataUnits[24]));
                //sb.Append(string.Format("{0:X2}.", this.dataUnits[23]));
                //sb.Append(string.Format("{0:X2}", this.dataUnits[22]));
                //return Convert.ToDecimal(sb.ToString());
                return MyDataConvert.FourBCDToDecimal(this.dataUnits, 22);

            }
            set
            {
                //string strLJMoney = Convert.ToInt64(value * 100).ToString().PadLeft(8, '0');
                //this.dataUnits[22] = (byte)Convert.ToInt32(strLJMoney.Substring(6, 2));//
                //this.dataUnits[23] = (byte)Convert.ToInt32(strLJMoney.Substring(4, 2));//
                //this.dataUnits[24] = (byte)Convert.ToInt32(strLJMoney.Substring(2, 2));//
                //this.dataUnits[25] = (byte)Convert.ToInt32(strLJMoney.Substring(0, 2));//
                MyDataConvert.DecimalToFourBCD(value, this.dataUnits, 22);
            }
        }

        [DataMember]
        public byte JSDay
        {
            get
            {
                return this.dataUnits[26];
            }
            set
            {
                this.dataUnits[26] = value;
            }
        }

        [DataMember]
        public ST1 ST1
        {
            get
            {
                return new ST1() { ST_0 = this.dataUnits[26], ST_1 = this.dataUnits[27] };
            }
            set
            {
                this.dataUnits[26] = value.ST_0;
                this.dataUnits[27] = value.ST_1;
            }
        }

        [DataMember]
        public ST2 ST2
        {
            get
            {
                return new ST2() { ST = this.dataUnits[28] };
            }
            set
            {
                this.dataUnits[28] = value.ST;
            }
        }       
    }


    /// <summary>
    /// 主动上报数据应答数据
    /// </summary>
    public class DataItem_C001_Answer : DataItem
    {
        public DataItem_C001_Answer(byte ser)
        {
            this.dataUnits = new byte[3];
            this.dataUnits[0] = 0xC0;
            this.dataUnits[1] = 0x01;
            this.SER = ser;
        }
    }
}
