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
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“ADUserDAL”。
    public class ADUserDAL : IADUserDAL
    {
        string conString = System.Configuration.ConfigurationManager.ConnectionStrings[System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"]].ConnectionString;

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public Message Add(ADUser model)
        {
            Message jsonMessage;
            bool resultB = false;
            string recStr = "";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ADUser(");
            strSql.Append("UserID,CompanyID,AP_ID,PublishDate,Street,Community,AC_ID,Adress,AddTime,Ver)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@CompanyID,@AP_ID,@PublishDate,@Street,@Community,@AC_ID,@Adress,@AddTime,@Ver)");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Char,10),
					new SqlParameter("@CompanyID", SqlDbType.Char,4),
					new SqlParameter("@AP_ID", SqlDbType.BigInt,8),
					new SqlParameter("@PublishDate", SqlDbType.DateTime),
					new SqlParameter("@Street", SqlDbType.VarChar,50),
					new SqlParameter("@Community", SqlDbType.VarChar,50),
					new SqlParameter("@AC_ID", SqlDbType.BigInt,8),
					new SqlParameter("@Adress", SqlDbType.VarChar,100),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@Ver", SqlDbType.Char,4)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.CompanyID;
            parameters[2].Value = model.AP_ID;
            parameters[3].Value = model.PublishDate;
            parameters[4].Value = model.Street;
            parameters[5].Value = model.Community;
            parameters[6].Value = model.AC_ID;
            parameters[7].Value = model.Adress;
            parameters[8].Value = model.AddTime;
            parameters[9].Value = model.Ver;


            resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;
            if (!resultB) recStr = "添加失败!" + model.UserID;

            jsonMessage = new Message()
            {
                Result = resultB,
                TxtMessage = recStr
            };
            return jsonMessage;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Message Edit(ADUser model)
        {
            Message jsonMessage;
            bool resultB = false;
            string errorStr = "";
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ADUser set ");
                strSql.Append("AP_ID=@AP_ID,");
                strSql.Append("PublishDate=@PublishDate,");
                strSql.Append("Street=@Street,");
                strSql.Append("Community=@Community,");
                strSql.Append("AC_ID=@AC_ID,");
                strSql.Append("Adress=@Adress,");
                strSql.Append("AddTime=@AddTime,");
                strSql.Append("Ver=@Ver");
                strSql.Append(" where UserID=@UserID and CompanyID=@CompanyID ");
                SqlParameter[] parameters = {
					new SqlParameter("@AP_ID", SqlDbType.BigInt,8),
					new SqlParameter("@PublishDate", SqlDbType.DateTime),
					new SqlParameter("@Street", SqlDbType.VarChar,50),
					new SqlParameter("@Community", SqlDbType.VarChar,50),
					new SqlParameter("@AC_ID", SqlDbType.BigInt,8),
					new SqlParameter("@Adress", SqlDbType.VarChar,100),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@Ver", SqlDbType.Char,4),
					new SqlParameter("@UserID", SqlDbType.Char,10),
					new SqlParameter("@CompanyID", SqlDbType.Char,4)};
                parameters[0].Value = model.AP_ID;
                parameters[1].Value = model.PublishDate;
                parameters[2].Value = model.Street;
                parameters[3].Value = model.Community;
                parameters[4].Value = model.AC_ID;
                parameters[5].Value = model.Adress;
                parameters[6].Value = model.AddTime;
                parameters[7].Value = model.Ver;
                parameters[8].Value = model.UserID;
                parameters[9].Value = model.CompanyID;

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

        public Message Delete(string UserID, string CompanyID)
        {
            Message jsonMessage;
            bool resultB = false;
            string reStr = "";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ADUser ");
            strSql.Append(" where UserID=@UserID and CompanyID=@CompanyID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Char,10),
					new SqlParameter("@CompanyID", SqlDbType.Char,10)};
            parameters[0].Value = UserID;
            parameters[1].Value = CompanyID;

            resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;
            if (!resultB) { reStr = "删除失败！"; } else { reStr = "删除成功！"; }
            jsonMessage = new Message()
            {
                Result = resultB,
                TxtMessage = reStr
            };
            return jsonMessage;
        }


        /// <summary>
        /// 更新发布数据
        /// </summary>
        /// <param name="AC_Id"></param>
        /// <returns></returns>
        public Message UpadteAdUserStatus(View_AdUser model)
        {
            Message jsonMessage;
            bool resultB = false;
            string reStr = "";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ADUser set ");
            strSql.Append(" AP_ID=@AP_ID,");
            strSql.Append(" PublishDate=@PublishDate,");
            strSql.Append(" AC_ID=@AC_ID ");
            strSql.Append(" where UserID=@UserID and CompanyID=@CompanyID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Char,10),
					new SqlParameter("@CompanyID", SqlDbType.Char,4),
                    new SqlParameter("@AP_ID", SqlDbType.BigInt,8),
                    new SqlParameter("@PublishDate", SqlDbType.DateTime),
                    new SqlParameter("@AC_ID", SqlDbType.BigInt,8),
                                        };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.CompanyID;
            parameters[2].Value = model.AP_ID;
            parameters[3].Value = model.PublishDate;
            parameters[4].Value = model.AC_ID;

            resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;

            if (!resultB) { reStr = "用户发布失败！"; } else { reStr = "用户发布成功！"; }
            jsonMessage = new Message()
            {
                Result = resultB,
                TxtMessage = reStr
            };
            return jsonMessage;
        }


        /// <summary>
        /// 更新发布数据
        /// </summary>
        /// <param name="AC_Id"></param>
        /// <returns></returns>
        public Message UpadteAdUserStatusGroup(View_AdUser model, string userIDArray)
        {
            Message jsonMessage;
            bool resultB = false;
            string reStr = "";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ADUser set ");
            strSql.Append(" AP_ID="+model.AP_ID);
            strSql.Append(" ,PublishDate='" + model.PublishDate );
            strSql.Append(" ',AC_ID=" + model.AC_ID);
            strSql.Append(" where UserID in(" + userIDArray + ")");

            resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString()) > 0;

            if (!resultB) { reStr = "用户发布失败！"; } else { reStr = "用户发布成功！"; }
            jsonMessage = new Message()
            {
                Result = resultB,
                TxtMessage = reStr
            };
            return jsonMessage;
        }

        public List<ADUserSC> getListSC()
        {
            List<ADUserSC> listSC = new List<ADUserSC>();
            string sqlStr = "select street,community,COUNT(*) Num from ADUser group by Street,Community;";
            SqlDataReader sqlRead = SQLHelper.ExecuteReader(conString, CommandType.Text, sqlStr.ToString());
            while(sqlRead.Read()){
                ADUserSC adSC = new ADUserSC();
                adSC.Street = sqlRead["street"].ToString();
                adSC.Community = sqlRead["community"].ToString();
                adSC.Num = int.Parse(sqlRead["Num"].ToString());
                listSC.Add(adSC);
            }
            sqlRead.Close();
            return listSC;
        }



        //显示已经排除添加过广告屏用户
        public List<View_UserInfo> getUserListShow(string CompanyID)
        {
            List<View_UserInfo> listSC = new List<View_UserInfo>();
            string sqlStr = "select * from View_UserInfoADD where UserID+CompanyID not in (select UserID+CompanyID from ADUser where CompanyID='" + CompanyID + "') and CompanyID ='" + CompanyID+"'";
            SqlDataReader sqlRead = SQLHelper.ExecuteReader(conString, CommandType.Text, sqlStr.ToString());
            while (sqlRead.Read())
            {
                View_UserInfo adSC = new View_UserInfo();
                adSC.CompanyID = sqlRead["CompanyID"].ToString();
                adSC.UserID = sqlRead["UserID"].ToString();
                adSC.Phone =  sqlRead["Phone"].ToString();
                adSC.Street = sqlRead["Street"].ToString();
                adSC.Community = sqlRead["Community"].ToString();
                adSC.Door = sqlRead["Door"].ToString();
                adSC.Address = sqlRead["Address"].ToString();
                adSC.State = char.Parse(sqlRead["State"].ToString());
                adSC.CommunityName = sqlRead["CommunityName"].ToString();
                adSC.StreetName = sqlRead["StreetName"].ToString();
                adSC.MeterNo = sqlRead["MeterNo"].ToString();
                listSC.Add(adSC);
            }
            sqlRead.Close();
            return listSC;
        }
 
    }
}
