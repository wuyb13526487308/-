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
    /// 用户档案管理接口
    /// </summary>
    [ServiceContract]
    public interface IUserManage
    {
      

        #region 燃气表用户增删改
        [OperationContract]
        Message Add(IoT_User info);
        [OperationContract]
        Message Edit(IoT_User info);
        [OperationContract]
        Message Delete(IoT_User info);
        [OperationContract]
        Message EditUserMeter(View_UserMeter info);
        [OperationContract]
        Message DeleteUserMeter(View_UserMeter info);
        [OperationContract]
        bool UpadteUserStatus(string state, string userId);
        [OperationContract]
        Message BatchDeleteUserMeter(string userId);
        [OperationContract]
        Message BatchAddUserMeter(IoT_User info, IoT_Meter meter);
        [OperationContract]
        Message AddTemp(IoT_UserTemp info);
        [OperationContract]
        Message BatchImport(string  meterNo);
        [OperationContract]
        void DeleteUserTemp();
        [OperationContract]
        string GetUserMeterByUserId(string userId, string companyId);
        #endregion


    }

  


}
