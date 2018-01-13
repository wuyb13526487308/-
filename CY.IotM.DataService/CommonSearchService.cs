using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IotM.Common;
using System.Data.Linq;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using CY.IotM.Common.Tool;
using System.IO;
using System.ServiceModel;

namespace CY.IotM.DataService
{
       [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
ConcurrencyMode = ConcurrencyMode.Single)]
    public class CommonSearchService<T> : ICommonSearch<T> where T : class,new()
    {


        public List<T> getListBySearchCondition(ref SearchCondition sCondition)
        {
            List<T> list = new List<T>();
            string sqlText = "SP_CommonPageSearch";
            try
            {

                SqlParameter[] parms = new SqlParameter[] { 
                    new SqlParameter("tbname", sCondition.TBName),
                    new SqlParameter( "FieldKey",  sCondition.TFieldKey),
                    new SqlParameter("FieldShow", sCondition.TFieldShow),
                    new SqlParameter( "Where", sCondition.TWhere),
                    new SqlParameter("TotalCount",  sCondition.TTotalCount),
                    new SqlParameter( "PageCount",  sCondition.TPageCount),
                    new SqlParameter( "PageCurrent",  sCondition.TPageCurrent),
                    new SqlParameter("PageSize",  sCondition.TPageSize),
                     new SqlParameter("FieldOrder",  sCondition.TFieldOrder)
                };
                parms[4].Direction = ParameterDirection.InputOutput;
                parms[5].Direction = ParameterDirection.InputOutput;
                using (DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.SchuleConnection, CommandType.StoredProcedure, sqlText, parms))
                {
                    if (ds.Tables.Count > 0)
                    {
                        list = ConvertHelper.GetList<T>(ds.Tables[0]);
                    }

                }
                sCondition.TPageCount = (int)parms[5].Value;
                sCondition.TTotalCount = (int)parms[4].Value;
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                //加载数据错误。//在此需要记录系统日志。
            }
            return list;
        }
        public int Test()
        {
            return 0;
        }

    }
}
