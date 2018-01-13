using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{
    /// <summary>
    /// 广告文件属性定义数据类型
    /// </summary>
    public class DataItem_C109 : DataItem
    {
        private List<ADFile> _aDFileList = new List<ADFile>();

        public List<ADFile> FileList { get { return this._aDFileList; } }
        /// <summary>
        /// 操作码
        /// </summary>
        public ADPublishOperatorCode OperatorCode
        {
            get
            {
                return (ADPublishOperatorCode)this.dataUnits[3];
            }
            set
            {
                this.dataUnits[3] = (byte)value;
            }
        }
        /// <summary>
        /// 文件条数
        /// </summary>
        public int ADFileCount
        {
            get
            {
                return this.dataUnits[4];
            }
            private set
            {
                this.dataUnits[4] = (byte)(value & 0xff);
            }
        }
        /// <summary>
        /// 构造广告发布指令
        /// </summary>
        /// <param name="ser">序号SER</param>
        /// <param name="operatorCode">操作码</param>
        /// <param name="fileCount">广告文件条数</param>
        /// <param name="fileList">文件列表</param>
        public DataItem_C109(byte ser, ADPublishOperatorCode operatorCode,byte fileCount,List<ADFile> fileList)
        {
            /*数据标识DI，序号SER，操作码(1),文件条数(1),条目1，条目2，… 条目n*/
            dataUnits = new byte[5 + 32 *fileCount];
            this.dataUnits[0] = 0xC1;
            this.dataUnits[1] = 0x09;
            this.dataUnits[2] = ser;
            this.dataUnits[3] = (byte)operatorCode;
            this.dataUnits[4] = fileCount;

            this._aDFileList = fileList;
            for(int i = 0; i < fileCount; i++)
            {
                fileList[i].getBytes.CopyTo(this.dataUnits, 5 + i * 32);
            }
        }
         public DataItem_C109(byte[] data)
            : base(data)
        {
            if (data.Length < 5 || (data.Length != (5 + this.dataUnits[4] * 32))) throw new Exception("数组长度不足（构建DataItem_C109对象失败)");
            this._aDFileList = new List<ADFile>();
            for(int i=0;i<this.dataUnits[4];i++)
            {
                byte[] tmp = new byte[32];
                for (int index = 0; index < 32; index++)
                {
                    tmp[index] = this.dataUnits[5 + i * 32 + index];
                }
                this._aDFileList.Add(new ADFile(tmp));
            }
        }
    }
    public class DataItem_C109_Answer : DataItem_Answer
    {
        public DataItem_C109_Answer(byte ser,string ver) : base(ser)
        {
            this.dataUnits[3] = (byte)'1';
            this.dataUnits[4] = (byte)'.';
            this.dataUnits[5] = (byte)'0';
            this.dataUnits[6] = (byte)'0';
        }
        protected override void Init()
        {
            this.dataUnits = new byte[7];
            this.dataUnits[0] = 0xC1;
            this.dataUnits[1] = 0x09;
        }
    }
    [DataContract]
    public class ADFile
    {
        byte[] buffer;
        /*
            buffer 结构定义：
            1	 文件编号	    hex数据   	1	1-255数字，0为无效的编号
            2	 文件名	         Ascii码	    20	变长<=20字节 
            3  起始显示日期	hex码数据	3	YYMMDD（年月日）
            4	  停止显示日期	hex码数据	3	YYMMDD（年月日）
            5	  轮询显示时间	hex数据	     2	秒单位（无符号整数，最大值为：65,535）
            6	  文件长度	     Hex  	     3	文件总字节数

            */
        /// <summary>
        /// 构造函数（创建ADFile对象）
        /// </summary>
        /// <param name="fileno">文件序号</param>
        /// <param name="filename">文件名称</param>
        /// <param name="dtstart">开始时间</param>
        /// <param name="dtend">停止时间</param>
        /// <param name="polltime">显示时长（秒）</param>
        /// <param name="fileLength">文件长度</param>
        public ADFile(int fileno, string filename, DateTime dtstart, DateTime dtend, int polltime,int fileLength)
        {
            buffer = new byte[32];
            this.FileNO = fileno;
            this.FileName = filename;
            this.DTStart = dtstart;
            this.DTEnd = dtend;
            this.FileLength = fileLength;
            this.PollTime = polltime;
        }
        public ADFile(byte[] data)
        {
            if (data.Length < 32)
                throw new Exception("数组长度不等于32（不符合ADFile类数据长度）");
            this.buffer = data;
        }
        /// <summary>
        /// 获取ADfile的数据（字节数组）
        /// </summary>
        [DataMember]
        public byte[] getBytes
        {
            get
            {
                return this.buffer;
            }
        }

        /// <summary>
        /// 文件编号
        /// </summary>
        [DataMember]
        public int FileNO
        {
            get
            {
                return this.buffer[0];
            }
            set
            {
                this.buffer[0] = (byte)(value & 0xff);
            }
        }
        /// <summary>
        /// 文件名
        /// </summary>
        [DataMember]
        public string FileName
        {
            get
            {
                return Encoding.ASCII.GetString(this.buffer, 1, 20);
            }
            set
            {
                string _filename = value.PadRight(20, ' ');
                byte[] byte_filename = Encoding.ASCII.GetBytes(_filename.Substring(0,20));
                byte_filename.CopyTo(buffer, 1);
            }
        }

        /// <summary>
        /// 文件长度
        /// </summary>
        [DataMember]
        public int FileLength
        {
            get
            {
                return this.ThreeBytesToint(this.buffer, 21);
            }
            set
            {
                this.IntToThreeBytes(value).CopyTo(this.buffer, 21);
            }
        }

        /// <summary>
        /// 开始日期
        /// </summary>
        [DataMember]
        public DateTime DTStart {
            get
            {
                return this.ThreeByteToDate(this.buffer,24);
            }
            set
            {
                this.DateToThreeByte(value).CopyTo(this.buffer, 24);
            }
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        [DataMember]
        public DateTime DTEnd {
            get
            {
                return this.ThreeByteToDate(this.buffer, 27);
            }
            set
            {
                this.DateToThreeByte(value).CopyTo(this.buffer, 27);
            }
        }
        /// <summary>
        /// 轮询时间s
        /// </summary>
        [DataMember]
        public int PollTime
        {
            get
            {
                return this.TwoBytesToint(this.buffer, 30);
            }
            set
            {
                this.IntToTwoBytes(value).CopyTo(this.buffer, 30);
            }
        }




        //private

        private byte[] DateToThreeByte(DateTime value)
        {
            byte[] tmp = new byte[3];
            tmp[0] = (byte)value.Day;
            tmp[1] = (byte)value.Month;
            tmp[2] = (byte)(value.Year - 2000);

            return tmp;
        }

        private DateTime ThreeByteToDate(byte[] buffer, int index)
        {
            int year = buffer[index + 2] + 2000;
            int month = buffer[index + 1];
            int day = buffer[index];
            return new DateTime(year, month, day);
        }
        private byte[] IntToThreeBytes(int value)
        {
            byte[] src = new byte[3];
            src[2] = (byte)((value >> 16) & 0xFF);
            src[1] = (byte)((value >> 8) & 0xFF);
            src[0] = (byte)(value & 0xFF);
            return src;
        }
        private int ThreeBytesToint(byte[] buffer, int index)
        {
            int iResult = 0;
            iResult += buffer[index];
            iResult += buffer[index + 1] << 8;
            iResult += buffer[index + 2] << 16;
            return iResult;
        }

        private byte[] IntToTwoBytes(int value)
        {
            byte[] src = new byte[2];
            src[1] = (byte)((value >> 8) & 0xFF);
            src[0] = (byte)(value & 0xFF);
            return src;
        }

        private int TwoBytesToint(byte[] buffer, int index)
        {
            int iResult = 0;
            iResult += buffer[index];
            iResult += buffer[index + 1] << 8;
            return iResult;
        }
    }
}
