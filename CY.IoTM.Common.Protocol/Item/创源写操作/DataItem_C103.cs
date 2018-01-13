using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 设置切断报警参数
    /// </summary>
    public class DataItem_C103 : DataItem
    {
        //数据标识DI，序号SER，切断报警启动控制开关标志，长期未与服务器通讯报警时间，燃气漏泄切断报警时间，燃气流量过载切断报警时间，异常大流量值，异常大流量切断报警时间，异常微小流量切断报警时间，持续流量切断报警时间，长期未使用切断报警时间
        public DataItem_C103(byte ser, WaringSwitchSign switchSign)
        {
            this.dataUnits = new byte[0x0E];
            this.dataUnits[0] = 0xC1;
            this.dataUnits[1] = 0x03;
            this.dataUnits[2] = ser;
            this.SwitchSign = switchSign;
            this.长期未与服务器通讯报警时间 = 30;
            this.燃气漏泄切断报警时间 = 10;
            this.燃气流量过载切断报警时间 = 120;
            this.异常大流量值 = 20;
            this.异常大流量切断报警时间 = 120;
            this.异常微小流量切断报警时间 = 240;
            this.持续流量切断报警时间 = 120;
            this.长期未使用切断报警时间 = 30;
        }

        public DataItem_C103(byte[] data)
            : base(data)
        { }

        public WaringSwitchSign SwitchSign
        {
            get
            {
                return new WaringSwitchSign() { Byte1 = this.dataUnits[4], Byte2 = this.dataUnits[3] };
            }
            set
            {
                this.dataUnits[4] = value.Byte1;
                this.dataUnits[3] = value.Byte2;
            }
        }
        public byte 长期未与服务器通讯报警时间{
            get{
                return this.dataUnits[5];
            }
            set{
                this.dataUnits [5] = Convert.ToByte((value > 0x1E) ? 0x1E : value);
            }
        }            
         public byte 燃气漏泄切断报警时间
         {
            get{
                return this.dataUnits[6];
            }
            set{
                this.dataUnits [6] = Convert.ToByte((value > 0x0A) ? 0x0A : value);
            }
         }
        
        public byte 燃气流量过载切断报警时间
        {
            get{
                return this.dataUnits[7];
            }
            set{
                this.dataUnits [7] = Convert.ToByte((value > 0x78) ? 0x78 : value);
            }
         }   
            
         public decimal 异常大流量值
         {
            get{
                return MyDataConvert.TwoBCDToDecimal(this.dataUnits, 8);
            }
            set{
                MyDataConvert.DecimalToTwoBDC(value, this.dataUnits, 8);
            }
         }   
         public byte 异常大流量切断报警时间{
             get
             {
                 return this.dataUnits[10];
             }
             set
             {
                 this.dataUnits[10] = Convert.ToByte((value > 0x78) ? 0x78 : value);
             }
         }
         public byte 异常微小流量切断报警时间
         {
             get
             {
                 return this.dataUnits[11];
             }
             set
             {
                 this.dataUnits[11] = Convert.ToByte((value > 0xF0) ? 0xF0 : value);
             }
         }
         public byte 持续流量切断报警时间
         {
             get
             {
                 return this.dataUnits[12];
             }
             set
             {
                 this.dataUnits[12] = value;// Convert.ToByte((value > 0x78) ? 0x78 : value);
             }
         }
         public byte 长期未使用切断报警时间
         {
             get
             {
                 return this.dataUnits[13];
             }
             set
             {
                 this.dataUnits[13] = Convert.ToByte((value > 0x1e) ? 0x1e : value);
             }
         }       
    }
    public class WaringSwitchSign
    {
        public WaringSwitchSign()
        {
        }
        public WaringSwitchSign(string sign)
        {
            /*报警控制开关标记参数共16个字符，从左至右，每个字符含义如下：
            第0 长期未与服务器通讯报警           0：关闭    1：开启
            第1 燃气漏气切断报警                 0：关闭    1：开启
            第2 流量过载切断报警                 0：关闭    1：开启
            第3 异常大流量切断报警               0：关闭    1：开启
            第4 异常微小流量切断报警             0：关闭    1：开启
            第5 持续流量超时切断报警             0：关闭    1：开启
            第6 燃气压力过低切断报警             0：关闭    1：开启
            第7 长期未使用切断报警               0：关闭    1：开启
            第8 移动报警/地址震感器动作切断报警  0：关闭    1：开启
            第9~第15 备用     */

            byte[] open = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };
            byte[] close = { 0xFE, 0xFD, 0xFB, 0xF7, 0xEF, 0xdf, 0xBF, 0x7F };

            int iLength = sign.Length;
            if (iLength > 8) iLength = 8;
            for (int i = 0; i < iLength; i++)
            {
                if(sign.Substring(i,1) =="0")
                    _data[1] &= close[i];
                else
                    _data[1] |= open[i];
            }
            if (sign.Substring(8, 1) == "0")
                this.移动报警_地址震感器动作切断报警 = false;
            else
                this.移动报警_地址震感器动作切断报警 = true;
        }
        private byte[] _data = { 0x00, 0x00 };
        public byte Byte1
        {
            get
            {
                return _data[0];
            }
            set
            {
                _data[0] = value;
            }
        }
        public byte Byte2
        {
            get
            {
                return _data[1];
            }
            set
            {
                _data[1] = value;
            }
        }

        public bool 长期未与服务器通讯报警
        {
            get
            {
                return (_data[1] & 0x01) == 0x01 ? true : false;
            }
            set
            {
                if (value)
                {
                    _data[1] |= 0x01;
                }
                else
                {
                    _data[1] &= 0xFE;
                }
            }
        }
        public bool 燃气漏气切断报警
        {
            get
            {
                return (_data[1] & 0x02) == 0x02 ? true : false;
            }
            set
            {
                if (value)
                {
                    _data[1] |= 0x02;
                }
                else
                {
                    _data[1] &= 0xFD;
                }
            }
        }
        public bool 流量过载切断报警
        {
            get
            {
                return (_data[1] & 0x04) == 0x04 ? true : false;
            }
            set
            {
                if (value)
                {
                    _data[1] |= 0x04;
                }
                else
                {
                    _data[1] &= 0xFB;
                }
            }
        }
        public bool 异常大流量切断报警
        {
            get
            {
                return (_data[1] & 0x08) == 0x08 ? true : false;
            }
            set
            {
                if (value)
                {
                    _data[1] |= 0x08;
                }
                else
                {
                    _data[1] &= 0xF7;
                }
            }
        }
        public bool 异常微小流量切断报警
        {
            get
            {
                return (_data[1] & 0x10) == 0x10 ? true : false;
            }
            set
            {
                if (value)
                {
                    _data[1] |= 0x10;
                }
                else
                {
                    _data[1] &= 0xEF;
                }
            }
        }
        public bool 持续流量超时切断报警
        {
            get
            {
                return (_data[1] & 0x20) == 0x20 ? true : false;
            }
            set
            {
                if (value)
                {
                    _data[1] |= 0x20;
                }
                else
                {
                    _data[1] &= 0xDF;
                }
            }
        }
        public bool 燃气压力过低切断报警
        {
            get
            {
                return (_data[1] & 0x40) == 0x40 ? true : false;
            }
            set
            {
                if (value)
                {
                    _data[1] |= 0x40;
                }
                else
                {
                    _data[1] &= 0xBF;
                }
            }
        }
        public bool 长期未使用切断报警
        {
            get
            {
                return (_data[1] & 0x80) == 0x80 ? true : false;
            }
            set
            {
                if (value)
                {
                    _data[1] |= 0x80;
                }
                else
                {
                    _data[1] &= 0x7F;
                }
            }
        }
        public bool 移动报警_地址震感器动作切断报警
        {
            get
            {
                return (_data[0] & 0x01) == 0x01 ? true : false;
            }
            set
            {
                if (value)
                {
                    _data[0] |= 0x01;
                }
                else
                {
                    _data[0] &= 0xFE;
                }
            }
        }

    }


    /// <summary>
    /// 设置切断报警参数应答数据对象
    /// </summary>
    public class DataItem_C103_Answer : DataItem
    {

        //切断报警启动控制开关标志，长期未与服务器通讯报警时间，燃气漏泄切断报警时间，燃气流量过载切断报警时间，异常大流量值，
        //异常大流量切断报警时间，异常微小流量切断报警时间，持续流量切断报警时间，长期未使用切断报警时间
        public DataItem_C103_Answer(byte ser,ST1 st1,ST2 st2)
        {
            this.dataUnits = new byte[0x06];
            this.dataUnits[0] = 0xC1;
            this.dataUnits[1] = 0x03;
            this.dataUnits[2] = ser;
            this.ST1 = st1;
            this.ST2 = st2;

        }
        public DataItem_C103_Answer(byte[] data) : base(data) { }

        public ST1 ST1
        {
            get
            {
                return new ST1() { ST_0 = this.dataUnits[27], ST_1 = this.dataUnits[26] };
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
                return new ST2() { ST = this.dataUnits[58] };
            }
            set
            {
                this.dataUnits[5] = value.ST;
            }
        } 
    }
}
