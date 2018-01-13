using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using CY.IotM.Common;
namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 点火操作接口
    /// </summary>
    [ServiceContract]
    public interface IChaoBiao
    {
        [OperationContract]
        List<View_UserMeterHistory> GetModelList(string where);

        [OperationContract]
        List<View_UserMeterDayFirstHistory> GetModelLists(string where);

        [OperationContract]
        View_UserMeterHistory GetMonthLastRecord(string userId, string companyId, string month);
    }
}
