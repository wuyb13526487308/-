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
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“ADPublishUserDAL”。
    public class ADPublishUserDAL : IADPublishUserDAL
    {
        string conString = System.Configuration.ConfigurationManager.ConnectionStrings[System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"]].ConnectionString;

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public Message Add(ADPublishUser model)
        {
            Message jsonMessage;
            bool resultB = false;
            string errorStr = "";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ADPublishUser(");
            strSql.Append("AP_ID,UserID,CompanyID,State,Context )");
            strSql.Append(" values (");
            strSql.Append("@AP_ID,@UserID,@CompanyID,@State,@Context");
            //strSql.Append(",@FinishedDate");
            strSql.Append(" )");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AP_ID", SqlDbType.BigInt,8),
					new SqlParameter("@UserID", SqlDbType.Char,10),
					new SqlParameter("@CompanyID", SqlDbType.Char,4),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@Context", SqlDbType.VarChar),
                    //new SqlParameter("@FinishedDate", SqlDbType.DateTime)
                    };
            parameters[0].Value = model.AP_ID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.CompanyID;
            parameters[3].Value = model.State;
            parameters[4].Value = model.Context;
            //parameters[4].Value = model.FinishedDate;

            resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;
            if (!resultB) errorStr = "添加失败!";

            jsonMessage = new Message()
            {
                Result = resultB,
                TxtMessage = errorStr
            };
            return jsonMessage;
        }

        /// <summary>
        /// 批量增加数据
        /// </summary>
        public Message groupAdd(ADPublishUser model,string userIDArray)
        {
            Message jsonMessage;
            bool resultB = false;
            string reStr = "";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete ADPublishUser where AP_ID=" + model.AP_ID + ";");//先清空表中人员内容
            strSql.Append("insert into ADPublishUser(");
            strSql.Append("AP_ID,UserID,CompanyID,State,Context)");
            strSql.Append(" select " + model.AP_ID + ",UserID,'" + model.CompanyID + "'," + model.State + ",'" + model.Context + "' from IoT_User where userID in(" + userIDArray + ")");

            resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString()) > 0;

            if (!resultB) { reStr = "删除失败！"; } else { reStr = "删除成功！"; }
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
        public Message Edit(ADPublishUser model)
        {
            Message jsonMessage;
            bool resultB = false;
            string errorStr = "";
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ADPublishUser set ");
                strSql.Append("CompanyID=@CompanyID,");
                strSql.Append("State=@State,");
                strSql.Append("FinishedDate=@FinishedDate,");
                strSql.Append("Context=@Context");
                strSql.Append(" where ID=@ID");
                SqlParameter[] parameters = {
					new SqlParameter("@CompanyID", SqlDbType.Char,4),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@FinishedDate", SqlDbType.DateTime),
					new SqlParameter("@Context", SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@AP_ID", SqlDbType.BigInt,8),
					new SqlParameter("@UserID", SqlDbType.Char,10)};
                parameters[0].Value = model.CompanyID;
                parameters[1].Value = model.State;
                parameters[2].Value = model.FinishedDate;
                parameters[3].Value = model.Context;
                parameters[4].Value = model.ID;
                parameters[5].Value = model.AP_ID;
                parameters[6].Value = model.UserID;

                resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;
            }
            catch (Exception e)
            {
                errorStr = e.ToString();
            }
            if (!resultB) errorStr = "修改失败!";
            jsonMessage = new Message()
            {
                Result = resultB,
                TxtMessage = errorStr
            };
            return jsonMessage;
        }

        public Message Delete(int ID)
        {
            Message jsonMessage;
            bool resultB = false;
            string errorStr = "";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ADPublishUser ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)};
            parameters[0].Value = ID;
            resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;
            if (!resultB) errorStr = "删除失败!";
            jsonMessage = new Message()
            {
                Result = resultB,
                TxtMessage = errorStr
            };
            return jsonMessage;
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ADPublishUser ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            return SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString()) > 0;
        }

        /// <summary>
        /// 更新数据状态
        /// </summary>
        /// <param name="AC_Id"></param>
        /// <returns></returns>
        public bool UpadteAdStatus(int ID, int State)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ADPublishUser set ");
            strSql.Append(" State=@State");
            strSql.Append(" where ID = @ID  ");
            SqlParameter[] parameters = {

                        new SqlParameter("@ID", SqlDbType.BigInt,20),
					    new SqlParameter("@State", SqlDbType.BigInt,8)};

            parameters[0].Value =ID;
            parameters[1].Value = State;
            return SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;

        }


    }
}
