using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IotM.Common;
using System.Data.Linq;
using CY.IotM.Common.Tool;
using System.Data;
using System.Data.SqlClient;

namespace CY.IotM.DataService
{
    public class CompanyOperatorService : ICompanyOperatorManage
    {
        public Message AddCompanyOperator(CompanyOperator info)
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
                    // 获得上下文对象中的用户信息表
                    Table<CompanyOperator> tbl = dd.GetTable<CompanyOperator>();
                    //判断账号是否存在。
                    int existsCount = (from p in tbl where p.CompanyID == info.CompanyID && p.OperID == info.OperID select p.OperID).Count();
                    if (existsCount > 0)
                    {
                        m = new Message()
                        {
                            Result = false,
                            TxtMessage = string.Format("新增失败！操作员编号{0}已存在。", info.OperID)
                        };
                    }
                    else
                    {
                        //判断做为登陆手机号账号是否存在
                        if (info.PhoneLogin == true && info.Phone != string.Empty)
                        {
                            existsCount = (from p in tbl where p.Phone == info.Phone && p.PhoneLogin == true select p.OperID).Count();
                            if (existsCount > 0)
                            {
                                m = new Message()
                                {
                                    Result = false,
                                    TxtMessage = string.Format("新增失败！该手机号{0}已被其他用户绑定，请更换手机号码进行绑定操作。", info.Phone)
                                };
                                return m;
                            }

                        }
                        // 调用新增方法
                        tbl.InsertOnSubmit(info);
                        // 更新操作
                        dd.SubmitChanges();
                        m = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.TToJson<CompanyOperator>(info)
                        };
                        if (info.State == null || (info.State != null && info.State.ToString() == "1"))
                        {
                            SetOperatorNoRight(info);
                        }
                    }

                }

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增用户失败！" + e.Message
                };
            }
            return m;
        }
        public Message EditCompanyOperator(CompanyOperator info)
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

                    //判断做为登陆手机号账号是否存在
                    // 获得上下文对象中的用户信息表
                    Table<CompanyOperator> tbl = dd.GetTable<CompanyOperator>();
                    int existsCount = 0;
                    if (info.PhoneLogin == true && info.Phone != string.Empty)
                    {
                        existsCount = (from p in tbl
                                       where p.Phone == info.Phone && p.PhoneLogin == true &&
                                           !(p.OperID == info.OperID && p.CompanyID == info.CompanyID)
                                       select p.OperID).Count();
                        if (existsCount > 0)
                        {
                            m = new Message()
                            {
                                Result = false,
                                TxtMessage = string.Format("编辑失败！该手机号{0}已被其他用户绑定，请更换手机号码进行绑定操作。", info.Phone)
                            };
                            return m;
                        }

                    }
                    // 获得上下文对象中的用户信息表                 
                    CompanyOperator dbinfo = dd.GetTable<CompanyOperator>().Where(p => p.OperID == info.OperID && p.CompanyID == info.CompanyID).SingleOrDefault();
                    //定义修改主键
                    string CompanyID = dbinfo.CompanyID;
                    string OperID = dbinfo.OperID;
                    short? OperType = dbinfo.OperType;
                    ConvertHelper.Copy<CompanyOperator>(dbinfo, info);
                    dbinfo.CompanyID = CompanyID;
                    dbinfo.OperID = OperID;
                    dbinfo.OperType = OperType;
                    // 更新操作
                    dd.SubmitChanges();
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.TToJson<CompanyOperator>(dbinfo)
                    };
                    if (dbinfo.State == null || (dbinfo.State != null && dbinfo.State.ToString() == "1"))
                    {
                        SetOperatorNoRight(dbinfo);
                    }
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

        public Message DeleteCompanyOperator(CompanyOperator info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                // 获得上下文对象中的表具信息表
                Table<CompanyOperator> tbl = dd.GetTable<CompanyOperator>();
                var s = tbl.Where(p => p.OperID == info.OperID && p.CompanyID == info.CompanyID).Single();
                CompanyOperator dbinfo = s as CompanyOperator;
                if (s.OperType == 1)
                {
                    m = new Message()
                    {
                        Result = false,
                        TxtMessage = "删除失败！企业主账号不能被删除。"
                    };

                }
                else
                {
                    tbl.DeleteOnSubmit(s as CompanyOperator);
                    // 更新操作
                    dd.SubmitChanges();
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = "删除成功！"
                    };
                    SetOperatorNoRight(s as CompanyOperator);
                  
                }

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "删除失败！" + e.Message
                };
            }
            return m;
        }
        private Message SetOperatorNoRight(CompanyOperator s)
        {
            OperRightManageService opRightService = new OperRightManageService();
            return opRightService.EditCompanyOperRight(s.CompanyID, s.OperID, new List<DefineRight>());
        }




    }

}
