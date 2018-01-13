using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    public class DataItem_C200 : DataItem
    {
        public DataItem_C200(byte[] buffer)
            : base(buffer)
        {
        }
        public DataItem_C200(byte ser)
        {
            Init();
            this.dataUnits[2] = ser;
        }

        protected virtual void Init()
        {
            this.dataUnits = new byte[3];
            this.dataUnits[0] = 0xC0;
            this.dataUnits[1] = 0x00;
        }
    }

    /// <summary>
    /// 读时钟
    /// </summary>
    public class DataItem_C200_Answer : DataItem
    {
        //实时时间
        private DateTime _time;
        /// <summary>
        /// 实时时间
        /// </summary>
        public DateTime Time 
        {
            get { return this._time; }
        
        }
        /// <summary>
        ///主站请求
        /// </summary>
        public DataItem_C200_Answer() 
        {
            //this.identityCode = IdentityCode.读时钟;
            //this.indexCode = 0x00;
            //this.controlCode = ControlCode.CYReadData;
            //this.length = 3;

        }
        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_C200_Answer(byte[] data):base(data)
        {
        }
    }
}
