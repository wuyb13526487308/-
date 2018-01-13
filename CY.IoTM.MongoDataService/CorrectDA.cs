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
    /// 数据修正Mongodb处理类
    /// </summary>
    public class CorrectDA
    {
        /// <summary>
        /// 添加修正任务
        /// </summary>
        /// <param name="correctRecord"></param>
        /// <returns></returns>
        public string AddCorrentTask(CorrectRecord correctRecord,out Task returnTask)
        {
            string result = "";
            returnTask = null;
            try
            {
                //先删除未执行的任务
                DeleteUnRunTask(correctRecord.MeterNo.Trim());
                MongoDBHelper<Meter> mongo = new MongoDBHelper<Meter>();
                //1、记录修正信息
                result = mongo.Insert(CollectionNameDefine.MeterCorrectRecord, correctRecord);
                if (result != "")
                    return result;
                //2、创建修正任务指令
                //创建一个任务
                MongoDBHelper<Task> mongo_task = new MongoDBHelper<Task>();
                Task task = new Task();
                task.MeterMac = correctRecord.MeterNo.Trim();
                task.TaskDate = QuShi.getDate();
                task.TaskID = Guid.NewGuid().ToString();//用于和指令进行进行关联
                task.TaskState = TaskState.Waitting;
                task.TaskType = TaskType.TaskType_校准;//点火任务(DH)，换表登记(HB)、开阀（KF)、关阀(GF)、充值(CZ)、调整价格(TJ)
                //写任务
                result = mongo_task.Insert(CollectionNameDefine.TaskCollectionName, task);
                if (result != "")
                    return result;

                Command cmd = new Command();
                //1.写校正指令
                byte ser = Convert.ToByte(new Random().Next(0, 255));
                DataItem_C102 item_C102 = new DataItem_C102(ser, correctRecord.TotalTopUp - correctRecord.SettlementBalance, correctRecord.SettlementBalance, correctRecord.LastSettlementDayLJGas);
                cmd.TaskID = task.TaskID;
                cmd.Identification = ((UInt16)item_C102.IdentityCode).ToString("X2");
                cmd.ControlCode = (byte)ControlCode.CYWriteData;//创源写操作
                cmd.DataLength = Convert.ToByte(item_C102.Length);
                cmd.DataCommand = MyDataConvert.BytesToHexStr(item_C102.GetBytes());
                cmd.Order = 1;
                //返回修正任务
                result = CommandDA.Insert(cmd);
                returnTask = task;
                task.CommandList.Add(cmd);
                if (result != "")
                    return result;

            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "";
        }
        //删除未执行的修正任务
        public void DeleteUnRunTask(string mac)
        {
            MongoDBHelper<Task> mongo = new MongoDBHelper<Task>();
            QueryDocument query = new QueryDocument();
            query.Add("MeterMac", mac);
            query.Add("TaskState", 0);
            query.Add("TaskType", "XZ");

            MongoCursor<Task> mongoCursor = mongo.Query(CollectionNameDefine.TaskCollectionName, query);
            var dataList = mongoCursor.ToList();
            if (dataList != null && dataList.Count > 0)
            {
                foreach (Task task in dataList)
                {
                    var iDelete = new QueryDocument();
                    iDelete.Add("TaskID", task.TaskID);
                    mongo.Delete(CollectionNameDefine.CommandCollectionName, iDelete);
                }
                mongo.Delete(CollectionNameDefine.TaskCollectionName, query);
            }
        }
    }
}
