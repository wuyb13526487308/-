using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.Business;


namespace CY.IoTM.DataService.Business
{
    public class SetAlarmService : ISetAlarm
    {


        /// <summary>
        /// 添加全部报警参数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meterList"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Message AddSetAlarmAll(IoT_SetAlarm info) 
        {

            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                List<IoT_Meter> meterTempList = dd.GetTable<IoT_Meter>().Where(p => p.CompanyID == info.CompanyID).ToList();

                List<IoT_AlarmMeter> meterList = new List<IoT_AlarmMeter>();

                foreach (IoT_Meter meter in meterTempList)
                {
                    IoT_AlarmMeter alarmMeter = new IoT_AlarmMeter();
                    alarmMeter.MeterNo = meter.MeterNo;
                    meterList.Add(alarmMeter);
                }
                m= Add(info, meterList);
              
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增设置报警参数失败！" + e.Message
                };
            }
            return m;
        }



        /// <summary>
        /// 添加区域报警参数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meterList"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Message AddSetAlarmArea(IoT_SetAlarm info, List<String> communityList)
        {

            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                List<View_UserMeter> meterTempList = dd.GetTable<View_UserMeter>().Where(p => p.CompanyID == info.CompanyID&&communityList.Contains(p.Community)).ToList();

                List<IoT_AlarmMeter> meterList = new List<IoT_AlarmMeter>();

                foreach (View_UserMeter meter in meterTempList)
                {
                    IoT_AlarmMeter alarmMeter = new IoT_AlarmMeter();
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
                    TxtMessage = "新增设置报警参数失败！" + e.Message
                };
            }
            return m;

        }




        /// <summary>
        /// 添加设置报警参数任务
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meterList"></param>
        /// <returns></returns>
        public Message Add(IoT_SetAlarm info,List<IoT_AlarmMeter> meterList)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                //添加设置报警参数任务到通讯队列
                string result = new SetMeterParameter().SetWarinningPararmeter(info, meterList);
                if (result != "")
                    throw new Exception(result);

                 Table<IoT_SetAlarm> tbl = dd.GetTable<IoT_SetAlarm>();
                // 调用新增方法
                tbl.InsertOnSubmit(info);
                // 更新操作
                dd.SubmitChanges();

                Table<IoT_AlarmMeter> tbl_meter = dd.GetTable<IoT_AlarmMeter>();
                foreach (IoT_AlarmMeter meter in meterList) {

                   IoT_Meter tempMeter = MeterManageService.QueryMeter(meter.MeterNo);
                   meter.MeterID = tempMeter.ID;
                   meter.ID = info.ID;
                   meter.Context = info.Context;
                   meter.State = '0';//申请
                   tbl_meter.InsertOnSubmit(meter);

                }
                // 更新操作
                dd.SubmitChanges();
          
                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_SetAlarm>(info)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增设置报警参数失败！" + e.Message
                };
            }
            return m;
        }

        public Message Edit(IoT_SetAlarm info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                   IoT_SetAlarm dbinfo = dd.GetTable<IoT_SetAlarm>().Where(p => p.CompanyID == info.CompanyID && p.ID==info.ID).SingleOrDefault();
                   ConvertHelper.Copy<IoT_SetAlarm>(dbinfo, info);

                    // 更新操作
                    dd.SubmitChanges();
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.TToJson<IoT_SetAlarm>(dbinfo)
                    };
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑设置报警参数失败！" + e.Message
                };
            }
            return m;
        }

        public Message Delete(IoT_SetAlarm info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                // 获得上下文对象中的表信息
                Table<IoT_SetAlarm> tbl = dd.GetTable<IoT_SetAlarm>();
                var s = tbl.Where(p => p.CompanyID == info.CompanyID && p.ID == info.ID).Single();
                tbl.DeleteOnSubmit(s as IoT_SetAlarm);
            
                // 更新操作
                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = "删除设置报警参数成功！"
                };
               
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "删除设置报警参数失败！" + e.Message
                };
            }
            return m;
        }

        /// <summary>
        /// 更新报警参数设置任务状态
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public string UpdateAlarmMeterState(string taskID, string state)
        {
            string result = "";
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_AlarmMeter dbinfo = dd.GetTable<IoT_AlarmMeter>().Where(p =>
                  p.TaskID ==taskID).SingleOrDefault();
                dbinfo.State = Convert.ToChar(state);
                // 更新操作
                dd.SubmitChanges();
                IoT_SetAlarm alarm = dd.GetTable<IoT_SetAlarm>().Where(p =>
    p.ID == dbinfo.ID).SingleOrDefault();
                this.UpdateMeterAlarmPar(dbinfo.MeterNo.Trim(), alarm);
                int iCount = dd.GetTable<IoT_AlarmMeter>().Where(p =>
                  p.ID == dbinfo.ID && p.State.ToString() == "0").Count();
                if (iCount == 0)
                {                    
                    alarm.State = Convert.ToChar(state);
                    dd.SubmitChanges();
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }


        public void UpdateMeterAlarmPar(string meterNo,IoT_SetAlarm para)
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);

            try
            {
                Iot_MeterAlarmPara map = dd.GetTable<Iot_MeterAlarmPara>().Where(p => p.MeterNo == meterNo).SingleOrDefault();
                if (map == null)
                {
                    map = new Iot_MeterAlarmPara();
                    map.MeterNo = meterNo;
                    map.Par1 = para.Par1;
                    map.Par2 = para.Par2;
                    map.Par3 = para.Par3;
                    map.Par4 = para.Par4;
                    map.Par5 = para.Par5;
                    map.Par6 = para.Par6;
                    map.Par7 = para.Par7;
                    map.Par8 = para.Par8;
                    map.Par9 = para.Par9;
                    map.SwitchTag = para.SwitchTag;
                    Table<Iot_MeterAlarmPara> tbl = dd.GetTable<Iot_MeterAlarmPara>();
                    tbl.InsertOnSubmit(map);
                    dd.SubmitChanges();
                }
                else
                {
                    map.Par1 = para.Par1;
                    map.Par2 = para.Par2;
                    map.Par3 = para.Par3;
                    map.Par4 = para.Par4;
                    map.Par5 = para.Par5;
                    map.Par6 = para.Par6;
                    map.Par7 = para.Par7;
                    map.Par8 = para.Par8;
                    map.Par9 = para.Par9;
                    map.SwitchTag = para.SwitchTag;
                    dd.SubmitChanges();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Iot_MeterAlarmPara GetMeterAlarmPara(string meterNo)
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                Iot_MeterAlarmPara map = dd.GetTable<Iot_MeterAlarmPara>().Where(p => p.MeterNo == meterNo).SingleOrDefault();
                return map;
            }
            catch
            {
                return null;
            }

        }



        /// <summary>
        /// 撤销设置参数任务
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meterList"></param>
        /// <returns></returns>
        public Message UnSetParamter(IoT_SetAlarm info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
  
                Table<IoT_AlarmMeter> tbl_meter = dd.GetTable<IoT_AlarmMeter>();
                List<IoT_AlarmMeter> list = tbl_meter.Where(p => p.ID == info.ID).ToList();

                foreach (IoT_AlarmMeter meter in list)
                {
                    string result = new SetMeterParameter().UnSetParamter(meter.TaskID);
                    meter.State='2';
                    meter.FinishedDate=DateTime.Now;
                }

                Table<IoT_SetAlarm> tbl = dd.GetTable<IoT_SetAlarm>();
                var dbinfo= tbl.Where(p => p.CompanyID == info.CompanyID && p.ID == info.ID).Single();
               
                dbinfo.State = '1';//任务撤销
                dbinfo.Context = info.Context;//撤销原因
                dd.SubmitChanges();
                
                m = new Message()
                {
                    Result = true,
                    TxtMessage ="操作成功"
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "撤销设置报警参数失败！" + e.Message
                };
            }
            return m;
        }
    }
}
