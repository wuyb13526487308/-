using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;

namespace CY.IoTM.Common.Item
{
    [DataContract]
    public class ST1
    {

        private byte[] _ST1;
        public ST1()
        {
            _ST1 = new byte[] { 0x00, 0x00 };
        }

        [DataMember]
        public byte ST_0
        {
            get
            {
                return this._ST1[0];
            }
            set
            {
                if(this._ST1 == null)
                    _ST1 = new byte[] { 0x00, 0x00 };
                this._ST1[0] = value;
            }
        }
        [DataMember]
        public byte ST_1
        {
            get
            {
                return _ST1[1];
            }
            set
            {
                if (this._ST1 == null)
                    _ST1 = new byte[] { 0x00, 0x00 };
                this._ST1[1] = value;
            }
        }

        /// <summary>
        /// 获取或设置阀门状态
        /// </summary>
        public bool ValveStatu
        {
            get
            {
                return (this.ST_0 & 0xfc ) == 0 ? true : false;
            }
            set
            {
                if (value)
                {
                    //开
                    this.ST_0 &= 0xFC;
                }
                else
                {
                    //关
                    this.ST_0 |= 0x01;
                }
            }
        }

        public override string ToString()
        {
            /*
             字符顺序：X X X 0 0 X X  X X  X  X   X X  X   X  X 
                       1 2 3 4 5 6 7  8 9  10 11 12 13 14 15 16 
第1-2 字符表示阀门状态，00：开；01：关；10：保留；11：异常
第3个字符表示电池电压，0：正常；1：欠压
第4-5个字符保留，填00
第6个字符表示电源状态，0：正常；1：异常
第7个字符表示外电源状态0：正常；1：异常
第8个字符锂电池状态0：正常；1：异常
第9个字符表类型0：气量表；1：金额表
第10个字符无外电（无外电和内电池）0：未报警  1：报警
第11个字符表示欠压（干电池）
第12个字符表示操作错误/磁干扰
第13个字符表示余额不足/气量用尽
第14个字符表示系统控制关阀
第15个字符燃气表内部错误关阀，请立即与燃气公司联系。（燃气表内部错误，如内电池电量低、内存损坏等）
第16个字符表示长期未与服务器通讯报警*/
            StringBuilder sb = new StringBuilder();
            sb.Append((this.ST_0 & 0x03).ToString().PadLeft(2, '0').ToString());
            sb.Append(((this.ST_0 >> 2) & 0x01).ToString());//第3个字符表示电池电压，0：正常；1：欠压
            sb.Append("00");
            sb.Append(((this.ST_0 >> 5) & 0x01).ToString());//第6个字符表示电源状态，0：正常；1：异常
            sb.Append(((this.ST_0 >> 6) & 0x01).ToString());//第7个字符表示外电源状态0：正常；1：异常
            sb.Append(((this.ST_0 >> 7) & 0x01).ToString());//第8个字符锂电池状态0：正常；1：异常

            sb.Append(((this.ST_1) & 0x01).ToString());//第9个字符表类型0：气量表；1：金额表
            sb.Append(((this.ST_1 >> 1) & 0x01).ToString());//第10个字符无外电（无外电和内电池）0：未报警  1：报警
            sb.Append(((this.ST_1 >> 2) & 0x01).ToString());//第11个字符表示欠压（干电池）
            sb.Append(((this.ST_1 >> 3) & 0x01).ToString());//第12个字符表示操作错误/磁干扰
            sb.Append(((this.ST_1 >> 4) & 0x01).ToString());//第13个字符表示余额不足/气量用尽
            sb.Append(((this.ST_1 >> 5) & 0x01).ToString());//第14个字符表示系统控制关阀
            sb.Append(((this.ST_1 >> 6) & 0x01).ToString());//第15个字符燃气表内部错误关阀，请立即与燃气公司联系。（燃气表内部错误，如内电池电量低、内存损坏等）
            sb.Append(((this.ST_1 >> 7) & 0x01).ToString());//第16个字符表示长期未与服务器通讯报警

            return sb.ToString();
        }

    }


    [DataContract]
    public class ST2
    {
        private byte _st2;

        [DataMember]
        public byte ST
        {
            get
            {
                return this._st2;
            }
            set
            {
                this._st2 = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(((this._st2 >> 7) & 0x01).ToString());//第1个移动报警/地震震感器动作切断报警  0：未报警   1：报警
            sb.Append(((this._st2 >> 6) & 0x01).ToString());//第2个长期未使用切断报警            0：未报警   1：报警
            sb.Append(((this._st2 >> 5) & 0x01).ToString());//第3个燃气压力过低切断报警        0：未报警   1：报警
            sb.Append(((this._st2 >> 4) & 0x01).ToString());//第4个持续流量超时切断报警        0：未报警   1：报警
            sb.Append(((this._st2 >> 3) & 0x01).ToString());//第5个异常微小流量切断报警        0：未报警   1：报警
            sb.Append(((this._st2 >> 2) & 0x01).ToString());//第6个异常大流量切断报警            0：未报警   1：报警
            sb.Append(((this._st2 >> 1) & 0x01).ToString());//第7个 流量过载切断报警                0：未报警   1：报警
            sb.Append(((this._st2 >> 0) & 0x01).ToString());//第8个燃气漏气切断报警                0：未报警   1：报警

            return sb.ToString();
        }
    }



    /// <summary>
    /// 阶梯气价控制标志(CT) 
    /// </summary>
    [DataContract]
    public class CT
    {

        //金额表、气量表选择位 bit0 bit1


        //00：气量表   01：金额表
        //10：保留     11：保留

        /// <summary>
        /// 金额表、气量表选择位
        /// </summary>
        [DataMember]
        public MeterType CTMeterType { get; set; }

        

        //阶梯气价选择位 bit2

        //0：未启用  1：启用

        /// <summary>
        /// 阶梯气价选择位
        /// </summary>
        [DataMember]
        public bool CTIsLadder { get; set; }


        //阶梯气价结算类型 bit3  bit4

        //00：月  01：季度  10：半年度  11：全年度

        /// <summary>
        /// 阶梯气价结算类型
        /// </summary>
        [DataMember]
        public JieSuanType CTJieSuanType { get; set; }



        //阶梯气价数 bit5  bit6	bit7

        //000：0个；001：1个
        //010：2个；011:3个
        //100：4个；101:5个

        /// <summary>
        /// 阶梯气价数
        /// </summary>
        [DataMember]
        public int CTLadderNum { get; set; }





        public CT(MeterType CTMeterType, bool CTIsLadder, JieSuanType CTJieSuanType, int CTLadderNum)
        {

            this.CTMeterType = CTMeterType;
            this.CTIsLadder = CTIsLadder;
            this.CTJieSuanType = CTJieSuanType;
            this.CTLadderNum = CTLadderNum;

        }



        public CT(byte ct)
        {

            byte[] ct_temp = { ct };

            BitArray bitArr = new BitArray(ct_temp);


            if ((ct & 0x01) == 0x01)
            {
                this.CTMeterType = MeterType.金额表;
            }
            else
            {
                this.CTMeterType = MeterType.气量表;
            }


            this.CTIsLadder = bitArr[2];


            if (!bitArr[3] && !bitArr[4])
            {
                this.CTJieSuanType = JieSuanType.月;
            }

            if (bitArr[3] && !bitArr[4])
            {
                this.CTJieSuanType = JieSuanType.季度;
            }

            if (!bitArr[3] && bitArr[4])
            {
                this.CTJieSuanType = JieSuanType.半年度;
            }

            if (bitArr[3] && bitArr[4])
            {
                this.CTJieSuanType = JieSuanType.全年度;
            }

            this.CTLadderNum = ct >> 5;
        }




        public byte GetByte() {


            BitArray bitArr = new BitArray(8);

            if (this.CTMeterType == MeterType.气量表) {

                bitArr.Set(0, false);
                bitArr.Set(1, false);
            
            }
            if (this.CTMeterType == MeterType.金额表)
            {

                bitArr.Set(0, true);
                bitArr.Set(1, false);

            }


            bitArr.Set(2, this.CTIsLadder);


            if (this.CTJieSuanType == JieSuanType.月) {

                bitArr.Set(3, false);
                bitArr.Set(4, false);
               
            }

            if (this.CTJieSuanType == JieSuanType.季度)
            {

                bitArr.Set(3, true);
                bitArr.Set(4, false);

            }

            if (this.CTJieSuanType == JieSuanType.半年度)
            {

                bitArr.Set(3, false);
                bitArr.Set(4, true);

            }

            if (this.CTJieSuanType == JieSuanType.全年度)
            {

                bitArr.Set(3, true);
                bitArr.Set(4, true);

            }



            switch(this.CTLadderNum)
            {
            
                case 0:

                    bitArr.Set(5, false);
                    bitArr.Set(6, false);
                    bitArr.Set(7, false);
                    break;
                case 1:

                    bitArr.Set(5, true);
                    bitArr.Set(6, false);
                    bitArr.Set(7, false);
                    break;
                case 2:

                    bitArr.Set(5, false);
                    bitArr.Set(6, true);
                    bitArr.Set(7, false);
                    break;
                case 3:

                    bitArr.Set(5, true);
                    bitArr.Set(6, true);
                    bitArr.Set(7, false);
                    break;
                case 4:

                    bitArr.Set(5, false);
                    bitArr.Set(6, false);
                    bitArr.Set(7, true);
                    break;
                case 5:

                    bitArr.Set(5, true);
                    bitArr.Set(6, false);
                    bitArr.Set(7, true);
                    break;
            
            
            }


            int[] intByBitArray = new int[1];

            bitArr.CopyTo(intByBitArray, 0);
     
            return (byte)intByBitArray[0];
        
        }

    }



    /// <summary>
    /// 
    /// </summary>
    public enum MeterType
    {
        气量表=0,
        金额表=1
    }


    /// <summary>
    /// /阶梯气价结算类型
    /// </summary>
    public enum JieSuanType
    {
      
        
        月 =0 ,
        季度=1,
        半年度  =10,
        全年度=11


    }



    /// <summary>
    /// 切断报警启动控制开关标志
    /// </summary>
    public class AlarmMark { 







        //public AlarmMark(MeterType CTMeterType, bool CTIsLadder, JieSuanType CTJieSuanType, int CTLadderNum)
        //{

        //    this.CTMeterType = CTMeterType;
        //    this.CTIsLadder = CTIsLadder;
        //    this.CTJieSuanType = CTJieSuanType;
        //    this.CTLadderNum = CTLadderNum;

        //}



        //public CT(byte ct)
        //{

        //    byte[] ct_temp = { ct };

        //    BitArray bitArr = new BitArray(ct_temp);


        //    if (!bitArr[0] && !bitArr[1]) {

        //        this.CTMeterType = MeterType.气量表;
        //    }

        //    if (!bitArr[0] && bitArr[1])
        //    {
        //        this.CTMeterType = MeterType.金额表;
        //    }


        //    this.CTIsLadder = bitArr[2];


        //    if (!bitArr[3] && !bitArr[4])
        //    {
        //        this.CTJieSuanType = JieSuanType.月;
        //    }

        //    if (!bitArr[3] && bitArr[4])
        //    {
        //        this.CTJieSuanType = JieSuanType.季度;
        //    }

        //    if (bitArr[3] && !bitArr[4])
        //    {

        //        this.CTJieSuanType = JieSuanType.半年度;
        //    }

        //    if (bitArr[3] && bitArr[4])
        //    {
        //        this.CTJieSuanType = JieSuanType.全年度;
        //    }


        //    if (!bitArr[5] && !bitArr[6] && !bitArr[7])
        //    {
        //        this.CTLadderNum = 0;
        //    }

        //    if (!bitArr[5] && !bitArr[6] && bitArr[7])
        //    {
        //        this.CTLadderNum = 1;
        //    }

        //    if (!bitArr[5] && bitArr[6] && !bitArr[7])
        //    {
        //        this.CTLadderNum = 2;
        //    }

        //    if (!bitArr[5] && bitArr[6] && bitArr[7])
        //    {
        //        this.CTLadderNum = 3;
        //    }

        //    if (bitArr[5] && !bitArr[6] && !bitArr[7])
        //    {
        //        this.CTLadderNum = 4;
        //    }

        //    if (!bitArr[5] && bitArr[6] && !bitArr[7])
        //    {
        //        this.CTLadderNum = 5;
        //    }
           

        //}




        //public byte GetByte() {


        //    BitArray bitArr = new BitArray(8);

        //    if (this.CTMeterType == MeterType.气量表) {

        //        bitArr.Set(0, false);
        //        bitArr.Set(1, false);
            
        //    }
        //    if (this.CTMeterType == MeterType.金额表)
        //    {

        //        bitArr.Set(0, false);
        //        bitArr.Set(1, true);

        //    }


        //    bitArr.Set(2, this.CTIsLadder);


        //    if (this.CTJieSuanType == JieSuanType.月) {

        //        bitArr.Set(3, false);
        //        bitArr.Set(4, false);
               
        //    }

        //    if (this.CTJieSuanType == JieSuanType.季度)
        //    {

        //        bitArr.Set(3, false);
        //        bitArr.Set(4, true);

        //    }

        //    if (this.CTJieSuanType == JieSuanType.半年度)
        //    {

        //        bitArr.Set(3, true);
        //        bitArr.Set(4, false);

        //    }

        //    if (this.CTJieSuanType == JieSuanType.全年度)
        //    {

        //        bitArr.Set(3, true);
        //        bitArr.Set(4, true);

        //    }



        //    switch(this.CTLadderNum)
        //    {
            
        //        case 0:

        //            bitArr.Set(5, false);
        //            bitArr.Set(6, false);
        //            bitArr.Set(7, false);
        //            break;
        //        case 1:

        //            bitArr.Set(5, false);
        //            bitArr.Set(6, false);
        //            bitArr.Set(7, true);
        //            break;
        //        case 2:

        //            bitArr.Set(5, false);
        //            bitArr.Set(6, true);
        //            bitArr.Set(7, false);
        //            break;
        //        case 3:

        //            bitArr.Set(5, false);
        //            bitArr.Set(6, true);
        //            bitArr.Set(7, true);
        //            break;
        //        case 4:

        //            bitArr.Set(5, true);
        //            bitArr.Set(6, false);
        //            bitArr.Set(7, false);
        //            break;
        //        case 5:

        //            bitArr.Set(5, true);
        //            bitArr.Set(6, false);
        //            bitArr.Set(7, true);
        //            break;
            
            
        //    }


        //    int[] intByBitArray = new int[1];

        //    bitArr.CopyTo(intByBitArray, 0);
     
        //    return (byte)intByBitArray[0];
        
        //}
    
    
    }






}
