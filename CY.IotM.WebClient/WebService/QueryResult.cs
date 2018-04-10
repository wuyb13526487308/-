using CY.IoTM.Common.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CY.IotM.WebClient.WebService
{
    public class QueryResult
    {
        /// <summary>
        /// 总记录条数
        /// </summary>
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<IotUser> Users { get; set; }
    }

    public class IotUser
    {
        public IoT_User User { get; set; }
        public IoT_Meter Meter { get; set; }
    }
}