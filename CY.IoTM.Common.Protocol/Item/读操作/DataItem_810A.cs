using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{

    /// <summary>
    /// 读地址
    /// </summary>
    public class DataItem_810A:DataItem
    {


         private string _adress;
        /// <summary>
         /// 地址
        /// </summary>
         public string Adress 
        {
            get { return this._adress; }
        
        }

        /// <summary>
        ///主站请求
        /// </summary>
        public DataItem_810A() 
        {
            //this.identityCode = IdentityCode.读地址;
            //this.indexCode = 0x00;
            //this.controlCode = ControlCode.ReadData;
            //this.length = 3;

        }



        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_810A(byte[] data)
        {
            //this.identityCode = IdentityCode.读地址;

        }


    }
}
