using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Classes
{
    /// <summary>
    /// 接收数据类型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        ///数据采集端监视信息：对应MonitorInfo
        /// </summary>
        MonitorData = 0,
        /// <summary>
        /// 燃气流量数据：对应ReadDataInfo
        /// </summary>
        ReadData = 1,
        /// <summary>
        /// 报警信息：对应WarningInfo
        /// </summary>
        WarningData = 2,
        /// <summary>
        /// 采集服务器列表
        /// </summary>
        CJDList = 3,
    }
}
