using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CY.IoTM.OlbCommon
{
    /// <summary>
    /// 充值缴费管理接口
    /// </summary>
	public interface IPaymentManage  {

        /// <summary>
        /// 添加缴费记录
        /// </summary>
        /// <param name="Olb_PaymentRecord"></param>
        /// <returns></returns>
		 Message AddPaymentRecord(Olb_PaymentRecord Olb_PaymentRecord);
        /// <summary>
        /// 获取缴费记录
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="account"></param>
        /// <returns></returns>
         List<Olb_PaymentRecord> GetPaymentRecord(DateTime startTime, DateTime endTime,string account);

		
	}

}