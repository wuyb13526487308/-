using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 写地址
    /// </summary>
    public class DataItem_A018:DataItem
    {

        public string NewAdress {
            get
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 6; i >=0; i--)
                    sb.Append(string.Format("{0:X2}", this.dataUnits[i + 3]));
                return sb.ToString();
            }
            set
            {
                byte[] _key = MyDataConvert.StrToToHexByte(value.PadLeft(14, '0'));
                for (int i = 6; i>=0; i--)
                    this.dataUnits[i + 3] = _key[i];
            }
        }

        public DataItem_A018(byte ser,string NewAdress) 
        {
            this.dataUnits = new byte[10];
            this.dataUnits[0] = 0xA0;
            this.dataUnits[1] = 0x18;
            this.dataUnits[2] = ser;
            this.NewAdress = NewAdress;
        }

        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_A018(byte[] data):base(data){
        }
    }

    public class DataItem_A018_Answer : DataItem_Answer
    {
        public DataItem_A018_Answer(byte ser)
            : base(ser)
        {
        }
        protected override void Init()
        {
            base.Init();
            this.dataUnits[0] = 0xA0;
            this.dataUnits[1] = 0x18;
        }
    }
}
