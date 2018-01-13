using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 充值接口类
    /// </summary>
    [ServiceContract]    
    public interface IMeterTopUp
    {
        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="meterNo">表号</param>
        /// <param name="money">充值金额</param>
        /// <param name="topUpType">充值方式</param>
        /// <param name="oper">操作员</param>
        /// <param name="orgCode">充值机构代码</param>
        /// <returns></returns>
        [OperationContract]
        string Topup(string meterNo, decimal money, TopUpType topUpType, string oper, string orgCode, IoT_MeterTopUp topUp);
        /// <summary>
        /// 撤销充值
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="reason"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        [OperationContract]
        string UnTopUp(string taskID,string reason,string oper);

    }

    /// <summary>
    /// 充值类型
    /// </summary>
    [DataContract]
    public enum TopUpType
    {
        //0  本地营业厅 1 接口  2 本地网站 3 换表补充
        [EnumMember]
        本地营业厅 = 0,
        [EnumMember]
        接口 = 1,
        [EnumMember]
        本地网站 = 2,
        [EnumMember]
        换表补充 = 3,
    }
}
