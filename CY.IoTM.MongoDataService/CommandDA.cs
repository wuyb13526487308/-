using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IoTM.Common.Business;
using MongoDB.Driver;

namespace CY.IoTM.MongoDataHelper
{
    class CommandDA
    {
        /// <summary>
        /// 更新指令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static string Update(Command command)
        {
            try
            {
                MongoDBHelper<Command> mongo = new MongoDBHelper<Command>();
                var query = new QueryDocument();
                query.Add("_id", command._id);
                var update = new UpdateDocument();
                update.Add("CommandState", command.CommandState);
                update.Add("AnswerDate", command.AnswerDate);
                update.Add("AnswerData", command.AnswerData);
                update.Add("ControlCode", command.ControlCode);
                update.Add("DataCommand", command.DataCommand);
                update.Add("DataLength", command.DataLength);
                update.Add("TaskID", command.TaskID);
                update.Add("Identification", command.Identification);
                update.Add("Order", command.Order);
                mongo.Update(CollectionNameDefine.CommandCollectionName, query, update);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "";
        }
        /// <summary>
        /// 向数据库中插入指令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static string Insert(Command command)
        {
            try
            {
                MongoDBHelper<Command> mongo_CMD = new MongoDBHelper<Command>();

                mongo_CMD.Insert(CollectionNameDefine.CommandCollectionName, command);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "";
        }

        public static List<Command> QueryCommandList(string taskID)
        {
            MongoDBHelper<Command> mongo = new MongoDBHelper<Command>();
            try
            {
                QueryDocument query = new QueryDocument();
                query.Add("TaskID", taskID);
                query.Add("CommandState", 0);
                MongoCursor<Command> mongoCursor = mongo.Query(CollectionNameDefine.CommandCollectionName, query);
                var dataList = mongoCursor.ToList();
                if (dataList != null && dataList.Count >= 1)
                {
                    var qu = from p in dataList orderby p.Order ascending select p;
                    return qu.ToList();
                }
            }
            catch (Exception e)
            {
                //记录日志
                Console.WriteLine(e.Message);
            }
            return new List<Command>();
        }
    }
}
