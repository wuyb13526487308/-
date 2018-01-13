using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.ADSystem
{
    /// <summary>
    /// 发布类型
    /// </summary>
    public enum PublishType
    {
        /// <summary>
        /// 新发布
        /// </summary>
        NewPublish,
        /// <summary>
        /// 追加发布
        /// </summary>
        AddPublish,
        /// <summary>
        /// 撤销发布
        /// </summary>
        UnPublish ,
    }
}
