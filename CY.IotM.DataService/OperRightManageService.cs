using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IotM.Common;
using System.Data.Linq;
using CY.IotM.Common.Tool;
using CY.IotM.Common.Memcached;
using System.Data;
using System.Data.SqlClient;

namespace CY.IotM.DataService
{
    public partial class OperRightManageService : IOperRightManage
    {

        #region 权限加载
        public List<DefineMenu> LoadDefineMenuByLoginOper(CompanyOperator info, bool withButtonMenuCode)
        {
            return GetDefineMenuByLoginOper(info).Where(p => p.Type == "00" || p.Type == "01" || p.Type == "03" || (withButtonMenuCode && p.Type == "02")).ToList();
        }
        public bool CheckMenuCode(CompanyOperator info, string menuCode)
        {
            return GetDefineMenuByLoginOper(info).Exists(p => p.MenuCode == menuCode);
        }
        public string LoadHiddenMenuCode(CompanyOperator info)
        {

            List<DefineMenu> list = GetDefineMenuByLoginOper(info).Where(p => p.Type == "02").ToList();
            StringBuilder Json = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                foreach (DefineMenu p in list)
                {
                    if (p.Type == "02")
                    {
                        Json.Append(",");
                        Json.Append(p.MenuCode);
                    }
                }
            }
            Json.Append(",");
            return Json.ToString();
        }
        private List<DefineMenu> GetDefineMenuByLoginOper(CompanyOperator info)
        {
            List<DefineMenu> list = new List<DefineMenu>();
            try
            {
                List<DefineOperRight> defineOperRightList = GetCompanyDefineOperRight(info.CompanyID).Where(p => p.OperID == info.OperID && p.CompanyID == info.CompanyID).ToList();
                List<DefineRightMenu> rightMenuList = new List<DefineRightMenu>();
                //有权限分配给他才加载
                if (defineOperRightList != null && defineOperRightList.Count > 0)
                {
                    //遍历
                    defineOperRightList.ForEach(p => rightMenuList.AddRange(GetCompanyDefineRightMenu(info.CompanyID).Where(i => i.CompanyID == p.CompanyID && i.RightCode == p.RightCode)));


                }
                if (rightMenuList != null && rightMenuList.Count > 0)
                {
                    var i =
                        from c in GetCompanyDefineMenu(info.CompanyID)
                        join p in rightMenuList on c.MenuCode equals p.MenuCode
                        select c;
                    list.AddRange(i.ToList());
                }
                list = list.Distinct().ToList();
            }
            catch { }
            finally
            { }
            return list;
        }
        #endregion
        #region 权限分配管理
        public List<DefineMenu> LoadCompanyDefineMenu(string companyID)
        {
            return GetCompanyDefineMenu(companyID);
        }
        public List<DefineRight> LoadCompanyDefineRight(string companyID)
        {
            return GetCompanyDefineRight(companyID);
        }
        public string LoadCompanyOperDefineRight(string companyID, string operID)
        {
            string result = string.Empty;
            List<DefineOperRight> list = GetCompanyDefineOperRight(companyID).Where(p => p.OperID == operID).ToList();
            if (list != null && list.Count > 0)
            {
                list.ForEach(p => result += p.RightCode + ",");
            }
            if (result != string.Empty)
            {
                result = result.Substring(0, result.LastIndexOf(','));
            }
            return result;
        }

        public Message AddCompanyDefineRight(DefineRight dRight, List<DefineRightMenu> list)
        {
            string sqlText = string.Empty;
            Message m = new Message() { Result = true, TxtMessage = "操作成功。" };
            try
            {
                //先删除再执行添加
                if (DelCompanyDefineRight(dRight).Result)
                {

                    sqlText = string.Format("Insert Into S_DefineRight" +
    "(RightCode,CompanyID,RightName,Context)" +
    "values('{0}','{1}','{2}','{3}')"
    , dRight.RightCode, dRight.CompanyID, dRight.RightName, dRight.Context);
                    SQLHelper.ExecuteScalar(SQLHelper.SchuleConnection, CommandType.Text, sqlText);
                    foreach (DefineRightMenu tmp in list)
                    {
                        sqlText = string.Format("Insert Into S_DefineRightMenu" +
    "(CompanyID,RightCode,MenuCode)" +
    "values('{0}','{1}','{2}')"
    , tmp.CompanyID, tmp.RightCode, tmp.MenuCode);
                        SQLHelper.ExecuteScalar(SQLHelper.SchuleConnection, CommandType.Text, sqlText);
                    }
                    m = new Message() { Result = true, TxtMessage = JSon.TToJson<DefineRight>(dRight) };
                }
                else
                {
                    m = new Message() { Result = false, TxtMessage = "操作失败，【删除相同编码权限出错】。" };
                }

            }
            catch (Exception e)
            {

                m = new Message() { Result = false, TxtMessage = "操作失败，【" + e.Message + "】。" };
            }
            return m;
        }

        public Message DelCompanyDefineRight(DefineRight dRight)
        {

            Message m = new Message() { Result = true, TxtMessage = "操作成功。" };
            string sqlText = string.Empty;
            try
            {

                //sqlText = string.Format("select COUNT(1) from S_DefineOperRight where CompanyID='{0}' and RightCode='{1}' ", dRight.CompanyID, dRight.RightCode);
                //object count=  SQLHelper.ExecuteScalar(SQLHelper.SchuleConnection, CommandType.Text, sqlText);
                //if (Convert.ToInt32(count) > 0) 
                //{
                //    m = new Message() { Result = false, TxtMessage = "操作失败，【权限组使用当中无法删除!】" };
                //    return m;
                //}

                sqlText = string.Format("delete from  S_DefineRightMenu where CompanyID='{0}' and RightCode='{1}' "
                + "delete from  S_DefineRight where CompanyID='{0}' and RightCode='{1}' ", dRight.CompanyID, dRight.RightCode);
                SQLHelper.ExecuteScalar(SQLHelper.SchuleConnection, CommandType.Text, sqlText);
                //此处需要清理缓存
                CompanyRightCached.getInstance().RemoveDefineOperRight(dRight.CompanyID);
                CompanyRightCached.getInstance().RemoveDefineRightMenu(dRight.CompanyID);
                CompanyRightCached.getInstance().RemoveDefineRight(dRight.CompanyID);


            }
            catch (Exception e)
            {
                m = new Message() { Result = false, TxtMessage = "操作失败，【" + e.Message + "】" };
            }
            return m;
        }

        public Message EditCompanyOperRight(string CompanyID, string OperID, List<DefineRight> list)
        {
            Message m = new Message() { Result = true, TxtMessage = "操作成功。" };
            string sqlText = string.Empty;
            try
            {

                sqlText = string.Format("delete from  S_DefineOperRight where CompanyID='{0}' and OperID='{1}'"
, CompanyID, OperID);
                SQLHelper.ExecuteScalar(SQLHelper.SchuleConnection, CommandType.Text, sqlText);
                foreach (DefineRight tmp in list)
                {
                    sqlText = string.Format("Insert Into S_DefineOperRight" +
"(CompanyID,RightCode,OperID)" +
"values('{0}','{1}','{2}')"
, tmp.CompanyID, tmp.RightCode, OperID);
                    SQLHelper.ExecuteScalar(SQLHelper.SchuleConnection, CommandType.Text, sqlText);
                }
                //此处需要清理缓存            
                CompanyRightCached.getInstance().RemoveDefineOperRight(CompanyID);

            }
            catch (Exception e)
            {
                m = new Message() { Result = false, TxtMessage = "操作失败，【" + e.Message + "】" };
            }
            return m;
        }



        public Message EditCompanyMenu(string CompanyID, List<String> list)
        {
            Message m = new Message() { Result = true, TxtMessage = "操作成功。" };
            string sqlText = string.Empty;
            try
            {

                sqlText = string.Format("delete from S_CompanyMenu where CompanyID='{0}'", CompanyID);
                SQLHelper.ExecuteScalar(SQLHelper.SchuleConnection, CommandType.Text, sqlText);
                foreach (String tmp in list)
                {
                    sqlText = string.Format("Insert Into S_CompanyMenu" +
                    "(CompanyID,MenuCode)" +
                    "values('{0}','{1}')"
                    , CompanyID, tmp);
                    SQLHelper.ExecuteScalar(SQLHelper.SchuleConnection, CommandType.Text, sqlText);
                }



                //给默认操作员赋权限
                sqlText = "SP_AddDefaultRight";
                SqlParameter[] parms = new SqlParameter[] { 
                    new SqlParameter("CompanyID", CompanyID)};
                SQLHelper.ExecuteNonQuery(SQLHelper.SchuleConnection, CommandType.StoredProcedure, sqlText, parms);

                //清理缓存
                RemoveCompanyRightCache(CompanyID);
            }
            catch (Exception e)
            {
                m = new Message() { Result = false, TxtMessage = "操作失败，【" + e.Message + "】" };
            }
            return m;
        }






        public string LoadCompanyDefineRightMenu(string companyID, string rightCode)
        {
            var right = from p in GetCompanyDefineMenu(companyID)
                        join b in
                            GetCompanyDefineRightMenu(companyID).Where(p => p.RightCode == rightCode) on p.MenuCode equals b.MenuCode
                        select b;
            StringBuilder Json = new StringBuilder();
            List<DefineRightMenu> list = right.ToList();
            if (list != null && list.Count > 0)
            {
                foreach (DefineRightMenu p in list)
                {

                    Json.Append(",");
                    Json.Append(p.MenuCode);
                }
            }
            Json.Append(",");
            return Json.ToString();
        }

        public Message RemoveCompanyRightCache(string CompanyID)
        {
            Message m = new Message() { Result = true, TxtMessage = "更新缓存操作成功。" };
            string sqlText = string.Empty;
            try
            {
                CompanyRightCached.getInstance().RemoveDefineRight(CompanyID);
                CompanyRightCached.getInstance().RemoveDefineOperRight(CompanyID);
                CompanyRightCached.getInstance().RemoveDefineRightMenu(CompanyID);
                CompanyRightCached.getInstance().RemoveDefineMenu(CompanyID);

            }
            catch (Exception e)
            {
                m = new Message() { Result = false, TxtMessage = "操作失败，【" + e.Message + "】" };
            }
            return m;
        }
        #endregion
        #region 报表管理
        public Message EditReportName(ReportTemplate info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                // 获得上下文对象中的用户信息表
                ReportTemplate dbinfo = dd.GetTable<ReportTemplate>().Where(p => p.RID == info.RID).SingleOrDefault();
                //定义修改主键
                dbinfo.ReportName = info.ReportName;
                // 更新操作
                dd.SubmitChanges();
                dbinfo.ReportTemplate1 = null;
                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<ReportTemplate>(dbinfo)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "修改失败！" + e.Message
                };
            }
            return m;

        }
        #endregion
        #region 缓存数据
        private List<DefineMenu> GetCompanyDefineMenu(string companyID)
        {
            List<DefineMenu> list = CompanyRightCached.getInstance().GetDefineMenu(companyID);
            if (list == null)
            {
                try
                {
                    string sqlText = "SP_GetDefineMenu";
                    SqlParameter[] parms = new SqlParameter[] { 
                    new SqlParameter("CompanyID", companyID)  };
                    using (DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.SchuleConnection, CommandType.StoredProcedure, sqlText, parms))
                    {
                        if (ds.Tables.Count > 0)
                        {
                            list = ConvertHelper.GetList<DefineMenu>(ds.Tables[0]);
                        }
                    }
                    CompanyRightCached.getInstance().SetDefineMenu(companyID, list);
                }
                catch { }
                finally
                { }
            }
            return list;
        }
        private List<DefineRight> GetCompanyDefineRight(string companyID)
        {
            List<DefineRight> list = CompanyRightCached.getInstance().GetDefineRight(companyID);
            if (list == null)
            {
                try
                {
                    string sqlText = "SP_GetDefineRight";
                    SqlParameter[] parms = new SqlParameter[] { 
                    new SqlParameter("CompanyID", companyID)  };
                    using (DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.SchuleConnection, CommandType.StoredProcedure, sqlText, parms))
                    {
                        if (ds.Tables.Count > 0)
                        {
                            list = ConvertHelper.GetList<DefineRight>(ds.Tables[0]);
                        }
                    }
                    CompanyRightCached.getInstance().SetDefineRight(companyID, list);
                }
                catch { }
                finally
                { }
            }
            return list;
        }
        private List<DefineRightMenu> GetCompanyDefineRightMenu(string companyID)
        {
            List<DefineRightMenu> list = CompanyRightCached.getInstance().GetDefineRightMenu(companyID);
            if (list == null)
            {
                try
                {
                    string sqlText = "SP_GetDefineRightMenu";
                    SqlParameter[] parms = new SqlParameter[] { 
                    new SqlParameter("CompanyID", companyID)  };
                    using (DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.SchuleConnection, CommandType.StoredProcedure, sqlText, parms))
                    {
                        if (ds.Tables.Count > 0)
                        {
                            list = ConvertHelper.GetList<DefineRightMenu>(ds.Tables[0]);
                        }
                    }
                    CompanyRightCached.getInstance().SetDefineRightMenu(companyID, list);
                }
                catch { }
                finally
                { }
            }
            return list;
        }
        private List<DefineOperRight> GetCompanyDefineOperRight(string companyID)
        {
            List<DefineOperRight> list = CompanyRightCached.getInstance().GetDefineOperRight(companyID);
            if (list == null)
            {
                try
                {
                    string sqlText = "SP_GetDefineOperRight";
                    SqlParameter[] parms = new SqlParameter[] { 
                    new SqlParameter("CompanyID", companyID)  };
                    using (DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.SchuleConnection, CommandType.StoredProcedure, sqlText, parms))
                    {
                        if (ds.Tables.Count > 0)
                        {
                            list = ConvertHelper.GetList<DefineOperRight>(ds.Tables[0]);
                        }
                    }
                    CompanyRightCached.getInstance().SetDefineOperRight(companyID, list);
                }
                catch { }
                finally
                { }
            }
            return list;
        }
        #endregion
    }
}
