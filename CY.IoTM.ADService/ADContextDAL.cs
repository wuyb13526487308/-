using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.ADSystem;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;


namespace CY.IoTM.ADService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“ADContextDAL”。
    public class ADContextDAL : IADContextDAL
    {
        string conString = System.Configuration.ConfigurationManager.ConnectionStrings[System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"]].ConnectionString;
        /// <summary>
        /// 添加主题
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Message Add(ADContext info)
        {
            Message jsonMessage;
            bool resultB = false;
            string reStr = "";
            try
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ADContext(");
                strSql.Append("CompanyID,Context,State,CreateDate)");
                strSql.Append(" values (");
                strSql.Append("@CompanyID,@Context,@State,@CreateDate)");
                SqlParameter[] parameters = {
					//new SqlParameter("@AC_ID", SqlDbType.BigInt,8),
					new SqlParameter("@CompanyID", SqlDbType.Char,4),
					new SqlParameter("@Context", SqlDbType.VarChar,200),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@CreateDate", SqlDbType.SmallDateTime)};
                parameters[0].Value = info.CompanyID;
                parameters[1].Value = info.Context;
                parameters[2].Value = info.State;
                parameters[3].Value = info.CreateDate;

                resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;
                //反回Json字段串:
                reStr = "{\"Context\":\"" + info.Context + "\",\"CreateDate\":\"" + info.CreateDate + "\",\"State\":\"" + info.State + "\"}";
            }
            catch (Exception e)
            {
                reStr = e.ToString();
            }
            if (!resultB) reStr = "添加失败!";

            jsonMessage = new Message()
             {
                 Result = resultB,
                 TxtMessage = reStr
             };
            return jsonMessage;

        }

        /// <summary>
        /// 修改主题
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Message Edit(ADContext info)
        {
            Message jsonMessage;
            bool resultB = false;
            string reStr = "";
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ADContext set ");
                strSql.Append("CompanyID=@CompanyID,");
                strSql.Append("Context=@Context ");
                strSql.Append(" where AC_ID = @AC_ID  ");
                SqlParameter[] parameters = {
					    new SqlParameter("@AC_ID", SqlDbType.BigInt,8),
					    new SqlParameter("@CompanyID", SqlDbType.Char,4),
					    new SqlParameter("@Context", SqlDbType.VarChar,200)};
                parameters[0].Value = info.AC_ID;
                parameters[1].Value = info.CompanyID;
                parameters[2].Value = info.Context;
                resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;
            }
            catch (Exception e)
            {
                reStr = e.ToString();
            }

            if (!resultB) { reStr = "修改失败！"; } else 
            {
            //反回Json字段串:
                reStr = "{\"AC_ID\":\"" + info.AC_ID + "\",\"Context\":\"" + info.Context + "\",\"CreateDate\":\"" + info.CreateDate + "\",\"State\":\"" + info.State + "\"}";
            }
            jsonMessage = new Message()
             {
                 Result = resultB,
                 TxtMessage = reStr
             };
            return jsonMessage;
        }

        public Message Delete(long Ac_ID)
        {
            Message jsonMessage;
            bool resultB = false;
            string reStr = "";
            short State = 0;
            //查询是否发布,如果发布禁止删除;
            string statusDel = "select State from ADContext where AC_ID = " + Ac_ID;
            SqlDataReader infoReader = SQLHelper.ExecuteReader(conString, CommandType.Text, statusDel.ToString());
            if (infoReader.Read())
            {
                State = short.Parse(infoReader["State"].ToString());
            }
            infoReader.Close();

            if (State == 2)
            {
                reStr = "信息已发布，不能删除！";
            }
            else
            {
                //执行删除
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from ADContext ");
                strSql.Append(" where AC_ID= " + Ac_ID + ";");

                strSql.Append("delete from ADItem ");
                strSql.Append(" where AC_ID= " + Ac_ID + ";");


                resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString()) > 0;

                if (!resultB) { reStr = "删除失败！"; } else { reStr = "删除成功！"; }
            }
            jsonMessage = new Message()
             {
                 Result = resultB,
                 TxtMessage = reStr
             };
            return jsonMessage;
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ADContext ");
            strSql.Append(" where AC_ID in (" + IDlist + ")  ");
            return SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString()) > 0;
        }

        /// <summary>
        /// 更新数据状态
        /// </summary>
        /// <param name="AC_Id"></param>
        /// <returns></returns>
        public Message UpadteAdStatus(long AC_Id, int State)
        {
            Message jsonMessage;
            bool resultB = false;
            string reStr = "";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ADContext set ");
            strSql.Append(" State=@State");
            strSql.Append(" where AC_ID = @AC_ID  ");
            SqlParameter[] parameters = {

					    
                        new SqlParameter("@AC_ID", SqlDbType.BigInt,20),
					    new SqlParameter("@State", SqlDbType.BigInt,8)     
                                        };
            parameters[0].Value = AC_Id;
            parameters[1].Value = State;
            resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;

            if (!resultB) { reStr = "修改失败！"; } else { reStr = "修改成功！"; }
            jsonMessage = new Message()
            {
                Result = resultB,
                TxtMessage = reStr
            };
            return jsonMessage;
        }
    }
}
