using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IoTM.Common.Business;
using CY.IoTM.MongoDataHelper;

namespace CY.IoTM.DataService.Business
{
    /// <summary>
    /// 金额表气费结算类
    /// </summary>
    class Settlement
    {
        /// <summary>
        /// 进行计费汇算，本次会算值作为是否校准的依据，如需要校准，则返回true
        /// </summary>
        /// <param name="meter"></param>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public bool Calculate(Meter meter, SubmitData dataItem, SubmitResult returnResult)
        {
            #region
            /*
            //计算当前阶段用气量
            decimal JieDuanYongQiLiang = dataItem.LJGas - meter.LastTotal;// dataItem.LastLJGas;
            decimal[] prices = new decimal[5];
            prices[0] = meter.Price1;
            prices[1] = meter.Price2;
            prices[2] = meter.Price3;
            prices[3] = meter.Price4;
            prices[4] = meter.Price5;
            decimal[] gas = new decimal[4];
            gas[0] = meter.Gas1;
            gas[1] = meter.Gas2;
            gas[2] = meter.Gas3;
            gas[3] = meter.Gas4;
            decimal currentPirce = meter.Price1;
            decimal currentGas = meter.Gas1;
            decimal amount = meter.LastSettlementAmount;

            decimal[] fees = new decimal[5];
            decimal[] usedGas = new decimal[5];
            if (meter.IsUsedLadder)
            {
                //启用了阶梯价
                int iLadder = 1;
                while (iLadder < meter.Ladder)
                {
                    currentPirce = prices[iLadder - 1];
                    currentGas = gas[iLadder - 1];
                    if (JieDuanYongQiLiang > currentGas)
                    {
                        //当前阶段总用气量大于当前阶梯用气量
                        fees[iLadder - 1] = currentGas * currentPirce;
                        usedGas[iLadder - 1] = currentGas;

                        amount -= fees[iLadder - 1];
                        JieDuanYongQiLiang -= currentGas;
                        iLadder++;
                    }
                    else if (JieDuanYongQiLiang > 0)
                    {
                        fees[iLadder - 1] = JieDuanYongQiLiang * currentPirce;
                        usedGas[iLadder - 1] = JieDuanYongQiLiang;

                        amount -= fees[iLadder - 1];
                        JieDuanYongQiLiang = 0;
                        iLadder++;
                        break;
                    }
                    else
                    {
                        iLadder++;
                        break;
                    }
                }
                if (JieDuanYongQiLiang > 0)
                {
                    //计算最后一个阶梯
                    currentPirce = prices[iLadder - 1];

                    fees[iLadder - 1] = JieDuanYongQiLiang * currentPirce;
                    usedGas[iLadder - 1] = JieDuanYongQiLiang;
                    amount -= fees[iLadder - 1];
                }
            }
            else
            {
                //未使用阶梯价
                amount -= JieDuanYongQiLiang * currentPirce;
                fees[0] = JieDuanYongQiLiang * currentPirce;
                usedGas[0] = JieDuanYongQiLiang;
            }
            */
            #endregion
            if (meter.BillID == null)
                meter.CreateBillID();
            decimal amount = CalculateGasFee(meter, dataItem);

            //判断结算日是否到达
            //判断是否到结算日,该处的功能依赖表必须传输结算日气量数据，且上报的数据必须按时间顺序传输。
            if (meter.Jiange(meter.GetSettlementTimePoint()) >= 0)
            {
                decimal[] gas = new decimal[5];
                gas[0] = meter.Gas1;
                gas[1] = meter.Gas2;
                gas[2] = meter.Gas3;
                gas[3] = meter.Gas4;
                gas[4] =-1;

                //记录结算记录
                BillRecord record = new BillRecord() { UserID = meter.UserID, MeterNo = meter.Mac };
                record.RecordDate = dataItem.ReadDate.ToString("yyyy-MM-dd HH:mm:ss");
                record.BillRecordType = BillRecordType.结算点记录;
                record.BillID = meter.BillID;
                record.Ladder = meter.CurrentLadder;

                if (meter.NextSettlementPointGas != -1 && dataItem.LJGas < meter.NextSettlementPointGas)
                {
                    record.EndPoint = dataItem.LJGas;
                    if (meter.IsPricing)
                    {
                        record.BeginPoint = meter.TiaoJiaPointGas;
                    }
                    else
                    {
                        record.BeginPoint = meter.NextSettlementPointGas - gas[meter.CurrentLadder - 1];
                    }
                    record.Gas = record.EndPoint - record.BeginPoint;
                    record.Price = meter.CurrentPrice;
                    record.Amount = record.Gas * record.Price;
                    record.Balance = meter.CurrentBalance;
                }
                else if (meter.NextSettlementPointGas != -1 && dataItem.LJGas == meter.NextSettlementPointGas)
                {
                    record.EndPoint = meter.NextSettlementPointGas;
                    if (meter.IsPricing)
                    {
                        record.BeginPoint = meter.TiaoJiaPointGas;
                    }
                    else
                    {
                        record.BeginPoint = meter.NextSettlementPointGas - gas[meter.CurrentLadder - 1];
                    } 
                    record.Gas = record.EndPoint - record.BeginPoint;
                    record.Price = meter.CurrentPrice;
                    record.Amount = record.Gas * record.Price;
                    record.Balance = meter.CurrentBalance;
                }
                else
                {
                    //最后一个阶梯
                    record.EndPoint = dataItem.LJGas;
                    if (meter.IsPricing)
                    {
                        record.BeginPoint = meter.TiaoJiaPointGas;
                    }
                    else
                    {
                        record.BeginPoint = meter.LastTotal;
                        for (int i = 0; i < meter.Ladder; i++)
                            record.BeginPoint += gas[i];
                    }
                    record.Gas = record.EndPoint - record.BeginPoint;
                    record.Price = meter.CurrentPrice;
                    record.Amount = record.Gas * record.Price;
                    record.Balance = meter.CurrentBalance;

                }
                new M_BillRecordService().AddBillRecord(record);
                meter.IsPricing = false;
                meter.CreateBillID();
                //创建新的账单
                Bill bill = new Bill() { BillID = meter.BillID,UserID = meter.UserID,BeginDate = DateTime.Now.ToString ("yyyy-MM-dd HH:mm:ss")};
                new M_BillRecordService().AddBill(bill);

                meter.SetNextSettlementDateTime(); 
                returnResult.IsReLoadMeter = true;
                //更新结算数据
                meter.TotalAmount = dataItem.LJGas;
                meter.LastSettlementAmount = amount;
                meter.LastTotal = dataItem.LJGas;
                meter.CurrentLadder = 1;
                meter.CurrentPrice = meter.Price1;

                if (meter.IsUsedLadder && meter.CurrentLadder < meter.Ladder)
                {
                    //设置第1个节点的结算点气量
                    meter.NextSettlementPointGas = meter.LastTotal + meter.Gas1;
                }
                else
                {
                    //下一次结算点气量为无穷大
                    meter.NextSettlementPointGas = -1;
                }
                new TaskManageDA().UpdateMeter(meter);
                //TODO:在此记录阶段用气账单
            }
            //处理调价。
            TiaoJiaDowith(meter, dataItem);

            decimal syMoney = Convert.ToDecimal(dataItem.SYMoney.ToString("0.00"));
            //在此可以定义系统参数，修正精度，默认差值大于1角做修正处理
            if (meter.IsDianHuo && Math.Abs(syMoney - amount) > 0.1m)
            {
                //修正时间、修正原因、 表号、表上传的当前余额、 上次结算日累计气量（4字节），上次结算日剩余金额（4字节），累计购入金额（4字节），当前结算日
                Console.WriteLine("需要修正表计量数据,表余额：{0} ,后台系统余额:{1}", dataItem.SYMoney, amount);
                CorrectRecord correct = new CorrectRecord();
                correct.MeterBalance = syMoney;
                correct.MeterNo = meter.Mac;
                correct.MeterLastSettleMentDayLJGas = dataItem.LastLJGas;
                correct.MeterLJGas = dataItem.LJGas;
                correct.MeterLJMoney = dataItem.LJMoney;
                correct.MeterReadDate = dataItem.ReadDate.ToString("yyyy-MM-dd HH:mm:ss");

                correct.SettlementDay = (byte)meter.SettlementDay;
                correct.CorrectDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                correct.CorrectReason = string.Format("表计量数据和后台不一致，表余额误差超出允许范围，表余额：{0} ,后台余额:{1} 差额：{2}", dataItem.SYMoney, amount, Math.Abs(syMoney - amount));
                correct.TotalTopUp = meter.TotalTopUp;
                correct.SettlementBalance = amount;
                correct.LastSettlementDayLJGas = meter.LastTotal;
                dataItem.SYMoney = amount;
                Task _task;
                string result = new CorrectDA().AddCorrentTask(correct, out _task);//记录修正数据和添加修正任务
                if (result == "")
                {
                    returnResult.IsCalibration = true;
                    returnResult.Calibrations.Add(_task);
                }

            }
            return returnResult.IsCalibration;
        }
        /// <summary>
        /// 计费计算和调价处理
        /// </summary>
        /// <param name="meter"></param>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        private decimal CalculateGasFee(Meter meter, SubmitData dataItem)
        {
            if (meter.Ladder <= 0)
                meter.Ladder = 1;
            //仅计算气费
            CalculateGasFee(meter, dataItem.LJGas,dataItem.ReadDate.ToString ("yyyy-MM-dd HH:mm:ss"));
            string result = new TaskManageDA().UpdateMeter(meter) ;
            if (result != "")
            {
                Console.WriteLine("更新Meter对象数据到Mongodb失败,原因：" + result);
            }
            return meter.CurrentBalance;
        }

        /// <summary>
        /// 调价处理，如存在调价则处理
        /// </summary>
        private void TiaoJiaDowith(Meter meter, SubmitData dataItem)
        {
            //检查是否存在调价计划， 如存在并调价启动时间已到达，则将新的价格写入执行体系中（即修改价格对应的变量值），设置成功，删除调价计划数据．
            meter.PricingPlan = new PricingPlanDA().QueryPricingPlan(meter.Mac);
            if (meter.PricingPlan != null)
            {
                decimal[] prices = new decimal[5];
                decimal[] gas = new decimal[5];
                prices[0] = meter.Price1;
                prices[1] = meter.Price2;
                prices[2] = meter.Price3;
                prices[3] = meter.Price4;
                prices[4] = meter.Price5;
                gas[0] = meter.Gas1;
                gas[1] = meter.Gas2;
                gas[2] = meter.Gas3;
                gas[3] = meter.Gas4;
                gas[4] = -1;

                //存在调价计划 
                DateTime pricingDate = Convert.ToDateTime(meter.PricingPlan.UseDate);
                if (getJianGe(dataItem.ReadDate, pricingDate) >= 0)
                {
                    //结算气量（原价格到调价时）
                    decimal jieSuanLJGas = dataItem.LJGas - meter.LastTotal;
                    //设置调价点（用于后续的阶梯量记账）
                    meter.TiaoJiaPointGas = dataItem.LJGas;
                    meter.IsPricing = true;
                    //记录结算记录
                    BillRecord record = new BillRecord() { UserID = meter.UserID, MeterNo = meter.Mac };
                    record.RecordDate = dataItem.ReadDate.ToString("yyyy-MM-dd HH:mm:ss");
                    record.BillRecordType = BillRecordType.调价点记录;
                    record.BillID = meter.BillID;
                    record.Ladder = meter.CurrentLadder;
                    decimal lastPoint = meter.NextSettlementPointGas - gas[meter.CurrentLadder - 1];
                    if (meter.NextSettlementPointGas > 0)
                    {
                        //当前没有计价到最后一个阶梯
                        record.BeginPoint = lastPoint;
                        record.EndPoint = dataItem.LJGas;
                        record.Gas = record.EndPoint - record.BeginPoint;
                        record.Price = meter.CurrentPrice;
                        record.Amount = record.Gas * record.Price;
                        record.Ladder = meter.CurrentLadder;
                    }
                    else
                    {
                        //当前已处于最后一个阶梯
                        record.BeginPoint = meter.LastTotal;
                        record.EndPoint = dataItem.LJGas;
                        for (int i = 0; i < meter.Ladder; i++)
                        {
                            record.BeginPoint += gas[i];
                        }
                        record.Gas = record.EndPoint - record.BeginPoint;
                        record.Price = meter.CurrentPrice;
                        record.Amount = record.Gas * record.Price;
                        record.Ladder = meter.CurrentLadder;
                    }
                    new M_BillRecordService().AddBillRecord(record);//添加结算记录

                    //调价启用时间到
                    meter.IsUsedLadder = meter.PricingPlan.IsUsedLadder;
                    meter.Ladder = meter.PricingPlan.Ladder;
                    meter.Price1 = meter.PricingPlan.Price1;
                    meter.Gas1 = meter.PricingPlan.Gas1;
                    meter.Price2 = meter.PricingPlan.Price2;
                    meter.Gas2 = meter.PricingPlan.Gas2;
                    meter.Price3 = meter.PricingPlan.Price3;
                    meter.Gas3 = meter.PricingPlan.Gas3;
                    meter.Price4 = meter.PricingPlan.Price4;
                    meter.Gas4 = meter.PricingPlan.Gas4;
                    meter.Price5 = meter.PricingPlan.Price5;
                    prices[0] = meter.Price1;
                    prices[1] = meter.Price2;
                    prices[2] = meter.Price3;
                    prices[3] = meter.Price4;
                    prices[4] = meter.Price5;
                    gas[0] = meter.Gas1;
                    gas[1] = meter.Gas2;
                    gas[2] = meter.Gas3;
                    gas[3] = meter.Gas4;
                    gas[4] = -1;

                    //结算周期
                    meter.SettlementType = meter.PricingPlan.SettlementType;

                    //设置当前计费价格
                    if (meter.IsUsedLadder)
                    {
                        meter.NextSettlementPointGas = meter.LastTotal;

                        for (int i = 0; i < meter.Ladder; i++)
                        {
                            meter.NextSettlementPointGas += gas[i];
                            meter.CurrentPrice = prices[i];
                            meter.CurrentLadder = i + 1;
                            if (meter.NextSettlementPointGas > dataItem.LJGas)
                            {
                                break;
                            }
                        }
                        meter.SetNextSettlementDateTime();//重新计算本阶段结算时间
                    }
                    else
                    {
                        //新价格未启用阶梯价
                        meter.CurrentPrice = meter.Price1;
                        meter.NextSettlementPointGas = -1;
                    }
                    //删除调价计划
                    new PricingPlanDA().DeletePlan(meter.PricingPlan);
                    meter.PricingPlan = null;
                    new TaskManageDA().UpdateMeter(meter);
                }
                meter.PricingPlan = null;
            }
        }

        /// <summary>
        /// 计算气费
        /// </summary>
        /// <param name="meter"></param>
        /// <param name="ljGas"></param>
        /// <returns></returns>
        public Meter CalculateGasFee(Meter meter, decimal ljGas,string calculateDate = "",bool isChangeMeter =false)
        {
            decimal[] prices = new decimal[5];
            prices[0] = meter.Price1;
            prices[1] = meter.Price2;
            prices[2] = meter.Price3;
            prices[3] = meter.Price4;
            prices[4] = meter.Price5;
            decimal[] gas = new decimal[4];
            gas[0] = meter.Gas1;
            gas[1] = meter.Gas2;
            gas[2] = meter.Gas3;
            gas[3] = meter.Gas4;
            if (meter.Ladder <= 0)
                meter.Ladder = 1;

            //计算当前阶段用气量
            ReCalulate:
            decimal JieDuanYongQiLiang = ljGas - meter.LastGasPoint;

            if (meter.IsUsedLadder && meter.IsDianHuo && meter.Ladder >1 && meter.NextSettlementPointGas != -1 && ljGas >= meter.NextSettlementPointGas)
            {
                //已到达阶梯结算点
                JieDuanYongQiLiang = meter.NextSettlementPointGas - meter.LastGasPoint;
                meter.CurrentBalance -= JieDuanYongQiLiang * meter.CurrentPrice;
                meter.LastGasPoint = meter.NextSettlementPointGas;

                if (!isChangeMeter)
                {
                    //记录结算记录
                    if (calculateDate == "")
                        calculateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    BillRecord record = new BillRecord() { UserID = meter.UserID, MeterNo = meter.Mac };
                    record.RecordDate = calculateDate;
                    record.BillID = meter.BillID;
                    record.BillRecordType = BillRecordType.阶梯点结算记录;

                    record.EndPoint = meter.NextSettlementPointGas;
                    if (meter.IsPricing)
                    {
                        record.BeginPoint = meter.TiaoJiaPointGas;
                        meter.IsPricing = false;

                    }
                    else
                    {
                        record.BeginPoint = meter.NextSettlementPointGas - gas[meter.CurrentLadder - 1];
                    }
                    record.Gas = record.EndPoint - record.BeginPoint;
                    record.Price = meter.CurrentPrice;
                    record.Amount = record.Gas * record.Price;
                    record.Balance = meter.CurrentBalance;
                    record.Ladder = meter.CurrentLadder;

                    new M_BillRecordService().AddBillRecord(record);
                }

                if (meter.CurrentLadder < meter.Ladder)
                    meter.CurrentLadder++;

                meter.CurrentPrice = prices[meter.CurrentLadder - 1];
                //重新设置下一个结算点
                if (meter.CurrentLadder == meter.Ladder)
                {
                    //已到达最大阶梯
                    meter.NextSettlementPointGas = -1;
                }
                else
                {
                    meter.NextSettlementPointGas += gas[meter.CurrentLadder - 1];
                }
                goto ReCalulate;
            }
            else
            {
                //未到达结算点或已超过最后阶梯
                meter.CurrentBalance -= JieDuanYongQiLiang * meter.CurrentPrice;
                meter.LastGasPoint = meter.TotalAmount;
            }

            return meter;
        }
        /// <summary>
        /// 计算两个日期之间相差的秒数。
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private int getJianGe(DateTime beginDate, DateTime endDate)
        {
            TimeSpan ts = beginDate.Subtract(endDate);
            int second = ts.Days * 24 * 60 * 60 + ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds;
            return second;
        }

        //根据日期、结算点参数和计费周期
        private DateTime getNextDate(int settlementDay, int settlementMonth, int monthCount, DateTime currentDate)
        {
            settlementDay = settlementDay < 0 ? 1 : settlementDay;
            settlementMonth = settlementMonth < 0 ? 1 : settlementMonth;
            settlementMonth = settlementMonth > 12 ? 12 : settlementMonth;

            DateTime settlementDate = currentDate;
            int lastDay = ((Convert.ToDateTime(string.Format("{0}-{1}-01 00:00:00", currentDate.Year, settlementMonth)).AddMonths(1)).AddDays(-1)).Day;

            DateTime bDate = Convert.ToDateTime(string.Format("{0}-{1}-{2} 00:00:00", currentDate.Year, settlementMonth, settlementDay > lastDay ? lastDay : settlementDay));

            DateTime eDate;
            if (currentDate.Month < settlementMonth)
            {
                for (int i = 0; i < 12 / monthCount; i++)
                {
                    eDate = bDate;
                    bDate = eDate.AddMonths(monthCount * -1);
                    if (bDate.Day < settlementDay)
                    {
                        lastDay = ((Convert.ToDateTime(string.Format("{0}-{1}-01 00:00:00", bDate.Year, bDate.Month)).AddMonths(1)).AddDays(-1)).Day;
                        if ((settlementDay >= lastDay) && (bDate.Day < lastDay))
                        {
                            bDate = bDate.AddDays(lastDay - bDate.Day);
                        }
                        else if ((settlementDay < lastDay))
                        {
                            bDate = bDate.AddDays(settlementDay - bDate.Day);
                        }
                    }
                    if (bDate <= currentDate && currentDate < eDate)
                    {
                        settlementDate = eDate;
                        break;
                    }

                }
            }
            else if (currentDate.Month == settlementMonth)
            {
                if (currentDate < bDate)
                {
                    settlementDate = bDate;
                }
                else
                {
                    eDate = bDate.AddMonths(monthCount);
                    if (eDate.Day < settlementDay)
                    {
                        lastDay = ((Convert.ToDateTime(string.Format("{0}-{1}-01 00:00:00", eDate.Year, eDate.Month)).AddMonths(1)).AddDays(-1)).Day;
                        if ((settlementDay >= lastDay) && (eDate.Day < lastDay))
                        {
                            eDate = eDate.AddDays(lastDay - eDate.Day);
                        }
                        else if ((settlementDay < lastDay))
                        {
                            eDate = eDate.AddDays(settlementDay - eDate.Day);
                        }
                    }
                    settlementDate = eDate;
                }
            }
            else
            {
                for (int i = 0; i < 12 / monthCount; i++)
                {
                    eDate = bDate.AddMonths(monthCount);
                    if (eDate.Day < settlementDay)
                    {
                        lastDay = ((Convert.ToDateTime(string.Format("{0}-{1}-01 00:00:00", eDate.Year, eDate.Month)).AddMonths(1)).AddDays(-1)).Day;
                        if ((settlementDay >= lastDay) && (eDate.Day < lastDay))
                        {
                            eDate = eDate.AddDays(lastDay - eDate.Day);
                        }
                        else if ((settlementDay < lastDay))
                        {
                            eDate = eDate.AddDays(settlementDay - eDate.Day);
                        }
                    }
                    if (bDate <= currentDate && currentDate < eDate)
                    {
                        settlementDate = eDate;
                        break;
                    }
                    bDate = eDate;
                }
            }
            //Console.WriteLine("settlementDay:{0} settlementMonth={1} monthCount={2}\r\n nextDate={3} Date={4}", settlementDay, settlementMonth, monthCount, settlementDate, currentDate);

            return settlementDate;
        }

        /// <summary>
        /// 下一个结算时间点
        /// </summary>
        /// <returns></returns>
        private DateTime NextSettlementDateTime(string settlementType, int settlementDay, int settlementMonth)
        {
            if (settlementMonth <= 0 || settlementMonth > 12)
                settlementMonth = 1;

            DateTime settlementDate = DateTime.Now;
            switch (settlementType)
            {
                case "00"://按月
                    settlementDate = getNextDate(settlementDay, settlementMonth, 1, DateTime.Now);
                    break;
                case "01"://按季度
                    settlementDate = getNextDate(settlementDay, settlementMonth, 3, DateTime.Now);
                    break;
                case "10"://按半年
                    settlementDate = getNextDate(settlementDay, settlementMonth, 6, DateTime.Now);
                    break;
                default://按全年
                    settlementDate = getNextDate(settlementDay, settlementMonth, 12, DateTime.Now);
                    break;
            }
            return settlementDate;
        }

    }
}
