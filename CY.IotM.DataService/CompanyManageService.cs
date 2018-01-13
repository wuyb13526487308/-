using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IotM.Common;
using System.Data.Linq;
using CY.IotM.Common.Tool;
using System.Data.SqlClient;
using System.Data;

namespace CY.IotM.DataService
{
    public class CompanyManageService : ICompanyManage
    {
        public Message AddCompany(CompanyInfo info)
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
                        TxtMessage = "新增失败！企业编号不能为空。"
                    };
                }
                else
                {
                    // 获得上下文对象中的信息表
                    Table<CompanyInfo> tbl = dd.GetTable<CompanyInfo>();
                    //判断账号是否存在。
                    int existsCount = (from p in tbl where p.CompanyID == info.CompanyID select p.CompanyID).Count();
                    if (existsCount > 0)
                    {
                        m = new Message()
                        {
                            Result = false,
                            TxtMessage = string.Format("新增失败！企业编号{0}已存在。", info.CompanyID)
                        };
                    }
                    else
                    {
                        // 调用新增方法
                        tbl.InsertOnSubmit(info);
                        // 更新操作
                        dd.SubmitChanges();
                        ResetCompanyAdmin(info);
                        m = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.TToJson<CompanyInfo>(info)
                        };
                    }
                }

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增失败！" + e.Message
                };
            }
            return m;
        }
        public Message EditCompany(CompanyInfo info)
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
                        TxtMessage = "编辑失败！企业编号不能为空。"
                    };

                }
                else
                {
                    // 获得上下文对象中的用户信息表
                    CompanyInfo dbinfo = dd.GetTable<CompanyInfo>().Where(p => p.CompanyID == info.CompanyID).SingleOrDefault();
                    //定义修改主键
                    string CompanyID = dbinfo.CompanyID;
                    ConvertHelper.Copy<CompanyInfo>(dbinfo, info);
                    dbinfo.CompanyID = CompanyID;
                    // 更新操作
                    dd.SubmitChanges();
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.TToJson<CompanyInfo>(dbinfo)
                    };
                }

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑失败！" + e.Message
                };
            }
            return m;
        }

        public Message ResetCompanyAdmin(CompanyInfo info)
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
                        TxtMessage = "操作失败！企业编号不能为空。"
                    };

                }
                else
                {
                    int existsCount = 0;
                    Table<CompanyOperator> tb2 = dd.GetTable<CompanyOperator>();
                    existsCount = (from p in tb2 where p.CompanyID == info.CompanyID && p.OperID == info.CompanyID select p.CompanyID).Count();
                    if (existsCount == 0)
                    {
                        tb2.InsertOnSubmit(new CompanyOperator()
                        {
                            CompanyID = info.CompanyID,
                            OperID = info.CompanyID,
                            Pwd = Md5.GetMd5(info.CompanyID + info.CompanyID),
                            Name = "默认账号",
                            OperType = 1,
                            State = '0'
                        });
                        dd.SubmitChanges();
                    }
                    else
                    {
                        CompanyOperator dbopinfo = dd.GetTable<CompanyOperator>().Where(p => p.CompanyID == info.CompanyID && p.OperID == info.CompanyID).SingleOrDefault();
                        dbopinfo.Pwd = Md5.GetMd5(info.CompanyID + info.CompanyID);
                        dbopinfo.OperType = 1;
                        dbopinfo.State = '0';
                        dd.SubmitChanges();
                    }
                    // 获得上下文对象中的用户信息表
                    CompanyInfo dbinfo = dd.GetTable<CompanyInfo>().Where(p => p.CompanyID == info.CompanyID && p.CompanyName == info.CompanyName).SingleOrDefault();
                    //定义修改主键
                    string CompanyID = dbinfo.CompanyID;
                    string sqlText = "SP_AddDefaultRight";
                    SqlParameter[] parms = new SqlParameter[] { 
                    new SqlParameter("CompanyID", dbinfo.CompanyID)};
                    SQLHelper.ExecuteNonQuery(SQLHelper.SchuleConnection, CommandType.StoredProcedure, sqlText, parms);
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = "操作成功。"
                    };
                    //初始化后，清空权限缓存。
                    new OperRightManageService().RemoveCompanyRightCache(dbinfo.CompanyID);
                }

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "操作失败！" + e.Message
                };
            }
            return m;
        }
     

    




    

        

    }
}
