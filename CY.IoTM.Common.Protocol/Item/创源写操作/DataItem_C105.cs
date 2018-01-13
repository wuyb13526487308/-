using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{

    /// <summary>
    /// 设置上传周期
    /// </summary>
    public class DataItem_C105 : DataItem
    {

        //上传周期

        /// <summary>
        /// 模式类型
        /// </summary>
        public ReportCycleType type {
            get { return (ReportCycleType)this.dataUnits[3]; }

            set { dataUnits[3] = BCD.I2B((byte)value); }
        }

        /// <summary>
        /// 日
        /// </summary>
        public int XX
        {
            get
            {
                return this.dataUnits[4];
            }
            set
            {
                dataUnits[4] = BCD.I2B((byte)value);
            }
        }
        /// <summary>
        /// 小时
        /// </summary>
        public int YY
        {
            get
            {
                return this.dataUnits[5];
            }
            set
            {
                dataUnits[5] = BCD.I2B((byte)value);
            }
        }
        /// <summary>
        /// 分钟
        /// </summary>
        public int ZZ
        {
            get
            {
                return this.dataUnits[6];
            }
            set
            {
                dataUnits[6] = BCD.I2B((byte)value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ser"></param>
        /// <param name="type"></param>
        /// <param name="XX">日</param>
        /// <param name="YY">时</param>
        /// <param name="ZZ">分钟</param>

        public DataItem_C105(byte ser,ReportCycleType type, int XX, int YY, int ZZ ) 
        {
            this.dataUnits = new byte[7];
            this.dataUnits[0] = 0xC1;
            this.dataUnits[1] = 0x05;
            this.dataUnits[2] = ser;
            this.type = type;
            this.XX = XX;
            this.YY = YY;
            this.ZZ = ZZ;            
        }



        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_C105(byte[] data):base(data)
        {
        }
    }


    public class DataItem_C105_Answer : DataItem_Answer
    {
        public DataItem_C105_Answer(byte ser) : base(ser) { }
        protected override void Init()
        {
            base.Init();
            this.dataUnits[0] = 0xC1;
            this.dataUnits[1] = 0x05;
        }
    }

    /// <summary>
    /// 上传周期模式
    /// </summary>
    public enum ReportCycleType 
    { 
    
        月周期=0,
        天周期=1,
        时周期 = 2,
        周期采集= 3    
    }
}
