using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    public class DataItem_C203 : DataItem_C200
    {
        public DataItem_C203(byte ser)
            : base(ser)
        {
        }

        public DataItem_C203(byte[] data)
            : base(data)
        {
        }
        protected override void Init()
        {
            base.Init();
            this.dataUnits[1] = 0x03;
        }
    }

    /// <summary>
    /// 读上传周期
    /// </summary>
    public class DataItem_C203_Answer : DataItem
    {

        //上传周期



        /// <summary>
        ///主站请求
        /// </summary>
        public DataItem_C203_Answer() 
        {
            //this.identityCode = IdentityCode.读上传周期;
            //this.indexCode = 0x00;
            //this.controlCode = ControlCode.CYReadData;
            //this.length = 3;

        }



        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_C203_Answer(byte[] data)
        {
            //this.identityCode = IdentityCode.读上传周期;

        }



    }
}
