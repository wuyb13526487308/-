using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CY.IotM.Common;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 调价计划接口
    /// </summary>
    [ServiceContract]
    public interface IPricingManage
    {
        #region 调价计划增删改
        [OperationContract]
        Message Add(IoT_Pricing info, List<IoT_PricingMeter> meterList);
        [OperationContract]
        Message Edit(IoT_Pricing info);
        [OperationContract]
        Message Delete(IoT_Pricing info);


        [OperationContract]
        Message AddPricingArea(IoT_Pricing info, List<String> communityList);

        [OperationContract]
        Message AddPricingAll(IoT_Pricing info);

        [OperationContract]
        Message UnSetParamter(IoT_Pricing info);
        #endregion
    }
}
