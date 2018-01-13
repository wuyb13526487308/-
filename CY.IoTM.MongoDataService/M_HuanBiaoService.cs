using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IoTM.Common;
using CY.IoTM.Common.Business;
using CY.IoTM.Common.Item;

namespace CY.IoTM.MongoDataHelper
{
    /// <summary>
    /// mongo 层换表服务
    /// </summary>
    public class M_HuanBiaoService
    {
        /// <summary>
        /// 换表申请指令
        /// </summary>
        /// <returns></returns>
        public string SubmitASK(string taskID,IoT_Meter meter,decimal LJGas)
        {
            /*数据标识DI，序号SER，当前累计用量（金额），当前剩余用量（气量/金额），本阶梯周期已使用累计用量（气量）*/

            //创建一个任务
            MongoDBHelper<Task> mongo_task = new MongoDBHelper<Task>();
            Task task = new Task();
            task.MeterMac = meter.MeterNo.Trim();
            task.TaskDate = QuShi.getDate();
            task.TaskID = taskID;//用于和指令进行进行关联
            task.TaskState = TaskState.Waitting;
            task.TaskType = TaskType.TaskType_换表登记;//点火任务(DH)，换表登记(HB)、开阀（KF)、关阀(GF)、充值(CZ)、调整价格(TJ)
            //写任务
            mongo_task.Insert(CollectionNameDefine.TaskCollectionName, task);

            Command cmd = new Command();
            byte ser = Convert.ToByte(new Random().Next(0, 255));
            cmd = new Command();
            //1.写密钥
            DataItem_A014 item_A014 = new DataItem_A014(ser, (byte)meter.MKeyVer, meter.MKey);//
            cmd.TaskID = task.TaskID;
            cmd.Identification = ((UInt16)item_A014.IdentityCode).ToString("X2");
            cmd.ControlCode = (byte)ControlCode.WriteData;//写操作
            cmd.DataLength = Convert.ToByte(item_A014.Length);
            cmd.DataCommand = MyDataConvert.BytesToHexStr(item_A014.GetBytes());
            cmd.Order = 1;
            CommandDA.Insert(cmd);

            //2.设置上传周期
            DataItem_C105 item_C105 = new DataItem_C105(Convert.ToByte(new Random().Next(0, 255)), ReportCycleType.天周期, 1, 23, 59);
            cmd = new Command();
            cmd.TaskID = task.TaskID;
            cmd.Identification = ((UInt16)item_C105.IdentityCode).ToString("X2");
            cmd.ControlCode = (byte)ControlCode.CYWriteData;//设置参数
            cmd.DataLength = Convert.ToByte(item_C105.Length);
            cmd.DataCommand = MyDataConvert.BytesToHexStr(item_C105.GetBytes());
            cmd.Order = 2;
            CommandDA.Insert(cmd);

            //3.设置报警参数
            DataItem_C103 item_C103 = new DataItem_C103(Convert.ToByte(new Random().Next(0, 255)), new WaringSwitchSign() { 长期未使用切断报警 = true, 长期未与服务器通讯报警 = true });
            cmd = new Command();
            cmd.TaskID = task.TaskID;
            cmd.Identification = ((UInt16)item_C103.IdentityCode).ToString("X2");
            cmd.ControlCode = (byte)ControlCode.CYWriteData;//设置参数
            cmd.DataLength = Convert.ToByte(item_C103.Length);
            cmd.DataCommand = MyDataConvert.BytesToHexStr(item_C103.GetBytes());
            cmd.Order = 3;
            CommandDA.Insert(cmd);

            //4.写价格表
            DataItem_A010 item_A010 = null;
            CT ct = new CT(meter.MeterType == "00" ? MeterType.气量表 : MeterType.金额表,
                (bool)meter.IsUsed, (JieSuanType)Convert.ToInt16(meter.SettlementType.ToString()), meter.Ladder == null ? 1 : (int)meter.Ladder);

            item_A010 = new DataItem_A010(Convert.ToByte(new Random().Next(0, 255)), ct, DateTime.Now);
            item_A010.Price1 = (decimal)meter.Price1;
            item_A010.Price2 = (decimal)meter.Price2;
            item_A010.Price3 = (decimal)meter.Price3;
            item_A010.Price4 = (decimal)meter.Price4;
            item_A010.Price5 = (decimal)meter.Price5;
            item_A010.UseGas1 = (decimal)meter.Gas1;
            item_A010.UseGas2 = (decimal)meter.Gas2;
            item_A010.UseGas3 = (decimal)meter.Gas3;
            item_A010.UseGas4 = (decimal)meter.Gas4;
            item_A010.StartDate = DateTime.Now;

            cmd = new Command();
            cmd.TaskID = task.TaskID;
            cmd.Identification = ((UInt16)item_A010.IdentityCode).ToString("X2");
            cmd.ControlCode = (byte)ControlCode.WriteData;//设置参数
            cmd.DataLength = Convert.ToByte(item_A010.Length);
            cmd.DataCommand = MyDataConvert.BytesToHexStr(item_A010.GetBytes());
            cmd.Order = 4;
            CommandDA.Insert(cmd);

            //5.写结算日
            DataItem_A011 item_a011 = new DataItem_A011(Convert.ToByte(new Random().Next(0, 255)), Convert.ToByte(meter.SettlementDay));
            item_a011.JieSuanMonth = 1;//根据系统定义取值
            cmd = new Command();
            cmd.TaskID = task.TaskID;
            cmd.Identification = ((UInt16)item_a011.IdentityCode).ToString("X2");
            cmd.ControlCode = (byte)ControlCode.WriteData;//设置参数
            cmd.DataLength = Convert.ToByte(item_a011.Length);
            cmd.DataCommand = MyDataConvert.BytesToHexStr(item_a011.GetBytes());

            cmd.Order = 5;
            CommandDA.Insert(cmd);
            //6.写换表指令
            DataItem_C107 item_C107 = new DataItem_C107(Convert.ToByte(new Random().Next(0, 255)), 0.0m, (decimal)meter.TotalTopUp, LJGas);
            cmd = new Command();
            cmd.TaskID = task.TaskID;
            cmd.Identification = ((UInt16)item_C107.IdentityCode).ToString("X2");
            cmd.ControlCode = (byte)ControlCode.CYWriteData;//设置参数
            cmd.DataLength = Convert.ToByte(item_C107.Length);
            cmd.DataCommand = MyDataConvert.BytesToHexStr(item_C107.GetBytes());
            cmd.Order = 6;
            CommandDA.Insert(cmd);
            return "";
        }


    }
}
