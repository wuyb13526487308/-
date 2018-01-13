using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    public class DataItem_C202 : DataItem_C200
    {
        public DataItem_C202(byte ser)
            : base(ser)
        {
        }
        public DataItem_C202(byte[] data)
            : base(data)
        {
        }
        protected override void Init()
        {
            base.Init();
            this.dataUnits[1] = 0x02;
        }
    }
    /// <summary>
    /// 读服务器信息
    /// </summary>
    public class DataItem_C202_Answer : DataItem
    {

        //服务器参数


  

        /// <summary>
        ///主站请求
        /// </summary>
        public DataItem_C202_Answer() 
        {
            //this.identityCode = IdentityCode.读服务器信息;
            //this.indexCode = 0x00;
            //this.controlCode = ControlCode.CYReadData;
            //this.length = 3;

        }



        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_C202_Answer(byte[] data)
        {
            //this.identityCode = IdentityCode.读服务器信息;

        }




    }
}
