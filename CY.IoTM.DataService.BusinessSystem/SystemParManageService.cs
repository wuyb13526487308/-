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
    public class SystemParManageService : ISystemParManage
    {
        public Message Add(IoT_SystemPar info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
              
                 Table<IoT_SystemPar> tbl = dd.GetTable<IoT_SystemPar>();


                // 调用新增方法
                tbl.InsertOnSubmit(info);
                // 更新操作
                dd.SubmitChanges();

                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_SystemPar>(info)
                };



            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增服务器参数失败！" + e.Message
                };
            }
            return m;
        }

        public Message Edit(IoT_SystemPar info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                   IoT_SystemPar dbinfo = dd.GetTable<IoT_SystemPar>().Where(p => 
                     p.CompanyID == info.CompanyID).SingleOrDefault();


                   if (dbinfo == null) { return Add(info); }


                    ConvertHelper.Copy<IoT_SystemPar>(dbinfo, info);

                    // 更新操作
                    dd.SubmitChanges();
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.TToJson<IoT_SystemPar>(dbinfo)
                    };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑服务器参数失败！" + e.Message
                };
            }
            return m;
        }

        public Message Delete(IoT_SystemPar info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                // 获得上下文对象中的表信息
                Table<IoT_SystemPar> tbl = dd.GetTable<IoT_SystemPar>();

                var s = tbl.Where(p =>  p.CompanyID == info.CompanyID ).Single();
                tbl.DeleteOnSubmit(s as IoT_SystemPar);
            

                // 更新操作
                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = "删除服务器参数成功！"
                };
               
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "删除服务器参数失败！" + e.Message
                };
            }
            return m;
        }
        public IoT_SystemPar getSystemPar(string companyID)
        {
            IoT_SystemPar dbinfo =null;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                dbinfo = dd.GetTable<IoT_SystemPar>().Where(p =>
                  p.CompanyID == companyID).SingleOrDefault();
            }
            catch (Exception e)
            {
            }
            if (dbinfo == null)
            {
                string strKey = $"{System.Configuration.ConfigurationManager.AppSettings["Key"]}";
                try
                {

                }
                catch { }
                dbinfo = new IoT_SystemPar() { AutoKey = false,MKey=strKey };
            }

            return dbinfo;
        }
    }
}
