using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 广告文件
    /// </summary>
    [Serializable]
    public class ADFile
    {
        public int id { get; set; }
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
        public int TotalSegment { get; set; }

        /// <summary>
        /// 当前分段数 从1开始
        /// </summary>
        public int CurrentSegment { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public int DatalLength { get; set; }
        /// <summary>
        /// 数据体
        /// </summary>
        public byte[] DataContent { get; set; }
    }
}
