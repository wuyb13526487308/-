using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IotM.Common;
using System.ServiceModel;
using System.Data.Linq;



namespace CY.IotM.DataService
{
    [ServiceBehavior(
         IncludeExceptionDetailInFaults = true,
         InstanceContextMode = InstanceContextMode.Single,
         ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class WebloginerManageService : ILoginerManage
    {

        public Message RegisterClient(string md5Cookie, string operID, string companyID)
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
                    ClientContext = OperationContext.Current,
                    ClientType = 1,
                    OperID = operID,
                    Company = companyID,
                    ExpiredDate = System.DateTime.Now.AddHours(24),
                    Md5Key = md5Cookie
                });
                LoginerManageHelper.getInstance().Semaphore.Release();
                jMessage = new Message() { Result = true, TxtMessage = "注册成功。" };
                Console.WriteLine(string.Format("{0}-客户端：md5[{1}]opr[{2}@{3}]。",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), md5Cookie, operID, companyID));

            }
            catch (Exception e)
            {
                //Log.Log.getInstance().Write(e, Log.MsgType.Error);
                jMessage = new Message() { Result = false, TxtMessage = "服务繁忙。" };
            }
            return jMessage;

        }
        public CompanyOperator GetLoginerByMd5Cookie(string md5Cookie)
        {
            CompanyOperator dbinfo = null;
            LoginerInfo info = LoginerManageHelper.getInstance().List.Where(p => p.Md5Key == md5Cookie).SingleOrDefault();
            if (info != null && info.OperID != null && info.OperID != string.Empty)
            {
                string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
                //Linq to SQL 上下文对象
                DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
                dbinfo = dd.GetTable<CompanyOperator>().Where(p => p.CompanyID == info.Company && p.OperID == info.OperID).SingleOrDefault();
                //账号已停用
                if (dbinfo.State != null && dbinfo.State.ToString() == "1")
                {
                    LoginerManageHelper.getInstance().Semaphore.WaitOne();
                    LoginerManageHelper.getInstance().List.RemoveAll(p => p.Md5Key == md5Cookie);
                    LoginerManageHelper.getInstance().Semaphore.Release();
                    dbinfo = null;
                }
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
                Console.WriteLine(string.Format("{0}-客户端：md5[{1}]注销成功。", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), md5Cookie));

            }
            catch (Exception e)
            {
                //Log.Log.getInstance().Write(e, Log.MsgType.Error);
                jMessage = new Message() { Result = false, TxtMessage = "服务繁忙。" };
            }
            return jMessage;
        }

    }
}
