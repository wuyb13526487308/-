using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 读公称流量请求数据 
    /// </summary>
    public class DataItem_C204 : DataItem_C200
    {
        public DataItem_C204(byte ser)
            : base(ser)
        {
        }
        public DataItem_C204(byte[] data)
            : base(data)
        {
        }
        protected override void Init()
        {
            base.Init();
            this.dataUnits[1] = 0x04;
        }
    }

    /// <summary>
    /// 读公称流量应答数据
    /// </summary>
    public class DataItem_C204_Anwser : DataItem
    {

        //燃气表公称流量



          /// <summary>
        ///主站请求
        /// </summary>
        public DataItem_C204_Anwser() 
        {
            //this.identityCode = IdentityCode.读公称流量;
            //this.indexCode = 0x00;
            //this.controlCode = ControlCode.CYReadData;
            //this.length = 3;

        }



        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_C204_Anwser(byte[] data)
        {
            //this.identityCode = IdentityCode.读公称流量;

        }


    }
}
