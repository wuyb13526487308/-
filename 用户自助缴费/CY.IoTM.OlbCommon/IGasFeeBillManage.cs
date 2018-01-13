using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CY.IoTM.OlbCommon
{
    /// <summary>
    /// 用气账单接口
    /// </summary>
    public interface IGasFeeBillManage
    {
        /// <summary>
        /// 获取燃气账户账单
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        List<GasFeeBill> GetGasFeeBill(DateTime startTime, DateTime endTime, string userId, string companyId);


    }
}
