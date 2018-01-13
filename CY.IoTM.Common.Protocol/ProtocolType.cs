using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common
{
    /// <summary>
    /// 仪表类型定义
    /// </summary>
    public enum IotProtocolType :byte
    {
        /// <summary>
        /// 未知协议
        /// </summary>
        UnKown = 0x00,
        /// <summary>
        /// 燃气表
        /// </summary>
        RanQiBiao = 0x30,
        /// <summary>
        /// LCD显示屏
        /// </summary>
        LCD = 0x31,
    }
}
