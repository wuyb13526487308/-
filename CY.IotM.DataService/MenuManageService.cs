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
    public class MenuManageService : IMenuManage
    {
        public Message AddMenu(MenuInfo info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                if (info.MenuCode == string.Empty)
                {
                    m = new Message()
                    {
                        Result = false,
                        TxtMessage = "新增失败！菜单编号不能为空。"
                    };
                }
                else
                {
                    // 获得上下文对象中的信息表
                    Table<MenuInfo> tbl = dd.GetTable<MenuInfo>();
                    //判断菜单是否存在。
                    int existsCount = (from p in tbl where p.MenuCode == info.MenuCode select p.MenuCode).Count();
                    if (existsCount > 0)
                    {
                        m = new Message()
                        {
                            Result = false,
                            TxtMessage = string.Format("新增失败！菜单编号{0}已存在。", info.MenuCode)
                        };
                    }
                    else
                    {

                        //加载单位报表菜单
                        if (info.Type == "03") {
                            info.UrlClass = "../Report/Report.aspx?id=" + info.RID;
                        }

                        // 调用新增方法
                        tbl.InsertOnSubmit(info);
                        // 更新操作
                        dd.SubmitChanges();
            
                        m = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.TToJson<MenuInfo>(info)
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
        public Message EditMenu(MenuInfo info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                if (info.MenuCode == string.Empty)
                {
                    m = new Message()
                    {
                        Result = false,
                        TxtMessage = "编辑失败！菜单编号不能为空。"
                    };

                }
                else
                {
                    // 获得上下文对象中的用户信息表
                    MenuInfo dbinfo = dd.GetTable<MenuInfo>().Where(p => p.MenuCode == info.MenuCode).SingleOrDefault();
                    //定义修改主键
                    string MenuCode = dbinfo.MenuCode;
                    ConvertHelper.Copy<MenuInfo>(dbinfo, info);
                    dbinfo.MenuCode = MenuCode;
                    // 更新操作
                    dd.SubmitChanges();
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.TToJson<MenuInfo>(dbinfo)
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

        public Message DeleteMenu(MenuInfo info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                if (info.MenuCode == string.Empty)
                {
                    m = new Message()
                    {
                        Result = false,
                        TxtMessage = "操作失败！菜单编号不能为空。"
                    };

                }
                else
                {

                    // 获得上下文对象中的表具信息表
                    Table<MenuInfo> tbl = dd.GetTable<MenuInfo>();
                    var s = tbl.Where(p => p.MenuCode == info.MenuCode).Single();
                    MenuInfo dbinfo = s as MenuInfo;

                    tbl.DeleteOnSubmit(s as MenuInfo);
                    // 更新操作
                    dd.SubmitChanges();
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = "删除成功！"
                    };
                    
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


        public void ReSetCompany(string CompanyID)
        {
            string sqlText = "SP_AddDefaultRight";
            SqlParameter[] parms = new SqlParameter[] { 
                    new SqlParameter("CompanyID", CompanyID)};
            SQLHelper.ExecuteNonQuery(SQLHelper.SchuleConnection, CommandType.StoredProcedure, sqlText, parms);
        }





     
    }

}
