using CY.IotM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 充值管理
    /// </summary>
    [ServiceContract]
   public interface IChongzhiManage
    {

       #region 用户表具增删改
       [OperationContract]
        Message Add(IoT_MeterTopUp info);

       [OperationContract]
       Message UPD(IoT_MeterTopUp info);


        [OperationContract]
        string PrintTicket(string id);


        ///// <summary>
        ///// 查询Meter中的用户用量讯息
        ///// </summary>
        // /// <param name="meterNo"></param>
        ///// <returns></returns>
        // [OperationContract]
        //IoT_Meter QuaryMeter(string Where);

        // /// <summary>
        // /// 查询IoT_MeterDataHistory中的用户历史用量讯息
        // /// </summary>
        // /// <param name="meterNo"></param>
        // /// <returns></returns>
        // [OperationContract]
        // IoT_MeterDataHistory QuaryMeterDataHistory(string meterNo);

        ///// <summary>
        ///// 导出数据
        ///// </summary>
        // [OperationContract]
        //Message EXPData(string Time, string User, string Where);

        #endregion
    }
}
