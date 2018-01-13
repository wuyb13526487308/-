using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 对物联网表发送控制命令的有序集合数据结构
    /// </summary>
    //[DataContract()]
    [Serializable]
    public class Task : BaseEntity
    {
        /// <summary>
        /// 任务ID，主要用于和关系数据的ID对应
        /// </summary>
        public string TaskID { get; set; }
        /// <summary>
        /// 任务类型，点火任务(DH)，换表登记(HB)、开阀（KF)、关阀(GF)、充值(CZ)、调整价格(TJ)等  
        /// </summary>
        public string TaskType { get; set; }
        /// <summary>
        /// 任务指令列表
        /// </summary>
        public List<Command> CommandList = new List<Command>();
        /// <summary>
        /// 任务对应燃气表MAC地址
        /// </summary>
        public string MeterMac { get; set; }
        DateTime _taskDate;
                    
        /// <summary>
        /// 任务日期
        /// </summary>
        public DateTime TaskDate { get { return this._taskDate; }
            set { this._taskDate = value; }
        }
        /// <summary>
        /// 任务状态 0 等待 1 撤销 2 完成 3 失败
        /// </summary>
        public TaskState TaskState { get; set; }
        DateTime _finished;
        public DateTime Finished { get { return _finished; } set { this._finished = value; } }
        /// <summary>
        /// 任务源标识符
        /// </summary>
        public string TaskSource
        { get; set; }

        /// <summary>
        /// 计数器
        /// </summary>
        public int Counter { get; set; }

    }


    /// <summary>
    /// 任务状态
    /// </summary>
    public enum TaskState : int
    {
        /// <summary>
        /// 等待执行
        /// </summary>
        Waitting =0,
        /// <summary>
        /// 任务已撤销
        /// </summary>
        Undo =1,
        /// <summary>
        /// 任务已完成
        /// </summary>
        Finished =2,
        /// <summary>
        /// 指令执行失败
        /// </summary>
        Failed =3,
    }

    public class TaskType
    {
        //点火任务(DH)，换表登记(HB)、开阀（KF)、关阀(GF)、充值(CZ)、调整价格(TJ)、校时(XS) 等  
        public const string TaskType_点火 = "DH";
        public const string TaskType_换表登记 = "HB";
        public const string TaskType_开阀 = "KF";
        public const string TaskType_关阀 = "GF";
        public const string TaskType_充值 = "CZ";
        public const string TaskType_调整价格 = "TJ";
        public const string TaskType_校准 = "XZ";
        public const string TaskType_设置上传周期 = "SCZQ";
        public const string TaskType_设置报警参数 = "BJCS";
        public const string TaskType_设置结算日期 = "JSR";
        public const string TaskType_校时 = "XS";
        public const string TaskType_发布广告 = "FBGG";
        //public const string TaskType_编辑广告配置 = "BJGG";

    }
}
