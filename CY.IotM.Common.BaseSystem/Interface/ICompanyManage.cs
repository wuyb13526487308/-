using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace CY.IotM.Common
{
    /// <summary>
    /// 单位管理接口
    /// </summary>
    [ServiceContract]
    public interface ICompanyManage
    {
        #region 单位增删改
        [OperationContract]
        Message AddCompany(CompanyInfo info);
        [OperationContract]
        Message EditCompany(CompanyInfo info);
        [OperationContract]
        Message ResetCompanyAdmin(CompanyInfo info);

        #endregion
    }
}
