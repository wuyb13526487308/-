using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CY.IoTM.OlbCommon
{
    /// <summary>
    /// ��ֵ�ɷѹ���ӿ�
    /// </summary>
	public interface IPaymentManage  {

        /// <summary>
        /// ��ӽɷѼ�¼
        /// </summary>
        /// <param name="Olb_PaymentRecord"></param>
        /// <returns></returns>
		 Message AddPaymentRecord(Olb_PaymentRecord Olb_PaymentRecord);
        /// <summary>
        /// ��ȡ�ɷѼ�¼
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="account"></param>
        /// <returns></returns>
         List<Olb_PaymentRecord> GetPaymentRecord(DateTime startTime, DateTime endTime,string account);

		
	}

}