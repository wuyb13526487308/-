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
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“ADPublishDAL”。
    public class ADPublishDAL : IADPublishDAL
    {
        string conString = System.Configuration.ConfigurationManager.ConnectionStrings[System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"]].ConnectionString;

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public Message Add(ADPublish model)
        {
            Message jsonMessage;
            bool resultB = false;
            string recStr = "",recAPP = "";
            long AutoId = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ADPublish(");
            strSql.Append("CompanyID,AC_ID,AreaContext,PublishDate,UserCount,State)");
            strSql.Append(" values (");
            strSql.Append("@CompanyID,@AC_ID,@AreaContext,@PublishDate,@UserCount,@State)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyID", SqlDbType.Char,4),
					new SqlParameter("@AC_ID", SqlDbType.BigInt,8),
					new SqlParameter("@AreaContext", SqlDbType.VarChar,200),
					new SqlParameter("@PublishDate", SqlDbType.DateTime),
					new SqlParameter("@UserCount", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.SmallInt,2)};
            parameters[0].Value = model.CompanyID;
            parameters[1].Value = model.AC_ID;
            parameters[2].Value = model.AreaContext;
            parameters[3].Value = model.PublishDate;
            parameters[4].Value = model.UserCount;
            parameters[5].Value = model.State;


            //resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;
            AutoId = long.Parse(SQLHelper.ExecuteScalar(conString, CommandType.Text, strSql.ToString(), parameters).ToString());
            if (AutoId > 0) {
                resultB = true; 
                recStr = AutoId.ToString(); 
                
                ////当状态为发布的时间调用发布接口;
                //string adpjk = new ADPublishManager().ADPublish(AutoId);
                //if(model.State==1||model.State==2){
                //    if (adpjk.Length > 0)
                //    { //不成功时显示特定失败信息
                //        recAPP = "APP接口调用失败！ " + adpjk;
                //        recStr +=",信息添加成功，但发布失败！" + recAPP;
                //        //状态重新修改为未发布
                //        string sqlStr = "update ADPublish set State= 0 where AP_ID = " + AutoId;
                //        SQLHelper.ExecuteNonQuery(conString, CommandType.Text, sqlStr.ToString());
                //    };

                //}

            } else { resultB = false; recStr = "添加失败！"; }

            jsonMessage = new Message()
            {
                Result = resultB,
                TxtMessage = recStr
            };
            return jsonMessage;
        }

        /// <summary>
        /// 修改主题
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Message Edit(ADPublish model)
        {
            Message jsonMessage;
            bool resultB = false;
            string recStr = "", recAPP="";
            try
            {
               

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ADPublish set ");
                strSql.Append("CompanyID=@CompanyID,");
                strSql.Append("AC_ID=@AC_ID,");
                strSql.Append("AreaContext=@AreaContext,");
                strSql.Append("PublishDate=@PublishDate,");
                strSql.Append("UserCount=@UserCount,");
                strSql.Append("State=@State");
                strSql.Append(" where AP_ID=@AP_ID");
                SqlParameter[] parameters = {
					new SqlParameter("@CompanyID", SqlDbType.Char,4),
					new SqlParameter("@AC_ID", SqlDbType.BigInt,8),
					new SqlParameter("@AreaContext", SqlDbType.VarChar,200),
					new SqlParameter("@PublishDate", SqlDbType.DateTime),
					new SqlParameter("@UserCount", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.SmallInt,2),
					new SqlParameter("@AP_ID", SqlDbType.BigInt,8)};
                parameters[0].Value = model.CompanyID;
                parameters[1].Value = model.AC_ID;
                parameters[2].Value = model.AreaContext;
                parameters[3].Value = model.PublishDate;
                parameters[4].Value = model.UserCount;
                parameters[5].Value = model.State;
                parameters[6].Value = model.AP_ID;

                resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;
                if (!resultB) { recStr = "修改失败！"; } 
                else { 
                    recStr = "修改成功！";
                    //if (model.State == 1 || model.State == 2) {
                    //    //当状态为发布的时间调用发布接口;
                    //    string adpjk = new ADPublishManager().ADPublish(model.AP_ID,PublishType.NewPublish);
                    //    if (adpjk.Length > 0)
                    //    { 
                    //        //不成功时显示特定失败信息
                    //        recStr = "信息修改成功，但发布失败！";
                    //        recAPP = "APP接口调用失败！ " + adpjk;
                    //        recStr += "," + recAPP;
                    //        //状态重新修改为未发布
                    //        string sqlStr = "update ADPublish set State= 0 where AP_ID = " + model.AP_ID;
                    //        SQLHelper.ExecuteNonQuery(conString, CommandType.Text, sqlStr.ToString());
                    //    };
                    //}
                }

            }
            catch (Exception e)
            {
                recStr = e.ToString();
            }

            
            jsonMessage = new Message()
            {
                Result = resultB,
                TxtMessage = recStr
            };
            return jsonMessage;
        }

        public Message Delete(long AP_ID)
        {
            Message jsonMessage;
            bool resultB = false;
            string recStr = "";
            short State = 0;
            //查询是否发布,如果发布禁止删除;
            string statusDel = "select State from ADPublish where AP_ID = " + AP_ID;
            SqlDataReader infoReader = SQLHelper.ExecuteReader(conString, CommandType.Text, statusDel.ToString());
            if (infoReader.Read())
            {
                State = short.Parse(infoReader["State"].ToString());
            }
            infoReader.Close();

            if (State == 1)
            {
                recStr = "信息已发布，不能删除！";
            }
            else { 
                //执行删除
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from ADPublish ");
                strSql.Append(" where AP_ID= "+ AP_ID +"; ");
                strSql.Append(" delete from ADPublishUser where AP_ID = " + AP_ID + "; ");

                resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString()) > 0;
                if (!resultB) { recStr = "删除失败!"; } else { recStr = "删除成功!"; }
            }
            jsonMessage = new Message()
            {
                Result = resultB,
                TxtMessage = recStr
            };
            return jsonMessage;
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ADPublish ");
            strSql.Append(" where AP_ID in (" + IDlist + ")  ");
            return SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString()) > 0;
        }

        /// <summary>
        /// 更新数据状态
        /// </summary>
        /// <param name="AC_Id"></param>
        /// <returns></returns>
        public Message UpadteAdStatus(long AP_ID, int State)
        {
            Message jsonMessage;
            bool resultB = false;
            string recStr = "";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ADPublish set ");
            strSql.Append(" State=@State");
            strSql.Append(" where AP_ID = @AP_ID  ");
            SqlParameter[] parameters = {

                        new SqlParameter("@AP_ID", SqlDbType.BigInt,20),
					    new SqlParameter("@State", SqlDbType.BigInt,8)};

            parameters[0].Value = AP_ID;
            parameters[1].Value = State;
            resultB =  SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;

            if (!resultB) { recStr = "发布失败!"; } else { 
                recStr = "发布成功!";

            }
            jsonMessage = new Message()
            {
                Result = resultB,
                TxtMessage = recStr
            };
            return jsonMessage;
        }

        public string ADPubManager(long AP_ID) {
            string recStr = "";
            //当状态为发布的时间调用发布接口;
            string adpjk = new ADPublishManager().ADPublish(AP_ID);
            if (adpjk.Length > 0)
            {
                //不成功时显示特定失败信息
                recStr = "发布失败！APP接口调用失败!" + adpjk;
                //失败时状态重新修改为未发布
                string sqlStr = "update ADPublish set State= 0 where AP_ID = " + AP_ID;
                SQLHelper.ExecuteNonQuery(conString, CommandType.Text, sqlStr.ToString());
            };

            return recStr;
        }



    }
}
