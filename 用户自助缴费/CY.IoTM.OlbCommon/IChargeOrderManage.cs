using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CY.IoTM.OlbCommon
{
    /// <summary>
    /// 充值订单接口
    /// </summary>
    public interface IChargeOrderManage
    {

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="Olb_User"></param>
        /// <returns></returns>
        Message Add(Olb_ChargeOrder order);
        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="Olb_User"></param>
        /// <returns></returns>
        Message Edit(Olb_ChargeOrder order);


        /// <summary>
        /// 获取充值订单
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Olb_ChargeOrder GetChargeOrderById(string id);

    }
}
