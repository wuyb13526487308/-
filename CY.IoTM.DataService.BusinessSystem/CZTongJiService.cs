using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IoTM.Common.Business;
using CY.IoTM.MongoDataHelper;
using System.Data;
using System.Data.SqlClient;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
namespace CY.IoTM.DataService.Business
{
    /// <summary>
    /// 点火服务执行类
    /// </summary>
    public class CZTongJiService : ICZTongJi
    {

        public List<View_UserMeter> GetList(string where)
        {
            CommonSearch<View_ChongZhi> InfoSearchView_ChongZhi = new CommonSearch<View_ChongZhi>();
            SearchCondition View_ChongZhi = new SearchCondition() { TBName = "View_ChongZhi", TFieldKey = "AID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "TopUpDate DESC", TWhere = Where };
            List<View_ChongZhi> lstView_ChongZhi = InfoSearchView_ChongZhi.GetList(ref View_ChongZhi, context);


            sCondition = new SearchCondition() { TBName = "View_UserMeter", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "UserID ASC", TWhere = Where };
            List<View_UserMeter> listView = InfoSearchView.GetList(ref sCondition, context);
                                
        }
    }
}
