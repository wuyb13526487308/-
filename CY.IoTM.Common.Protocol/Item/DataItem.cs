using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common
{
    [DataContract]
    public class DataItem
    {
        ///// <summary>
        ///// 控制码
        ///// </summary>
        //protected ControlCode controlCode;

        ///// <summary>
        ///// 状态符
        ///// </summary>
        //protected byte[] statusCode;

        ///// <summary>
        ///// 数据域长度
        ///// </summary>
        //protected int length;

        ///// <summary>
        ///// 标识符
        ///// </summary>
        //private IdentityCode identityCode;

        /// <summary>
        /// 数据
        /// </summary>
        [DataMember]
        protected byte[] dataUnits;

        public DataItem(byte[] data)
        {
            this.dataUnits = data;
        }
        public DataItem()
        {
        }

        /// <summary>
        /// 数据域长度
        /// </summary>
        public int Length { get { return dataUnits.Length; } }

        public IdentityCode IdentityCode
        {
            get
            {
                byte[] tmp = new byte[2];
                tmp[0] = dataUnits[1];
                tmp[1] = dataUnits[0];
                return (IdentityCode)BitConverter.ToUInt16(tmp, 0); 
            }
        }
        /// <summary>
        /// 获取或设置指令序号
        /// </summary>
        public byte SER
        {
            set
            {
                if (this.dataUnits != null)
                    this.dataUnits[2] = value;
            }
            get
            {
                return this.dataUnits[2];
            }
        }
        public byte[] GetBytes()
        {
            //byte[] data = new byte[this.length]; 

            ////标识符
            //byte[] identityData =BCD.S2BArr(Convert.ToString((int)this.identityCode,16));

            ////读操作
            //if (controlCode == ControlCode.ReadData || controlCode == ControlCode.CYReadData 
            //    || identityCode == IdentityCode.出厂启用 || identityCode == IdentityCode.恢复初始设置 )
            //{
            //    data[0] = identityData[0];
            //    data[1] = identityData[1];
            //    data[2] = indexCode;
            
            //}
            ////写操作
            //if (controlCode == ControlCode.WriteData || controlCode == ControlCode.CYWriteData)
            //{
            //    if (dataUnits != null && dataUnits.Length > 0 && dataUnits.Length + 3 == this.length)
            //    {

            //        data[0] = identityData[0];
            //        data[1] = identityData[1];
            //        data[2] = indexCode;
            //        for (int i = 0; i < dataUnits.Length; i++) {
            //            data[3 + i] = dataUnits[i];
            //        }
            //    }
            //}
            //return data;
            return this.dataUnits;
        }

    }
}
