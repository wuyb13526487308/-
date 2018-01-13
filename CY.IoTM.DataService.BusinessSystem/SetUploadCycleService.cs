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
    public class SetUploadCycleService : ISetUploadCycle
    {

        /// <summary>
        /// 添加全部上传周期
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meterList"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Message AddSetAlarmAll(IoT_SetUploadCycle info)
        {

            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                List<IoT_Meter> meterTempList = dd.GetTable<IoT_Meter>().Where(p => p.CompanyID == info.CompanyID).ToList();

                List<IoT_UploadCycleMeter> meterList = new List<IoT_UploadCycleMeter>();

                foreach (IoT_Meter meter in meterTempList)
                {
                    IoT_UploadCycleMeter alarmMeter = new IoT_UploadCycleMeter();
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
                    TxtMessage = "新增设置上传周期失败！" + e.Message
                };
            }
            return m;
        }



        /// <summary>
        /// 添加区域上传周期
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meterList"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Message AddSetAlarmArea(IoT_SetUploadCycle info, List<String> communityList)
        {

            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                List<View_UserMeter> meterTempList = dd.GetTable<View_UserMeter>().Where(p => p.CompanyID == info.CompanyID && communityList.Contains(p.Community)).ToList();

                List<IoT_UploadCycleMeter> meterList = new List<IoT_UploadCycleMeter>();

                foreach (View_UserMeter meter in meterTempList)
                {
                    IoT_UploadCycleMeter alarmMeter = new IoT_UploadCycleMeter();
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
                    TxtMessage = "新增设置上传周期失败！" + e.Message
                };
            }
            return m;

        }

        public Message Add(IoT_SetUploadCycle info,List<IoT_UploadCycleMeter> meterList)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
              
                //添加设置上传周期参数任务到通讯队列
                string result = new SetMeterParameter().SetUploadCycle(info, meterList);
                if (result != "")
                    throw new Exception(result);

                 Table<IoT_SetUploadCycle> tbl = dd.GetTable<IoT_SetUploadCycle>();

                 // 调用新增方法
                 tbl.InsertOnSubmit(info);
                 // 更新操作
                 dd.SubmitChanges();
                // 更新操作
                Table<IoT_UploadCycleMeter> tbl_meter = dd.GetTable<IoT_UploadCycleMeter>();
                foreach (IoT_UploadCycleMeter meter in meterList) {

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
                    TxtMessage = JSon.TToJson<IoT_SetUploadCycle>(info)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增设置上传周期失败！" + e.Message
                };
            }
            return m;
        }

        public Message Edit(IoT_SetUploadCycle info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                   IoT_SetUploadCycle dbinfo = dd.GetTable<IoT_SetUploadCycle>().Where(p => 
                     p.CompanyID == info.CompanyID && p.ID==info.ID).SingleOrDefault();

                    ConvertHelper.Copy<IoT_SetUploadCycle>(dbinfo, info);

                    // 更新操作
                    dd.SubmitChanges();
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.TToJson<IoT_SetUploadCycle>(dbinfo)
                    };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑设置上传周期失败！" + e.Message
                };
            }
            return m;
        }

        public Message Delete(IoT_SetUploadCycle info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                // 获得上下文对象中的表信息
                Table<IoT_SetUploadCycle> tbl = dd.GetTable<IoT_SetUploadCycle>();

                var s = tbl.Where(p => p.CompanyID == info.CompanyID && p.ID == info.ID).Single();
                tbl.DeleteOnSubmit(s as IoT_SetUploadCycle);
            

                // 更新操作
                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = "删除设置上传周期成功！"
                };
               
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "删除设置上传周期失败！" + e.Message
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
                IoT_SetUploadCycle dbinfo = dd.GetTable<IoT_SetUploadCycle>().Where(p =>
                  p.CompanyID == CompanyID && p.ID == int.Parse(ID.Trim()) ).SingleOrDefault();
                
                dbinfo.State = '1';

                ps.UnSetParameter(dbinfo.TaskID);

                List<IoT_UploadCycleMeter> LstdbinfoMeter = dd.GetTable<IoT_UploadCycleMeter>().Where(p =>
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
                    TxtMessage = JSon.TToJson<IoT_SetUploadCycle>(dbinfo)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑设置上传周期失败！" + e.Message
                };
            }
            return m;
        }

        /// <summary>
        /// 更新设置上传周期任务状态，用于上传周期设置完成，撤销，失败时调用更新。
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="state">状态： 1 完成 2 撤销  3 失败</param>
        /// <returns></returns>
        public string UpdateUploadCycleState(string taskID, TaskState state)
        {
            if (state == TaskState.Waitting) return "状态不能为申请";
            string result = "";
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_UploadCycleMeter dbinfo = dd.GetTable<IoT_UploadCycleMeter>().Where(p =>
                  p.TaskID == taskID).SingleOrDefault();
                dbinfo.State = Convert.ToChar(((byte)state).ToString());
                dbinfo.FinishedDate = DateTime.Now;
                // 更新操作
                dd.SubmitChanges();

                IoT_SetUploadCycle uploadCycle = null;
                int iCount = dd.GetTable<IoT_UploadCycleMeter>().Where(p => p.TaskID == taskID && p.State.ToString() == "0").Count();
                if (iCount == 0)
                {
                    uploadCycle = dd.GetTable<IoT_SetUploadCycle>().Where(p =>p.ID == dbinfo.ID).SingleOrDefault();
                    uploadCycle.State = Convert.ToChar(((byte)state).ToString());
                    uploadCycle.FinishedDate = DateTime.Now;
                }
                dd.SubmitChanges();
                if (state == TaskState.Undo)
                {
                    new M_SetParameterService().UnSetParameter(taskID);
                }
                if (state == TaskState.Finished)
                {
                    IoT_Meter meterInfo = dd.GetTable<IoT_Meter>().Where(p => p.MeterNo == dbinfo.MeterNo).SingleOrDefault();
                    meterInfo.UploadCycle = uploadCycle.ReportType;
                    
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

        //注：撤销设置上传周期任务请调用SetMeterParameter.UnSetParamter(taskID)方法
    }
}
