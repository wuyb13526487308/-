using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{

    /// <summary>
    /// 读抄表日
    /// </summary>
    public class DataItem_8104: DataItem
    {

        private int _chapBiaoDay;
        /// <summary>
        /// 抄表日
        /// </summary>
        public int ChaoBiaoDay 
        {
            get { return this._chapBiaoDay; }
        
        }

        /// <summary>
        ///主站请求
        /// </summary>
        public DataItem_8104() 
        {
            //this.identityCode = IdentityCode.读抄表日;
            //this.indexCode = 0x00;
            //this.controlCode = ControlCode.ReadData;
            //this.length = 3;

        }



        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_8104(byte[] data)
        {
            //this.identityCode = IdentityCode.读抄表日;

            //抄表日 一个字节
            this._chapBiaoDay = BCD.B2I(data[3]);
        }





    }
}
