using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CY.IoTM.OlbCommon
{
    /// <summary>
    /// 用户登录管理接口
    /// </summary>
    public interface ILoginerManage
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="md5Cookie"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        Message UserLogin(string md5Cookie, string account);
       /// <summary>
       /// 根据缓存获取用户
       /// </summary>
       /// <param name="md5Cookie"></param>
       /// <returns></returns>
        Olb_User GetLoginerByMd5Cookie(string md5Cookie);
       /// <summary>
       /// 注销
       /// </summary>
       /// <param name="md5Cookie"></param>
       /// <returns></returns>
        Message UnLRegisterClientByMd5Cookie(string md5Cookie);

    }
}
