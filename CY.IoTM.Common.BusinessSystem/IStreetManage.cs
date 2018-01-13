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
    /// 街道管理接口
    /// </summary>
    [ServiceContract]
    public interface IStreetManage
    {


        #region 街道增删改
        [OperationContract]
        Message Add(IoT_Street info);
        [OperationContract]
        Message Edit(IoT_Street info);
        [OperationContract]
        Message Delete(IoT_Street info);
        #endregion


    }

  


}
