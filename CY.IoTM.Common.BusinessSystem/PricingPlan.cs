using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CY.IoTM.Common.Business
{
    [DataContract()]
    public class PricingPlan : BaseEntity
    {
        /// <summary>
        /// 00 气量表 01 金额表
        /// </summary>
        [DataMember]
        public string MeterType { get; set; }

        /// <summary>
        /// 启用阶梯价 true 启用
        /// </summary>
        [DataMember]
        public bool IsUsedLadder { get; set; }
        /// <summary>
        /// 阶梯数
        /// </summary>
        [DataMember]
        public int Ladder { get; set; }
        /// <summary>
        /// 价格1
        /// </summary>
        [DataMember]
        public decimal Price1 { get; set; }
        /// <summary>
        /// 气量1
        /// </summary>
        [DataMember]
        public decimal Gas1 { get; set; }
        /// <summary>
        /// 价格2
        /// </summary>
        [DataMember]
        public decimal Price2 { get; set; }
        /// <summary>
        /// 气量2
        /// </summary>
        [DataMember]
        public decimal Gas2 { get; set; }
        /// <summary>
        /// 价格3
        /// </summary>
        [DataMember]
        public decimal Price3 { get; set; }
        /// <summary>
        /// 气量3
        /// </summary>
        [DataMember]
        public decimal Gas3 { get; set; }
        /// <summary>
        /// 价格4
        /// </summary>
        [DataMember]
        public decimal Price4 { get; set; }
        /// <summary>
        /// 气量4
        /// </summary>
        [DataMember]
        public decimal Gas4 { get; set; }
        /// <summary>
        /// 价格5
        /// </summary>
        [DataMember]
        public decimal Price5 { get; set; }

        /// <summary>
        /// 结算周期：00 月  01 季度 10 半年 11 全年
        /// </summary>
        [DataMember]
        public string SettlementType { get; set; }

        /// <summary>
        /// 启用日期
        /// </summary>
        [DataMember]
        public string UseDate { get; set; }

        public string TaskID { get; set; }

        public string MeterNo { get; set; }
    }
}
