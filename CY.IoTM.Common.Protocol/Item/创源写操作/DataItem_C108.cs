using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
该文件废弃
*/
namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 发送文件
    /// </summary>
    public class DataItem_C108 : DataItem
    {

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件长度
        /// </summary>
        public int FilelLength { get; set; }
        /// <summary>
        /// 总分段数
        /// </summary>
        public int TotalSegmentCount { get; set; }
        /// <summary>
        /// 当前分段序号
        /// </summary>
        public int CurrentNum { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int DatalLength { get; set; }
        /// <summary>
        /// 数据体
        /// </summary>
        public byte[] DataContent { get; set; }

        //文件名，文件长度，总分段数，当前分段序号，数据长度，数据体
         public DataItem_C108(byte ser,string filename,int filelength,int totalsegmentcount ,int currentnum,int datalength,byte[] datacontent)
         {
             this.FileName = filename;
             this.FilelLength = filelength;
             this.TotalSegmentCount = totalsegmentcount;
             this.CurrentNum = currentnum;
             this.DatalLength = datalength;
             this.DataContent = datacontent;

             this.dataUnits = new byte[32 + datacontent.Length];
             this.dataUnits[0] = 0xC1;
             this.dataUnits[1] = 0x08;
             this.dataUnits[2] = ser;
             //文件名
             //不足部分在尾部用空格补充（0x20）
             string _filename = filename.PadRight(20, ' ');
             byte[] byte_filename = Encoding.ASCII.GetBytes(_filename);
             byte_filename.CopyTo(dataUnits,3); 
             

             //文件长度
             byte[] byte_filelength = intToBytes(filelength);
             for (int i = 0; i < byte_filelength.Length; i++)
             {
                 dataUnits[23 + i] = byte_filelength[i];
             }

             //总分段数             
             dataUnits[26] = (byte)(totalsegmentcount & 0xFF);
             dataUnits[27] = (byte)((totalsegmentcount >> 8) & 0xFF);

              
             //，当前分段序号
             dataUnits[28] = (byte)(currentnum & 0xFF);
             dataUnits[29] = (byte)((currentnum >> 8) & 0xFF); 

              
             //，数据长度
             dataUnits[30] = (byte)(datalength & 0xFF);
             dataUnits[31] = (byte)((datalength >> 8) & 0xFF);

             //数据体
             datacontent.CopyTo(dataUnits,32); 

         }
         public DataItem_C108(byte[] data)
             : base(data)
        {
            
        }
         /**  
     * 将int数值转换为占3个字节的byte数组，本方法适用于(低位在前，高位在后)的顺序。 和bytesToInt（）配套使用 
     * @param value  
     *            要转换的int值 
     * @return byte数组 
     */ 
       
         private  byte[] intToBytes(int value)
         {
             byte[] src = new byte[3];             
             src[2] = (byte)((value >> 16) & 0xFF);
             src[1] = (byte)((value >> 8) & 0xFF);
             src[0] = (byte)(value & 0xFF);
             return src;
         }  
    }
    public class DataItem_C108_Answer : DataItem_Answer
    {
        public DataItem_C108_Answer(byte ser) : base(ser) { }
        protected override void Init()
        {
            base.Init();
            this.dataUnits[0] = 0xC1;
            this.dataUnits[1] = 0x08;
        }
    }
}
