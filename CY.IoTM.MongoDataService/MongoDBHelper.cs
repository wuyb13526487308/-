using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CY.IoTM.MongoDataHelper
{
    public class MongoDBHelper<T>
    {
        string conn = "mongodb://192.168.1.2:27017";
        string database = "IoTMeter";//MongoConn  MongoDBName

        public MongoDBHelper()
        {
            try
            {
                this.conn = System.Configuration.ConfigurationSettings.AppSettings["MongoConn"];
                this.database = System.Configuration.ConfigurationSettings.AppSettings["MongoDBName"];
            }
            catch
            {
            }
        }

        public MongoCursor<T> Query(string collection, QueryDocument query)
        {
            MongoCursor<T> m = null;
            try
            {
                MongoDB.Driver.MongoCollection mongoCollection = getMongoCollection(collection);
                
                m = mongoCollection.FindAs<T>(query);
                mongoCollection.Database.Server.Disconnect();
            }
            catch
            {
                //记录日志信息
            }
            return m;
        }


        public MongoCursor<BsonDocument> Query(string collection)
        {
            MongoDB.Driver.MongoCollection mongoCollection = getMongoCollection(collection);
            MongoCursor<BsonDocument> cursor;

            cursor = mongoCollection.FindAllAs<BsonDocument>();
            return cursor;
        }

        public string Insert(string collection, MongoDB.Bson.BsonDocument doc)
        {
            string result = "";
            try
            {
                MongoDB.Driver.MongoCollection mongoCollection = getMongoCollection(collection);
                mongoCollection.Insert(doc);
                mongoCollection.Database.Server.Disconnect();
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }

        public string Insert(string collection, Object document)
        {
            string result = "";
            try
            {
                MongoDB.Driver.MongoCollection mongoCollection = getMongoCollection(collection);
                mongoCollection.Insert(document);
                mongoCollection.Database.Server.Disconnect();
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }

        public string Update(string collection, IMongoQuery query, IMongoUpdate update)
        {
            string result = "";
            try
            {
                MongoDB.Driver.MongoCollection mongoCollection = getMongoCollection(collection);
                mongoCollection.Update(query, update);

                //mongoCollection.DropIndex (
                
                mongoCollection.Database.Server.Disconnect();               
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }

        //public string Delete(string collection, string meterNo)
        //{
        //    string result = "";
        //    try
        //    {
        //        MongoDB.Driver.MongoCollection mongoCollection = getMongoCollection(collection);
        //        var query = new QueryDocument();
        //        query.Add("MeterNo", meterNo);
        //        mongoCollection.Remove(query);

        //        mongoCollection.Database.Server.Disconnect();
        //    }
        //    catch (Exception e)
        //    {
        //        result = e.Message;
        //    }

        //    return result;
        //}


        public string Delete(string collection, IMongoQuery query)
        {
            string result = "";
            try
            {
                MongoDB.Driver.MongoCollection mongoCollection = getMongoCollection(collection);
                mongoCollection.Remove(query);
                mongoCollection.Database.Server.Disconnect();
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }


        private MongoDB.Driver.MongoCollection getMongoCollection(string collection)
        {
            MongoDB.Driver.MongoDatabase mongoDataBase = getMongoServer();
            if (!mongoDataBase.CollectionExists(collection))
                mongoDataBase.CreateCollection(collection);
            MongoDB.Driver.MongoCollection mongoCollection = mongoDataBase.GetCollection(collection);//选择集合，相当于表
            return mongoCollection;           
        }

        private MongoDB.Driver.MongoDatabase getMongoServer()
        {
            //string collection = "CommLog";
            MongoServerSettings msSettings = new MongoServerSettings();
            MongoClient mongoClient = new MongoClient(conn);
            MongoServer mongodb = mongoClient.GetServer();// MongoServer.Create(conn);//连接数据库new MongoServer(msSettings); //
            MongoDB.Driver.MongoDatabase mongoDataBase = mongodb.GetDatabase(database);//选择数据库名
            mongodb.Connect();
            return mongoDataBase;
        }

        //public string AddDoc(byte[] content)
        //{
        //    string result = "";
        //    MongoDatabase db = null;
        //    try
        //    {
        //        string filename = Guid.NewGuid().ToString();
        //        db = getMongoServer();

        //        MongoDB.Driver.GridFS.MongoGridFS fs = new MongoDB.Driver.GridFS.MongoGridFS(db, new MongoDB.Driver.GridFS.MongoGridFSSettings() { Root = "ecDocs" });
        //        MongoDB.Driver.GridFS.MongoGridFSFileInfo info = new MongoDB.Driver.GridFS.MongoGridFSFileInfo(fs, filename);
        //        using (MongoDB.Driver.GridFS.MongoGridFSStream gfs = info.Create())
        //        {
        //            gfs.Write(content, 0, content.Length);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (db != null)
        //            db.Server.Disconnect();
        //    }
        //    return result;
        //}
    }
}
