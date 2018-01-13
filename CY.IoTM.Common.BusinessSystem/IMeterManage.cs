using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CY.IotM.Common;

namespace CY.IoTM.Common.Business
{    
    /// <summary>
    /// 用户表具管理接口
    /// </summary>
    [ServiceContract]
    public interface IMeterManage
    {
      

        #region 用户表具增删改
        [OperationContract]
        Message Add(IoT_Meter info);
        [OperationContract]
        Message Edit(IoT_Meter info);
        [OperationContract]
        Message Delete(IoT_Meter info);

        [OperationContract]
        IoT_Meter GetMeterByNo(string meterNo);
        #endregion

    }
}
