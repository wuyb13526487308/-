using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    public class DataItem_C201 : DataItem_C200
    {
        public DataItem_C201(byte ser)
            : base(ser)
        {
        }
        public DataItem_C201(byte[] data)
            : base(data)
        {
        }
        protected override void Init()
        {
            base.Init();
            this.dataUnits[1] = 0x01;
        }

    }

    /// <summary>
    /// 读切断报警参数
    /// </summary>
    public class DataItem_C201_Answer : DataItem
    {

        //切断报警启动控制开关标志，长期未与服务器通讯报警时间，燃气漏泄切断报警时间，燃气流量过载切断报警时间，异常大流量值，
        //异常大流量切断报警时间，异常微小流量切断报警时间，持续流量切断报警时间，长期未使用切断报警时间





        public DataItem_C201_Answer() 
        {
            //this.identityCode = IdentityCode.读切断报警参数;
            //this.indexCode = 0x00;
            //this.controlCode = ControlCode.CYReadData;
            //this.length = 3;

        }



        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_C201_Answer(byte[] data)
        {
            //this.identityCode = IdentityCode.读切断报警参数;

        }




    }
}
