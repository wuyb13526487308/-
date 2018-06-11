using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IoTM.Common.Business;
using MongoDB.Driver;

namespace CY.IoTM.MongoDataHelper
{
    /// <summary>
    /// 任务管理数据访问
    /// </summary>
    public class TaskManageDA
    {
        public List<Task> GetTaskList(string mac)
        {
            List<Task> list = new List<Task> ();
            try
            {
                MongoDBHelper<Task> mongo = new MongoDBHelper<Task>();
                QueryDocument query = new QueryDocument();
                query.Add("MeterMac", mac);
                query.Add("TaskState", 0);
                MongoCursor<Task> mongoCursor = mongo.Query(CollectionNameDefine.TaskCollectionName, query);
                var dataList = mongoCursor.ToList();
                foreach (Task task in dataList)
                {
                    if (task.TaskType != TaskType.TaskType_校准)
                    {
                        task.CommandList.AddRange(CommandDA.QueryCommandList(task.TaskID));
                        list.Add(task);
                    }
                }
            }
            catch (Exception e)
            {
                //记录日志
                Console.WriteLine(e.Message);
            }
            return list;
        }


        public Task QueryTask(string taskID)
        {
            try
            {
                MongoDBHelper<Task> mongo = new MongoDBHelper<Task>();
                QueryDocument query = new QueryDocument();
                query.Add("TaskID", taskID);
                MongoCursor<Task> mongoCursor = mongo.Query(CollectionNameDefine.TaskCollectionName, query);
                var dataList = mongoCursor.ToList();
                 

                if (dataList != null)
                    return dataList[0];
            }
            catch (Exception e)
            {
                //记录日志
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public Meter QueryMeter(string mac)
        {
            return MeterDA.QueryMeter(mac);
        }     
        
        public string InsertMeter(Meter meter)
        {
            return MeterDA.InsertMeter(meter);
        }

        /// <summary>
        /// 添加表对象到mongodb库中（用于新表安装）
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string InsertMeter(IoT_Meter info)
        {
            Meter _meter = QueryMeter(info.MeterNo.Trim());
            if (_meter != null)
            {
                MongoDBHelper<Meter> mongo = new MongoDBHelper<Meter>();
                var iDelete = new QueryDocument();
                iDelete.Add("Mac", info.MeterNo.Trim ());
                mongo.Delete(CollectionNameDefine.MeterCollectionName, iDelete);
            }

            Meter meter = new Meter();
            meter.MeterID = info.ID;
            meter.UserID = info.CompanyID.Trim () + info.UserID.Trim();
            meter.Key = info.MKey;
            meter.Mac = info.MeterNo.Trim();
            meter.MeterState = info.MeterState.ToString();
            meter.MeterType = info.MeterType;
            meter.MKeyVer = (byte)(info.MKeyVer & 0xff);
            meter.PriceCheck = info.PriceCheck.ToString();
            meter.SettlementDay = (int)info.SettlementDay;
            meter.SettlementType = info.SettlementType;
            meter.TotalAmount = (decimal)info.TotalAmount;
            meter.TotalTopUp = (decimal)info.TotalTopUp;
            meter.ValveState = info.ValveState.ToString();
            meter.LastTopUpSer = 0;
            meter.IsUsedLadder = (bool)info.IsUsed;
            meter.Ladder = (int)info.Ladder;
            meter.MeterType = info.MeterType;
            meter.Price1 = (decimal)info.Price1;
            meter.Price2 = (decimal)info.Price2;
            meter.Price3 = (decimal)info.Price3;
            meter.Price4 = (decimal)info.Price4;
            meter.Price5 = (decimal)info.Price5;
            meter.Gas1 = (decimal)info.Gas1;
            meter.Gas2 = (decimal)info.Gas2;
            meter.Gas3 = (decimal)info.Gas3;
            meter.Gas4 = (decimal)info.Gas4;
            meter.SettlementMonth = (int)info.SettlementMonth;
            meter.SettlementDay =(int) info.SettlementDay;
            meter.SettlementType = info.SettlementType;
            meter.CurrentLadder = 1;
            meter.CurrentPrice = meter.Price1;
            if (meter.IsUsedLadder && meter.CurrentLadder < meter.Ladder)
            {
                meter.NextSettlementPointGas = meter.TotalAmount + meter.Gas1;
                meter.SetNextSettlementDateTime();
            }
            meter.LastTotal = meter.TotalAmount;
            meter.LastSettlementAmount = (decimal)info.TotalTopUp;
            meter.LastGasPoint = meter.TotalAmount;
            meter.CurrentBalance = (decimal)info.TotalTopUp;
            meter.LJMoney = 0;
            meter.IsDianHuo = false;//点火标记 false 未点火
            meter.IsPricing = false;
            meter.CreateBillID();

            return this.InsertMeter(meter);//同时插入数据到mongoDB中
        }
        public string DeleteMeter(string mac)
        {
            MongoDBHelper<Meter> mongo = new MongoDBHelper<Meter>();
            var iDelete = new QueryDocument();
            iDelete.Add("Mac", mac.Trim());
            mongo.Delete(CollectionNameDefine.MeterCollectionName, iDelete);

            //删除未被执行的指令队列
            List<Task> list = GetTaskList(mac);
            MongoDBHelper<Task> mongo_task = new MongoDBHelper<Task>();
            MongoDBHelper<Command> mongo_cmd = new MongoDBHelper<Command>();

            foreach (Task t in list)
            {
                var cmdDel = new QueryDocument();
                cmdDel.Add("TaskID", t.TaskID);
                //cmdDel.Add("CommandState", "0");
                mongo_cmd.Delete(CollectionNameDefine.CommandCollectionName, cmdDel);
                var taskDel = new QueryDocument();
                taskDel.Add("MeterMac", mac.Trim());
                //taskDel.Add("TaskState", "0");
                mongo_task.Delete(CollectionNameDefine.TaskCollectionName, taskDel);
            }
            return "";
        }

        public string ChangeMeterInsert(IoT_Meter info,int currentLader,decimal currentPrice,decimal changeUseGas)
        {
            Meter _meter = QueryMeter(info.MeterNo.Trim());
            if (_meter != null)
            {
                MongoDBHelper<Meter> mongo = new MongoDBHelper<Meter>();
                var iDelete = new QueryDocument();
                iDelete.Add("Mac", info.MeterNo.Trim());
                mongo.Delete(CollectionNameDefine.MeterCollectionName, iDelete);
            }

            Meter meter = new Meter();
            meter.MeterID = info.ID;
            meter.UserID = info.CompanyID + info.UserID;
            meter.Key = info.MKey;
            meter.Mac = info.MeterNo.Trim();
            meter.MeterState = info.MeterState.ToString();
            meter.MeterType = info.MeterType;
            meter.MKeyVer = (byte)(info.MKeyVer & 0xff);
            meter.PriceCheck = info.PriceCheck.ToString();
            meter.SettlementDay = (int)info.SettlementDay;
            meter.SettlementType = info.SettlementType;
            meter.TotalAmount = (decimal)info.TotalAmount;//表当前累计总量
            meter.TotalTopUp = (decimal)info.TotalTopUp;//充值金额
            meter.ValveState = info.ValveState.ToString();
            meter.LastTopUpSer = 0;//上次充值ver
            meter.IsUsedLadder = (bool)info.IsUsed;
            meter.Ladder = (int)info.Ladder;
            meter.MeterType = info.MeterType;
            meter.Price1 = (decimal)info.Price1;
            meter.Price2 = (decimal)info.Price2;
            meter.Price3 = (decimal)info.Price3;
            meter.Price4 = (decimal)info.Price4;
            meter.Price5 = (decimal)info.Price5;
            meter.Gas1 = (decimal)info.Gas1;
            meter.Gas2 = (decimal)info.Gas2;
            meter.Gas3 = (decimal)info.Gas3;
            meter.Gas4 = (decimal)info.Gas4;
            meter.SettlementMonth = (int)info.SettlementMonth;
            meter.SettlementDay = (int)info.SettlementDay;
            meter.SettlementType = info.SettlementType;
            meter.CurrentLadder = currentLader;
            meter.CurrentPrice = currentPrice;
            decimal[] gas = new decimal[4];
            gas[0] = meter.Gas1;
            gas[1] = meter.Gas2;
            gas[2] = meter.Gas3;
            gas[3] = meter.Gas4;

            if (meter.IsUsedLadder)
            {
                meter.NextSettlementPointGas = meter.TotalAmount - changeUseGas;
                if (meter.CurrentLadder < meter.Ladder)
                {
                    for (int i = 0; i < meter.CurrentLadder; i++)
                    {
                        meter.NextSettlementPointGas += gas[i];
                    }
                }
                else
                {
                    meter.NextSettlementPointGas = -1;
                }
                meter.SetNextSettlementDateTime();
            }

            meter.LastGasPoint = meter.TotalAmount;
            meter.CurrentBalance = (decimal)info.TotalTopUp;
            meter.LastSettlementAmount = meter.CurrentBalance;
            meter.LastTotal = meter.TotalAmount - changeUseGas;
            meter.LJMoney = 0;
            meter.IsDianHuo = true;//点火完成标记
            return this.InsertMeter(meter);//同时插入数据到mongoDB中
        }

        public string UpdateMeter(Meter meter)
        {
            return new MeterDA().UpdateMeter(meter);            
        }
        public string UpdateMeter(IoT_Meter info)
        {
            return MeterDA.UpdateMeter(info);
        }

        public void CommandCompile(Command command)
        {
            CommandDA.Update(command);
        }

        public void AddDuiShiTask(Task task, Command cmd)
        {
            MongoDBHelper<Task> mongo_task = new MongoDBHelper<Task>();
            mongo_task.Insert(CollectionNameDefine.TaskCollectionName, task);
            CommandDA.Insert(cmd);
        }

        public string TaskCompile(Task task)
        {
            MongoDBHelper<Task> mongo = new MongoDBHelper<Task>();
            var query = new QueryDocument();
            query.Add("_id", task._id);
            var update = new UpdateDocument();
           
            update.Add("TaskState", task.TaskState);
            update.Add("Finished", QuShi.getDate());
            update.Add("MeterMac", task.MeterMac);
            update.Add("TaskDate", task.TaskDate);
            update.Add("TaskID", task.TaskID);
            update.Add("TaskType", task.TaskType);
            update.Add("TaskSource", task.TaskSource == null ?"":task.TaskSource);
            //update.Add("CommandList", task.CommandList);
            return mongo.Update(CollectionNameDefine.TaskCollectionName, query, update);            
        }
    }
}
