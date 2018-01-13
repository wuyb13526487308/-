using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace TestMongodb
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            Test();

            Console.ReadLine();
            Console.ReadLine();
        }

        private static void WriteFile(List<string> info)
        {
            File.AppendAllLines(System.Environment.CurrentDirectory + "\\Test.txt", info);
        }


        private static void Test(int settlementDay, int monthCount)
        {
            List<string> list = new List<string>();
            DateTime bDate = Convert.ToDateTime("2014-11-01");
            DateTime eDate = Convert.ToDateTime("2016-01-01");
            DateTime settlementDate = DateTime.Now;
            DateTime result = getNextDate(settlementDay, 8, monthCount, Convert.ToDateTime("2015-01-20"));
            DateTime head;
            //测试按月结算日为1号的
            string info = string.Format ("结算日为{0}号，结算周期：{1}",settlementDay,monthCount);
            Console.WriteLine("结算日为{0}号，结算周期：{1}",settlementDay,monthCount);
            //list.Add(info);
            int iStartMonth = 1;
            while (iStartMonth <= 12)
            {
                bDate = Convert.ToDateTime("2014-11-01");
                Console.WriteLine("开始月份为：{0}", iStartMonth);
                string info1 = string.Format("开始月份为：{0}", iStartMonth);
                //list.Add(info);

                result = getNextDate(settlementDay, iStartMonth, monthCount, bDate);
                settlementDate = result;
                head = bDate;
                while (bDate <= eDate)
                {
                    result = getNextDate(settlementDay, iStartMonth, monthCount, bDate);
                    if (settlementDate != result)
                    {
                        Console.WriteLine("从{0}至{1}期间的结算日为：{2}", head, bDate, settlementDate);
                        string info2 = string.Format("从{0}至{1}期间的结算日为：{2}", head, bDate, settlementDate);
                        settlementDate = result;
                        list.Add(info + " " + info1 + " " + info2);

                        head = bDate;
                    }

                    bDate = bDate.AddHours(1);
                }
                iStartMonth++;
                list.Add("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            }
            list.Add("==========================================================================================");
            WriteFile(list);
            
        }

        private static void Test()
        {
            int[] SettlementMonths = {1,  3, 6, 12 };
            int[] SettlementDays = { 1, 10, 28, 29, 30, 31 };
            for (int i = 0; i < SettlementMonths.Length ; i++)
            {
                for (int j = 0; j < SettlementDays.Length; j++)
                {
                    Test(SettlementDays[j], SettlementMonths[i]);
                }
            }
        }

        private static DateTime getNextDate(int settlementDay, int settlementMonth, int monthCount,DateTime currentDate)
        {
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
                    settlementDate = bDate.AddMonths(monthCount);
                }
            }
            else
            {
                for (int i = 0; i < 12 / monthCount; i++)
                {
                    eDate = bDate.AddMonths(monthCount);
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
    }

}
