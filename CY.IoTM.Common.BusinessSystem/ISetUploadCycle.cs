using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CY.IotM.Common;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 设置上传周期接口
    /// </summary>
    [ServiceContract]
    public interface ISetUploadCycle
    {
        #region 设置上传周期增删改
        [OperationContract]
        Message Add(IoT_SetUploadCycle info, List<IoT_UploadCycleMeter> meterList);
        [OperationContract]
        Message Edit(IoT_SetUploadCycle info);
        [OperationContract]
        Message Delete(IoT_SetUploadCycle info);

        [OperationContract]
        Message revoke(string ID, string CompanyID);
        [OperationContract]
        Message AddSetAlarmArea(IoT_SetUploadCycle info, List<String> communityList);
        [OperationContract]
        Message AddSetAlarmAll(IoT_SetUploadCycle info);
        #endregion
    }
}
