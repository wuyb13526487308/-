using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using CY.IotM.Common;
namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 点火操作接口
    /// </summary>
    [ServiceContract]
    public interface IDianHuo
    {
        /// <summary>
        /// 点火操作
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string Do(List<IoT_Meter> meters);
        /// <summary>
        /// 撤销点火
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string Undo(IoT_Meter meter);
        /// <summary>
        /// 根据表号查询表信息
        /// </summary>
        /// <param name="meterNo"></param>
        /// <returns></returns>
        [OperationContract]
        IoT_Meter QueryMeter(string meterNo);

        /// <summary>
        /// 点火
        /// </summary>
        /// <param name="meterNoList"></param>
        /// <param name="priceId"></param>
        /// <param name="companyId"></param>
        /// <param name="meterType">表启用类型：00 气量表 01 金额表</param>
        /// <returns></returns>
        [OperationContract]
        Message DianHuo(List<String> meterNoList,string meterType, Int64 priceId, string companyId, DateTime enableDate, List<String> lstUserID, string EnableMeterOper);
    }
}
