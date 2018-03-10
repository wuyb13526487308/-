using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using CY.IoTM.Common.Business;
using CY.IoTM.Common.Log;
using CY.IoTM.MongoDataHelper;

namespace CY.IoTM.DataService.Business
{
    public class TaskManageService:ITaskManage
    {
        object _readlock = new object();

        /// <summary>
        /// 根据表号读取表对象（mongo）
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public Meter GetMeter(string mac)
        {
            Meter meter = new CY.IoTM.MongoDataHelper.TaskManageDA().QueryMeter(mac);
            if (meter == null)
            {
                IoT_Meter iot_meter = new MeterManageService().GetMeterByNo(mac);
                if (iot_meter != null)
                {
                    new CY.IoTM.MongoDataHelper.TaskManageDA().InsertMeter(iot_meter);
                    meter = new CY.IoTM.MongoDataHelper.TaskManageDA().QueryMeter(mac);
                }
            }
            short ver = meter.LastTopUpSer;
            Random rd = new Random();
            int newVer = rd.Next(0, 255);
            while(ver == newVer)
            {
                rd = new Random();
                newVer = rd.Next(0, 255);
            }
            meter.LastTopUpSer = (byte)newVer;
            return meter;
        }

        /// <summary>
        /// 读取表任务队列
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        public List<Task> GetTaskList(string mac)
        {
            return new CY.IoTM.MongoDataHelper.TaskManageDA().GetTaskList(mac);
        }

        #region 任务及指令完成部分
        /// <summary>
        /// 指令完成
        /// </summary>
        /// <param name="command"></param>
        /// <param name="task"></param>
        public void CommandCompletes(Command command,Task task)
        {
            lock(_readlock)
            {
                //A014 写新密钥
                if (command.Identification.ToUpper() == "A014")
                {
                    //更新通讯密钥完成
                    Meter meter = new CY.IoTM.MongoDataHelper.TaskManageDA().QueryMeter(task.MeterMac);
                    if (!meter.IsDianHuo)
                    {
                        meter.IsDianHuo = true;
                        new CY.IoTM.MongoDataHelper.TaskManageDA().UpdateMeter(meter);
                    }
                }
                //更新mongodb任务状态
                new CY.IoTM.MongoDataHelper.TaskManageDA().CommandCompile(command);
            }
        }

        /// <summary>
        /// 任务执行完成
        /// </summary>
        /// <param name="task"></param>
        public string TaskCompletes(Task task,decimal ljGas =0)
        {
            string result = "";
            lock (_readlock)
            {
                //任务完成，设置表状态点火完成
                try
                {
                    //根据任务类型，处理《任务类型，点火任务(DH)，换表登记(HB)、开阀（KF)、关阀(GF)、充值(CZ)、调整价格(TJ)等  》
                    switch (task.TaskType)
                    {
                        case TaskType.TaskType_点火:
                            MeterManageService mms = new MeterManageService();
                            IoT_Meter m = new DianHuoService().QueryMeter(task.MeterMac);
                            m.MeterState = '0';
                            m.TotalAmount = ljGas;
                            CY.IotM.Common.Message msg = mms.Edit(m);
                            //更新mongo层Meter对象计价参数
                            new UserManageService().UpadteUserStatus("3", m.UserID);//用户表状态置为 正常使用
                            if (msg.Result == false)
                                throw new Exception(msg.TxtMessage);
                            break;
                        case TaskType.TaskType_换表登记:
                            //在此实现换表完成代码
                            new HuanBiaoService().ChangeMeterFinished(task.TaskID, TaskState.Finished);
                            break;
                        case TaskType.TaskType_开阀:
                        case TaskType.TaskType_关阀:
                            ValveControlService valveControl = new ValveControlService();
                            valveControl.ValveControlFinished(task.TaskID, task.TaskType, TaskState.Finished);
                            break;
                        case TaskType.TaskType_充值:
                            //充值完成
                            TopUpService tps = new TopUpService();
                            string topup = tps.TopupFinished(task.TaskID, task.TaskState, "");
                            if (topup != "")
                            {
                                throw new Exception($"表号：{task.MeterMac} 充值完成，但更新mongo内存数据失败，原因：{topup}");
                            }
                            break;
                        case TaskType.TaskType_调整价格:
                            //表具已接受调价指令
                            new PricingManageService().UpdatePricingTaskState(task.TaskID, TaskState.Finished);
                            break;
                        case TaskType.TaskType_校准:
                            //系统自动发送和完成，无需做其他处理
                            new M_SetParameterService().XZFinished(task.TaskID);
                            break;
                        case TaskType.TaskType_设置报警参数:
                            new SetAlarmService().UpdateAlarmMeterState(task.TaskID, ((byte)TaskState.Finished).ToString());
                            break;
                        case TaskType.TaskType_设置结算日期:
                            //修改结算日期完成
                            new SettlementService().SettlementDaySetFinished(task.TaskID, TaskState.Finished);
                            break;
                        case TaskType.TaskType_设置上传周期:
                            //
                            new SetUploadCycleService().UpdateUploadCycleState(task.TaskID, TaskState.Finished);
                            break;
                        case TaskType.TaskType_发布广告:
                            if (task.TaskState != TaskState.Waitting)
                            {
                                new AdInfoService().ADPublishFinished(task);
                            }

                            break;
                            //case TaskType.TaskType_编辑广告配置:
                            //    new AdInfoService().UpdateTaskStatus(task.TaskID, TaskState.Finished);

                            //    break;

                    }

                    new CY.IoTM.MongoDataHelper.TaskManageDA().TaskCompile(task);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Log.getInstance().Write(MsgType.Error, "TaskCompletes 任务完成状态更新失败，原因：" + e.Message);
                    Log.getInstance().Write(new OneMeterDataLogMsg(task.MeterMac, e.Message));
                }

                return result;
            }
        }
        #endregion

        /// <summary>
        /// 添加对时任务
        /// </summary>
        /// <param name="task"></param>
        public void SetDuiShiTask(Task task, Command cmd)
        {
            new CY.IoTM.MongoDataHelper.TaskManageDA().AddDuiShiTask(task,cmd);
        }

        /// <summary>
        /// 更新表对象（mongo中）
        /// </summary>
        /// <param name="meter"></param>
        /// <returns></returns>
        public string UpdateMeter(Meter meter)
        {
            return new TaskManageDA().UpdateMeter(meter);//同时更新数据到mongoDB中
        }

        /// <summary>
        /// 获取所有表（用户模拟终端查询）
        /// </summary>
        /// <returns></returns>
        public List<IoT_Meter> getIotMeters(string address = "", string meterMac = "")
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                Table<IoT_Meter> dbinfo = dd.GetTable<IoT_Meter>();
                Table<IoT_User> dbUser = dd.GetTable<IoT_User>();
                if (address != "" && meterMac != "")
                {
                    var query1 = from p in dbinfo
                                 join u in dbUser on p.UserID equals u.UserID
                                 where u.Address.Contains(address) && p.MeterNo.Contains(meterMac)
                                 select p;
                    return query1.ToList();
                }
                else if(address != "" && meterMac =="")
                {
                    var query2 = from p in dbinfo
                                 join u in dbUser on p.UserID equals u.UserID
                                 where u.Address.Contains(address)
                                 select p;
                    return query2.ToList();
                }

                return dbinfo.ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        /// <summary>
        /// 用于通道可用性测试，无其他作用
        /// </summary>
        /// <returns></returns>
        public int TestChannel()
        {
            return 0;
        }
    }
}
