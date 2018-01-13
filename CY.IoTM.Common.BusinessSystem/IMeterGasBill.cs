using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CY.IotM.Common;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 气量表结算
    /// </summary>
    [ServiceContract]
    public interface IMeterGasBill
    {
        #region 气量表结算
        [OperationContract]
        Message SettleMeterGas(string meterNo, string month);


        [OperationContract]
        List<IoT_Meter> GetMeterByPriceId(string companyId, int priceId);

        [OperationContract]
        List<View_MeterGasBill> GetGasBillByMonth(string month, string companyId); 


        #endregion
    }
}
