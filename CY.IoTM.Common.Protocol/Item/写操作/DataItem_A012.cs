using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 写抄表日
    /// </summary>
    public class DataItem_A012 : DataItem
    {
        /// <summary>
        /// 抄表日
        /// </summary>
        public int ChaoBiaoDay { get; set; }



        public DataItem_A012(int ChaoBiaoDay) 
        {

     
            this.ChaoBiaoDay = ChaoBiaoDay;


            //this.identityCode = IdentityCode.写抄表日;
            //this.indexCode = 0x00;
            //this.controlCode = ControlCode.WriteData;
            //this.length = 4;

            this.dataUnits = new byte[1];


            dataUnits[0] = BCD.I2B(this.ChaoBiaoDay);
                
                


        }

        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_A012(byte[] data)
        {
            //this.identityCode = IdentityCode.写抄表日;

        }


    }
}
