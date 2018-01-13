using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IoTM.Common;
using CY.IoTM.Common.Business;
using CY.IoTM.Common.Item;
using MongoDB.Driver;

namespace CY.IoTM.MongoDataHelper
{
    /// <summary>
    /// mongodb 层数据服务类
    /// </summary>
    public class M_ValveControlSevice
    {
        public static string TurnOn(IoT_Meter meter,out string taskID)
        {
            //创建一个任务
            string result = "";
            MongoDBHelper<Task> mongo_task = new MongoDBHelper<Task>();
            Task task = new Task();
            task.MeterMac = meter.MeterNo.Trim();
            task.TaskDate = QuShi.getDate();
            task.TaskID = Guid.NewGuid().ToString();//用于和指令进行进行关联
            task.TaskState = TaskState.Waitting;
            task.TaskType = TaskType.TaskType_开阀;//点火任务(DH)，换表登记(HB)、开阀（KF)、关阀(GF)、充值(CZ)、调整价格(TJ)
            //写任务
            result = mongo_task.Insert(CollectionNameDefine.TaskCollectionName, task);
            taskID = task.TaskID;
            if (result != "") return result;

            //创建命令
            Command cmd = new Command();
            byte ser = Convert.ToByte(new Random().Next(0, 255));
            DataItem_A017 item_A017 = new DataItem_A017(ser, true);
            cmd.TaskID = task.TaskID;
            cmd.Identification = ((UInt16)item_A017.IdentityCode).ToString("X2");
            cmd.ControlCode = (byte)ControlCode.WriteData;//写操作
            cmd.DataLength = Convert.ToByte(item_A017.Length);
            cmd.DataCommand = MyDataConvert.BytesToHexStr(item_A017.GetBytes());
            cmd.Order = 1;
            result = CommandDA.Insert(cmd);
            if (result != "") return result;
            return "";
        }

        public static string TurnOff(IoT_Meter meter, out string taskID)
        {
            //创建一个任务
            string result = "";
            MongoDBHelper<Task> mongo_task = new MongoDBHelper<Task>();
            Task task = new Task();
            task.MeterMac = meter.MeterNo.Trim();
            task.TaskDate = QuShi.getDate();
            task.TaskID = Guid.NewGuid().ToString();//用于和指令进行进行关联
            task.TaskState = TaskState.Waitting;
            task.TaskType = TaskType.TaskType_关阀;//点火任务(DH)，换表登记(HB)、开阀（KF)、关阀(GF)、充值(CZ)、调整价格(TJ)
            //写任务
            result = mongo_task.Insert(CollectionNameDefine.TaskCollectionName, task);
            taskID = task.TaskID;
            if (result != "") return result;

            //创建命令
            Command cmd = new Command();
            byte ser = Convert.ToByte(new Random().Next(0, 255));
            DataItem_A017 item_A017 = new DataItem_A017(ser, false);
            cmd.TaskID = task.TaskID;
            cmd.Identification = ((UInt16)item_A017.IdentityCode).ToString("X2");
            cmd.ControlCode = (byte)ControlCode.WriteData;//写操作
            cmd.DataLength = Convert.ToByte(item_A017.Length);
            cmd.DataCommand = MyDataConvert.BytesToHexStr(item_A017.GetBytes());
            cmd.Order = 1;
            result = CommandDA.Insert(cmd);
            if (result != "") return result;
            return "";
        }

        public static string Undo(string taskID)
        {
            //先读任务
            try
            {
                MongoDBHelper<Task> mongo = new MongoDBHelper<Task>();
                QueryDocument query = new QueryDocument();
                query.Add("TaskID", taskID);
                MongoCursor<Task> mongoCursor = mongo.Query(CollectionNameDefine.TaskCollectionName, query);
                var dataList = mongoCursor.ToList();
                if (dataList == null || dataList.Count == 0)
                    return "没有找到TaskID:【" + taskID + "】的任务。";

                Task task = dataList[0];
                if (task.TaskState != TaskState.Waitting)
                    return string.Format("任务TaskID:{0}状态为：{1},不能撤销.", taskID, task.TaskState);
                task.TaskState = TaskState.Undo;
                List<Command> list = CommandDA.QueryCommandList(task.TaskID);
                if(list!= null && list.Count ==0)
                    return string.Format("任务TaskID:{0}的指令已执行,不能撤销.", taskID);
                foreach (Command cmd in list)
                {
                    cmd.CommandState = CommandState.Undo;
                    cmd.AnswerDate = QuShi.getDate();
                    CommandDA.Update(cmd);
                }
                new TaskManageDA().TaskCompile(task);
                return "";
            }
            catch (Exception e)
            {
                //记录日志
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }

        public static string ValveControlFinished(string taskID,TaskState state)
        {
            try
            {
                MongoDBHelper<Task> mongo = new MongoDBHelper<Task>();
                QueryDocument query = new QueryDocument();
                query.Add("TaskID", taskID);
                MongoCursor<Task> mongoCursor = mongo.Query(CollectionNameDefine.TaskCollectionName, query);
                var dataList = mongoCursor.ToList();
                if (dataList == null || dataList.Count == 0)
                    return "没有找到TaskID:【" + taskID + "】的任务。";
                string result = "";
                Task task = dataList[0];
                task.Finished = QuShi.getDate();
                task.TaskState = state;
                TaskManageDA tm = new TaskManageDA();
                result = tm.TaskCompile(task);
                if (result != "")
                    return result;
                Meter _meter = tm.QueryMeter(task.MeterMac);
                if (state == TaskState.Finished)
                {
                    _meter.ValveState = task.TaskType == TaskType.TaskType_开阀 ? "0" : "1";
                    result = tm.UpdateMeter(_meter);
                }
                return result;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
