using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CY.IoTM.OlbCommon
{
    /// <summary>
    /// 燃气账户管理接口
    /// </summary>
    public interface IGasUserManage
    {
        /// <summary>
        /// 添加燃气账号
        /// </summary>
        /// <param name="account"></param>
        /// <param name="gasUser"></param>
        /// <returns></returns>
        Message AddGasUser(string account, string userId, string companyId);
 
        /// <summary>
        /// 删除燃气账号
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        Message RemoveGasUser(string account, string userId, string companyId);

        /// <summary>
        /// 获取燃气账号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        List<GasUser> GetGasUserList(string account);

	}
}


