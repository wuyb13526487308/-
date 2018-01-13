using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CY.IoTM.OlbCommon
{
    /// <summary>
    /// ȼ���˻�����ӿ�
    /// </summary>
    public interface IGasUserManage
    {
        /// <summary>
        /// ���ȼ���˺�
        /// </summary>
        /// <param name="account"></param>
        /// <param name="gasUser"></param>
        /// <returns></returns>
        Message AddGasUser(string account, string userId, string companyId);
 
        /// <summary>
        /// ɾ��ȼ���˺�
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        Message RemoveGasUser(string account, string userId, string companyId);

        /// <summary>
        /// ��ȡȼ���˺�
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        List<GasUser> GetGasUserList(string account);

	}
}


