using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CY.IotM.Common;

namespace CY.IoTM.Common.Business
{  /// <summary>
    /// 设置上传周期接口
    /// </summary>
    [ServiceContract]
    public interface IHuanBiao
    {
        /// <summary>
        /// 获取表用户
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="MeterNo"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        [OperationContract]
        View_UserMeter getView_UserMeterList(string UserID, string MeterNo, string CompanyID);


        /// <summary>
        /// 添加全部上传周期
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meterList"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [OperationContract]
        Message AddShenQing(IoT_ChangeMeter info);
        /// <summary>
        /// 修改换表申请
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [OperationContract]
        Message Edit(IoT_ChangeMeter info);
        /// <summary>
        /// 取消换表
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        [OperationContract]
        Message revoke(string ID, string CompanyID);


        /// <summary>
        /// 更新设置上传周期任务状态
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [OperationContract]
        Message Dengji(IoT_ChangeMeter info);

    }
}
