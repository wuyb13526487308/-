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
    public class StreetManageService : IStreetManage
    {
        public Message Add(IoT_Street info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
              
                 Table<IoT_Street> tbl = dd.GetTable<IoT_Street>();

                 var count = dd.GetTable<IoT_Street>().Where(p =>
                     p.CompanyID == info.CompanyID && p.Ser == info.Ser).Count();

                 if (count > 0) {

                     m = new Message()
                     {
                         Result = false,
                         TxtMessage = "街区编号不能重复!"
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
                    TxtMessage = JSon.TToJson<IoT_Street>(info)
                };



            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增街道参数失败！" + e.Message
                };
            }
            return m;
        }

        public Message Edit(IoT_Street info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                    var count = dd.GetTable<IoT_Street>().Where(p =>
                        p.CompanyID == info.CompanyID && p.Ser == info.Ser && p.ID != info.ID).Count();

                    if (count > 0)
                    {

                        m = new Message()
                        {
                            Result = false,
                            TxtMessage = "街区编号不能重复!"
                        };
                        return m;
                    }

                   IoT_Street dbinfo = dd.GetTable<IoT_Street>().Where(p => 
                     p.CompanyID == info.CompanyID&&p.ID==info.ID).SingleOrDefault();

                    ConvertHelper.Copy<IoT_Street>(dbinfo, info);

                    // 更新操作
                    dd.SubmitChanges();
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.TToJson<IoT_Street>(dbinfo)
                    };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑街道参数失败！" + e.Message
                };
            }
            return m;
        }

        public Message Delete(IoT_Street info)
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
                    throw new Exception("街区已被使用，不能删除");

                var count = dd.GetTable<IoT_Community>().Where(p =>
                       p.StreetID == info.ID).Count();

                if (count > 0)
                {

                    m = new Message()
                    {
                        Result = false,
                        TxtMessage = "街区存在下属小区，请先删除小区!"
                    };
                    return m;
                }


                // 获得上下文对象中的表信息
                Table<IoT_Street> tbl = dd.GetTable<IoT_Street>();

                var s = tbl.Where(p => p.CompanyID == info.CompanyID && p.ID == info.ID).Single();
                tbl.DeleteOnSubmit(s as IoT_Street);
            

                // 更新操作
                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = "删除街道参数成功！"
                };
               
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "删除街道参数失败！" + e.Message
                };
            }
            return m;
        }
    }
}
