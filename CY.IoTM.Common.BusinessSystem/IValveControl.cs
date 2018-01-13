using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using CY.IotM.Common;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 阀门控制操作接口
    /// </summary>
    [ServiceContract]
    public interface IValveControl
    {
        /// <summary>
        /// 开阀操作
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string TurnOn(IoT_Meter meter, string reason, string oper);
        /// <summary>
        /// 关阀操作
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string TurnOff(IoT_Meter meter, string reason, string oper);
        /// <summary>
        /// 撤销开/关阀操作
        /// </summary>
        /// <taskID>阀门控制任务id</taskID>
        /// <context>撤销原因</context>
        /// <returns></returns>
        [OperationContract]
        string Undo(string taskID, string context);

        #region 阀门控制记录增删改
        [OperationContract]
        Message Add(IoT_ValveControl info);
        [OperationContract]
        Message Edit(IoT_ValveControl info);
        [OperationContract]
        Message Delete(IoT_ValveControl info);
        #endregion

    }

    public enum ValveControlType : byte
    {
        //控制类型：0 关阀 1 开发
        关阀 =0,
        开阀 =1,
    }
}
