using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IoTM.Common.Business;
using MongoDB.Driver;

namespace CY.IoTM.MongoDataHelper
{
    public class PricingPlanDA
    {

        public string NewPricingPlan(PricingPlan plan)
        {
            string result = "";
            try
            {
                MongoDBHelper<PricingPlan> mongo_plan = new MongoDBHelper<PricingPlan>();
                QueryDocument query = new QueryDocument();
                query.Add("MeterNo", plan.MeterNo);
                MongoCursor<PricingPlan> mongoCursor = mongo_plan.Query(CollectionNameDefine.MeterPricingPlan, query);
                var dataList = mongoCursor.ToList();
                if (dataList != null && dataList.Count >= 1)
                {
                    var iDelete = new QueryDocument();
                    iDelete.Add("MeterNo", plan.MeterNo);

                    //删除老计划
                    mongo_plan.Delete(CollectionNameDefine.MeterPricingPlan, iDelete);
                }

                mongo_plan.Insert(CollectionNameDefine.MeterPricingPlan, plan);
            }
            catch(Exception e) {
                result = e.Message;
            }
            return result;
        }

        public void DeletePlan(PricingPlan pp)
        {
            try
            {
                MongoDBHelper<PricingPlan> mongo_plan = new MongoDBHelper<PricingPlan>();
                var iDelete = new QueryDocument();
                iDelete.Add("TaskID", pp.TaskID);
                //删除老计划
                mongo_plan.Delete(CollectionNameDefine.MeterPricingPlan, iDelete);
            }
            catch { }
        }

        public PricingPlan QueryPricingPlan(string meterNo)
        {
            MongoDBHelper<PricingPlan> mongo_plan = new MongoDBHelper<PricingPlan>();
            QueryDocument query = new QueryDocument();
            query.Add("MeterNo", meterNo);
            MongoCursor<PricingPlan> mongoCursor = mongo_plan.Query(CollectionNameDefine.MeterPricingPlan, query);
            if (mongoCursor == null) return null;
            var dataList = mongoCursor.ToList();//.OrderBy(c => c.UseDate).ToList ();
            if (dataList != null && dataList.Count >= 1)
                return dataList[0];
            else
                return null;
        }


        public string PricingPlanFinished(PricingPlan plan)
        {
            string result = "";
            try
            {
                MongoDBHelper<PricingPlan> mongo_plan = new MongoDBHelper<PricingPlan>();
                QueryDocument query = new QueryDocument();
                query.Add("MeterNo", plan.MeterNo);
                MongoCursor<PricingPlan> mongoCursor = mongo_plan.Query(CollectionNameDefine.MeterPricingPlan, query);
                var dataList = mongoCursor.ToList();
                if (dataList != null && dataList.Count >= 1)
                {
                    //删除老计划
                    List<string> list = new List<string>();
                    foreach (PricingPlan p in dataList)
                        list.Add(p._id.ToString());

                    var iDelete = new QueryDocument();
                    iDelete.Add("MeterNo", plan.MeterNo);

                    mongo_plan.Delete(CollectionNameDefine.MeterPricingPlan, iDelete);
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}
