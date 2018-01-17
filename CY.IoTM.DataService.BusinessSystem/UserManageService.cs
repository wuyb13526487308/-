using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.Business;
using System.Data;
using System.Data.SqlClient;

namespace CY.IoTM.DataService.Business
{
    public class UserManageService:IUserManage
    {

        public Message Add(IoT_User info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                Table<IoT_User> tbl = dd.GetTable<IoT_User>();

                info.UserID = GetUserID();
                if (info.UserID == "-1") {
                    m = new Message()
                    {
                        Result = false,
                        TxtMessage = "新增用户失败！" +"获取用户id错误"
                    };
                    return m;
                }

                // 调用新增方法
                tbl.InsertOnSubmit(info);
                // 更新操作
                dd.SubmitChanges();

                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_User>(info)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增用户失败！" + e.Message
                };
            }
            return m;
        }



        public Message AddTemp(IoT_UserTemp info)
        {

            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                var count = dd.GetTable<IoT_Meter>().Where(p =>
                    p.CompanyID == info.CompanyID && p.MeterNo == info.MeterNo).Count();

                if (count > 0)
                {
                    m = new Message()
                    {
                        Result = false,
                        TxtMessage = "表号不能重复!"
                    };
                    return m;
                }

                Table<IoT_UserTemp> tbl = dd.GetTable<IoT_UserTemp>();
                count = tbl.Where(p => p.CompanyID == info.CompanyID && p.MeterNo == info.MeterNo).Count();
                if (count > 0)
                {
                    m = new Message()
                    {
                        Result = false,
                        TxtMessage = "表号不能重复!"
                    };
                    return m;
                }

                // 调用新增方法
                tbl.InsertOnSubmit(info);
                // 更新操作
                dd.SubmitChanges();

                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_UserTemp>(info)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增临时用户失败！" + e.Message
                };
            }
            return m;


        }


        public Message Edit(IoT_User info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_User dbinfo = dd.GetTable<IoT_User>().Where(p =>
                  p.CompanyID == info.CompanyID&&p.UserID==info.UserID).SingleOrDefault();

                ConvertHelper.Copy<IoT_User>(dbinfo, info);

                // 更新操作
                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_User>(dbinfo)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑用户失败！" + e.Message
                };
            }
            return m;
        }

        public Message Delete(IoT_User info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                Table<IoT_User> tbl = dd.GetTable<IoT_User>();
                var s = tbl.Where(p => p.CompanyID == info.CompanyID && p.UserID == info.UserID).Single();
                tbl.DeleteOnSubmit(s as IoT_User);
                

                // 更新操作
                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = "删除用户成功！"
                };
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "删除用户失败！" + e.Message
                };
            }
            return m;
        }




        /// <summary>
        /// 编辑用户档案
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Message EditUserMeter(View_UserMeter info){

            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_User dbinfo = dd.GetTable<IoT_User>().Where(p => p.CompanyID == info.CompanyID && p.UserID == info.UserID).SingleOrDefault();

                //dbinfo.UserID = info.UserID;
                dbinfo.UserName = info.UserName;
                dbinfo.Street = info.Street;
                dbinfo.Community = info.Community;
                dbinfo.Door = info.Door;
                dbinfo.Address = info.Address;

                IoT_Meter dbMeterinfo = dd.GetTable<IoT_Meter>().Where(p =>
                 p.CompanyID == info.CompanyID && p.UserID == info.UserID).SingleOrDefault();

                if (dbMeterinfo.MeterNo.Trim() != info.MeterNo.Trim()) {
                    UpadteMeterNo(dbMeterinfo.MeterNo, info.MeterNo);
                }

                // 更新操作
                dd.SubmitChanges();

                View_UserMeter viewInfo = dd.GetTable<View_UserMeter>().Where(p =>
                p.CompanyID == info.CompanyID && p.UserID == info.UserID).SingleOrDefault();

                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<View_UserMeter>(viewInfo)
                };
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑用户失败！" + e.Message
                };
            }
            return m;
        
        }

        /// <summary>
        /// 删除用户档案
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Message DeleteUserMeter(View_UserMeter info) {

            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                // 获得上下文对象中的表信息
                Table<IoT_Meter> tbl = dd.GetTable<IoT_Meter>();

                var s = tbl.Where(p => p.CompanyID == info.CompanyID && p.UserID == info.UserID).Single();
                tbl.DeleteOnSubmit(s as IoT_Meter);

                Table<IoT_MeterDataHistory> dhTbl = dd.GetTable<IoT_MeterDataHistory>();

                var data = dhTbl.Where(p => p.MeterNo == info.MeterNo);
                dhTbl.DeleteAllOnSubmit(data);

                Table<IoT_ValveControl> valTB = dd.GetTable<IoT_ValveControl>();
                var vavData = valTB.Where(p => p.CompanyID == info.CompanyID && p.MeterNo == info.MeterNo);

                valTB.DeleteAllOnSubmit(vavData);

                Table<IoT_AlarmInfo> alertTb = dd.GetTable<IoT_AlarmInfo>();
                var alertData = alertTb.Where(p => p.MeterNo == info.MeterNo);
                alertTb.DeleteAllOnSubmit(alertData);

                Table<Iot_MeterAlarmPara> mapTb = dd.GetTable<Iot_MeterAlarmPara>();
                var meterAlertData = mapTb.Where(p => p.MeterNo == info.MeterNo);
                mapTb.DeleteAllOnSubmit(meterAlertData);

                Table<IoT_User> tbl_user = dd.GetTable<IoT_User>();
                var u = tbl_user.Where(p => p.CompanyID == info.CompanyID && p.UserID == info.UserID).Single();
                tbl_user.DeleteOnSubmit(u as IoT_User);

                // 更新操作
                dd.SubmitChanges();
                new MongoDataHelper.TaskManageDA().DeleteMeter(info.MeterNo);
                m = new Message()
                {
                    Result = true,
                    TxtMessage = "删除用户成功！"
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "删除用户失败！" + e.Message
                };
            }
            return m;

        }


         /// <summary>
        /// 获取表号根据户号
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetUserMeterByUserId(string userId, string companyId)
        {

            string meterNo;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                View_UserMeter m = dd.GetTable<View_UserMeter>().Where(p => p.CompanyID == companyId && p.UserID == userId).SingleOrDefault();
                meterNo = m.MeterNo;
            }
            catch (Exception e) {
                meterNo = "";
            }
            return meterNo;
        }






        /// <summary>
        /// 更新表号
        /// </summary>
        /// <param name="oldNum"></param>
        /// <param name="newNum"></param>
        /// <returns></returns>
        public bool UpadteMeterNo(string oldNum, string newNum) {

            try
            {
                string sqlText = string.Format(" update IoT_Meter set MeterNo='{0}' where [MeterNo]='{1}' ", newNum, oldNum);
                SQLHelper.ExecuteScalar(SQLHelper.SchuleConnection, CommandType.Text, sqlText);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }




        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="oldNum"></param>
        /// <param name="newNum"></param>
        /// <returns></returns>
        public bool UpadteUserStatus(string state,string userId) {

            try
            {
                string sqlText = string.Format(" update IoT_User set State='{0}' where UserID='{1}'", state, userId);
                SQLHelper.ExecuteScalar(SQLHelper.SchuleConnection, CommandType.Text, sqlText);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Message BatchDeleteUserMeter(string userId)
        {
            // 定义执行结果
            Message m;
             try
            {
                 //什么状态的用户能删除，是否删除抄表和任务相关待定
                string sqlText = string.Format(" delete from IoT_User where userid='{0}'  delete from IoT_Meter where userid='{0}'", userId);
                SQLHelper.ExecuteScalar(SQLHelper.SchuleConnection, CommandType.Text, sqlText);
                m = new Message()
                {
                    Result = true,
                    TxtMessage = "删除用户成功！"
                };
            }
            catch (Exception e) {
                 m = new Message()
                {
                    Result = false,
                    TxtMessage = "删除用户失败！" + e.Message
                };
            }
            return m;
        }



        /// <summary>
        /// 批量添加用户
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meter"></param>
        /// <returns></returns>
        public Message BatchAddUserMeter(IoT_User info,IoT_Meter meter)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
             
                var count = dd.GetTable<IoT_Meter>().Where(p => p.CompanyID == meter.CompanyID && p.MeterNo == meter.MeterNo).Count();
                if (count > 0)
                {
                    m = new Message()
                    {
                        Result = false,
                        TxtMessage = "表号不能重复!"
                    };
                    return m;
                }


                Table<IoT_User> tbl = dd.GetTable<IoT_User>();
                info.UserID = GetUserID();
                if (info.UserID == "-1")
                {
                    m = new Message()
                    {
                        Result = false,
                        TxtMessage = "新增用户失败！" + "获取用户id错误"
                    };
                    return m;
                }
                // 调用新增方法
                tbl.InsertOnSubmit(info);
                // 更新操作
                dd.SubmitChanges();

                meter.UserID = info.UserID;
                MeterManageService meterService = new MeterManageService();
                m=meterService.Add(meter);
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增用户失败！" + e.Message
                };
            }
            return m;
        }



        /// <summary>
        /// 获取用户编号
        /// </summary>
        /// <returns></returns>
        public String GetUserID()
        {

            string id = "";
            try
            {
                id = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0');

                string sqlText = string.Format(" select  Max(UserID)  from  IoT_User  where UserID like '{0}%' ", id);
                object obj=SQLHelper.ExecuteScalar(SQLHelper.SchuleConnection, CommandType.Text, sqlText);

                if (obj == DBNull.Value)
                {
                    id =id+ "0001";
                }
                else
                {
                    id = (Convert.ToInt64(obj) + 1).ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                id = "-1";
            }
            return id;

        }



        /// <summary>
        /// 清除临时导入用户
        /// </summary>
        public void DeleteUserTemp() 
        {
            try
            {
                string sqlText = string.Format(" delete from IoT_UserTemp ");
                object obj = SQLHelper.ExecuteNonQuery(SQLHelper.SchuleConnection, CommandType.Text, sqlText);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



        /// <summary>
        /// 批量导入用户
        /// </summary>
        /// <param name="meterNo"></param>
        /// <returns></returns>
        public Message BatchImport(string meterNo)
        {

            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                Table<IoT_UserTemp> tbl = dd.GetTable<IoT_UserTemp>();
                IoT_UserTemp dbInfo = tbl.Where(p => p.MeterNo == meterNo).SingleOrDefault();

                IoT_User user = new IoT_User()
                {
                    UserName = dbInfo.UserName,
                    CompanyID = dbInfo.CompanyID,
                    Address = dbInfo.Address,
                    Phone = dbInfo.Phone,
                    State='1'

                };
                IoT_Meter meter = new IoT_Meter()
                {
                    MeterNo = dbInfo.MeterNo,
                    TotalAmount = dbInfo.MeterNum,
                    CompanyID = dbInfo.CompanyID
                };

                m = BatchAddUserMeter(user, meter);

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增用户失败！" + e.Message
                };
            }
            return m;
        }


    }
}
