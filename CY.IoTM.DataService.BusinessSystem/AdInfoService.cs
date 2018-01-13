using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.Business;
using CY.IoTM.MongoDataHelper;

//TODO:废弃: IAdInfoManage
namespace CY.IoTM.DataService.Business
{
    public class AdInfoService 
    {
        public string ADPublishFinished(Common.Business.Task task)
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            string taskSource = task.TaskSource ;
            string[] ids = taskSource.Split(new char[] { '|' });
            string ap_id = ids[0];
            string userid = ids[1];
            string companyid = ids[2];
            int state = (task.TaskState == TaskState.Finished ? 1 : (task.TaskState == TaskState.Failed ? 2 : 0));

            string sql = string.Format(@"declare @State smallint,@AP_ID int,@UserID varchar(10),@CompanyID char(4),@FinishedDate datetime
set @State = {0}
set @AP_ID = {1}
set @UserID = '{2}'
set @CompanyID = '{3}'
set @FinishedDate ='{4}'
update ADPublishUser set State = @State ,FinishedDate = GETDATE() where  AP_ID = @AP_ID and UserID = @UserID and CompanyID = @CompanyID
if (@State = 2)
  update ADUser set AP_ID= @AP_ID,AC_ID = b.AC_ID from ADUser a,ADPublish b,ADPublishUser c where a.UserID = c.UserID and a.CompanyID = c.CompanyID and b.AP_ID = c.AP_ID and b.CompanyID = c.CompanyID 
  and c.AP_ID = @AP_ID and c.UserID = @UserID and c.CompanyID =@CompanyID",state,ap_id,userid,companyid,task.Finished);
            try
            {
                object[] param = new object[0];
                dd.ExecuteCommand(sql, param);
            }
            catch(Exception e)
            {
                return e.Message;
            }

            return "";
        }      




    }
}
