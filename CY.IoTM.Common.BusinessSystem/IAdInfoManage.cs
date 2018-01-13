using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CY.IotM.Common;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 广告接口
    /// </summary>
    [ServiceContract]
    public interface IAdInfoManage
    {
        #region 广告增删改
        [OperationContract]
        Message Add(IoT_AdInfo info);
        [OperationContract]
        Message Edit(IoT_AdInfo info);
        [OperationContract]
        Message Delete(IoT_AdInfo info);

        [OperationContract]
        Message Publish(IoT_SetAdInfo info);

        [OperationContract]
        Message UnPublish(IoT_SetAdInfo info);

        [OperationContract]
        Message EditAdInfo(IoT_SetAdInfo info);
   

        [OperationContract]
        IoT_AdInfo GetAdInfoData(Int64 id);
        [OperationContract]
        List<ADFile> GetAdFileList();



        #endregion
    }
}
