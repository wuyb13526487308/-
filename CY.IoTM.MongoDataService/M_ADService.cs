using CY.IoTM.Common;
using CY.IoTM.Common.ADSystem;
using CY.IoTM.Common.Business;
using CY.IoTM.Common.Item;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CY.IoTM.MongoDataHelper
{
   public class M_ADService
    {
        private byte ser = 0;

        /// <summary>
        /// 发布广告
        /// </summary>
        /// <param name="ap">广告发布信息</param>
        /// <param name="listMeter">广告用户列表</param>
        /// <param name="aDItems">广告内容列表</param>
        /// <param name="type">发布类型</param>
        /// <returns></returns>
        public string PublishAD(ADPublish ap, List<ADPublisMeter> listMeter, List<ADItem> aDItems,PublishType type)
        {
            //创建一个任务
            ser++;

            MongoDBHelper<Task> mongo_task = new MongoDBHelper<Task>();
            // DataItem_C109(byte ser, ADPublishOperatorCode operatorCode,byte fileCount,List<ADFile> fileList)
            //发布文件列表
            List<Common.Item.ADFile> fileList = new List<Common.Item.ADFile>();
            //操作码
            ADPublishOperatorCode operatorCode = type == PublishType.NewPublish ? ADPublishOperatorCode.ReDefineList : ADPublishOperatorCode.AddList;
            //文件条数
            byte fileCount = (byte)aDItems.Count;
            foreach (ADItem item in aDItems)
            {
                Common.Item.ADFile aDfile = new Common.Item.ADFile((int)item.OrderID, item.StoreName, (DateTime)item.BDate, (DateTime)item.EDate, (int)item.Length, (int)item.FileLength);
                fileList.Add(aDfile);
            }

            foreach (ADPublisMeter meter in listMeter)
            {
                Task task = new Task();
                task.MeterMac = meter.MeterNo.Trim();
                task.TaskDate = QuShi.getDate();
                task.TaskID = Guid.NewGuid().ToString();//用于和指令进行进行关联
                task.TaskState = TaskState.Waitting;
                task.TaskType = TaskType.TaskType_发布广告;//广告文件(GGF)
                task.TaskSource = string.Format("{0}|{1}|{2}", meter.AP_ID, meter.UserID,meter.CompanyID);
                mongo_task.Insert(CollectionNameDefine.TaskCollectionName, task);

                //meter.TaskID = task.TaskID;  //设置任务编号
                //准备指令
                Command cmd = new Command();
                DataItem_C109 item_c109 = new DataItem_C109(ser, operatorCode, fileCount, fileList);
                cmd.Identification = ((UInt16)item_c109.IdentityCode).ToString("X2"); ;
                cmd.ControlCode = (byte)ControlCode.CYWriteData;//设置参数
                byte[] tmp = item_c109.GetBytes();
                cmd.DataLength = Convert.ToByte(tmp.Length);
                cmd.DataCommand = MyDataConvert.BytesToHexStr(tmp);  //指令内容
                cmd.Order = (byte)1;

                cmd.TaskID = task.TaskID;
                
                CommandDA.Insert(cmd);
            }

            //Command cmd = new Command();
            //byte ser = Convert.ToByte(new Random().Next(0, 255));
            //byte fileid = (byte)adfile.id;//广告命令 只存储文件编号
            //byte[] contentAllBytes = new byte[1];
            //contentAllBytes[0] = fileid;
            ////1.文件发送             
            //DataItem_C108 item_C108 = new DataItem_C108(ser, adfile.FileName, 1, 1, 1, 1, contentAllBytes);//
            //cmd.TaskID = task.TaskID;
            //cmd.Identification = ((UInt16)item_C108.IdentityCode).ToString("X2"); ;
            //cmd.ControlCode = (byte)ControlCode.CYWriteData;//设置参数
            //cmd.DataLength = Convert.ToByte(contentAllBytes.Length);
            //cmd.DataCommand = MyDataConvert.BytesToHexStr(item_C108.GetBytes()); ; //文件内容
            //cmd.Order = (byte)1;
            //CommandDA.Insert(cmd);  


            //1.文件配置发送指令
            //cmd = new Command();
            //DataItem_C109 item_C109 = new DataItem_C109(Convert.ToByte(new Random().Next(0, 255)), adddfile.FileNO, adddfile.FileName, adddfile.DTStart, adddfile.DTEnd, adddfile.PollTime, adddfile.OpShowStatus, adddfile.OpDeleteStatus);//
            //cmd.TaskID = task.TaskID;
            //cmd.Identification = ((UInt16)item_C109.IdentityCode).ToString("X2");
            //cmd.ControlCode = (byte)ControlCode.CYWriteData;//设置参数
            //cmd.DataLength = Convert.ToByte(item_C109.Length);
            //cmd.DataCommand = MyDataConvert.BytesToHexStr(item_C109.GetBytes());
            //cmd.Order = (byte)(2);
            //CommandDA.Insert(cmd);

            return "";//返回空表示成功
        }

        

       /// <summary>
       /// 撤销广告
       /// </summary>
       /// <param name="meter"></param>
       /// <returns></returns>
       public string UnPublish(string taskID)
       {

           string result = "";
           List<Command> list = CommandDA.QueryCommandList(taskID);
           foreach (Command cmd in list)
           {
               if (cmd.CommandState == CommandState.Waitting)
               {
                   cmd.CommandState = CommandState.Undo;
                   result += CommandDA.Update(cmd);
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
    }
}
