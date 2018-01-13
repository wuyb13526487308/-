using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CY.IotM.Common;

namespace CY.IoTM.Common.ADSystem
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IADPublishUserDAL”。
    [ServiceContract]
    public interface IADPublishUserDAL
    {
        // 增删改
        [OperationContract]
        Message Add(ADPublishUser info);

        [OperationContract]
        Message Edit(ADPublishUser info);

        [OperationContract]
        Message Delete(int ID);

        [OperationContract]
        bool DeleteList(string IDlist);

        [OperationContract]
        Message groupAdd(ADPublishUser model, string userIDArray);

        //修改状态
        [OperationContract]
        bool UpadteAdStatus(int ID, int State);
    }
}
