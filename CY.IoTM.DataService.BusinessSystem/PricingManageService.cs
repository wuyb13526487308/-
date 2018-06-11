using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.Business;
using CY.IoTM.MongoDataHelper;


namespace CY.IoTM.DataService.Business
{
    /// <summary>
    /// 调价计划处理
    /// </summary>
    public class PricingManageService : IPricingManage
    {

        /// <summary>
        /// 添加调价计划
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meterList"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Message AddPricingAll(IoT_Pricing info)
        {

            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                List<IoT_Meter> meterTempList = dd.GetTable<IoT_Meter>().Where(p => p.CompanyID == info.CompanyID).ToList();
                List<IoT_PricingMeter> meterList = new List<IoT_PricingMeter>();
                foreach (IoT_Meter meter in meterTempList)
                {
                    IoT_PricingMeter alarmMeter = new IoT_PricingMeter();
                    alarmMeter.MeterNo = meter.MeterNo;
                    meterList.Add(alarmMeter);
                }
                m = Add(info, meterList);

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增调价计划失败！" + e.Message
                };
            }
            return m;
        }



        /// <summary>
        /// 添加区域添加调价计划
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meterList"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Message AddPricingArea(IoT_Pricing info, List<String> communityList)
        {

            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                List<View_UserMeter> meterTempList = dd.GetTable<View_UserMeter>().Where(p => p.CompanyID == info.CompanyID && communityList.Contains(p.Community)).ToList();
                List<IoT_PricingMeter> meterList = new List<IoT_PricingMeter>();

                foreach (View_UserMeter meter in meterTempList)
                {
                    IoT_PricingMeter alarmMeter = new IoT_PricingMeter();
                    alarmMeter.MeterNo = meter.MeterNo;
                    meterList.Add(alarmMeter);
                }
                m = Add(info, meterList);
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增调价计划失败！" + e.Message
                };
            }
            return m;

        }


        public Message Add(IoT_Pricing info, List<IoT_PricingMeter> meterList)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                //获取价格信息
                IoT_PricePar priceInfo = dd.GetTable<IoT_PricePar>().Where(p =>
                   p.CompanyID == info.CompanyID && p.ID ==int.Parse(info.PriceType)).SingleOrDefault();

                info.Price1 = priceInfo.Price1 == null ? 0 : priceInfo.Price1;
                info.Gas1 = priceInfo.Gas1 == null ? 0 : priceInfo.Gas1;
                info.Price2 = priceInfo.Price2 == null ? 0 : priceInfo.Price2;
                info.Gas2 = priceInfo.Gas2 == null ? 0 : priceInfo.Gas2;
                info.Price3 = priceInfo.Price3 == null ? 0 : priceInfo.Price3;
                info.Gas3 = priceInfo.Gas3 == null ? 0 : priceInfo.Gas3;
                info.Price4 = priceInfo.Price4 == null ? 0 : priceInfo.Price4;
                info.Gas4 = priceInfo.Gas4 == null ? 0 : priceInfo.Gas4;
                info.Price5 = priceInfo.Price5 == null ? 0 : priceInfo.Price5;

                info.IsUsed = priceInfo.IsUsed == null ? false : priceInfo.IsUsed;
                info.Ladder = priceInfo.Ladder == null ? 3 : priceInfo.Ladder;
                info.SettlementType = priceInfo.SettlementType;
                info.MeterType="01";
            
                //设置调价计划参数和条件任务
                string result = new SetMeterParameter().SetPricingPlan(info, meterList);
                if (result != "")
                    throw new Exception(result);


                Table<IoT_Pricing> tbl = dd.GetTable<IoT_Pricing>();
                // 调用新增方法
                tbl.InsertOnSubmit(info);
                // 更新操作
                dd.SubmitChanges();

                Table<IoT_PricingMeter> tbl_meter = dd.GetTable<IoT_PricingMeter>();
                foreach (IoT_PricingMeter meter in meterList)
                {
                    IoT_Meter tempMeter = MeterManageService.QueryMeter(meter.MeterNo);
                    meter.MeterID = tempMeter.ID;
                    meter.ID = info.ID;
                    meter.Context = info.Context;
                    meter.State = '0';//申请
                    tbl_meter.InsertOnSubmit(meter);

                }
                // 更新操作
                dd.SubmitChanges();

                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_Pricing>(info)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增调价计划失败！" + e.Message
                };
            }
            return m;
        }


        public Message Edit(IoT_Pricing info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                   IoT_Pricing dbinfo = dd.GetTable<IoT_Pricing>().Where(p => 
                     p.CompanyID == info.CompanyID).SingleOrDefault();

                    ConvertHelper.Copy<IoT_Pricing>(dbinfo, info);

                    // 更新操作
                    dd.SubmitChanges();
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.TToJson<IoT_Pricing>(dbinfo)
                    };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑调价计划失败！" + e.Message
                };
            }
            return m;
        }

        public Message Delete(IoT_Pricing info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                // 获得上下文对象中的表信息
                Table<IoT_Pricing> tbl = dd.GetTable<IoT_Pricing>();

                var s = tbl.Where(p =>  p.CompanyID == info.CompanyID ).Single();
                tbl.DeleteOnSubmit(s as IoT_Pricing);
            
                // 更新操作
                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = "删除调价计划成功！"
                };
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "删除调价计划失败！" + e.Message
                };
            }
            return m;
        }
               


        /// <summary>
        /// 撤销调价计划
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meterList"></param>
        /// <returns></returns>
        public Message UnSetParamter(IoT_Pricing info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                Table<IoT_PricingMeter> tbl_meter = dd.GetTable<IoT_PricingMeter>();
                List<IoT_PricingMeter> list = tbl_meter.Where(p => p.ID == info.ID && p.State == '0').ToList();
                M_SetParameterService ps = new M_SetParameterService();
                foreach (IoT_PricingMeter meter in list)
                {
                    string result = new SetMeterParameter().UnSetParamter(meter.TaskID);
                    meter.State = '2';
                    meter.FinishedDate = DateTime.Now;
                    ps.UnSetParameter(meter.TaskID);
                }
                if (list.Count > 0)
                {
                    Table<IoT_Pricing> tbl = dd.GetTable<IoT_Pricing>();
                    var dbinfo = tbl.Where(p => p.CompanyID == info.CompanyID && p.ID == info.ID).Single();

                    dbinfo.State = '1';//任务撤销
                    dbinfo.Context = info.Context;//撤销原因
                    dd.SubmitChanges();
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = "操作成功"
                    };
                }
                else
                {
                    m = new Message()
                    {
                        Result = false,
                        TxtMessage = "调价计划已执行完成。"
                    };
                }
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "撤销设置报警参数失败！" + e.Message
                };
            }
            return m;
        }

        /// <summary>
        /// 更新调价任务状态
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="state">状态：0 申请 1 完成 2 撤销  3 失败</param>
        /// <returns></returns>
        public string UpdatePricingTaskState(string taskID, TaskState state)
        {
            if (state == TaskState.Waitting) return "状态不能为申请";

            string result = "";
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_PricingMeter dbinfo = dd.GetTable<IoT_PricingMeter>().Where(p =>
                  p.TaskID == taskID).SingleOrDefault();

                dbinfo.State = Convert.ToChar(((byte)state).ToString());
                dbinfo.FinishedDate = DateTime.Now;
                // 更新操作
                dd.SubmitChanges();

                IoT_Pricing uploadCycle=null;
                int iCount = dd.GetTable<IoT_PricingMeter>().Where(p => p.TaskID == taskID && p.State.ToString() == "0").Count();
                if (iCount == 0)//表具任务都执行完成后 更新调价任务状态
                {
                    uploadCycle = dd.GetTable<IoT_Pricing>().Where(p => p.ID == dbinfo.ID).SingleOrDefault();
                    uploadCycle.State = Convert.ToChar(((byte)state).ToString());                   
                }
                dd.SubmitChanges();
                IoT_PricePar pricePar = dd.GetTable<IoT_PricePar>().Where(p => p.ID.ToString() == uploadCycle.PriceType).SingleOrDefault();


                if (state == TaskState.Undo)
                {
                    new M_SetParameterService().UnSetParameter(taskID);
                }
                if (state == TaskState.Finished)
                {

                    IoT_Meter meterInfo = dd.GetTable<IoT_Meter>().Where(p => p.MeterNo == dbinfo.MeterNo).SingleOrDefault();

                    meterInfo.Price1 = uploadCycle.Price1 == null ? 0 : uploadCycle.Price1;
                    meterInfo.Gas1 = uploadCycle.Gas1 == null ? 0 : uploadCycle.Gas1;
                    meterInfo.Price2 = uploadCycle.Price2 == null ? 0 : uploadCycle.Price2;
                    meterInfo.Gas2 = uploadCycle.Gas2 == null ? 0 : uploadCycle.Gas2;
                    meterInfo.Price3 = uploadCycle.Price3 == null ? 0 : uploadCycle.Price3;
                    meterInfo.Gas3 = uploadCycle.Gas3 == null ? 0 : uploadCycle.Gas3;
                    meterInfo.Price4 = uploadCycle.Price4 == null ? 0 : uploadCycle.Price4;
                    meterInfo.Gas4 = uploadCycle.Gas4 == null ? 0 : uploadCycle.Gas4;
                    meterInfo.Price5 = uploadCycle.Price5 == null ? 0 : uploadCycle.Price5;

                    meterInfo.IsUsed = uploadCycle.IsUsed == null ? false : uploadCycle.IsUsed;
                    meterInfo.Ladder = uploadCycle.Ladder == null ? 3 : uploadCycle.Ladder;

                    meterInfo.SettlementType = uploadCycle.SettlementType;
                    meterInfo.SettlementDay = pricePar.SettlementDay;
                    meterInfo.SettlementMonth = pricePar.SettlementMonth;

                    // 更新操作
                    dd.SubmitChanges();

                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }

    }
}
