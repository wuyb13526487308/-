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
    /// 社区管理接口
    /// </summary>
    [ServiceContract]
    public interface ICommunityManage
    {


        #region 社区增删改
        [OperationContract]
        Message Add(IoT_Community info);
        [OperationContract]
        Message Edit(IoT_Community info);
        [OperationContract]
        Message Delete(IoT_Community info);
        #endregion


    }

  


}
