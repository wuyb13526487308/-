using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.MongoDataHelper
{
    class CollectionNameDefine
    {
        /// <summary>
        /// 任务
        /// </summary>
        public const string TaskCollectionName = "Task";
        /// <summary>
        /// 指令
        /// </summary>
        public const string CommandCollectionName = "Command";
        /// <summary>
        /// 表
        /// </summary>
        public const string MeterCollectionName = "Meter";
        /// <summary>
        /// 表修正
        /// </summary>
        public const string MeterCorrectRecord = "CorrectRecord";
        /// <summary>
        /// 调价数据
        /// </summary>
        public const string MeterPricingPlan = "Temp_PricingPlan";
        /// <summary>
        /// 资金结算纪录
        /// </summary>
        public const string BillRecord = "BillRecord";
        /// <summary>
        /// 账单数据集合
        /// </summary>
        public const string Bill = "Bill";

    }
}
