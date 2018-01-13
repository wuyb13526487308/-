using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CY.IotM.Common;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 设置报警参数接口
    /// </summary>
    [ServiceContract]
    public interface ISetAlarm
    {
        #region 设置报警参数增删改
        [OperationContract]
        Message Add(IoT_SetAlarm info,List<IoT_AlarmMeter> meterList);
        [OperationContract]
        Message Edit(IoT_SetAlarm info);
        [OperationContract]
        Message Delete(IoT_SetAlarm info);
        [OperationContract]
        Message UnSetParamter(IoT_SetAlarm info);

        [OperationContract]
        Message AddSetAlarmArea(IoT_SetAlarm info, List<String> communityList);
        [OperationContract]
        Message AddSetAlarmAll(IoT_SetAlarm info);
        [OperationContract]
        void UpdateMeterAlarmPar(string meterNo, IoT_SetAlarm para);
        #endregion
        [OperationContract]
        Iot_MeterAlarmPara GetMeterAlarmPara(string meterNo);
    }
}
