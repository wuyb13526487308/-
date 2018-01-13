using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 表数据结构类
    /// </summary>
    [DataContract()]
    public class Meter : BaseEntity //: BaseEntity
    {
        /// <summary>
        /// 燃气表在关系数据库中的索引号
        /// </summary>
        [DataMember]
        public Int64 MeterID { get; set; }
        /// <summary>
        /// 获取或设置用户号（由4位企业编码+用户ID）
        /// </summary>
        [DataMember]
        public string UserID { get; set; }
        /// <summary>
        /// 燃气表地址
        /// </summary>
        [DataMember]
        public string Mac { get; set; }
        /// <summary>
        /// 通信密钥：用于加密物联网表和后台的通信数据，由{color:red}0~9和A~F{color}之间的16个字符组成表出厂时默认的密钥为：16个8
        /// </summary>
        [DataMember]
        public string Key { get; set; }
        /// <summary>
        /// 通讯密钥版本0-255，0表示出厂版本
        /// </summary>
        [DataMember]
        public byte MKeyVer { get; set; }
        /// <summary>
        /// 00 气量表 01 金额表
        /// </summary>
        [DataMember]
        public string MeterType { get; set; }
        /// <summary>
        /// 当前表的燃气总用量，单位：立方米
        /// </summary>
        [DataMember]
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 总充值金额：指当前表到目前的总充值金额，仅对金额表有效。
        /// </summary>
        [DataMember]
        public decimal TotalTopUp { get; set; }
        /// <summary>
        /// 结算周期：00 月  01 季度 10 半年 11 全年
        /// </summary>
        [DataMember]
        public string SettlementType { get; set; }
        /// <summary>
        /// 结算日 1-31
        /// </summary>
        [DataMember]
        public int SettlementDay { get; set; }
        /// <summary>
        /// 结算月份1-12
        /// </summary>
        [DataMember]
        public int SettlementMonth { get; set; }
        /// <summary>
        /// 阀门状态： 0 开  1 关
        /// </summary>
        [DataMember]
        public string ValveState { get; set; }
        /// <summary>
        /// 表状态：0 正常 1 换表申请 2 换表登记  4 已安装  5 点火（表示点火登记已完成，等待表通讯设置完成）
        /// </summary>
        [DataMember]
        public string MeterState { get; set; }
        /// <summary>
        /// 当前价格是否 和表中价格进行校验，0 未校验 1 已校验
        /// 当表新安装、重新设置价格、换表后，必须要校验系统设置和表是否一致，当发现不一致，用当前值自动重新设置。
        /// </summary>
        [DataMember]
        public string PriceCheck { get; set; }
        /// <summary>
        /// 上次充值时ser
        /// </summary>
        [DataMember]
        public byte LastTopUpSer { get; set; }
        /// <summary>
        /// 启用阶梯价 true 启用
        /// </summary>
        [DataMember]
        public bool IsUsedLadder { get; set; }
        /// <summary>
        /// 阶梯数
        /// </summary>
        [DataMember]
        public int Ladder { get; set; }
        /// <summary>
        /// 价格1
        /// </summary>
        [DataMember]
        public decimal Price1 { get; set; }
        /// <summary>
        /// 气量1
        /// </summary>
        [DataMember]
        public decimal Gas1 { get; set; }
        /// <summary>
        /// 价格2
        /// </summary>
        [DataMember]
        public decimal Price2 { get; set; }
        /// <summary>
        /// 气量2
        /// </summary>
        [DataMember]
        public decimal Gas2 { get; set; }
        /// <summary>
        /// 价格3
        /// </summary>
        [DataMember]
        public decimal Price3 { get; set; }
        /// <summary>
        /// 气量3
        /// </summary>
        [DataMember]
        public decimal Gas3 { get; set; }
        /// <summary>
        /// 价格4
        /// </summary>
        [DataMember]
        public decimal Price4 { get; set; }
        /// <summary>
        /// 气量4
        /// </summary>
        [DataMember]
        public decimal Gas4 { get; set; }
        /// <summary>
        /// 价格5
        /// </summary>
        [DataMember]
        public decimal Price5 { get; set; }
        /// <summary>
        /// 获取或设置上次结算总用量：最后一次计费周期结算时的表总用气量(仅对金额表有效）
        /// 注：当出现负数时，表示由于换表导致，其绝对值为本期间换表时期间累计用气量
        /// </summary>
        [DataMember]
        public decimal LastTotal { get; set; }
        /// <summary>
        /// 获取或设置上次结算后的可用金额。
        /// </summary>
        [DataMember]
        public decimal LastSettlementAmount { get; set; }
        /// <summary>
        /// 调价计划
        /// </summary>
        [DataMember]
        public PricingPlan PricingPlan{ get; set;}
        /// <summary>
        /// 获取或设置用于计算气量的上次表底
        /// </summary>
        [DataMember]
        public decimal LastGasPoint { get; set; }
        /// <summary>
        /// 获取或设置下一个阶梯截至点的累计气量，值为-1，表示下一个阶梯累计点为无穷大
        /// </summary>
        [DataMember]
        public decimal NextSettlementPointGas { get; set; }
        /// <summary>
        /// 结算日期 
        /// </summary>
        [DataMember]
        public string SettlementDateTime { get; set; }
        /// <summary>
        /// 获取或设置当前计费所在阶梯
        /// </summary>
        [DataMember]
        public int CurrentLadder { get; set; }
        /// <summary>
        /// 获取或设置当前结算价格
        /// </summary>
        [DataMember]
        public decimal CurrentPrice { get; set; }

        /// <summary>
        /// 获取或设置上次校时时间
        /// </summary>
        [DataMember]
        public string LastJiaoShiDate { get; set; }

        /// <summary>
        /// 当前总累计金额
        /// </summary>
        [DataMember]
        public decimal LJMoney;
        /// <summary>
        /// 当前剩余金额
        /// </summary>
        [DataMember]
        public decimal CurrentBalance { get; set; }
        /// <summary>
        /// 获取或设置点火完成 true 点火完成 false 为点火完成
        /// </summary>
        [DataMember]
        public bool IsDianHuo { get; set; }
        /// <summary>
        /// 当前周期账单ID，在结算日时将更改ID，
        /// </summary>
        [DataMember]
        public string BillID { get; set; }
        /// <summary>
        /// 指示当前计价周期类是否调整价格，true 调整价格 false 未调整价格
        /// </summary>
        [DataMember]
        public bool IsPricing { get; set; }
        /// <summary>
        /// 调价点累计量（表上示数)
        /// </summary>
        [DataMember]
        public decimal TiaoJiaPointGas { get; set; }


        /// <summary>
        /// 创建新账单ID
        /// </summary>
        public void CreateBillID()
        {
            this.BillID = DateTime.Now.ToString("yyyyMMddHHmmss"+ new Random(DateTime.Now.Millisecond).Next (0,9999).ToString ().PadLeft (4,'0'));
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
        /// 根据当前系统日期计算下一个结算时间点
        /// </summary>
        /// <returns></returns>
        public DateTime NextSettlementDateTime(string settlementType, int settlementDay, int settlementMonth)
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

        /// <summary>
        /// 根据当前表参数和当前时间计算下一个结算时间点
        /// </summary>
        /// <returns></returns>
        public DateTime NextSettlementDateTime()
        {
            return NextSettlementDateTime(this.SettlementType, this.SettlementDay, this.SettlementMonth);
        }

        /// <summary>
        /// 计算当前时间到指定时间间隔秒数；（间隔秒数 = 当前时间 - 指定时间）。
        /// </summary>
        /// <param name="nextSettlemnetDay"></param>
        /// <returns></returns>
        public int Jiange(DateTime nextSettlemnetDay)
        {
            if (nextSettlemnetDay.Year == 1 && nextSettlemnetDay.Month == 1)
                nextSettlemnetDay = Convert.ToDateTime("2015-01-01");

            TimeSpan ts = DateTime.Now.Subtract(nextSettlemnetDay);
            int second = ts.Days * 24 * 60 * 60 + ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds;
            return second;
        }
        /// <summary>
        /// 设置表的下一个结算点
        /// </summary>
        public void SetNextSettlementDateTime()
        {
            this.SettlementDateTime = NextSettlementDateTime().ToString ("yyyy-MM-dd");
        }
        /// <summary>
        /// 计费周期结算时间点
        /// </summary>
        /// <returns></returns>
        public DateTime GetSettlementTimePoint()
        {
            try
            {
                if(SettlementDateTime == null || SettlementDateTime == "")
                    SettlementDateTime = "2015-01-01";

                return Convert.ToDateTime(this.SettlementDateTime);
            }
            catch
            {
                return DateTime.Now;
            }
        }
        /// <summary>
        /// 计算是否需要校时
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public bool IsJiaoShi(int days)
        {
            if (this.LastJiaoShiDate == null || this.LastJiaoShiDate == "")
                this.LastJiaoShiDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                if (Jiange(Convert.ToDateTime(this.LastJiaoShiDate)) > days * 24 * 60 * 60)
                {
                    this.LastJiaoShiDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                this.LastJiaoShiDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                return IsJiaoShi(days);
            }
        }

    }
}
