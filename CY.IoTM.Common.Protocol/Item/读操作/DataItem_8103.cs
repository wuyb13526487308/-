using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{

    /// <summary>
    /// 读结算日
    /// </summary>
    public class DataItem_8103 : DataItem
    {

        private int _jieSuanDay;
        /// <summary>
        /// 结算日
        /// </summary>
        public int JieSuanDay 
        {
            get { return this._jieSuanDay; }
        
        }

        /// <summary>
        ///主站请求
        /// </summary>
        public DataItem_8103() 
        {
            //this.identityCode = IdentityCode.读结算日;
            //this.indexCode = 0x00;
            //this.controlCode = ControlCode.ReadData;
            //this.length = 3;

        }



        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_8103(byte [] data)
        {
            //this.identityCode = IdentityCode.读结算日;

            //结算日 一个字节
            this._jieSuanDay = BCD.B2I(data[3]);
        }









    }
}
