using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CY.IotM.Common;
using CY.IoTM.Common.ADSystem;

namespace CY.IoTM.Common.ADSystem
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IADContextDAL”。
    [ServiceContract]
    public interface IADContextDAL
    {
        // 增删改
        [OperationContract]
        Message Add(ADContext info);

        [OperationContract]
        Message Edit(ADContext info);

        [OperationContract]
        Message Delete(long AC_ID);

        [OperationContract]
        bool DeleteList(string IDlist);

        //修改状态
        [OperationContract]
        Message UpadteAdStatus(long AC_Id, int State);

    }
}
