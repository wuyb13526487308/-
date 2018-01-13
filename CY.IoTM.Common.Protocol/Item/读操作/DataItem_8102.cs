using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{

    /// <summary>
    /// 读价格表
    /// </summary>
    public class DataItem_8102:DataItem
    {

        //阶梯气价控制标志(CT)，价格1，用量1，价格2，用量2，价格3，用量3，价格4，用量4，价格5 ，启用日期


        private CT ct;

        public CT CT { get { return ct; } }


        private decimal price1;


        public decimal Price1 { get { return price1; } }



        private int useGas1;

        public int UseGas1 { get { return useGas1; } }



        private decimal price2;

        public decimal Price2 { get { return price2; } }


        private int useGas2;

        public int UseGas2 { get { return useGas2; } }


        private decimal price3;

        public decimal Price3 { get { return price3; } }


        private int useGas3;

        public int UseGas3 { get { return useGas3; } }


        private decimal price4;

        public decimal Price4 { get { return price4; } }


        private int useGas4;

        public int UseGas4 { get { return useGas4; } }


        private decimal price5;

        public decimal Price5 { get { return price5; } }


        private int startDate;


        public int StartDate { get { return startDate; } }




        /// <summary>
        ///主站请求
        /// </summary>
        public DataItem_8102() 
        {
            //this.identityCode = IdentityCode.读价格表;
            //this.indexCode = 0x00;
            //this.controlCode = ControlCode.ReadData;
            //this.length = 3;

        }



        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_8102(byte[] data)
        {
            //this.identityCode = IdentityCode.读价格表;

          
        }

    }
}
