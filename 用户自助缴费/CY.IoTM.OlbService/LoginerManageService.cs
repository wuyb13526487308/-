using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IoTM.OlbCommon;
using System.Data;
using System.Data.Linq;
using CY.IoTM.OlbCommon.Tool;


namespace CY.IoTM.OlbService
{
    /// <summary>
    /// 用户登录管理
    /// </summary>
  public class LoginerManageService:ILoginerManage 
  {


        private static LoginerManageService instance = null;
        public static LoginerManageService GetInstance()
        {
            if (instance == null)
            {
                instance = new LoginerManageService();
            }
            return instance;
        }

        string configName = "";
     
        private LoginerManageService()
        {
            configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
           
        }


        public Message UserLogin(string md5Cookie, string account) 
        {

            Message jMessage;
            try
            {
                LoginerManageHelper.getInstance().Semaphore.WaitOne();
                LoginerManageHelper.getInstance().List.RemoveAll(p => p.ExpiredDate <= System.DateTime.Now);
                LoginerManageHelper.getInstance().List.RemoveAll(p => p.Md5Key == md5Cookie);
                while (LoginerManageHelper.getInstance().List.Count > 800)
                {
                    LoginerManageHelper.getInstance().List.RemoveRange(0, 300);
                }
                LoginerManageHelper.getInstance().List.Add(new LoginerInfo()
                {
                    AddTime = System.DateTime.Now,
                    ClientType = 1,
                    OperID = account,
                    ExpiredDate = System.DateTime.Now.AddHours(24),
                    Md5Key = md5Cookie
                });
                LoginerManageHelper.getInstance().Semaphore.Release();
                jMessage = new Message() { Result = true, TxtMessage = "登录成功。" };
            }
            catch (Exception e)
            {
                jMessage = new Message() { Result = false, TxtMessage = "服务繁忙。" };
            }
            return jMessage;
        
        
        }

        public Olb_User GetLoginerByMd5Cookie(string md5Cookie) 
        {

            Olb_User dbinfo = null;
            LoginerInfo info = LoginerManageHelper.getInstance().List.Where(p => p.Md5Key == md5Cookie).SingleOrDefault();
            if (info != null && info.OperID != null && info.OperID != string.Empty)
            {
                DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
                dbinfo = dd.GetTable<Olb_User>().Where(p => p.Account == info.OperID).SingleOrDefault();
               
            }
            return dbinfo;
        }


        public Message UnLRegisterClientByMd5Cookie(string md5Cookie) 
        {
            Message jMessage;
            try
            {
                LoginerManageHelper.getInstance().Semaphore.WaitOne();
                LoginerManageHelper.getInstance().List.RemoveAll(p => p.Md5Key == md5Cookie);
                LoginerManageHelper.getInstance().Semaphore.Release();
                jMessage = new Message() { Result = true, TxtMessage = "注销成功。" };
            }
            catch (Exception e)
            {
                jMessage = new Message() { Result = false, TxtMessage = "服务繁忙。" };
            }
            return jMessage;
        
        
        }





	}
}





