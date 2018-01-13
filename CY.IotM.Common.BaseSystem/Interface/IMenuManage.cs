using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace CY.IotM.Common
{
    /// <summary>
    /// 菜单管理接口
    /// </summary>
    [ServiceContract]
    public interface IMenuManage
    {
        #region 菜单增删改
        [OperationContract]
        Message AddMenu(MenuInfo info);
        [OperationContract]
        Message EditMenu(MenuInfo info);
        [OperationContract]
        Message DeleteMenu(MenuInfo info);
        [OperationContract]
        void ReSetCompany(string CompanyID);

        #endregion
    }
}
