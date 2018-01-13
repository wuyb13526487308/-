using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 广告配置文件
    /// </summary>
    [Serializable]
    public class ADCFile
    {

        /// <summary>
        /// 文件编号
        /// </summary>
        public int FileNO { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 起始显示日期
        /// </summary>
        public DateTime DTStart { get; set; }
        /// <summary>
        /// 停止显示日期
        /// </summary>
        public DateTime DTEnd { get; set; }
        /// <summary>
        /// 轮询显示时间（秒）
        /// </summary>
        public int PollTime { get; set; }
        /// <summary>
        /// 操作码 是否显示本图片 true-显示  false-不显示
        /// </summary>
        public bool OpShowStatus { get; set; }

        /// <summary>
        /// 操作码 是否删除文件 true-删除  false-不删除
        /// </summary>
        public bool OpDeleteStatus { get; set; }


    }
    ///// <summary>
    ///// 操作码
    ///// </summary>
    //public enum OpCode : int
    //{
    //    /// <summary>
    //    /// 无操作
    //    /// </summary>
    //    无操作 = 0,
    //    /// <summary>
    //    /// 显示文件
    //    /// </summary>
    //    显示文件 = 1,
    //    /// <summary>
    //    /// 删除文件
    //    /// </summary>
    //    删除文件 = 2 
    //}
}
