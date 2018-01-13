using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using CY.IoTM.Common;
using CY.IoTM.Common.Business;
using CY.IoTM.MongoDataHelper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace TestMongodb
{
    class Program
    {
        static void Find()
        {
            CY.IoTM.DataService.Business.TaskManageService tms = new CY.IoTM.DataService.Business.TaskManageService();
            tms.GetMeter("31613526487308");
        }
        static void Main(string[] args)
        {
            Semaphore _semaphore = new Semaphore(1, 1);

            for(int i=0;i<10000;i++)
            {
                if (_semaphore.WaitOne(1000,true))
                {
                    Console.WriteLine("收到信号");
                }
                else
                {
                    Console.WriteLine("没有收到信号:");
                }

            }
            Console.Read();
            return;

            int iLength =255;
            byte[] data = new byte[iLength];
            for (int i = 0; i < iLength; i++)
                data[i] = (byte)(i + 1);
            int iCount = 0;
            Console.WriteLine("原文：");
            for (int i = 0; i < iLength; i++)
            {
                iCount++;
                if (iCount < 15)
                {
                    Console.Write("{0:X2} ,", data[i]);
                }
                else
                {
                    Console.Write("{0:X2} ,\r\n", data[i]);
                    iCount = 0;
                }
            }

            Console.WriteLine("");

           
            byte[] key = { 0x88, 0x88, 0x88, 0x88, 0x88, 0x88, 0x88, 0x88 };
            byte aa =(byte) (data[0] ^ key[0]);


            //byte[] ss = YiHuoYunSuan(data, key);


            byte[] resultData = Encryption.Encry(data, key);
            Console.WriteLine("密文：");

            iCount = 0;
            for (int i = 0; i < iLength; i++)
            {
                iCount++;
                if (iCount < 15)
                {
                    Console.Write("{0:X2} ,", resultData[i]);
                }
                else
                {
                    Console.Write("{0:X2} ,\r\n", resultData[i]);
                    iCount = 0;
                }
            }

            byte[] oldData = Encryption.Decry(resultData, key);
            Console.WriteLine("解密后的原文：");

            iCount = 0;
            for (int i = 0; i < iLength; i++)
            {
                iCount++;
                if (iCount < 15)
                {
                    Console.Write("{0:X2} ,", oldData[i]);
                }
                else
                {
                    Console.Write("{0:X2} ,\r\n", oldData[i]);
                    iCount = 0;
                }
            }
            //
            Test();



            Console.ReadLine();

            return;
            MongoDBHelper<Task> mongo = new MongoDBHelper<Task>();

            string collectin = "Task";
            Task task = new Task() {MeterMac="15413526487307" ,TaskDate=DateTime.Now,TaskID ="20150701003",TaskState=TaskState.Waitting ,TaskType="点火"};
            task.CommandList.Add(new Command() { CommandState = CommandState.Waitting, ControlCode = 0x21, DataCommand = "", DataLength = 0x1e, Identification = "", TaskID = task.TaskID });
            task.CommandList.Add(new Command() { CommandState = CommandState.Waitting, ControlCode = 0x21, DataCommand = "", DataLength = 0x1e, Identification = "", TaskID = task.TaskID });
            task.CommandList.Add(new Command() { CommandState = CommandState.Waitting, ControlCode = 0x21, DataCommand = "", DataLength = 0x1e, Identification = "", TaskID = task.TaskID });

            mongo.Insert(collectin, task);

            QueryDocument query = new QueryDocument();
            query.Add("MeterMac", task.MeterMac);
            query.Add("TaskState", (int)TaskState.Waitting);
            MongoCursor<Task> mongoCursor = mongo.Query(collectin, query);
            var dataList = mongoCursor.ToList();
            foreach (Task doc in dataList)
            {
                Console.WriteLine("{0}", doc.ToJson());
                Console.WriteLine("");
                foreach(Command cmd in task.CommandList)
                {
                    Console.WriteLine("{0}", cmd.ToJson());
                }

            }
            Console.ReadLine();
        }
        private static byte[] YiHuoYunSuan(byte[] data, byte[] key)
        {
            byte[] result = new byte[8];
            //if (data.Length == 8 && key.Length == 8)
            {
                for (int i = 0; i < 8; i++)
                    result[i] = (byte)(data[i] ^ key[i]);
            }
            return result;
        }

        private static void WriteFile(List<string> info)
        {
            File.AppendAllLines(System.Environment.CurrentDirectory + "\\Test.txt", info);
        }


        private static void Test(int settlementDay, int monthCount)
        {
            List<string> list = new List<string>();
            DateTime bDate = Convert.ToDateTime("2014-1-1");
            DateTime eDate = Convert.ToDateTime("2017-01-01");
            DateTime settlementDate = DateTime.Now;
            DateTime result = getNextDate(settlementDay, 8, monthCount, Convert.ToDateTime("2015-01-20"));
            DateTime head;
            //测试按月结算日为1号的
            string info = string.Format ("结算日为{0}号，结算周期：{1}",settlementDay,monthCount);
            Console.WriteLine("结算日为{0}号，结算周期：{1}",settlementDay,monthCount);
            //list.Add(info);
            int iStartMonth = 1;
            while (iStartMonth <= 12)
            {
                bDate = Convert.ToDateTime("2013-12-29");
                Console.WriteLine("开始月份为：{0}", iStartMonth);
                string info1 = string.Format("开始月份为：{0}", iStartMonth);
                //list.Add(info);

                result = getNextDate(settlementDay, iStartMonth, monthCount, bDate);
                settlementDate = result;
                head = bDate;
                while (bDate <= eDate)
                {
                    result = getNextDate(settlementDay, iStartMonth, monthCount, bDate);
                    if (settlementDate != result)
                    {
                        Console.WriteLine("从{0}至{1}期间的结算日为：{2}", head, bDate, settlementDate);
                        string info2 = string.Format("从{0}至{1}期间的结算日为：{2}", head, bDate, settlementDate);
                        settlementDate = result;
                        list.Add(info + " " + info1 + " " + info2);

                        head = bDate;
                    }

                    bDate = bDate.AddHours(1);
                }
                iStartMonth++;
                list.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            }
            list.Add("==========================================================================================");
            WriteFile(list);
            
        }

        private static void Test()
        {
            int[] SettlementMonths = {1,  3, 6, 12 };
            int[] SettlementDays = { /*1, 10, 28, 29, 30,*/ 31 };
            for (int i = 0; i < SettlementMonths.Length ; i++)
            {
                for (int j = 0; j < SettlementDays.Length; j++)
                {
                    Test(SettlementDays[j], SettlementMonths[i]);
                }
            }
        }

        struct Area
        {
            public int b;
            public int e;
            public Area(int b, int e)
            {
                this.b = b;
                this.e = e;
            }
        }
        private static DateTime getNextDate(int settlementDay, int settlementMonth, int monthCount,DateTime currentDate)
        {
            DateTime settlementDate = currentDate;
            int lastDay = ((Convert.ToDateTime(string.Format("{0}-{1}-01 00:00:00", currentDate.Year, settlementMonth)).AddMonths(1)).AddDays(-1)).Day;

            DateTime bDate = Convert.ToDateTime(string.Format("{0}-{1}-{2} 00:00:00", currentDate.Year, settlementMonth, settlementDay > lastDay ? lastDay : settlementDay));

            DateTime eDate;
            if (currentDate.Month < settlementMonth)
            {
                for (int i = 0; i < 12 / monthCount; i++)
                {
                    eDate = bDate;
                    bDate = eDate.AddMonths(monthCount * -1);
                    if (bDate.Day < settlementDay)
                    {
                        lastDay = ((Convert.ToDateTime(string.Format("{0}-{1}-01 00:00:00", bDate.Year, bDate.Month)).AddMonths(1)).AddDays(-1)).Day;
                        if ((settlementDay >= lastDay) && (bDate.Day < lastDay))
                        {
                            bDate = bDate.AddDays(lastDay - bDate.Day);
                        }
                        else if ((settlementDay < lastDay))
                        {
                            bDate = bDate.AddDays(settlementDay - bDate.Day);
                        }
                    }
                    if (bDate <= currentDate && currentDate < eDate)
                    {
                        settlementDate = eDate;
                        break;
                    }

                }
            }
            else if (currentDate.Month == settlementMonth)
            {
                if (currentDate < bDate)
                {
                    settlementDate = bDate;
                }
                else
                {
                    eDate = bDate.AddMonths(monthCount);
                    if (eDate.Day < settlementDay)
                    {
                        lastDay = ((Convert.ToDateTime(string.Format("{0}-{1}-01 00:00:00", eDate.Year, eDate.Month)).AddMonths(1)).AddDays(-1)).Day;
                        if ((settlementDay >= lastDay) && (eDate.Day < lastDay))
                        {
                            eDate = eDate.AddDays(lastDay - eDate.Day);
                        }
                        else if ((settlementDay < lastDay))
                        {
                            eDate = eDate.AddDays(settlementDay - eDate.Day);
                        }
                    }
                    settlementDate = eDate;
                }
            }
            else
            {
                for (int i = 0; i < 12 / monthCount; i++)
                {
                    eDate = bDate.AddMonths(monthCount);
                    if (eDate.Day < settlementDay)
                    {
                        lastDay = ((Convert.ToDateTime(string.Format("{0}-{1}-01 00:00:00", eDate.Year, eDate.Month)).AddMonths(1)).AddDays(-1)).Day;
                        if ((settlementDay >= lastDay) && (eDate.Day < lastDay))
                        {
                            eDate = eDate.AddDays(lastDay - eDate.Day);
                        }
                        else if ((settlementDay < lastDay))
                        {
                            eDate = eDate.AddDays(settlementDay - eDate.Day);
                        }
                    }
                    if (bDate <= currentDate && currentDate < eDate)
                    {
                        settlementDate = eDate;
                        break;
                    }
                    bDate = eDate;
                }
            }
            //Console.WriteLine("settlementDay:{0} settlementMonth={1} monthCount={2}\r\n nextDate={3} Date={4}", settlementDay, settlementMonth, monthCount, settlementDate, currentDate);

            return settlementDate;
        }

//        static void Main(string[] args)
//        {
//            //连接信息
//            string conn = "mongodb://192.168.1.2:27017";
//            string database = "IoTMeter";
//            string collection = "CommLog";
//            MongoServerSettings msSettings = new MongoServerSettings();
//            MongoClient mongoClient = new MongoClient(conn);
//            //mongoClient.Settings.Server = new MongoServerAddress("192.168.1.2", 27017);
//            //mongoClient.Settings.SocketTimeout = new TimeSpan(0, 0, (int)(1000 / 1000));

//            MongoServer mongodb = mongoClient.GetServer();// MongoServer.Create(conn);//连接数据库new MongoServer(msSettings); //
//            MongoDB.Driver.MongoDatabase mongoDataBase = mongodb.GetDatabase(database);//选择数据库名
//            if(!mongoDataBase.CollectionExists(collection))
//                mongoDataBase.CreateCollection(collection);

//            MongoDB.Driver.MongoCollection mongoCollection = mongoDataBase.GetCollection(collection);//选择集合，相当于表

//            mongodb.Connect();
            
//            //BsonDocument bsonDocumnet = new BsonDocument();
//            //bsonDocumnet.Add("Name", "武");
//            //List<BsonValue> list = new List<BsonValue> ();
//            //list.Add ("武宜波");
//            MongoCursor<Person> cursor = mongoCollection.FindAs<Person>(Query.EQ("Name","武宜波"));

//            var dataList = cursor.ToList();// cursor.Where<Person>(p1 => p1.Name == "武宜波");
//            foreach (Person doc in dataList)
//            {
//                Console.WriteLine("{0}", doc.ToJson());
                
//            }

            

//            /*---------------------------------------------
//* sql : SELECT * FROM table 
//*---------------------------------------------
//*/
//            MongoCursor<Person> p = mongoCollection.FindAllAs<Person>();
//            long perCount = p.Count();
//            Console.WriteLine("{0}", perCount);

//            //普通插入
//            var o = new { Uid = 15, Name = "武宜波", PassWord = "111111" };
//            mongoCollection.Insert(o);

//            //对象插入
//            Person person = new Person { Uid = 102, Name = "IoTMeter", PassWord = "222222", Car = new Car { _id = ObjectId.GenerateNewId(), Name="baoma",arge=40} };
//            mongoCollection.Insert(person);

//            //BsonDocument 插入
//            MongoDB.Bson.BsonDocument b = new MongoDB.Bson.BsonDocument();
//            b.Add("Uid", 125);
//            b.Add("Name", "郑州创源");
//            b.Add("PassWord", "333333");
//            mongoCollection.Insert(b);


//            /*---------------------------------------------
//             * sql : SELECT * FROM table WHERE Uid > 10 AND Uid < 20
//             *---------------------------------------------
//             */
//            QueryDocument query = new QueryDocument();
//            b = new BsonDocument();
//            b.Add("$gt", 10);
//            b.Add("$lt", 20);
//            query.Add("Uid", b);
            

//            MongoCursor<Person> m = mongoCollection.FindAs<Person>(query);
//            Print(m);

//            /*-----------------------------------------------
//             * sql : SELECT COUNT(*) FROM table WHERE Uid > 10 AND Uid < 20
//             *-----------------------------------------------
//             */
//            long c = mongoCollection.Count(query);

//            /*-----------------------------------------------
//            * sql : SELECT Name FROM table WHERE Uid > 10 AND Uid < 20
//            *-----------------------------------------------
//            */
//            query = new QueryDocument();
//            b = new BsonDocument();
//            b.Add("$gt", 20);
//            b.Add("$lt", 200);
//            query.Add("Uid", b);
//            FieldsDocument f = new FieldsDocument();
//            f.Add("Name", 1);

//            m = mongoCollection.FindAs<Person>(query).SetFields(f);
//            Print(m);

//            /*-----------------------------------------------
//            * sql : SELECT * FROM table ORDER BY Uid DESC LIMIT 10,10
//            *-----------------------------------------------
//            */
//            query = new QueryDocument();
//            SortByDocument s = new SortByDocument();
//            s.Add("Uid", -1);//-1=DESC

//            m = mongoCollection.FindAllAs<Person>().SetSortOrder(s).SetSkip(10).SetLimit(10);
//            Print(m);

//            Console.ReadLine();
//        }
        static void Print(MongoCursor<Person> persons)
        {
            
            //Console.WriteLine(persons.ToJson());
            var list = persons.ToList ();//<Person>();

            foreach (var p in list)
            {
                
                Console.WriteLine(p.ToJson());
            }
        }

    }


    class Person : baseObect
    {

        public int Uid{get;set;}
        public string Name { get; set; }
        public string PassWord { get; set; }
        public DateTime DateTime { get; set; }
        public Car Car;
    }

    class Car
    {
        public ObjectId _id;
        public string Name;
        public int arge;
    }

    class baseObect
    {
        [Browsable(false)]
        public ObjectId _id;
    }

    public class Encryption
    {
        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="data">明文数据</param>
        /// <param name="key">密钥</param>
        /// <returns>返回密文</returns>
        public static byte[] Encry(byte[] data, byte[] key)
        {
            //return data;

            MemoryStream memStr = new MemoryStream();
            int ilength = data.Length / 8;
            for (int i = 0; i < ilength; i++)
                memStr.Write(YiHuoYunSuan(data, i * 8, key),0,8);

            int syLength = data.Length % 8;
            if ( syLength> 0)
            {
                byte[] tmp = new byte[8];
                for (int i = 0; i < syLength; i++)
                    tmp[i] = data[(ilength) * 8 + i];

                memStr.Write(YiHuoYunSuan(tmp, 0, key), 0, syLength);
            }
            byte[] buffer = new byte[data.Length];
            memStr.Position = 0;
            memStr.Read(buffer, 0, data.Length);
            return buffer;
        }

        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>返回明文数据</returns>
        public static byte[] Decry(byte[] data, byte[] key)
        {
            //return data;
            return Encry(data, key);
        }
        private static byte[] YiHuoYunSuan(byte[] data, int offset, byte[] key)
        {
            byte[] tmp = new byte[8];
            //if (data.Length == 8 && key.Length == 8)
            {
                for (int i = 0; i < 8; i++)
                   tmp[i] = (byte)(data[i + offset] ^ key[i]);
            }
            return tmp;
        }
    }
}
