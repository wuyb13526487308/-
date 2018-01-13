using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{

    /// <summary>
    /// 读购入金额
    /// </summary>
    public class DataItem_8105: DataItem
    {

        //本次购入序号，本次购入金额，累计购入金额，剩余金额，状态ST1


        private int _buyNum;

        private double _buyMoney;

        private double _buyMoneySum;

        private double _lastMoney;

       

        /// <summary>
        /// 本次购入序号
        /// </summary>
        public int BuyNum 
        {
            get { return this._buyNum; }
        }

        public double BuyMoney
        {
            get { return this._buyMoney; }
        }


        public double BuyMoneySum
        {
            get { return this._buyMoneySum; }
        }

        public double LastMoney
        {
            get { return this._lastMoney; }
        }

        /// <summary>
        ///主站请求
        /// </summary>
        public DataItem_8105() 
        {
            //this.identityCode = IdentityCode.读购入金额;
            //this.indexCode = 0x00;
            //this.controlCode = ControlCode.ReadData;
            //this.length = 3;

        }



        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_8105(byte[] data)
        {
            //this.identityCode = IdentityCode.读购入金额;

          
        }



        //public override byte[] GetBytes()
        //{
        //    return base.GetBytes();
        //}











    }
}
