using CY.IoTM.Common.Classes;
using CY.IoTM.Common.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 获取采集端监视信息：CPU，内存等使用情况
    /// </summary>
    [ServiceContract]
    public interface IGetMonitorInfo
    {
        /// <summary>
        /// 获取监视信息
        /// </summary>
        /// <param name="dcsID"></param>
        /// <returns></returns>
        [OperationContract]
        DataArge GetMonitorInfo(string dcsID);

        /// <summary>
        /// 获取MeterService服务器上日志信息
        /// </summary>
        /// <param name="cjdID"></param>
        /// <param name="mac"></param>
        /// <param name="date"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="lType"></param>
        /// <returns></returns>
        [OperationContract]
        LogCollection GetDCSLog(string cjdID, string mac, DateTime date, int pageNum, int pageSize, ReadLogDataType lType);
        
    }
}
