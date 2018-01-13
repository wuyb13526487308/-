using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IoTM.Common.Business;
using CY.IoTM.MongoDataHelper;

namespace CY.IoTM.DataService.Business
{
    /// <summary>
    /// 设置表参数类
    /// </summary>
    class SetMeterParameter
    {
        #region 设置报警参数

        /// <summary>
        /// 添加报警参数设置任务
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meterList"></param>
        /// <returns></returns>
        public string SetWarinningPararmeter(IoT_SetAlarm info, List<IoT_AlarmMeter> meterList)
        {
            string result = "";
            M_SetParameterService sp = new M_SetParameterService();
            foreach (IoT_AlarmMeter alarm in meterList)
            {
                result = sp.SetWariningParameter(info, alarm);
            }

            return result;
        }

        /// <summary>
        /// 撤销参数设置
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public string UnSetParamter(string taskID)
        {
            M_SetParameterService sp = new M_SetParameterService();
            return sp.UnSetParameter(taskID);
        }

        #endregion

        #region 设置上传周期
        /// <summary>
        /// 设置上传周期（通讯层）
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meterList"></param>
        /// <returns></returns>
        public string SetUploadCycle(IoT_SetUploadCycle info, List<IoT_UploadCycleMeter> meterList)
        {
            string result = "";
            M_SetParameterService sp = new M_SetParameterService();
            foreach (IoT_UploadCycleMeter m in meterList)
            {
                result = sp.SetUploadCycle(info, m);
            }
            return result;
        }

        #endregion

        /// <summary>
        /// 设置结算日
        /// </summary>
        /// <param name="settlementDay"></param>
        /// <param name="meters"></param>
        /// <returns></returns>
        public string SetSettlementDay(IoT_SetSettlementDay settlementDay,List<IoT_SettlementDayMeter> meters)
        {
            string result = "";
            M_SetParameterService sp = new M_SetParameterService();
            foreach (IoT_SettlementDayMeter m in meters)
            {
                result = sp.SetSettlementDay(settlementDay, m);
            }
            return result;
        }

        public string SetPricingPlan(IoT_Pricing info, List<IoT_PricingMeter> meterList)
        {
            string result = "";
            M_SetParameterService sp = new M_SetParameterService();

            //整理调价参数
            foreach (IoT_PricingMeter m in meterList)
            {
                PricingPlan pp = new PricingPlan()
                {
                    IsUsedLadder = (bool)info.IsUsed,
                    Ladder = (int)info.Ladder,
                    MeterType = info.MeterType,
                    SettlementType = info.SettlementType,
                    Price1 = (decimal)info.Price1,
                    Price2 = (decimal)info.Price2,
                    Price3 = (decimal)info.Price3,
                    Price4 = (decimal)info.Price4,
                    Price5 = (decimal)info.Price5,
                    Gas1 = (decimal)info.Gas1,
                    Gas2 = (decimal)info.Gas2,
                    Gas3 = (decimal)info.Gas3,
                    Gas4 = (decimal)info.Gas4,
                    UseDate = ((DateTime)info.UseDate).ToString("yyyy-MM-dd")
                };
                pp.MeterNo = m.MeterNo.Trim ();
                result = sp.SetPricingPlan(pp, m);
                //记录调价计划

            }
            return result;
        }

    }
}
