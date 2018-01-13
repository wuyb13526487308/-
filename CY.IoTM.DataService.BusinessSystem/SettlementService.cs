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


namespace CY.IoTM.DataService.Business
{
    /// <summary>
    /// 金额表气费结算类
    /// </summary>
    public class SettlementService : ISettlement
    {
        /// <summary>
        /// 添加全部结算日
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meterList"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Message AddSetAlarmAll(IoT_SetSettlementDay info)
        {

            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                List<IoT_Meter> meterTempList = dd.GetTable<IoT_Meter>().Where(p => p.CompanyID == info.CompanyID).ToList();

                List<IoT_SettlementDayMeter> meterList = new List<IoT_SettlementDayMeter>();

                foreach (IoT_Meter meter in meterTempList)
                {
                    IoT_SettlementDayMeter alarmMeter = new IoT_SettlementDayMeter();
                    alarmMeter.MeterNo = meter.MeterNo;
                    meterList.Add(alarmMeter);
                }
                m = Add(info, meterList);

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增设置结算日失败！" + e.Message
                };
            }
            return m;
        }



        /// <summary>
        /// 添加区域结算日
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meterList"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Message AddSetAlarmArea(IoT_SetSettlementDay info, List<String> communityList)
        {

            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                List<View_UserMeter> meterTempList = dd.GetTable<View_UserMeter>().Where(p => p.CompanyID == info.CompanyID && communityList.Contains(p.Community)).ToList();

                List<IoT_SettlementDayMeter> meterList = new List<IoT_SettlementDayMeter>();

                foreach (View_UserMeter meter in meterTempList)
                {
                    IoT_SettlementDayMeter alarmMeter = new IoT_SettlementDayMeter();
                    alarmMeter.MeterNo = meter.MeterNo;
                    meterList.Add(alarmMeter);
                }
                m = Add(info, meterList);

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增设置结算日失败！" + e.Message
                };
            }
            return m;

        }

        public Message Add(IoT_SetSettlementDay info, List<IoT_SettlementDayMeter> meterList)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                //添加设置结算日参数任务到通讯队列.
                string result = new SetMeterParameter().SetSettlementDay(info, meterList);
                if (result != "")
                    throw new Exception(result);

                Table<IoT_SetSettlementDay> tbl = dd.GetTable<IoT_SetSettlementDay>();
                // 调用新增方法
                tbl.InsertOnSubmit(info);
                // 更新操作
                dd.SubmitChanges();

                Table<IoT_SettlementDayMeter> tbl_meter = dd.GetTable<IoT_SettlementDayMeter>();
                foreach (IoT_SettlementDayMeter meter in meterList)
                {

                    IoT_Meter tempMeter = MeterManageService.QueryMeter(meter.MeterNo);
                    meter.MeterID = tempMeter.ID;
                    meter.ID = info.ID;
                    meter.Context = info.Context;
                    meter.State = '0';//申请
                    info.TaskID = meter.TaskID;
                    tbl_meter.InsertOnSubmit(meter);

                }
                // 更新操作
                dd.SubmitChanges();

                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_SetSettlementDay>(info)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增设置结算日失败！" + e.Message
                };
            }
            return m;
        }

        public Message Edit(IoT_SetSettlementDay info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_SetSettlementDay dbinfo = dd.GetTable<IoT_SetSettlementDay>().Where(p =>
                  p.CompanyID == info.CompanyID && p.ID == info.ID).SingleOrDefault();

                ConvertHelper.Copy<IoT_SetSettlementDay>(dbinfo, info);

                // 更新操作
                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_SetSettlementDay>(dbinfo)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑设置结算日失败！" + e.Message
                };
            }
            return m;
        }

        public Message Delete(IoT_SetSettlementDay info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                // 获得上下文对象中的表信息
                Table<IoT_SetSettlementDay> tbl = dd.GetTable<IoT_SetSettlementDay>();

                var s = tbl.Where(p => p.CompanyID == info.CompanyID && p.ID == info.ID).Single();
                tbl.DeleteOnSubmit(s as IoT_SetSettlementDay);


                // 更新操作
                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = "删除设置结算日成功！"
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "删除设置结算日失败！" + e.Message
                };
            }
            return m;
        }


        public Message revoke(string ID, string CompanyID)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                 M_SetParameterService ps = new M_SetParameterService();

                //更新主表
                IoT_SetSettlementDay dbinfo = dd.GetTable<IoT_SetSettlementDay>().Where(p =>
                  p.CompanyID == CompanyID && p.ID == int.Parse(ID.Trim())).SingleOrDefault();
            
                dbinfo.State = '1';
                ps.UnSetParameter(dbinfo.TaskID);
                List<IoT_SettlementDayMeter> LstdbinfoMeter = dd.GetTable<IoT_SettlementDayMeter>().Where(p =>
                  p.ID == int.Parse(ID.Trim())).ToList();
                foreach (var item in LstdbinfoMeter)
                {
                    item.State = '1';
                    ps.UnSetParameter(item.TaskID);
                }

                dd.SubmitChanges();
                // 更新操作
                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_SetSettlementDay>(dbinfo)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑设置结算日失败！" + e.Message
                };
            }
            return m;
        }

        /// <summary>
        /// 更新设置结算日任务状态
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="state">状态：0 申请 1 完成 2 撤销  3 失败</param>
        /// <returns></returns>
        public string SettlementDaySetFinished(string taskID, TaskState state)
        {
            string result = "";
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                TaskManageService tms = new TaskManageService();

                IoT_SettlementDayMeter dbinfo = dd.GetTable<IoT_SettlementDayMeter>().Where(p =>
                  p.TaskID == taskID).SingleOrDefault();
                dbinfo.State = Convert.ToChar(((byte)state).ToString ());
                dbinfo.FinishedDate = DateTime.Now;
                IoT_SetSettlementDay uploadCycle = dd.GetTable<IoT_SetSettlementDay>().Where(p =>p.ID == dbinfo.ID).SingleOrDefault();

                Meter meter = tms.GetMeter(dbinfo.MeterNo.Trim());
                if (meter != null)
                {
                    meter.SettlementDay = (int)uploadCycle.SettlementDay;
                    meter.SettlementMonth = (int)uploadCycle.SettlementMonth;
                    meter.SetNextSettlementDateTime();
                    result = new TaskManageDA().UpdateMeter(meter);
                }
                int iCount = dd.GetTable<IoT_SettlementDayMeter>().Where(p =>
                  p.TaskID == taskID && p.State.ToString() == "0").Count();
                if (iCount == 1)
                {
                    uploadCycle.State = Convert.ToChar(((byte)state).ToString());
                }
                if (result != "")
                    throw new Exception(result);
                // 更新操作
                dd.SubmitChanges();

                if (state == TaskState.Undo)
                {
                    //撤销操作需要更新任务队列数据
                    new M_SetParameterService().UnSetParameter(taskID);
                }
                if (state == TaskState.Finished)
                {
                    IoT_Meter meterInfo = dd.GetTable<IoT_Meter>().Where(p => p.MeterNo == dbinfo.MeterNo).SingleOrDefault();
                    meterInfo.SettlementDay = (int)uploadCycle.SettlementDay;
                    meterInfo.SettlementMonth = (int)uploadCycle.SettlementMonth;
                    // 更新操作
                    dd.SubmitChanges();
                }


            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }

    }
}
