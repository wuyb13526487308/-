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
    /// 价格参数管理接口
    /// </summary>
    [ServiceContract]
    public interface IPriceParManage
    {


        #region 服务器参数增删改
        [OperationContract]
        Message Add(IoT_PricePar info);
        [OperationContract]
        Message Edit(IoT_PricePar info);
        [OperationContract]
        Message Delete(IoT_PricePar info);
       


        #endregion


    }

  


}
