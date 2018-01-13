using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item.上报数据
{
    [DataContract]
    public class DataItem_C002 : DataItem
    {
        public DataItem_C002()
        {
        }
        public DataItem_C002(byte[] data):base(data)
        {
        }
        /// <summary>
        /// 创建DataItem_C002对象
        /// </summary>
        /// <param name="ser"></param>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileLength">文件长度</param>
        /// <param name="totalSegments">总分段数</param>
        /// <param name="currentSegmentsIndex">要读取的分段</param>
        /// <param name="dataLength">数据长度</param>
        public DataItem_C002(byte ser,string fileName,int fileLength,int totalSegments,int currentSegmentsIndex,int dataLength)
        {
            this.dataUnits = new byte[0x20];
            this.dataUnits[0] = 0xc0;
            this.dataUnits[1] = 0x02;
            this.dataUnits[2] = ser;
            this.FileName = fileName;
            this.FileLength = fileLength;
            this.TotalSegments = totalSegments;
            this.CurrentSegmentsIndex = currentSegmentsIndex;
            this.DataLength = dataLength;
        }

        [DataMember]
        public string FileName
        {
            get
            {
                return Encoding.ASCII.GetString(this.dataUnits, 3, 20);
            }
            set
            {
                string _filename = value.PadRight(20, ' ');
                byte[] byte_filename = Encoding.ASCII.GetBytes(_filename);
                byte_filename.CopyTo(dataUnits, 3);
            }
        }
        [DataMember]
        public int FileLength
        {
            get
            {
                return this.ThreeBytesToint(this.dataUnits, 23);
            }
            set
            {
                this.IntToThreeBytes(value).CopyTo(this.dataUnits, 23);
            }
        }

        [DataMember]
        public int TotalSegments
        {
            get
            {
                return this.TwoBytesToint(this.dataUnits, 26);
            }
            set
            {
                this.IntToTwoBytes(value).CopyTo(this.dataUnits, 26);
            }
        }
        [DataMember]
        public int CurrentSegmentsIndex
        {
            get
            {
                return this.TwoBytesToint(this.dataUnits, 28);
            }
            set
            {
                this.IntToTwoBytes(value).CopyTo(this.dataUnits, 28);
            }
        }
        [DataMember]
        public int DataLength
        {
            get
            {
                return this.TwoBytesToint(this.dataUnits, 30);
            }
            set
            {
                this.IntToTwoBytes(value).CopyTo(this.dataUnits, 30);
            }
        }

        /// <summary>
        /// 将int数值转换为占3个字节的byte数组，本方法适用于(低位在前，高位在后)的顺序。 
        /// </summary>
        /// <param name="value">要转换的int值 </param>
        /// <returns>byte数组(3个字节)</returns>
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

    public class DataItem_C002_Answer : DataItem_C002
    {
        public DataItem_C002_Answer(byte[] data):base(data)
        {
        }
        public DataItem_C002_Answer(byte ser, string fileName, int fileLength, int totalSegments, int currentSegmentsIndex, byte[] data) : base()
        {
            this.dataUnits = new byte[0x20 + data.Length];
            this.dataUnits[0] = 0xc0;
            this.dataUnits[1] = 0x02;
            this.dataUnits[2] = ser;
            this.FileName = fileName;
            this.FileLength = fileLength;
            this.TotalSegments = totalSegments;
            this.CurrentSegmentsIndex = currentSegmentsIndex;
            this.DataLength = data.Length;
            data.CopyTo(this.dataUnits, 32);
        }

        public byte[] getFileSegment
        {
            get
            {
                byte[] buffer = new byte[this.DataLength];
                for (int i = 0; i < this.DataLength; i++)
                    buffer[i] = this.dataUnits[i + 32];
                return buffer;
            }
        }
    }
    public class DataItem_C002_Answer_Err : DataItem
    {
        public DataItem_C002_Answer_Err(byte[] data):base(data)
        {
        }
        public DataItem_C002_Answer_Err(byte ser)
        {
            this.dataUnits = new byte[0x03];
            this.dataUnits[0] = 0xc0;
            this.dataUnits[1] = 0x02;
            this.dataUnits[2] = ser;
        }
    }
}
