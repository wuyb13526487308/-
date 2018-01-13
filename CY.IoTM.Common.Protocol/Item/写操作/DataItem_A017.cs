using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 写阀门控制
    /// </summary>
    public class DataItem_A017 : DataItem
    {
        /// <summary>
        /// 开阀/关阀操作
        /// </summary>
        public bool IsOpen
        {
            get
            {
                if (this.dataUnits[3] == 0x55)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.dataUnits[3] = 0x55;
                else
                    this.dataUnits[3] = 0x99;
            }
        }


        public DataItem_A017(byte ser,bool isOpen) 
        {
            this.dataUnits = new byte[4];
            this.dataUnits[0] = 0xA0;
            this.dataUnits[1] = 0x17;
            this.dataUnits[2] = ser;
            this.IsOpen = isOpen;
        }
        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_A017(byte[] data):base(data){}
    }

    public class DataItem_A017_ASK : DataItem_Answer
    {
        public DataItem_A017_ASK(byte ser, ST1 st1)
            : base(ser)
        {
            this.ST1 = st1;
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
        protected override void Init()
        {
            this.dataUnits = new byte[5];
            this.dataUnits[0] = 0xA0;
            this.dataUnits[1] = 0x17;
        }
    }

}
