using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 结算单据（每只表每个结算周期的结算信息）
    /// </summary>
    [DataContract()]
    public class Bill : BaseEntity
    {
        /// <summary>
        /// 用户号（4位企业编码+用户ID）
        /// </summary>
        [DataMember]
        public string UserID { get; set; }

        /// <summary>
        /// 账单ID
        /// </summary>
        [DataMember]
        public string BillID { get; set; }
        [DataMember]
        public string BeginDate { get; set; }
        [DataMember]
        public string EndDate { get; set; }

    }

    /// <summary>
    /// 资金结算纪录，包括：每个结算周期的阶梯点结算记录、结算周期的结算点记录、调价点记录、换表结算记录、缴费成功记录
    /// </summary>
    [DataContract()]
    public class BillRecord : BaseEntity
    {
        /// <summary>
        /// 用户号（4位企业编码+用户ID）
        /// </summary>
        [DataMember]
        public string UserID { get; set; }
        /// <summary>
        /// 账单ID
        /// </summary>
        [DataMember]
        public string BillID { get; set; }
        /// <summary>
        /// 表号
        /// </summary>
        [DataMember]
        public string MeterNo { get; set; }
        /// <summary>
        /// 记录时间，格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        [DataMember]
        public string RecordDate { get; set; }
        /// <summary>
        /// 结算开始点气量
        /// </summary>
        [DataMember]
        public decimal BeginPoint { get; set; }
        /// <summary>
        /// 结算截至点气量
        /// </summary>
        [DataMember]
        public decimal EndPoint { get; set; }
        /// <summary>
        /// 结算价格
        /// </summary>
        [DataMember]
        public decimal Price { get; set;}
        /// <summary>
        /// 结算气量
        /// </summary>
        [DataMember]
        public decimal Gas { get; set; }
        /// <summary>
        /// 结算阶梯
        /// </summary>
        [DataMember]
        public int Ladder { get; set; }
        /// <summary>
        /// 本次结算金额
        /// </summary>
        [DataMember]
        public decimal Amount { get; set; }
        /// <summary>
        /// 本次结算余额
        /// </summary>
        [DataMember]
        public decimal Balance { get; set; }
        /// <summary>
        /// 记录类型
        /// </summary>
        [DataMember]
        public BillRecordType BillRecordType { get; set; }
    }


    /// <summary>
    /// 账单记录类型
    /// </summary>
    
    public enum BillRecordType
    {
        阶梯点结算记录 = 0,
        结算点记录 =1,
        调价点记录 =2,
        换表结算记录 =3,
        缴费记录 =4
    }
}
