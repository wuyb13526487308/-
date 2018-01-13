using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IoTM.Common.Business;
using MongoDB.Driver;

namespace CY.IoTM.MongoDataHelper
{
    class MeterDA
    {
        public static Meter QueryMeter(string mac)
        {
            Meter _m = null;
            try
            {
                MongoDBHelper<Meter> mongo = new MongoDBHelper<Meter>();
                QueryDocument query = new QueryDocument();
                query.Add("Mac", mac);
                MongoCursor<Meter> mongoCursor = mongo.Query(CollectionNameDefine.MeterCollectionName, query);
                var dataList = mongoCursor.ToList();
                if (dataList != null && dataList.Count >= 1)
                {
                    _m = dataList[0];
                    _m.PricingPlan = new PricingPlanDA().QueryPricingPlan(mac);
                }
            }
            catch (Exception e)
            {
                //记录日志
                Console.WriteLine(e.Message);
            }
            return _m;
        }     

        public static string InsertMeter(Meter meter)
        {
            try
            {
                Meter _m = QueryMeter(meter.Mac);
                if (_m != null)
                    return "表已存在，不能重复插入";
                if (meter.Mac == null || meter.Mac == "")
                    return "表号不能为空";
                if (meter.Mac.Length != 14)
                    return "非法的表号。";
                MongoDBHelper<Meter> mongo = new MongoDBHelper<Meter>();
                mongo.Insert(CollectionNameDefine.MeterCollectionName, meter);
                //创建新的账单
                Bill bill = new Bill() { BillID = meter.BillID, UserID = meter.UserID, BeginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                new M_BillRecordService().AddBill(bill);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "";
        }

        public string UpdateMeter(Meter meter)
        {
            MongoDBHelper<Meter> mongo = new MongoDBHelper<Meter>();
            var query = new QueryDocument();
            query.Add("_id", meter._id);
            var update = new UpdateDocument();
            update.Add("MeterID", meter.MeterID);
            update.Add("UserID", meter.UserID==null?"":meter.UserID);
            update.Add("Mac", meter.Mac);
            update.Add("MKeyVer", meter.MKeyVer);
            update.Add("Key", meter.Key);
            update.Add("MeterType", meter.MeterType);
            update.Add("TotalAmount", (double)meter.TotalAmount);
            update.Add("TotalTopUp", (double)meter.TotalTopUp);
            update.Add("SettlementType", meter.SettlementType);
            update.Add("SettlementDay", meter.SettlementDay);
            update.Add("SettlementMonth", meter.SettlementMonth);

            update.Add("ValveState", meter.ValveState);
            update.Add("MeterState", meter.MeterState);
            update.Add("PriceCheck", meter.PriceCheck);
            update.Add("LastTopUpSer", meter.LastTopUpSer);
            update.Add("LastJiaoShiDate", meter.LastJiaoShiDate == null ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : meter.LastJiaoShiDate);

            update.Add("IsUsedLadder", meter.IsUsedLadder);
            update.Add("Ladder", meter.Ladder);
            update.Add("Price1", (double)meter.Price1);
            update.Add("Price2", (double)meter.Price2);
            update.Add("Price3", (double)meter.Price3);
            update.Add("Price4", (double)meter.Price4);
            update.Add("Price5", (double)meter.Price5);
            update.Add("Gas1", (double)meter.Gas1);
            update.Add("Gas2", (double)meter.Gas2);
            update.Add("Gas3", (double)meter.Gas3);
            update.Add("Gas4", (double)meter.Gas4);
            update.Add("LastSettlementAmount", (double)meter.LastSettlementAmount);
            update.Add("LastTotal", (double)meter.LastTotal);

            //update.Add("MeterType", meter.MeterType);
            update.Add("NextSettlementPointGas", (double)meter.NextSettlementPointGas);//下一个结算点气量
            update.Add("CurrentLadder", meter.CurrentLadder);
            update.Add("CurrentPrice", (double)meter.CurrentPrice);
            update.Add("LastGasPoint", (double)meter.LastGasPoint);
            update.Add("CurrentBalance", (double)meter.CurrentBalance);
            update.Add("LJMoney", (double)meter.LJMoney);
            update.Add("SettlementDateTime", meter.SettlementDateTime == null ? "":meter.SettlementDateTime);
            update.Add("IsDianHuo", meter.IsDianHuo);
            update.Add("IsPricing", meter.IsPricing);
            update.Add("BillID", meter.BillID);
            update.Add("TiaoJiaPointGas", (double)meter.TiaoJiaPointGas);

            if (meter.PricingPlan != null)
            {
                MongoDB.Bson.BsonDocument b = new MongoDB.Bson.BsonDocument();
                b.Add("IsUsedLadder", meter.PricingPlan.IsUsedLadder);
                b.Add("Ladder", meter.PricingPlan.Ladder);
                b.Add("Price1", (double)meter.PricingPlan.Price1);
                b.Add("Gas1", (double)meter.PricingPlan.Gas1);
                b.Add("Price2", (double)meter.PricingPlan.Price2);
                b.Add("Gas2", (double)meter.PricingPlan.Gas2);
                b.Add("Price3", (double)meter.PricingPlan.Price3);
                b.Add("Gas3", (double)meter.PricingPlan.Gas3);
                b.Add("Price4", (double)meter.PricingPlan.Price4);
                b.Add("Gas4", (double)meter.PricingPlan.Gas4);
                b.Add("Price5", (double)meter.PricingPlan.Price5);
                b.Add("MeterType", meter.PricingPlan.MeterType);
                b.Add("UseDate", meter.PricingPlan.UseDate);
                update.Add("PricingPlan", b);
            }
            mongo.Update(CollectionNameDefine.MeterCollectionName, query, update);
            return "";

        }

        public static string UpdateMeter(IoT_Meter info)
        {
            Meter meter = QueryMeter(info.MeterNo.Trim());
            meter.MeterID = info.ID;
            meter.UserID = info.CompanyID + info.UserID;
            meter.Key = info.MKey;
            meter.Mac = info.MeterNo.Trim();
            meter.MeterState = info.MeterState.ToString();
            meter.MeterType = info.MeterType;
            meter.MKeyVer = (byte)(info.MKeyVer & 0xff);
            meter.PriceCheck = info.PriceCheck.ToString();
            meter.SettlementDay = (int)info.SettlementDay;
            meter.SettlementMonth = (int)info.SettlementMonth;
            meter.SettlementType = info.SettlementType;
            meter.TotalAmount = Convert.ToDecimal(((decimal)info.TotalAmount).ToString("0.00"));
            meter.TotalTopUp = Convert.ToDecimal(((decimal)info.TotalTopUp).ToString("0.00"));
            meter.ValveState = info.ValveState.ToString();

            meter.IsUsedLadder = (bool)info.IsUsed;
            meter.Ladder = (int)info.Ladder;
            meter.Price1 = (decimal)info.Price1;
            meter.Price2 = (decimal)info.Price2;
            meter.Price3 = (decimal)info.Price3;
            meter.Price4 = (decimal)info.Price4;
            meter.Price5 = (decimal)info.Price5;
            meter.Gas1 = (decimal)info.Gas1;
            meter.Gas2 = (decimal)info.Gas2;
            meter.Gas3 = (decimal)info.Gas3;
            meter.Gas4 = (decimal)info.Gas4;

            return new MeterDA().UpdateMeter(meter);
        }
    }
}
