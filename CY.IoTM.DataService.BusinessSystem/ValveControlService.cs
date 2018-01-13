using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IoTM.Common.Business;
using CY.IoTM.MongoDataHelper;
using CY.IotM.Common;
using CY.IotM.Common.Tool;



namespace CY.IoTM.DataService.Business
{
    public class ValveControlService : IValveControl
    {
        #region IValveControl 成员

        /// <summary>
        /// 开阀操作
        /// </summary>
        /// <param name="meter"></param>
        /// <returns></returns>
        public string TurnOn(IoT_Meter meter,string reason,string oper)
        {
            //
            string taskID;
            string result = "";
            //1、在通讯层注册开阀任务，成功返回空，输出参数为本次注册的任务ID
            result = M_ValveControlSevice.TurnOn(meter, out taskID);
            //2、登记开阀操作
            IoT_ValveControl valve = new IoT_ValveControl();
            valve.CompanyID = meter.CompanyID;
            valve.MeterID = meter.ID;
            valve.MeterNo = meter.MeterNo;
            valve.RegisterDate = DateTime.Now;
            valve.State = '0';
            valve.TaskID = taskID;
            valve.UserID = meter.UserID;
            valve.Reason = reason;
            valve.Oper = oper;
            valve.ControlType = Convert.ToChar (((byte)ValveControlType.开阀).ToString ());
            result = Insert(valve);
            return result;
        }

        private string Insert(IoT_ValveControl valve)
        {
            string result = "";

            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                Table<IoT_ValveControl> tbl = dd.GetTable<IoT_ValveControl>();
                tbl.InsertOnSubmit(valve);
                dd.SubmitChanges();
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }

        private string Update(IoT_ValveControl valve)
        {
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_ValveControl dbinfo = dd.GetTable<IoT_ValveControl>().Where(p =>
                  p.TaskID == valve.TaskID).SingleOrDefault();
                if (dbinfo == null) return "阀门控制任务不存在。";
                dbinfo.State = valve.State;
                dbinfo.FinishedDate = valve.FinishedDate;
                dbinfo.Context = valve.Context;

                if (valve.State.ToString() == "2")
                {
                    //修改阀门状态
                    IoT_Meter meterInfo = dd.GetTable<IoT_Meter>().Where(p => p.MeterNo == valve.MeterNo).SingleOrDefault();
                    meterInfo.ValveState = valve.ControlType;
                }
                dd.SubmitChanges();
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string TurnOff(IoT_Meter meter, string reason, string oper)
        {
            //
            string taskID;
            string result = "";
            //1、在通讯层注册关阀任务，成功返回空，输出参数为本次注册的任务ID
            result = M_ValveControlSevice.TurnOff(meter, out taskID);
            //2、在数据库中登记关阀操作
            IoT_ValveControl valve = new IoT_ValveControl();
            valve.CompanyID = meter.CompanyID;
            valve.MeterID = meter.ID;
            valve.MeterNo = meter.MeterNo;
            valve.RegisterDate = DateTime.Now;
            valve.State = '0';
            valve.TaskID = taskID;
            valve.UserID = meter.UserID;
            valve.Reason = reason;
            valve.Oper = oper;
            valve.ControlType = Convert.ToChar(((byte)ValveControlType.关阀).ToString());
            result = Insert(valve);

            return result;
        }

        public string Undo(string taskID,string context)
        {
            string result = "";
            //TODO:在做撤销操作前，应先通知通讯调度中心，撤销当前任务，如任务已执行，则本次撤销操作失败。

            //1、向通讯层提交任务撤销请求,返回空表示撤销任务成功。
            result = M_ValveControlSevice.Undo(taskID);
            if (result != "")
                return result;

            //2、在数据库登记撤销
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_ValveControl dbinfo = dd.GetTable<IoT_ValveControl>().Where(p =>
                  p.TaskID == taskID).SingleOrDefault();
                if (dbinfo == null) return "阀门控制任务不存在。";
                dbinfo.State = '1';//任务撤销
                dbinfo.FinishedDate = DateTime.Now;
                dbinfo.Context = context;//撤销原因
                dd.SubmitChanges();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return result;
        }       

        #endregion


        public string ValveControlFinished(string taskID,string taskType,TaskState state)
        {
            string result = "";
            //1、向通讯层更新任务执行状态。
            result = M_ValveControlSevice.ValveControlFinished(taskID,state);
            if (result != "")
                return result;

            //2、在数据库登记            
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_ValveControl dbinfo = dd.GetTable<IoT_ValveControl>().Where(p =>
                  p.TaskID == taskID).SingleOrDefault();
                if (dbinfo == null) return "阀门控制任务不存在。";
                dbinfo.State = '2';//任务完成
                dbinfo.FinishedDate = DateTime.Now;
                dbinfo.Context = "";

                //修改阀门状态
                IoT_Meter meterInfo = dd.GetTable<IoT_Meter>().Where(p => p.MeterNo == dbinfo.MeterNo).SingleOrDefault();
                meterInfo.ValveState =taskType == "KF"?'0':'1';
                dd.SubmitChanges();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return result;
        }



        public Message Add(IoT_ValveControl info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                Table<IoT_ValveControl> tbl = dd.GetTable<IoT_ValveControl>();

                // 调用新增方法
                tbl.InsertOnSubmit(info);
                // 更新操作
                dd.SubmitChanges();

                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_ValveControl>(info)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增阀门控制记录失败！" + e.Message
                };
            }
            return m;
        }

        public Message Edit(IoT_ValveControl info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_ValveControl dbinfo = dd.GetTable<IoT_ValveControl>().Where(p =>
                  p.CompanyID == info.CompanyID && p.ID == info.ID).SingleOrDefault();

                ConvertHelper.Copy<IoT_ValveControl>(dbinfo, info);

                // 更新操作
                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_ValveControl>(dbinfo)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑阀门控制记录失败！" + e.Message
                };
            }
            return m;
        }

        public Message Delete(IoT_ValveControl info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                // 获得上下文对象中的表信息
                Table<IoT_ValveControl> tbl = dd.GetTable<IoT_ValveControl>();

                var s = tbl.Where(p => p.CompanyID == info.CompanyID && p.ID == info.ID).Single();
                tbl.DeleteOnSubmit(s as IoT_ValveControl);

                // 更新操作
                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = "删除阀门控制记录成功！"
                };
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "删除阀门控制记录失败！" + e.Message
                };
            }
            return m;
        }



    }
}
