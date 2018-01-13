using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CY.IoTM.Common.Item;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 上报数据接口
    /// </summary>
    [ServiceContract]
    public interface IReportData
    {
        [OperationContract]
        SubmitResult Submit(Meter meter, SubmitData item);
    }
    /// <summary>
    /// 数据上报返回结果
    /// </summary>
    [DataContract]
    public class SubmitResult
    {
        /// <summary>
        /// 是否需要校准 true 需要校准
        /// </summary>
        [DataMember]
        public bool IsCalibration = false;
        /// <summary>
        /// 是否需要重新加载表对象 true 需要重新加载
        /// </summary>
        [DataMember]
        public bool IsReLoadMeter = false;
        /*数据标识DI，序号SER，上次结算日累计气量（4字节），上次结算日剩余金额（4字节），累计购入金额（4字节），当前结算日*/
        [DataMember]
        public List<Task> Calibrations = new List<Task>();

    }
    /// <summary>
    /// 上报数据结构
    /// </summary>
    [DataContract]
    public class SubmitData
    {
        /// <summary>
        /// 抄表时间
        /// </summary>
        [DataMember]
        public DateTime ReadDate
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置累计用气量
        /// </summary>
        [DataMember]
        public decimal LJGas
        {
            get;
            set;
        }
        /// <summary>
        /// 累计金额
        /// </summary>
        [DataMember]
        public decimal LJMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 剩余金额
        /// </summary>
        [DataMember]
        public decimal SYMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 上次结算累计量
        /// </summary>
        [DataMember]
        public decimal LastLJGas
        {
            get;
            set;
        }
        /// <summary>
        /// 结算日
        /// </summary>
        [DataMember]
        public byte JSDay
        {
            get;
            set;
        }
        /// <summary>
        /// 表状态1
        /// </summary>
        [DataMember]
        public ST1 ST1
        {
            get;
            set;
        }
        /// <summary>
        /// 表状态2
        /// </summary>
        [DataMember]
        public ST2 ST2
        {
            get;
            set;
        }       

    }
}
