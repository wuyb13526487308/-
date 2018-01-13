using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace CY.IotM.Common
{
    /// <summary>
    /// 用户登录管理接口
    /// </summary>
    [ServiceContract]
    public  interface ILoginerManage
    {
        [OperationContract]
        Message RegisterClient(string md5Cookie, string operID, string companyID);
        [OperationContract]
        CompanyOperator GetLoginerByMd5Cookie(string md5Cookie);
        [OperationContract]
        Message UnLRegisterClientByMd5Cookie(string md5Cookie);
    }
    
}
