using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CY.IotM.Common;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 设置结算日接口
    /// </summary>
    [ServiceContract]
    public interface ISettlement
    {
        #region 设置结算日增删改
        [OperationContract]
        Message AddSetAlarmArea(IoT_SetSettlementDay info, List<String> communityList);
        [OperationContract]
        Message AddSetAlarmAll(IoT_SetSettlementDay info);
        [OperationContract]
        Message Add(IoT_SetSettlementDay info, List<IoT_SettlementDayMeter> meterList);
        [OperationContract]
        Message Edit(IoT_SetSettlementDay info);
        [OperationContract]
        Message Delete(IoT_SetSettlementDay info);

        [OperationContract]
        Message revoke(string ID,  string CompanyID);
        #endregion
    }
}
