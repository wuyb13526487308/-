using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{

    /// <summary>
    /// 设置公称流量 
    /// </summary>
    public class DataItem_C101 : DataItem
    {

       

        /// <summary>
        /// 燃气表公称流量
        /// </summary>
        public int GCLiuLiang { get; set; }



        public DataItem_C101(int GCLiuLiang) 
        {

     
            this.GCLiuLiang = GCLiuLiang;

            this.dataUnits = new byte[1];


            dataUnits[0] = (byte)this.GCLiuLiang;
            


        }



        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_C101(byte[] data):base(data)
        {
            //this.identityCode = IdentityCode.设置公称流量;

        }



    }

    public class DataItem_C101_Answer : DataItem_Answer
    {
        public DataItem_C101_Answer(byte ser) : base(ser) { }
        protected override void Init()
        {
            base.Init();
            this.dataUnits[0] = 0xC1;
            this.dataUnits[1] = 0x01;
        }
    }
}
