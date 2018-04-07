using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.Business;


namespace CY.IoTM.DataService.Business
{
    public class PriceParManageService : IPriceParManage
    {
        public Message Add(IoT_PricePar info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                 Table<IoT_PricePar> tbl = dd.GetTable<IoT_PricePar>();
                 string ser = GetSerNo();
                 if (ser != "-1") {
                     info.Ser = ser;
                 }

                // 调用新增方法
                tbl.InsertOnSubmit(info);
                // 更新操作
                dd.SubmitChanges();

                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_PricePar>(info)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增价格参数失败！" + e.Message
                };
            }
            return m;
        }

        public Message Edit(IoT_PricePar info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                   IoT_PricePar dbinfo = dd.GetTable<IoT_PricePar>().Where(p => 
                     p.CompanyID == info.CompanyID&&p.ID==info.ID).SingleOrDefault();

                    ConvertHelper.Copy<IoT_PricePar>(dbinfo, info);

                    // 更新操作
                    dd.SubmitChanges();
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.TToJson<IoT_PricePar>(dbinfo)
                    };
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑价格参数失败！" + e.Message
                };
            }
            return m;
        }

        public Message Delete(IoT_PricePar info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                string checkSQL = $"Select count(*) from IoT_Meter where PriceID={info.ID}";
                object[] param = new object[0];
                int iCount =  dd.ExecuteQuery<int>(checkSQL, param).Single<int>();
                if (iCount > 0)
                    throw new Exception("价格类型已被使用，不能删除");
                //检查是否被调价计划试用
                checkSQL = $"Select count(*) from IoT_Pricing where PriceType={info.ID}";
                iCount = dd.ExecuteQuery<int>(checkSQL, param).Single<int>();
                if (iCount > 0)
                    throw new Exception("价格类型已被调价计划使用，不能删除");
                // 获得上下文对象中的表信息
                Table<IoT_PricePar> tbl = dd.GetTable<IoT_PricePar>();
                var s = tbl.Where(p => p.CompanyID == info.CompanyID && p.ID == info.ID).Single();
                tbl.DeleteOnSubmit(s as IoT_PricePar);
            
                // 更新操作
                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = "删除价格参数成功！"
                };
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "删除价格参数失败！" + e.Message
                };
            }
            return m;
        }


        /// <summary>
        /// 获取价格类型序号
        /// </summary>
        /// <returns></returns>
        public string GetSerNo()
        {
            // 定义执行结果
            string m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                Table<IoT_PricePar> tbl = dd.GetTable<IoT_PricePar>();
                var s = tbl.Max(p => p.Ser);
                if (s == null)
                {
                    m = "0001";
                }
                else {
                    m = (int.Parse(s)+1).ToString().PadLeft(4, '0');
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                m = "-1";
            }
            return m;
        }





    }
}
