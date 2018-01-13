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
    public class HuanBiaoService : IHuanBiao
    {

        public View_UserMeter getView_UserMeterList(string UserID, string MeterNo, string CompanyID)
        {
            View_UserMeter m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                View_UserMeter meterTempList = dd.GetTable<View_UserMeter>().Where(p => p.CompanyID == CompanyID & p.MeterNo == MeterNo & p.UserID == UserID).FirstOrDefault();
                m = meterTempList;

            }
            catch (Exception e)
            {
                m = null;
            }
            return m;
        }


        /// <summary>
        /// 添加申请
        /// </summary>
        /// <param name="info"></param>
        /// <param name="meterList"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Message AddShenQing(IoT_ChangeMeter info)
        {

            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                Table<IoT_ChangeMeter> tbl = dd.GetTable<IoT_ChangeMeter>();
                //查询数据库中是否存在该用户的申请单（申请单状态为申请中或者是在审核流程中）
                int count = (from n in tbl
                             where n.UserID == info.UserID && n.OldMeterNo == info.OldMeterNo && (n.State == '1' || n.State == '2')
                             select n).Count();
                if (count > 0)
                {
                    return new Message()
                    {
                        Result = false,
                        TxtMessage = "新增换表申请失败,该用户已提交申请！"
                    };
                }
                // 调用新增方法
                tbl.InsertOnSubmit(info);
                // 更新操作
                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_ChangeMeter>(info)
                };
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "新增换表申请失败！" + e.Message
                };
            }
            return m;
        }



        /// <summary>
        /// 修改换标申请(换标申请后申请单一直可以进行修改，但是只能修改申请原因)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public Message Edit(IoT_ChangeMeter info)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {

                IoT_ChangeMeter dbinfo = dd.GetTable<IoT_ChangeMeter>().Where(p =>
                  p.CompanyID == info.CompanyID && p.ID == info.ID).SingleOrDefault();
                dbinfo.Reason = info.Reason;
                // 更新操作
                dd.SubmitChanges();
                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_ChangeMeter>(dbinfo)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "编辑换表申请失败！" + e.Message
                };
            }
            return m;
        }



        /// <summary>
        /// 撤销申请(分两种状态，1.在申请单状态为申请状态时撤销，此时删除申请单即可2.在换标登记状态下，需要和表进行通讯)
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public Message revoke(string ID, string CompanyID)
        {
            // 定义执行结果
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                //更新主表
                IoT_ChangeMeter dbinfo = dd.GetTable<IoT_ChangeMeter>().Where(p =>
                  p.CompanyID == CompanyID && p.ID == int.Parse(ID.Trim())).SingleOrDefault();

                IoT_MeterHistory dbMeterHistory = dd.GetTable<IoT_MeterHistory>().Where(p =>
                  p.CompanyID == CompanyID && p.UserID == dbinfo.UserID && p.MeterNo == dbinfo.OldMeterNo).SingleOrDefault();

                IoT_Meter dbMeter = dd.GetTable<IoT_Meter>().Where(p =>
                  p.CompanyID == CompanyID && p.UserID == dbinfo.UserID && p.MeterNo == dbinfo.NewMeterNo).SingleOrDefault();

                //dbinfo.Total = dbinfo.Total - 1;
                if (dbinfo.State == '1')//申请状态
                {
                    //直接删除表中的讯息
                    Table<IoT_ChangeMeter> tbl = dd.GetTable<IoT_ChangeMeter>();
                    tbl.DeleteOnSubmit(dbinfo as IoT_ChangeMeter);
                    dd.SubmitChanges();
                }
                else if (dbinfo.State == '2')//换标登记状态
                {
                    Table<IoT_Meter> tbl = dd.GetTable<IoT_Meter>();
                    Table<IoT_MeterHistory> tb2 = dd.GetTable<IoT_MeterHistory>();
                    if (dbMeter != null)
                    {
                        //删除新表
                        tbl.DeleteOnSubmit(dbMeter as IoT_Meter);
                    }
                    if (dbMeterHistory != null)
                    {
                        IoT_Meter History_Model = new IoT_Meter()
                        {//实体对象
                            ID = dbMeterHistory.ID,
                            MeterNo = dbMeterHistory.MeterNo,
                            MeterType = dbMeterHistory.MeterType,
                            CompanyID = dbMeterHistory.CompanyID,
                            UserID = dbMeterHistory.UserID,
                            TotalAmount = dbMeterHistory.TotalAmount,
                            TotalTopUp = dbMeterHistory.TotalTopUp,
                            Direction = dbMeterHistory.Direction,
                            InstallDate = dbMeterHistory.InstallDate,
                            Price1 = dbMeterHistory.Price1,
                            Gas1 = dbMeterHistory.Gas1,
                            Price2 = dbMeterHistory.Price2,
                            Gas2 = dbMeterHistory.Gas2,
                            Price3 = dbMeterHistory.Price3,
                            Gas3 = dbMeterHistory.Gas3,
                            Price4 = dbMeterHistory.Price4,
                            Gas4 = dbMeterHistory.Gas4,
                            Price5 = dbMeterHistory.Price5,
                            IsUsed = dbMeterHistory.IsUsed,
                            Ladder = dbMeterHistory.Ladder,
                            SettlementType = dbMeterHistory.SettlementType,
                            SettlementDay = dbMeterHistory.SettlementDay,
                            ValveState = dbMeterHistory.ValveState,
                            MeterState = dbMeterHistory.MeterState,
                            ReadDate = dbMeterHistory.ReadDate,
                            RemainingAmount = dbMeterHistory.RemainingAmount,
                            LastTotal = dbMeterHistory.LastTotal,
                            PriceCheck = dbMeterHistory.PriceCheck,
                            MKeyVer = dbMeterHistory.MKeyVer,
                            MKey = dbMeterHistory.MKey,
                            EnableMeterDate = dbMeterHistory.EnableMeterDate,
                            EnableMeterOper = dbMeterHistory.EnableMeterOper,
                            UploadCycle = dbMeterHistory.UploadCycle,
                            SettlementMonth = dbMeterHistory.SettlementMonth
                        };
                        //将旧表资料重新插入
                        tbl.InsertOnSubmit(History_Model);
                        //将历史表中历史讯息删除
                        tb2.DeleteOnSubmit(dbMeterHistory as IoT_MeterHistory);
                    }
                    dd.SubmitChanges();
                    //调用换标接口
                    ChangeMeterFinished(dbinfo.TaskID, TaskState.Undo);

                }
                else if (dbinfo.State == '3')
                {
                    return new Message()
                {
                    Result = false,
                    TxtMessage = "换表已成功，无法撤消！"
                };
                }
                // 更新操作
                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_ChangeMeter>(dbinfo)
                };

            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "撤消换表失败！" + e.Message
                };
            }
            return m;
        }

        /// <summary>
        /// 换标登记动作
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public Message Dengji(IoT_ChangeMeter info)
        {
            Message m;
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                //查看该申请单的状态是否是"换表登记"或者"换表完成"状态，如果是该状态则填写换表登记失败
                IoT_ChangeMeter dbinfo = dd.GetTable<IoT_ChangeMeter>().Where(p =>
                 p.CompanyID == info.CompanyID && p.ID == info.ID).SingleOrDefault();
                //判断当前的申请单的状态
                if (dbinfo.State == '2')//换表登记状态下提示
                {
                    return new Message()
                       {
                           Result = false,
                           TxtMessage = "不能重复提交换表登记！"
                       };
                }
                else if (dbinfo.State == '3')//换表完成状态下提示
                {
                    return new Message()
                    {
                        Result = false,
                        TxtMessage = "该用户已换标成功，若需要换表请重新填写申请单！"
                    };
                }
                Table<IoT_MeterHistory> tbHistory = dd.GetTable<IoT_MeterHistory>();
                Table<IoT_Meter> tbMeter = dd.GetTable<IoT_Meter>();
                IoT_Meter Meter_Model = tbMeter.Where(u => u.MeterNo == info.OldMeterNo && u.UserID == info.UserID).FirstOrDefault();
                List<IoT_Meter> lstMeter_ModelExect = tbMeter.Where(u => u.MeterNo == info.NewMeterNo).ToList();
                //判断是否存在表具讯息
                if (Meter_Model == null)
                {
                    return new Message()
                  {
                      Result = false,
                      TxtMessage = "换表登记失败，该用户暂无表具信息！"
                  };
                }
                //判断新表号是否已经存在
                if (lstMeter_ModelExect.Count>0)
                {
                    return new Message()
                    {
                        Result = false,
                        TxtMessage = "表号:" + info.NewMeterNo+"已存在，请更换表号"
                    };
                }
                IoT_MeterHistory History_Model = new IoT_MeterHistory()
                {//历史数据实体对象
                    ID = Meter_Model.ID,
                    MeterNo = Meter_Model.MeterNo,
                    MeterType = Meter_Model.MeterType,
                    CompanyID = Meter_Model.CompanyID,
                    UserID = Meter_Model.UserID,
                    TotalAmount = Meter_Model.TotalAmount,
                    TotalTopUp = Meter_Model.TotalTopUp,
                    Direction = Meter_Model.Direction,
                    InstallDate = Meter_Model.InstallDate,
                    Price1 = Meter_Model.Price1,
                    Gas1 = Meter_Model.Gas1,
                    Price2 = Meter_Model.Price2,
                    Gas2 = Meter_Model.Gas2,
                    Price3 = Meter_Model.Price3,
                    Gas3 = Meter_Model.Gas3,
                    Price4 = Meter_Model.Price4,
                    Gas4 = Meter_Model.Gas4,
                    Price5 = Meter_Model.Price5,
                    IsUsed = Meter_Model.IsUsed,
                    Ladder = Meter_Model.Ladder,
                    SettlementType = Meter_Model.SettlementType,
                    SettlementDay = Meter_Model.SettlementDay,
                    ValveState = Meter_Model.ValveState,
                    MeterState = Meter_Model.MeterState,
                    ReadDate = Meter_Model.ReadDate,
                    RemainingAmount = Meter_Model.RemainingAmount,
                    LastTotal = Meter_Model.LastTotal,
                    PriceCheck = Meter_Model.PriceCheck,
                    MKeyVer = Meter_Model.MKeyVer,
                    MKey = Meter_Model.MKey,
                    EnableMeterDate = Meter_Model.EnableMeterDate,
                    EnableMeterOper = Meter_Model.EnableMeterOper,
                    UploadCycle = Meter_Model.UploadCycle,
                    SettlementMonth = Meter_Model.SettlementMonth
                };
                IoT_Meter Meter_Add = new IoT_Meter()
                {//表实体对象
                    MeterNo = info.NewMeterNo,
                    MeterType = Meter_Model.MeterType,
                    CompanyID = info.CompanyID,
                    UserID = info.UserID,
                    TotalAmount = info.ChangeUseSum,//新表表底
                    TotalTopUp = Meter_Model.TotalTopUp,//需要更新
                    Direction = Meter_Model.Direction,
                    InstallDate = Meter_Model.InstallDate,//需要更新
                    Price1 = Meter_Model.Price1,
                    Gas1 = Meter_Model.Gas1,
                    Price2 = Meter_Model.Price2,
                    Gas2 = Meter_Model.Gas2,
                    Price3 = Meter_Model.Price3,
                    Gas3 = Meter_Model.Gas3,
                    Price4 = Meter_Model.Price4,
                    Gas4 = Meter_Model.Gas4,
                    Price5 = Meter_Model.Price5,
                    IsUsed = Meter_Model.IsUsed,
                    Ladder = Meter_Model.Ladder,
                    SettlementType = Meter_Model.SettlementType,
                    SettlementDay = Meter_Model.SettlementDay,
                    ValveState = Meter_Model.ValveState,
                    MeterState = Meter_Model.MeterState,
                    ReadDate = Meter_Model.ReadDate,
                    RemainingAmount = Meter_Model.RemainingAmount,//需要更新
                    LastTotal = Meter_Model.LastTotal,            //需要更新
                    PriceCheck = Meter_Model.PriceCheck,          //需要更新
                    MKeyVer = Meter_Model.MKeyVer,
                    MKey = Meter_Model.MKey,
                    EnableMeterDate = Meter_Model.EnableMeterDate,//需要更新
                    EnableMeterOper = Meter_Model.EnableMeterOper,//需要更新
                    UploadCycle = Meter_Model.UploadCycle,
                    SettlementMonth = Meter_Model.SettlementMonth
                };
                //实现数据操作
                dbinfo.State = info.State;
                dbinfo.ChangeUseSum = info.ChangeUseSum;
                dbinfo.OldGasSum = info.OldGasSum;
                dbinfo.ChangeGasSum = info.ChangeGasSum;
                dbinfo.RemainingAmount = info.RemainingAmount;
                dbinfo.NewMeterNo = info.NewMeterNo;
                dbinfo.TaskID = Guid.NewGuid().ToString();
                tbMeter.DeleteOnSubmit(Meter_Model);    //删除表
                tbMeter.InsertOnSubmit(Meter_Add);      //新增表
                tbHistory.InsertOnSubmit(History_Model);//新增历史表

                dd.SubmitChanges();

                //获取meterId
                Meter_Add = tbMeter.Where(u => u.MeterNo == info.NewMeterNo && u.UserID == info.UserID).FirstOrDefault();
                //此处调用接口
                if (Meter_Add.MeterType == "01")
                {
                    TaskManageService tms = new TaskManageService();
                    //查原表数据
                    Meter _m = tms.GetMeter(info.OldMeterNo.Trim());
                    //_m.TotalAmount = (decimal)info.OldGasSum;
                    //金额表
                    //1、换表结算
                    decimal LastSettlementGas = _m.LastTotal;//原表上次结算时的表底数
                    //换表用气量 = 换表时表底 - 上次结算表底
                    dbinfo.ChangeUseSum = info.ChangeGasSum - LastSettlementGas;
                    
                    _m = new Settlement().CalculateGasFee(_m, (decimal)info.ChangeGasSum,"",true);
                   
                    Meter_Add.TotalTopUp =  _m.CurrentBalance;//新表上总金额（就是换表的余额）
                    Meter_Add.RemainingAmount =  _m.CurrentBalance;//新表上剩余金额（就是换表的余额）
                    Meter_Add.LastTotal = Meter_Add.TotalAmount;

                    //添加mongo队列表对象
                    string str = new TaskManageDA().ChangeMeterInsert(Meter_Add, _m.CurrentLadder, _m.CurrentPrice,(decimal)dbinfo.ChangeUseSum);
                    if (str != "")
                    {
                        throw new Exception(str);
                    }
                    //2、登记换表操作任务（登记操作指令）
                    str = new M_HuanBiaoService().SubmitASK(dbinfo.TaskID, Meter_Add, (decimal)dbinfo.ChangeUseSum);
                    if (str != "")
                    {
                        throw new Exception(str);
                    }
                }
                else
                {
                    //气量表
                }
                // 更新操作
                dd.SubmitChanges();

                m = new Message()
                {
                    Result = true,
                    TxtMessage = JSon.TToJson<IoT_ChangeMeter>(info)
                };
            }
            catch (Exception e)
            {
                m = new Message()
                {
                    Result = false,
                    TxtMessage = "换表登记失败！" + e.Message
                };
            }
            return m;
        }

        public string ChangeMeterFinished(string taskID, TaskState state)
        {
            string result = "";
            string configName = System.Configuration.ConfigurationManager.AppSettings["defaultDatabase"];
            //Linq to SQL 上下文对象
            DataContext dd = new DataContext(System.Configuration.ConfigurationManager.ConnectionStrings[configName].ConnectionString);
            try
            {
                IoT_ChangeMeter dbinfo = dd.GetTable<IoT_ChangeMeter>().Where(p =>
                  p.TaskID == taskID).SingleOrDefault();
                if (state == TaskState.Finished)
                {
                    dbinfo.State = '3';
                }
                else if (state == TaskState.Undo)
                {
                    dbinfo.State = '4';//撤销
                    new M_SetParameterService().UnSetParameter(taskID);
                }
                else
                {
                    return "未知状态";
                }
                // 更新操作
                dd.SubmitChanges();

            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return "";
        }

    }
}
