using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CY.IotM.Common;



namespace CY.IoTM.Common.ADSystem
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IADPublishDAL”。
    [ServiceContract]
    public interface IADPublishDAL
    {
        // 增删改
        [OperationContract]
        Message Add(ADPublish info);

        [OperationContract]
        Message Edit(ADPublish info);

        [OperationContract]
        Message Delete(long AP_ID);

        [OperationContract]
        bool DeleteList(string IDlist);

        //修改状态
        [OperationContract]
        Message UpadteAdStatus(long AP_ID, int State);

        //调用APP的发布接口
        [OperationContract]
        string ADPubManager(long AP_ID);

    }
}
