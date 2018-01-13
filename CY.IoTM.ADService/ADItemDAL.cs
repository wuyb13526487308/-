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
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“ADItemDAL”。
    public class ADItemDAL : IADItemDAL
    {
        string conString = System.Configuration.ConfigurationManager.ConnectionStrings[System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"]].ConnectionString;

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public Message Add(ADItem model)
        {
            Message jsonMessage;
            bool resultB = false;
            string reStr = "";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ADItem(");
            strSql.Append("AC_ID,OrderID,FileName,BDate,EDate,Length,StoreName,IsDisplay,StorePath,FileLength)");
            strSql.Append(" values (");
            strSql.Append("@AC_ID,@OrderID,@FileName,@BDate,@EDate,@Length,@StoreName,@IsDisplay,@StorePath,@FileLength)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AC_ID", SqlDbType.BigInt,8),
					new SqlParameter("@OrderID", SqlDbType.SmallInt,2),
					new SqlParameter("@FileName", SqlDbType.VarChar,20),
					new SqlParameter("@BDate", SqlDbType.DateTime),
					new SqlParameter("@EDate", SqlDbType.DateTime),
					new SqlParameter("@Length", SqlDbType.SmallInt,2),
					new SqlParameter("@StoreName", SqlDbType.Char,20),
					new SqlParameter("@IsDisplay", SqlDbType.Bit,1),
					new SqlParameter("@StorePath", SqlDbType.VarChar),
					new SqlParameter("@FileLength", SqlDbType.Int,4)};
            parameters[0].Value = model.AC_ID;
            parameters[1].Value = model.OrderID;
            parameters[2].Value = model.FileName;
            parameters[3].Value = model.BDate;
            parameters[4].Value = model.EDate;
            parameters[5].Value = model.Length;
            parameters[6].Value = model.StoreName;
            parameters[7].Value = model.IsDisplay;
            parameters[8].Value = model.StorePath;
            parameters[9].Value = model.FileLength;

            //resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;
            int IDNum = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) ;
            if (IDNum > 0) resultB = true;
            if (!resultB) { reStr = "添加失败!"; }
            else{
                reStr = "";
            }

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
        public Message Edit(ADItem model)
        {
            Message jsonMessage;
            bool resultB = false;
            string reStr = "";
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update ADItem set ");
                strSql.Append("AC_ID=@AC_ID,");
                strSql.Append("OrderID=@OrderID,");
                strSql.Append("FileName=@FileName,");
                strSql.Append("BDate=@BDate,");
                strSql.Append("EDate=@EDate,");
                strSql.Append("Length=@Length,");
                strSql.Append("StoreName=@StoreName,");
                strSql.Append("IsDisplay=@IsDisplay,");
                strSql.Append("StorePath=@StorePath,");
                strSql.Append("FileLength=@FileLength");
                strSql.Append(" where AI_ID=@AI_ID");
                SqlParameter[] parameters = {
					new SqlParameter("@AC_ID", SqlDbType.BigInt,8),
					new SqlParameter("@OrderID", SqlDbType.SmallInt,2),
					new SqlParameter("@FileName", SqlDbType.VarChar,20),
					new SqlParameter("@BDate", SqlDbType.DateTime),
					new SqlParameter("@EDate", SqlDbType.DateTime),
					new SqlParameter("@Length", SqlDbType.SmallInt,2),
					new SqlParameter("@StoreName", SqlDbType.Char,20),
					new SqlParameter("@IsDisplay", SqlDbType.Bit,1),
					new SqlParameter("@StorePath", SqlDbType.VarChar),
					new SqlParameter("@FileLength", SqlDbType.Int,4),
					new SqlParameter("@AI_ID", SqlDbType.BigInt,8)};
                parameters[0].Value = model.AC_ID;
                parameters[1].Value = model.OrderID;
                parameters[2].Value = model.FileName;
                parameters[3].Value = model.BDate;
                parameters[4].Value = model.EDate;
                parameters[5].Value = model.Length;
                parameters[6].Value = model.StoreName;
                parameters[7].Value = model.IsDisplay;
                parameters[8].Value = model.StorePath;
                parameters[9].Value = model.FileLength;
                parameters[10].Value = model.AI_ID;

                resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;
            }
            catch (Exception e)
            {
                reStr = e.ToString();
            }
            if (!resultB) { reStr = "修改失败！"; }
            else
            {
                //反回Json字段串:
                reStr = "{\"AI_ID\":\"" + model.AI_ID + "\",\"IsDisplay\":\"" + model.IsDisplay + "\",\"Length\":\"" + model.Length + "\",\"EDate\":\"" + model.EDate + "\",\"BDate\":\"" + model.BDate + "\",\"AC_ID\":\"" + model.AC_ID + "\",\"OrderID\":\"" + model.OrderID + "\",\"FileName\":\"" + model.FileName + "\"}";
            }
            jsonMessage = new Message()
            {
                Result = resultB,
                TxtMessage = reStr
            };
            return jsonMessage;
        }

        public Message Delete(long AI_ID)
        {
            Message jsonMessage;
            bool resultB = false;
            string reStr = "";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ADItem ");
            strSql.Append(" where AI_ID=@AI_ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@AI_ID", SqlDbType.BigInt)};
            parameters[0].Value = AI_ID;
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
        /// 取得莫一企业当天发布的图片数量;
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public int userPuFileNum(string companyID) {
            int uPFN = 0;
            string strSql = "select  top 1  substring(storeName,11,5) from ADItem where [StoreName] like '%" + companyID + string.Format("{0:yyMMdd}", DateTime.Now) + "%' order by ai_id desc";
            SqlDataReader infoReader = SQLHelper.ExecuteReader(conString, CommandType.Text, strSql.ToString());
            if (infoReader.Read()) {
                uPFN =int.Parse( infoReader[0].ToString());
            }
            infoReader.Close();
            return uPFN;
        }

        /// <summary>
        /// 排序上移
        /// </summary>
        /// <param name="AI_ID"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public Message upOrder(ADItem info)
        {
            Message jsonMessage;
            bool resultB = false;
            string reStr = ""; long aiID = -1; int upOrderID = -1;
            //查找比当前OrderID还要小的值是否存在;
            string sqlStr = "select max(ai_id) ai_id,max(orderID) orderID from ADItem where orderID<" + info.OrderID + " and AC_ID=" + info.AC_ID;
            SqlDataReader infoReader = SQLHelper.ExecuteReader(conString, CommandType.Text, sqlStr.ToString());
            
            if (infoReader.Read() && infoReader["ai_id"].ToString() != "")
            {
                
                aiID = long.Parse(infoReader["ai_id"].ToString());
                upOrderID = int.Parse(infoReader["orderID"].ToString());
            }
            infoReader.Close();

            //当没有查到数据时,证明为第一行,不能上移;
            if (aiID == -1) {
                reStr = "已排第一行,不需要上移!";
            }
            else //进行排名修改,即把两条记录的排序编号进行调换;
            {
                string sqlUpOrderID = "update ADItem set orderID = " + upOrderID + " where Ai_ID=" + info.AI_ID + ";";
                sqlUpOrderID += "update ADItem set orderID = " + info.OrderID + " where Ai_ID=" + aiID + ";";
                resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, sqlUpOrderID.ToString()) > 0;
            }
            if (resultB) {reStr = "上移成功！"; } 
            jsonMessage = new Message()
            {
                Result = resultB,
                TxtMessage = reStr
            };
            return jsonMessage;
        }


        /// <summary>
        /// 排序下移
        /// </summary>
        /// <param name="AI_ID"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public Message downOrder(ADItem info)
        {
            Message jsonMessage;
            bool resultB = false;
            string reStr = ""; long aiID = -1; int upOrderID = -1;
            //查找比当前OrderID还要小的值是否存在;
            string sqlStr = "select min(ai_id) ai_id,min(orderID) orderID from ADItem where orderID>" + info.OrderID +" and AC_ID=" + info.AC_ID;
            SqlDataReader infoReader = SQLHelper.ExecuteReader(conString, CommandType.Text, sqlStr.ToString());
            if (infoReader.Read() && infoReader["ai_id"].ToString() != "")
            {
                aiID = long.Parse(infoReader["ai_id"].ToString());
                upOrderID = int.Parse(infoReader["orderID"].ToString());
            }
            infoReader.Close();

            //当没有查到数据时,证明为最后一行,不能下移;
            if (aiID == -1)
            {
                reStr = "已排最后一行,不需要下移!";
            }
            else //进行排名修改,即把两条记录的排序编号进行调换;
            {
                string sqlUpOrderID = "update ADItem set orderID = " + upOrderID + " where Ai_ID=" + info.AI_ID + ";";
                sqlUpOrderID += "update ADItem set orderID = " + info.OrderID + " where Ai_ID=" + aiID + ";";
                resultB = SQLHelper.ExecuteNonQuery(conString, CommandType.Text, sqlUpOrderID.ToString()) > 0;
            }
            if (resultB) { reStr = "上移成功！"; } 

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
            strSql.Append("delete from ADItem ");
            strSql.Append(" where AI_ID in (" + IDlist + ")  ");
            return SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString()) > 0;
        }

        /// <summary>
        /// 更新数据状态
        /// </summary>
        /// <param name="AC_Id"></param>
        /// <returns></returns>
        public bool UpadteAdStatus(int Ai_Id, int IsDisplay)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ADItem set ");
            strSql.Append(" IsDisplay=@IsDisplay");
            strSql.Append(" where Ai_ID = @Ai_ID  ");
            SqlParameter[] parameters = {
                        new SqlParameter("@IsDisplay", SqlDbType.BigInt,20),
					    new SqlParameter("@Ai_ID", SqlDbType.BigInt,8)};
            parameters[0].Value = IsDisplay;
            parameters[1].Value = Ai_Id;
            
            return SQLHelper.ExecuteNonQuery(conString, CommandType.Text, strSql.ToString(), parameters) > 0;

        }

        //图片预览
        public List<ADItem> getListShow(long AC_ID)
        {
            List<ADItem> listSC = new List<ADItem>();
            string sqlStr = "select AI_ID,FileName,OrderID,Length,FileLength,StoreName from ADItem where IsDisplay =1 and BDate <=getdate() and getdate()<=EDate and AC_ID =" + AC_ID;
            SqlDataReader sqlRead = SQLHelper.ExecuteReader(conString, CommandType.Text, sqlStr.ToString());
            while (sqlRead.Read())
            {
                ADItem adSC = new ADItem();
                adSC.AI_ID = long.Parse(sqlRead["AI_ID"].ToString());
                adSC.Length = short.Parse(sqlRead["Length"].ToString());
                adSC.OrderID = short.Parse(sqlRead["OrderID"].ToString());
                adSC.FileName = sqlRead["FileName"].ToString();
                adSC.StoreName = sqlRead["StoreName"].ToString();
                adSC.FileLength = int.Parse(sqlRead["FileLength"].ToString());
                listSC.Add(adSC);
            }
            sqlRead.Close();
            return listSC;
        }
 
    }
}
