using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IotM.Common;
using System.Data.Linq;
using CY.IotM.Common.Tool;

using System.ServiceModel;

namespace CY.IotM.DataService
{
    public class SystemLogManageService : ISystemLogManage
    {

        public Message AddSystemLog(SystemLog info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                if (info.CompanyID == string.Empty)
                {
                    m = new Message()
                    {
                        Result = false,
                        TxtMessage = "记录系统日志失败！企业编号不能为空。"
                    };
                }
                else
                {
                    // 获得上下文对象中的信息表
                    Table<SystemLog> tbl = dd.GetTable<SystemLog>();
                    // 调用新增方法
                    tbl.InsertOnSubmit(info);
                    // 更新操作
                    dd.SubmitChanges();
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.TToJson<SystemLog>(info)
                    };
                }
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "记录系统日志失败！" + e.Message
                };
            }
            return m;
        }      
    }
}
