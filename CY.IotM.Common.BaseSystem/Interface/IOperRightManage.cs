using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace CY.IotM.Common
{
    /// <summary>
    /// 权限管理接口
    /// </summary>   
    [ServiceContract]
    public interface IOperRightManage
    {
        #region 权限加载
        /// <summary>
        /// 获取操作员具有的菜单
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [OperationContract]
        List<DefineMenu> LoadDefineMenuByLoginOper(CompanyOperator info, bool withButtonMenuCode);
        /// <summary>
        /// 检查操作员是否具有menuCode权限。
        /// </summary>
        /// <param name="info"></param>
        /// <param name="menuCode"></param>
        /// <returns></returns>
        [OperationContract]
        bool CheckMenuCode(CompanyOperator info, string menuCode);
        /// <summary>
        /// 加载操作员具有按钮类权限。
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [OperationContract]
        string LoadHiddenMenuCode(CompanyOperator info);
        #endregion
        #region 权限分配管理
        /// <summary>
        /// 加载单位能操作的菜单，包括按钮
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        [OperationContract]
        List<DefineMenu> LoadCompanyDefineMenu(string companyID);
        /// <summary>
        /// 加载单位的权限组列表
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        [OperationContract]
        List<DefineRight> LoadCompanyDefineRight(string companyID);
        /// <summary>
        /// 加载单位指定人员编码的权限组列表
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        [OperationContract]
        string LoadCompanyOperDefineRight(string companyID, string operID);
        /// <summary>
        /// 添加单位权限组
        /// </summary>
        /// <param name="dRight"></param>
        /// <returns></returns>
        [OperationContract]
        Message AddCompanyDefineRight(DefineRight dRight, List<DefineRightMenu> list);
        /// <summary>
        /// 删除单位权限组
        /// </summary>
        /// <param name="dRight"></param>
        /// <returns></returns>
        [OperationContract]
        Message DelCompanyDefineRight(DefineRight dRight);
        /// <summary>
        /// 编辑人员具有权限
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Message EditCompanyOperRight(string CompanyID, string OperID, List<DefineRight> list);
        /// <summary>
        /// 加载指定权限具有的操作菜单
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="rightCode"></param>
        /// <returns></returns>
        [OperationContract]
        string LoadCompanyDefineRightMenu(string companyID, string rightCode);
        /// <summary>
        /// 清除权限缓存项
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Message RemoveCompanyRightCache(string CompanyID);

        /// <summary>
        /// 编辑公司菜单
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Message EditCompanyMenu(string CompanyID, List<String> list);
        #endregion
        #region 报表管理
        [OperationContract]
        Message EditReportName(ReportTemplate info);
        #endregion
    }

}
