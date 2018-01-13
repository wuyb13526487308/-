using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace CY.IotM.Common
{
    /// <summary>
    /// 操作员管理接口
    /// </summary>
    [ServiceContract]
    public interface ICompanyOperatorManage
    {
        #region 操作员增删改
        [OperationContract]
        Message AddCompanyOperator(CompanyOperator info);
        [OperationContract]
        Message EditCompanyOperator(CompanyOperator info);
        [OperationContract]
        Message DeleteCompanyOperator(CompanyOperator info);

        #endregion
    }
}
