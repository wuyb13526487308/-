using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IoTM.OlbCommon;
using System.Data;
using System.Data.Linq;
using CY.IoTM.OlbCommon.Tool;

namespace CY.IoTM.OlbService
{
    /// <summary>
    /// 用气账单管理
    /// </summary>
    public class GasFeeBillManageService:IGasFeeBillManage
    {

        public List<GasFeeBill> GetGasFeeBill(DateTime startTime, DateTime endTime, string userId, string companyId)
        {
            return null;
        }


    }
}
