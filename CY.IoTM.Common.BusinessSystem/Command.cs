using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 表示对表的每个操作请求指令
    /// </summary>
    [DataContract()]
    [Serializable]
    public class Command : BaseEntity
    {
        /// <summary>
        /// 控制码
        /// </summary>
        [DataMember]
        public byte ControlCode { get; set; }
        /// <summary>
        /// 数据标识，字符表示的十六进制数，如：A010
        /// </summary>
        [DataMember]
        public string Identification { get; set; }
        /// <summary>
        /// 指令数据长度
        /// </summary>
        [DataMember]
        public byte DataLength { get; set; }

        /// <summary>
        /// 指令数据体，字符表示的十六进制数
        /// </summary>
        [DataMember]
        public string DataCommand { get; set; }

        /// <summary>
        /// 所属任务ID
        /// </summary>
        [DataMember]
        public string TaskID { get; set; }

        /// <summary>
        /// 指令执行状态，0 等待执行 1 已撤销 2 已完成  3 执行失败
        /// </summary>
        [DataMember]
        public CommandState CommandState { get; set; }

        [DataMember]
        public string AnswerData { get; set; }
        DateTime _answerDate;
        [DataMember]
        public DateTime AnswerDate { get { return _answerDate; } set { this._answerDate = value; } }

        /// <summary>
        /// 指令在任务中的执行顺序
        /// </summary>
        [DataMember]
        public byte Order { get; set; }
    }

    /// <summary>
    /// 指令状态
    /// </summary>
    public enum CommandState : int
    {
        /// <summary>
        /// 指令等待下发
        /// </summary>
        Waitting =0,
        /// <summary>
        /// 指令已撤销
        /// </summary>
        Undo = 1,
        /// <summary>
        /// 指令已完成
        /// </summary>
        Finished = 2,
        /// <summary>
        ///  指令执行失败
        /// </summary>
        Failed=3,

    }
}
