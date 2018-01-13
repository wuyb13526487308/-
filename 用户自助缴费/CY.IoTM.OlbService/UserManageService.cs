using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Linq;
using CY.IoTM.OlbCommon;
using CY.IoTM.OlbCommon.Tool;

namespace CY.IoTM.OlbService
{
    /// <summary>
    /// 用户管理
    /// </summary>
  public class UserManageService:IUserManage  {


        private static UserManageService instance = null;
        public static UserManageService GetInstance()
        {
            if (instance == null)
            {
                instance = new UserManageService();
            }
            return instance;
        }

        string configName = "";

        private UserManageService()
        {
            configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"]; 
        }

      /// <summary>
      /// 添加用户
      /// </summary>
      /// <param name="info"></param>
      /// <returns></returns>
      public Message Add(Olb_User info) 
      {

          Message m;
          try
          {
              //需要为每个方法创建一个DataContext实例 原因在于DataContext的缓存机制
              DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
              Table<Olb_User> tbl = dd.GetTable<Olb_User>();

              string guid = Guid.NewGuid().ToString();
              info.ID = guid;

              tbl.InsertOnSubmit(info);
              dd.SubmitChanges();

              m = new Message()
              {
                  Result = true,
                  TxtMessage = JSon.TToJson<Olb_User>(info)
              };

          }
          catch (Exception e)
          {
              m = new Message()
              {
                  Result = false,
                  TxtMessage = "注册用户失败！" + e.Message
              };
          }
          return m;
      }
      /// <summary>
      /// 编辑用户
      /// </summary>
      /// <param name="info"></param>
      /// <returns></returns>
      public Message Edit(Olb_User info)
      {
          Message m;
          try
          {
              DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
              Olb_User dbinfo = dd.GetTable<Olb_User>().Where(p => p.ID == info.ID).SingleOrDefault();

              info.PassWord = dbinfo.PassWord;
              ConvertHelper.Copy<Olb_User>(dbinfo, info);

              dd.SubmitChanges();
              m = new Message()
              {
                  Result = true,
                  TxtMessage = JSon.TToJson<Olb_User>(dbinfo)
              };
          }
          catch (Exception e)
          {
              m = new Message()
              {
                  Result = false,
                  TxtMessage = "修改用户信息失败！" + e.Message
              };
          }
          return m;
      }
      /// <summary>
      /// 删除用户
      /// </summary>
      /// <param name="account"></param>
      /// <returns></returns>
      public Message Delete(string account)
      {

          return null;

      }
      /// <summary>
      /// 获取用户
      /// </summary>
      /// <param name="account"></param>
      /// <returns></returns>
      public Olb_User GetUserByAccount(string account)
      {

          Olb_User dbinfo=null;
          try
          {
              DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
              dbinfo = dd.GetTable<Olb_User>().Where(p => p.Account == account).SingleOrDefault();
          }
          catch (Exception e)
          {
              //记录日志
          }
          return dbinfo;

      }
      /// <summary>
      /// 修改密码
      /// </summary>
      /// <param name="oldPwd"></param>
      /// <param name="newPwd"></param>
      /// <param name="account"></param>
      /// <returns></returns>
      public Message UpdatePwd(string oldPwd, string newPwd, string account)
      {
          Message m;
          try
          {
              DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
              Olb_User dbinfo = dd.GetTable<Olb_User>().Where(p => p.Account == account).SingleOrDefault();

              if (oldPwd == dbinfo.PassWord)
              {

                  dbinfo.PassWord = newPwd;
                  dd.SubmitChanges();
                  m = new Message()
                  {
                      Result = true,
                      TxtMessage = JSon.TToJson<Olb_User>(dbinfo)
                  };
              }
              else {
                  m = new Message()
                  {
                      Result = false,
                      TxtMessage = "原密码错误！" 
                  };
              }

          }
          catch (Exception e)
          {
              m = new Message()
              {
                  Result = false,
                  TxtMessage = "修改密码失败！" + e.Message
              };
          }
          return m;
      }
	}
}





