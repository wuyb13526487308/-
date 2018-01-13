using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 燃气表修正记录
    /// </summary>
    public class CorrectRecord : BaseEntity
    {
        //修正时间、修正原因、 表号、表上传的当前余额、 上次结算日累计气量（4字节），上次结算日剩余金额（4字节），累计购入金额（4字节），当前结算日
        /// <summary>
        /// 修正日期
        /// </summary>
        public string CorrectDate
        {
            get;
            set;
        }
        /// <summary>
        /// 表号
        /// </summary>
        public string MeterNo
        {
            get;
            set;
        }
        /// <summary>
        /// 修正原因
        /// </summary>
        public string CorrectReason
        {
            get;
            set;
        }
        /// <summary>
        /// 表上传的当前余额
        /// </summary>
        public decimal MeterBalance
        {
            get;
            set;
        }
        /// <summary>
        /// 表上传的累计量
        /// </summary>
        public decimal MeterLJGas
        {
            get;
            set;
        }
        /// <summary>
        /// 表上传的上次结算日累计气量
        /// </summary>
        public decimal MeterLastSettleMentDayLJGas
        {
            get;
            set;
        }
        /// <summary>
        /// 表上传的累计金额
        /// </summary>
        public decimal MeterLJMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 表采集数据的时间
        /// </summary>
        public string MeterReadDate
        {
            get;
            set;
        }

        /// <summary>
        /// 上次结算日累计气量
        /// </summary>
        public decimal LastSettlementDayLJGas
        {
            get;
            set;
        }
        /// <summary>
        /// 剩余金额(校对)
        /// </summary>
        public decimal SettlementBalance
        {
            get;
            set;
        }
        /// <summary>
        /// 累计购入金额
        /// </summary>
        public decimal TotalTopUp
        {
            get;
            set;
        }
        /// <summary>
        /// 当前结算日
        /// </summary>
        public byte SettlementDay
        {
            get;
            set;
        }
    }
}
