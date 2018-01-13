using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CY.IoTM.Common;
using CY.IoTM.Common.Item;
using CY.IoTM.Common.Protocol;

namespace CY.IoTM.Protocol
{
    public class Frame
    {
        /// <summary>
        /// 数据域长度
        /// </summary>
        private int dataLength =0;
        /// <summary>
        /// 校验码
        /// </summary>
        private byte checkCode;
        /// <summary>
        /// 地址域
        /// </summary>
        public String Adress;
        /// <summary>
        /// 控制码
        /// </summary>
        public ControlCode ControlCode;
        /// <summary>
        /// 数据
        /// </summary>
        public DataItem DataItem;

        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="frameData"></param>
        //public Frame(byte[] frameData)
        //{
        //    MemoryStream memStream = new MemoryStream();
        //    memStream.Write(frameData, 0, frameData.Length);

        //    //地址
        //    byte[] adressData = new byte[7];
        //    memStream.Read(adressData, 3, 7);
        //    this.Adress = BCD.B2S(adressData);


        //    //控制码
        //    this.ControlCode = (ControlCode)BCD.B2I(frameData[10]);


        //    //数据域长度
        //    this.dataLength = frameData[11];

        //    //数据域
        //    byte[] dataArea = new byte[this.dataLength];
        //    memStream.Read(dataArea, 12, this.dataLength);
        //    this.DataItem = GetDataItem(dataArea);

        //    //校验码
        //    this.checkCode = frameData[frameData.Length - 2];

        //}

        private byte[] mkey;
        /// <summary>
        /// 帧协议类型
        /// </summary>
        IotProtocolType _protocolType = IotProtocolType.RanQiBiao;


        /// <summary>
        /// 数据帧构造函数
        /// </summary>
        /// <param name="task"></param>
        public Frame(TaskArge task)
        {
            this.Adress = task.IoTMac;
            this.ControlCode = task.ControlCode;
            this.DataItem = task.Data;
            this._protocolType = task.IotProtocolType;

            mkey = task.MKey;
            if(this.DataItem!=null)
                this.dataLength = this.DataItem.Length;
        }      

        public byte[] GetBytes()
        {
            int index = 2;
            byte[] arr = new byte[15 + (this._protocolType == IotProtocolType.LCD ? 1 : 0) + this.dataLength];
            arr[0] = 0xfe;
            arr[1] = 0xfe;

            arr[2] = 0x68;
            //arr[3] = 0x30;
            arr[3] = (byte)this._protocolType;
            //地址域
            byte[] _key = MyDataConvert.StrToToHexByte(this.Adress.PadLeft(14, '0'));
            for (int i = 6; i >= 0; i--)
                arr[index + i + 2] = _key[6-i];

            //byte[] adress = BCD.S2BArr(this.Adress);
            //for (int i = 3; i < 10; i++) {
            //    arr[i] = adress[i - 3];
            //}

            //控制码
            arr[index + 9] = (byte)this.ControlCode;
            //数据长度
            arr[index + 10] = (byte)(this.dataLength & 0xff);
            int iOffset = 11; //定义从包头（0x68）到数据区域字节数
            if (this._protocolType == IotProtocolType.LCD)
            {
                iOffset = 12;
                arr[index + 11] = (byte)((this.dataLength >> 8) & 0xff);
            }
            //数据域
            byte[] dataItemArr = this.DataItem.GetBytes();

            byte[] desData;
            if (mkey == null || this._protocolType == IotProtocolType.LCD)  //广告任务命令不加密
            {
                desData=new byte[dataItemArr.Length];
                dataItemArr.CopyTo(desData,0);
            }
            else
            {
                desData = Encryption.Encry(this.DataItem.GetBytes(), mkey);
            }
            //desData = Encryption.Encry(dataItemArr, mkey);

            for (int i = 0; i < desData.Length; i++)
            {
                arr[index + i + iOffset] = desData[i];
            }
            
            //校验码
            byte cs = 0x00;
            for (int i = index; i < index + dataLength + iOffset; i++)
            {
                cs += Convert.ToByte(arr[i]);
            }
            arr[index + iOffset + this.dataLength] = cs;
            //结束符
            arr[index + iOffset + 1 + this.dataLength] = 0x16;
            return arr;
        }

        //public static bool CheckFrame(byte[] data, string adress)
        //{

        //    /*
        //     * 数据帧的结构。
        //        FEH	        1	    帧前导符
        //        68H         1       帧起始符
        //        TYPE        1       仪表类型
        //        ADRESS	    7       地址域
        //        C	        1	    控制码
        //        L	        2	    数据长度
        //        DATA	    变长	数据域
        //        CS	        1	    校验码
        //        16H	        1	    结束码

        //     * */

        //    MemoryStream memStream = new MemoryStream();

        //    try
        //    {


        //        //数据长度
        //        if (data.Length < 15)
        //        {
        //            return false;
        //        }

        //        //帧前导符 起始符 仪表类型
        //        if (data[0] != 0xfe || data[1] != 0x68 || data[2] != 0x30)
        //        {
        //            return false;
        //        }

        //        // 结束符
        //        if (data[data.Length - 1] != 0x16)
        //        {
        //            return false;
        //        }

        //        //校验码
        //        memStream = new MemoryStream();
        //        memStream.Write(data, 0, data.Length);

        //        byte[] csData = new byte[data.Length - 3];
        //        memStream.Read(csData, 1, data.Length - 3);

        //        byte cs = 0x00;
        //        for (int i = 0; i < csData.Length; i++)
        //        {
        //            cs += csData[i];
        //        }
        //        if (cs != data[data.Length - 1])
        //        {

        //            return false;
        //        }

        //        //地址
        //        byte[] adressData = new byte[7];
        //        memStream.Read(adressData, 3, 7);

        //        string temp_adress = BCD.B2S(adressData);

        //        if (temp_adress != adress)
        //        {
        //            return false;
        //        }

        //        return true;

        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.Message);
        //        return false;

        //    }
        //    finally
        //    {
        //        memStream.Close();
        //    }


        //}
    }
}


