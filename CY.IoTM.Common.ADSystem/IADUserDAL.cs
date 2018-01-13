using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CY.IotM.Common;

namespace CY.IoTM.Common.ADSystem
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IADUserDAL”。
    [ServiceContract]
    public interface IADUserDAL
    {


        // 增删改
        [OperationContract]
        Message Add(ADUser info);

        [OperationContract]
        Message Edit(ADUser info);

        [OperationContract]
        Message Delete(string UserID, string CompanyID);


        [OperationContract]
        List<ADUserSC> getListSC();

        /// <summary>
        /// 更新发布状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [OperationContract]
        Message UpadteAdUserStatus(View_AdUser model);

        [OperationContract]
        Message UpadteAdUserStatusGroup(View_AdUser model, string userIDArray);

        /// <summary>
        /// 显示已经排除添加过广告屏用户
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        [OperationContract]
        List<View_UserInfo> getUserListShow(string CompanyID);
    }
}
