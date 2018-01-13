using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.Business;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;

namespace CY.IoTM.DataService.Business
{
    public class ChongzhiManageService : IChongzhiManage
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Message Add(IoT_MeterTopUp info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            string strMessagge = string.Empty;
            try
            {
                //添加设置上传周期参数任务到通讯队列
                //Table<IoT_MeterTopUp> tbl = dd.GetTable<IoT_MeterTopUp>();

                //// 调用新增方法
                //tbl.InsertOnSubmit(info);
                //// 更新操作
                //dd.SubmitChanges();
                //调用接口
                WCFServiceProxy<IMeterTopUp> proxy1 = null;
                int intTopUpType = int.Parse(info.TopUpType.ToString());
                TopUpType TopUpTypea = (TopUpType)intTopUpType;
                proxy1 = new WCFServiceProxy<IMeterTopUp>();
                strMessagge = proxy1.getChannel.Topup(info.MeterNo, decimal.Parse(info.Amount.ToString()), TopUpTypea, info.Oper, info.OrgCode, info);
                m = JsonConvert.DeserializeObject<Message>(strMessagge);                
                //if (!msg.Result)
                //{
                //    m = new Message()
                //    {
                //        Result = false,
                //        TxtMessage = strMessagge
                //    };
                //}
                //else
                //{
                //    m = new Message()
                //    {
                //        Result = true,
                //        TxtMessage = "充值成功！"
                //    };
                //}
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "充值失败！" + e.Message
                };
            }
            return m;
        }

        public string PrintTicket(string id)
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            try
            {
                DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
                Table<IoT_MeterTopUp> tbl = dd.GetTable<IoT_MeterTopUp>();
                // 调用新增方法
                IoT_MeterTopUp IoT_MeterTopUpDB = tbl.Where(u => u.ID == Convert.ToInt64(id)).SingleOrDefault();
                IoT_MeterTopUpDB.IsPrint = true;
                // 更新操作
                dd.SubmitChanges();
            }
            catch(Exception e) {

                return e.Message;
            }
            return "";
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Message UPD(IoT_MeterTopUp info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            string strMessagge = string.Empty;
            try
            {
                //添加设置上传周期参数任务到通讯队列
                //Table<IoT_MeterTopUp> tbl = dd.GetTable<IoT_MeterTopUp>();
                //// 调用新增方法
                //IoT_MeterTopUp IoT_MeterTopUpDB = tbl.Where(u => u.ID == info.ID).SingleOrDefault();
                //IoT_MeterTopUpDB.State = '2';
                //IoT_MeterTopUpDB.Oper = info.Oper;
                //IoT_MeterTopUpDB.Context = info.Context;
                //// 更新操作
                //dd.SubmitChanges();
                //调用接口
                WCFServiceProxy<IMeterTopUp> proxy1 = null;
                TopUpType TopUpTypea = (TopUpType)info.TopUpType;
                proxy1 = new WCFServiceProxy<IMeterTopUp>();
                strMessagge = proxy1.getChannel.UnTopUp(info.TaskID, info.Context, info.Oper);
                if (strMessagge != "")
                {
                    m = new Message()
                    {
                        Result = false,
                        TxtMessage = strMessagge
                    };
                }
                else
                {
                    m = new Message()
                    {
                        Result = true,
                        TxtMessage = "撤销成功！"
                    };
                }

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "撤销充值失败！" + e.Message
                };
            }
            return m;
        }
    }
}
