using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 写新密钥（主站请求数据）
    /// </summary>
    public class DataItem_A014 : DataItem
    {
        /// <summary>
        /// 获取或设置新密钥版本号
        /// </summary>
        public byte Version
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
        /// 获取或设置新密钥
        /// </summary>
        public string Key
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 8; i++)
                    sb.Append(string.Format("{0:X2}", this.dataUnits[i + 4]));
                return sb.ToString();
            }
            set
            {
                byte[] _key = MyDataConvert.StrToToHexByte(value.PadLeft(16, '0'));
                for (int i = 0; i < 8; i++)
                    this.dataUnits[i + 4] = _key[i];
            }
        }


        public DataItem_A014(byte ser,byte version, string key) 
        {
            this.dataUnits = new byte[0x0c];
            this.dataUnits[0] = 0xA0;
            this.dataUnits[1] = 0x14;
            this.dataUnits[2] = ser;
            this.Version = version;
            this.Key = key;
        }

        public DataItem_A014(byte[] data):base(data)
        {
        }
    }

    public class DataItem_A014_ASK : DataItem_Answer
    {
        public DataItem_A014_ASK(byte ser, byte ver):base(ser)
        {
            this.dataUnits[3] = ver;
        }
        protected override void Init()
        {
            this.dataUnits = new byte[4];
            this.dataUnits[0] = 0xA0;
            this.dataUnits[1] = 0x14;
        }
    }
}
