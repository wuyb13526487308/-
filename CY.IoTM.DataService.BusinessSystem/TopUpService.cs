using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.Business;
using CY.IoTM.MongoDataHelper;
using CY.IotM.Common;
using Newtonsoft.Json;

namespace CY.IoTM.DataService.Business
{
    public class TopUpService : IMeterTopUp
    {
        #region IMeterTopUp 成员

        public string Topup(string meterNo, decimal money, TopUpType topUpType, string oper, string orgCode, IoT_MeterTopUp info)
        {
            IoT_Meter _meter = MeterManageService.QueryMeter(meterNo);
            Meter _m = new TaskManageService().GetMeter(meterNo.Trim());
            if (_m == null) return string.Format("表:{0}不存在，充值失败。", meterNo);
            if (_meter == null) return string.Format("表:{0}不存在，充值失败。", meterNo);
            string taskID;//充值任务ID，通讯任务层提供
            if (_m.LastTopUpSer == 0)
                _m.LastTopUpSer++;

            //写充值记录到数据库
            IoT_MeterTopUp _topUp = new IoT_MeterTopUp();
            _topUp.CompanyID = _meter.CompanyID;
            _topUp.UserID = _meter.UserID;
            _topUp.MeterID = _meter.ID;
            _topUp.MeterNo = _meter.MeterNo;
            _topUp.TopUpType = Convert.ToChar(((byte)topUpType).ToString());
            _topUp.Amount = (decimal)money;
            _topUp.Oper = oper;
            _topUp.OrgCode = orgCode;
            _topUp.Ser = _m.LastTopUpSer++;
            _topUp.State = '0';
            _topUp.IsPrint = false;
            _topUp.TopUpDate = DateTime.UtcNow.AddHours(8);
            _topUp.PayType = info.PayType;
            _topUp.SFOperID = info.SFOperID;
            _topUp.SFOperName = info.SFOperName;
            //任务提交层（mongo）
            M_MeterTopUpService _mtus = new M_MeterTopUpService();
            string result = _mtus.TopUp(_topUp, out taskID);
            if(result != "")
                return string.Format("表:{0}不存在，充值失败,原因：{1}", meterNo,result);
            _topUp.TaskID = taskID;
            _topUp.Context = "";

            new TaskManageDA().UpdateMeter(_m);
            result = InsertTopUp(_topUp);
            Message message = new Message();
            if (result == "")
            {
                message.Result = true;
                message.TxtMessage = JsonConvert.SerializeObject(_topUp);
            }
            else
            {
                message.Result = false;
                message.TxtMessage = result;
            }
            return JsonConvert.SerializeObject(message); ;
        }

        private string InsertTopUp(IoT_MeterTopUp topUp)
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                Table<IoT_MeterTopUp> tbl = dd.GetTable<IoT_MeterTopUp>();
                // 调用新增方法
                tbl.InsertOnSubmit(topUp);
                // 更新操作
                dd.SubmitChanges();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "";

        }
        /// <summary>
        /// 撤销充值
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="reason"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        public string UnTopUp(string taskID, string reason, string oper)
        {
            string result = "";
            //TODO:在做撤销操作前，应先通知通讯调度中心，撤销当前任务，如任务已执行，则本次撤销操作失败。

            //1、向通讯层提交任务撤销请求,返回空表示撤销任务成功。
            result = new M_MeterTopUpService().UnTopUp(taskID,reason);
            if (result != "")
                return result;

            //2、在数据库登记撤销
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_MeterTopUp dbinfo = dd.GetTable<IoT_MeterTopUp>().Where(p =>
                  p.TaskID == taskID).SingleOrDefault();
                if (dbinfo == null) return "充值任务不存在。";
                dbinfo.State =Convert.ToChar(((byte)TaskState.Undo).ToString ());//任务撤销(1)
                dbinfo.TopUpDate = DateTime.Now;
                dbinfo.Context = reason;//撤销原因
                dd.SubmitChanges();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return result;
        }

        #endregion

        /// <summary>
        /// 充值完成
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="state"></param>
        /// <context></context>
        /// <returns></returns>
        public string TopupFinished(string taskID, TaskState state,string context)
        {
            string result = "";
            IoT_MeterTopUp topUp = QueryTopUp(taskID);
            M_MeterTopUpService _mmtopup = new M_MeterTopUpService();
            //1、将充值金额增加到用户账户（表）上。
            if (state == TaskState.Finished)
            {
                //充值执行完成
                if (topUp.State == '0')
                {
                    result = Merged(taskID, topUp.MeterNo, (decimal)topUp.Amount);
                    if (result != "") return result;
                    result = _mmtopup.TopUpFinished(taskID, (decimal)topUp.Amount);
                }
                else if (topUp.State == '1')
                {
                    //已撤销
                    _mmtopup.UnTopUp(taskID, "充值已撤销");
                }
            }
            else
            {
                //result = TopupFailed(taskID, (short)state);
            }
            
           
            if (result != "") return result;

            return "";
        }

        /// <summary>
        /// 充值金额合并到总账户中
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="meterNo"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        private string Merged(string taskID,string meterNo, decimal money)
        {
            try
            {
                string sqlText = string.Format("UPDATE IoT_Meter SET TotalTopUp = ISNULL(TotalTopUp,0) + {0},RemainingAmount = ISNULL(RemainingAmount,0) + {0} WHERE MeterNo ='{1}';UPDATE IoT_MeterTopUp set State ='2',TopUpDate = getdate() where TaskID = '{2}'", money, meterNo, taskID);
                SQLHelper.ExecuteScalar(SQLHelper.SchuleConnection, CommandType.Text, sqlText);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private string TopupFailed(string taskID, short state)
        {
            try
            {
                string sqlText = $"UPDATE IoT_MeterTopUp set State ='{state}',TopUpDate = getdate() where TaskID = '{taskID}'";
                SQLHelper.ExecuteScalar(SQLHelper.SchuleConnection, CommandType.Text, sqlText);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        private IoT_MeterTopUp QueryTopUp(string taskID)
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_MeterTopUp topUp = dd.GetTable<IoT_MeterTopUp>().Where(p =>p.TaskID == taskID).SingleOrDefault();
                return topUp;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }        
    }
}
