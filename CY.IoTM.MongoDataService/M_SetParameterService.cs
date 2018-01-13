using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IoTM.Common;
using CY.IoTM.Common.Business;
using CY.IoTM.Common.Item;

namespace CY.IoTM.MongoDataHelper
{
    public class M_SetParameterService
    {
        /// <summary>
        /// 设置报警参数任务
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meter"></param>
        /// <returns></returns>
        public string SetWariningParameter(IoT_SetAlarm info, IoT_AlarmMeter meter)
        {
            string result = "";
            try
            {
                MongoDBHelper<Task> mongo_task = new MongoDBHelper<Task>();
                Task task = new Task();
                task.MeterMac = meter.MeterNo.Trim();
                task.TaskDate = QuShi.getDate();
                task.TaskID = Guid.NewGuid().ToString();//用于和指令进行进行关联
                task.TaskState = TaskState.Waitting;
                task.TaskType = TaskType.TaskType_设置报警参数;//
                //写任务
                mongo_task.Insert(CollectionNameDefine.TaskCollectionName, task);
                meter.TaskID = task.TaskID;

                //3.设置报警参数
                DataItem_C103 item_C103 = new DataItem_C103(Convert.ToByte(new Random().Next(0, 255)), new WaringSwitchSign(info.SwitchTag));
                item_C103.长期未与服务器通讯报警时间 = Convert.ToByte(info.Par1);
                item_C103.燃气漏泄切断报警时间 = Convert.ToByte(info.Par2);
                item_C103.燃气流量过载切断报警时间 = Convert.ToByte(info.Par3);
                item_C103.异常大流量值 = MyDataConvert.TwoBCDStrToDecimal(info.Par4);
                item_C103.异常大流量切断报警时间 = Convert.ToByte(info.Par5);
                item_C103.异常微小流量切断报警时间 = Convert.ToByte(info.Par6);
                item_C103.持续流量切断报警时间 = Convert.ToByte(info.Par7);
                item_C103.长期未使用切断报警时间 = Convert.ToByte(info.Par8);
                //item_C103.长期未使用切断报警时间 = Convert.ToByte(info.Par7);

                Command cmd = new Command();
                cmd.TaskID = task.TaskID;
                cmd.Identification = ((UInt16)item_C103.IdentityCode).ToString("X2");
                cmd.ControlCode = (byte)ControlCode.CYWriteData;//设置参数
                cmd.DataLength = Convert.ToByte(item_C103.Length);
                cmd.DataCommand = MyDataConvert.BytesToHexStr(item_C103.GetBytes());
                cmd.Order = 3;
                CommandDA.Insert(cmd);
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }
        /// <summary>
        /// 设置上传周期
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meter"></param>
        /// <returns></returns>
        public string SetUploadCycle(IoT_SetUploadCycle info, IoT_UploadCycleMeter meter)
        {
            string result = "";
            try
            {
                MongoDBHelper<Task> mongo_task = new MongoDBHelper<Task>();
                Task task = new Task();
                task.MeterMac = meter.MeterNo.Trim();
                task.TaskDate = QuShi.getDate();
                task.TaskID = Guid.NewGuid().ToString();//用于和指令进行进行关联
                task.TaskState = TaskState.Waitting;
                task.TaskType = TaskType.TaskType_设置上传周期;//
                //写任务
                mongo_task.Insert(CollectionNameDefine.TaskCollectionName, task);
                meter.TaskID = task.TaskID;
                /*上传周期模式：
                00：以月为周期，在每个月的XX日YY时ZZ分上传数据；
                01：以XX天为周期，在每XX天的YY时ZZ分上传数据，起点为每月的01日00时00分；
                02：以YY时为周期，在每YY小时的ZZ分上传数据，起点为每天的00时00分； 
                03：以燃气表启动开始计时，以XX日YY时ZZ分上传数据
                */
                ReportCycleType cycleType = ReportCycleType.时周期;

                if(info.ReportType == "00")
                    cycleType = ReportCycleType.月周期;
                else if(info.ReportType == "01")
                    cycleType = ReportCycleType.天周期;
                else if (info.ReportType == "02")
                    cycleType = ReportCycleType.时周期;
                else if (info.ReportType == "03")
                    cycleType = ReportCycleType.周期采集;
                /*周期参数，
                DD 天    范围：00-31
                HH 小时  范围：00-23
                MM 分钟  范围：00-59*/
                int DD = 1;
                int HH = 23;
                int MM = 59;
                try
                {
                    DD = Convert.ToInt32(info.Par.Substring(0, 2));
                    HH = Convert.ToInt32(info.Par.Substring(2, 2));
                    MM = Convert.ToInt32(info.Par.Substring(4, 2));
                }
                catch
                { }

                //2.设置上传周期
                DataItem_C105 item_C105 = new DataItem_C105(Convert.ToByte(new Random().Next(0, 255)), cycleType, DD, HH, MM);
                item_C105.type = (ReportCycleType)Convert.ToByte(info.ReportType);
                item_C105.XX = Convert.ToUInt16(info.Par.Substring(0, 2));
                item_C105.YY = Convert.ToUInt16(info.Par.Substring(2, 2));
                item_C105.ZZ = Convert.ToUInt16(info.Par.Substring(4, 2));

                Command cmd = new Command();
                cmd.TaskID = task.TaskID;
                cmd.Identification = ((UInt16)item_C105.IdentityCode).ToString("X2");
                cmd.ControlCode = (byte)ControlCode.CYWriteData;//设置参数
                cmd.DataLength = Convert.ToByte(item_C105.Length);
                cmd.DataCommand = MyDataConvert.BytesToHexStr(item_C105.GetBytes());
                cmd.Order = 2;
                CommandDA.Insert(cmd);
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }
        /// <summary>
        /// 设置结算日
        /// </summary>
        /// <param name="settlementDay"></param>
        /// <param name="meter"></param>
        /// <returns></returns>
        public string SetSettlementDay(IoT_SetSettlementDay settlementDay, IoT_SettlementDayMeter meter)
        {
            string result = "";
            try
            {
                MongoDBHelper<Task> mongo_task = new MongoDBHelper<Task>();
                Task task = new Task();
                task.MeterMac = meter.MeterNo.Trim();
                task.TaskDate = QuShi.getDate();
                task.TaskID = Guid.NewGuid().ToString();//用于和指令进行进行关联
                task.TaskState = TaskState.Waitting;
                task.TaskType = TaskType.TaskType_设置结算日期;//
                //写任务
                mongo_task.Insert(CollectionNameDefine.TaskCollectionName, task);
                meter.TaskID = task.TaskID;

                //2.设置上传周期
                int JSDay = (int)settlementDay.SettlementDay;
                int JSMonth = (int)settlementDay.SettlementMonth;

                DataItem_A011 item_A011 = new DataItem_A011(Convert.ToByte(new Random().Next(0, 255)), JSDay, JSMonth);
                Command cmd = new Command();
                cmd.TaskID = task.TaskID;
                cmd.Identification = ((UInt16)item_A011.IdentityCode).ToString("X2");
                cmd.ControlCode = (byte)ControlCode.WriteData;//设置参数
                cmd.DataLength = Convert.ToByte(item_A011.Length);
                cmd.DataCommand = MyDataConvert.BytesToHexStr(item_A011.GetBytes());
                cmd.Order = 1;
                CommandDA.Insert(cmd);
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }
        /// <summary>
        /// 设置调价计划
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meter"></param>
        /// <returns></returns>
        public string SetPricingPlan(PricingPlan info, IoT_PricingMeter meter)
        {
            string result = "";
            try
            {
                MongoDBHelper<Task> mongo_task = new MongoDBHelper<Task>();
                Task task = new Task();
                task.MeterMac = meter.MeterNo.Trim();
                task.TaskDate = QuShi.getDate();
                task.TaskID = Guid.NewGuid().ToString();//用于和指令进行进行关联
                task.TaskState = TaskState.Waitting;
                task.TaskType = TaskType.TaskType_调整价格;//
                //读取表信息
                info.MeterNo = meter.MeterNo.Trim();
                info.TaskID = task.TaskID;

                //写任务
                mongo_task.Insert(CollectionNameDefine.TaskCollectionName, task);
                meter.TaskID = task.TaskID;

                //2.设置调价计划数据
                DataItem_A010 item_A010 = null;
                CT ct = new CT(info.MeterType == "00" ? MeterType.气量表 : MeterType.金额表,
                    (bool)info.IsUsedLadder, (JieSuanType)Convert.ToInt16(info.SettlementType.ToString()), ((int)info.Ladder) <=0 ? 1 : (int)info.Ladder);

                item_A010 = new DataItem_A010(Convert.ToByte(new Random().Next(0, 255)), ct, DateTime.Now);
                item_A010.Price1 = (decimal)info.Price1;
                item_A010.Price2 = (decimal)info.Price2;
                item_A010.Price3 = (decimal)info.Price3;
                item_A010.Price4 = (decimal)info.Price4;
                item_A010.Price5 = (decimal)info.Price5;
                item_A010.UseGas1 = (decimal)info.Gas1;
                item_A010.UseGas2 = (decimal)info.Gas2;
                item_A010.UseGas3 = (decimal)info.Gas3;
                item_A010.UseGas4 = (decimal)info.Gas4;
                item_A010.StartDate = Convert.ToDateTime(info.UseDate);

                Command cmd = new Command();
                cmd.TaskID = task.TaskID;
                cmd.Identification = ((UInt16)item_A010.IdentityCode).ToString("X2");
                cmd.ControlCode = (byte)ControlCode.WriteData;//设置参数
                cmd.DataLength = Convert.ToByte(item_A010.Length);
                cmd.DataCommand = MyDataConvert.BytesToHexStr(item_A010.GetBytes());
                cmd.Order = 1;
                CommandDA.Insert(cmd);
                new PricingPlanDA().NewPricingPlan(info);

                //注：该函数如何进行事务处理，保证所有数据更新都成功。
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 撤销任务队列
        /// </summary>
        /// <param name="taskID">任务ID</param>
        /// <returns></returns>
        public string UnSetParameter(string taskID)
        {
            string result = "";
            List<Command> list =  CommandDA.QueryCommandList(taskID);
            foreach (Command cmd in list)
            {
                if (cmd.CommandState == CommandState.Waitting)
                {
                    cmd.CommandState = CommandState.Undo;
                    result +=CommandDA.Update(cmd);
                }
            }
            TaskManageDA taskMDa = new TaskManageDA();
            Task task = taskMDa.QueryTask(taskID);
            if (task.TaskState == TaskState.Waitting)
            {
                task.TaskState = TaskState.Undo;
                result += taskMDa.TaskCompile(task);
            }

            return result;
        }

        public string XZFinished(string taskID)
        {
            string result = "";
            List<Command> list = CommandDA.QueryCommandList(taskID);
            foreach (Command cmd in list)
            {
                if (cmd.CommandState == CommandState.Waitting)
                {
                    cmd.CommandState = CommandState.Finished;
                    result += CommandDA.Update(cmd);
                }
            }
            TaskManageDA taskMDa = new TaskManageDA();
            Task task = taskMDa.QueryTask(taskID);
            if (task.TaskState == TaskState.Waitting)
            {
                task.TaskState = TaskState.Finished;
                result += taskMDa.TaskCompile(task);
            }

            return result;
        }
    }
}
