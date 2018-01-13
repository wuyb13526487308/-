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
    public class CommunityManageService : ICommunityManage
    {
        public Message Add(IoT_Community info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
              
                 Table<IoT_Community> tbl = dd.GetTable<IoT_Community>();

                 var count = tbl.Where(p => p.StreetID == info.StreetID && p.Ser == info.Ser).Count();

                 if (count > 0)
                 {

                     m = new Message()
                     {
                         Result = false,
                         TxtMessage = "小区编号不能重复!"
                     };
                     return m;
                 }


                // 调用新增方法
                tbl.InsertOnSubmit(info);
                // 更新操作
                dd.SubmitChanges();

                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_Community>(info)
                };



            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增小区参数失败！" + e.Message
                };
            }
            return m;
        }

        public Message Edit(IoT_Community info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                string checkSQL = $"Select count(*) from IoT_Street where ID={info.ID}";
                object[] param = new object[0];
                int iCount = dd.ExecuteQuery<int>(checkSQL, param).Single<int>();
                if (iCount > 0)
                    throw new Exception("小区已被使用，不能删除");

                var count = dd.GetTable<IoT_Community>().Where(p => p.StreetID == info.StreetID && p.Ser == info.Ser && p.ID != info.ID).Count();

                    if (count > 0)
                    {

                        m = new Message()
                        {
                            Result = false,
                            TxtMessage = "小区编号不能重复!"
                        };
                        return m;
                    }

                   IoT_Community dbinfo = dd.GetTable<IoT_Community>().Where(p =>
                      p.ID == info.ID).SingleOrDefault();

                    ConvertHelper.Copy<IoT_Community>(dbinfo, info);

                    // 更新操作
                    dd.SubmitChanges();
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.TToJson<IoT_Community>(dbinfo)
                    };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑小区参数失败！" + e.Message
                };
            }
            return m;
        }

        public Message Delete(IoT_Community info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                // 获得上下文对象中的表信息
                Table<IoT_Community> tbl = dd.GetTable<IoT_Community>();

                var s = tbl.Where(p => p.ID == info.ID).Single();
                tbl.DeleteOnSubmit(s as IoT_Community);
            

                // 更新操作
                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = "删除小区参数成功！"
                };
               
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "删除小区参数失败！" + e.Message
                };
            }
            return m;
        }
    }
}
