using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IoTM.Common.Business;

namespace CY.IoTM.MongoDataHelper
{
    /// <summary>
    /// 资金结算记录Mongodb层服务类
    /// </summary>
    public class M_BillRecordService
    {
        /// <summary>
        /// 记录资金结算记录
        /// </summary>
        /// <param name="record"></param>
        public void AddBillRecord(BillRecord record)
        {
            try
            {
                if (record == null) return;

                MongoDBHelper<BillRecord> mongo = new MongoDBHelper<BillRecord>();
                mongo.Insert(CollectionNameDefine.BillRecord, record);
                if (record.BillRecordType == BillRecordType.结算点记录)
                {
                    //更新账单

                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// 新建账单
        /// </summary>
        /// <param name="bill"></param>
        public void AddBill(Bill bill)
        {
            try
            {
                if (bill == null) return;

                MongoDBHelper<Bill> mongo = new MongoDBHelper<Bill>();
                mongo.Insert(CollectionNameDefine.Bill, bill);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


    }
}
