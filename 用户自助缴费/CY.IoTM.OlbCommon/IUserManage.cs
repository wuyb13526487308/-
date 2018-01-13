using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.OlbCommon
{
    /// <summary>
    /// 用户管理接口
    /// </summary>
  public interface IUserManage  {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="Olb_User"></param>
        /// <returns></returns>
		Message Add(Olb_User Olb_User);
       /// <summary>
       /// 编辑用户
       /// </summary>
       /// <param name="Olb_User"></param>
       /// <returns></returns>
		Message Edit(Olb_User Olb_User);
      /// <summary>
      /// 删除用户
      /// </summary>
      /// <param name="account"></param>
      /// <returns></returns>
		Message Delete(string  account);
      /// <summary>
      /// 通过账号获取用户
      /// </summary>
      /// <param name="account"></param>
      /// <returns></returns>
        Olb_User GetUserByAccount(string account);
      /// <summary>
      /// 修改用户密码
      /// </summary>
      /// <param name="oldPwd"></param>
      /// <param name="newPwd"></param>
      /// <param name="account"></param>
      /// <returns></returns>
        Message UpdatePwd(string oldPwd, string newPwd, string account);
	}
}





