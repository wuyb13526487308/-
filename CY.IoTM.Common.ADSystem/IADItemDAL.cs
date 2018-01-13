using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CY.IotM.Common;



namespace CY.IoTM.Common.ADSystem
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IADItemDAL”。
    [ServiceContract]
    public interface IADItemDAL
    {
        
        // 增删改
        [OperationContract]
        Message Add(ADItem info);

        [OperationContract]
        Message Edit(ADItem info);

        [OperationContract]
        Message upOrder(ADItem info);

        [OperationContract]
        Message downOrder(ADItem info);

        [OperationContract]
        Message Delete(long AI_ID);

        [OperationContract]
        int userPuFileNum(string companyID);

        [OperationContract]
        bool DeleteList(string IDlist);

        //修改状态
        [OperationContract]
        bool UpadteAdStatus(int Ai_Id, int IsDisplay);

        //显示图片
        [OperationContract]
        List<ADItem> getListShow(long AC_ID);
        
    }
}
