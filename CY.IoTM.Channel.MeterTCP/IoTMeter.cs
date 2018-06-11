using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using CY.IoTM.Common;
using CY.IoTM.Common.Business;
using CY.IoTM.Common.Item;
using CY.IoTM.Common.Protocol;
using CY.IoTM.Common.Log;
using CY.IoTM.Common.Item.上报数据;
using CY.IoTM.DataService.Business;
using System.IO;

namespace CY.IoTM.Channel.MeterTCP
{
    class IoTMeter:IDisposable
    {
        MeterLink _meterLink;
        private Stopwatch _reportWatch;

        /// <summary>
        /// 连接空闲时间，单位：毫秒
        /// </summary>
        private const int LinkFreeTime = 1000 * 10;

        /// <summary>
        /// 校时间隔天数
        /// </summary>
        private const int JiaoShiDays = 30;

        /// <summary>
        /// 当前执行的Command是否需要重新执行
        /// </summary>
        private bool IsReSend = false;
        /// <summary>
        /// 当前执行的CommandList的索引
        /// </summary>
        private int CurCommandIndex = 0;
        /// <summary>
        /// 应答延时长度
        /// </summary>
        private int Answer_YanShi = 300;


        /// <summary>
        /// 密钥版本 6E4A572D98C92D9B
        /// </summary>
        byte keyVer = 0;
        private byte[] MKey// = { 0x88, 0x88, 0x88, 0x88, 0x88, 0x88, 0x88, 0x88 };
        {
            get
            {
                if(meter == null ) return new byte[] { 0x88, 0x88, 0x88, 0x88, 0x88, 0x88, 0x88, 0x88 };
                return MyDataConvert.StrToToHexByte(meter.Key.PadLeft(16, '0'));
                //if (meter.IsDianHuo )
                //    return MyDataConvert.StrToToHexByte(meter.Key.PadLeft(16, '0'));
                //else
                //    return new byte[] { 0x88, 0x88, 0x88, 0x88, 0x88, 0x88, 0x88, 0x88 };
            }
        }
        Meter meter;
       
        /// <summary>
        /// 指令处理的同步线程
        /// </summary>
        Semaphore _semaphore = new Semaphore(0, 1);
        //Semaphore _waitTaskDowith = new Semaphore(0, 1);


        /// <summary>
        /// 信号量等待时间 30s
        /// </summary>
        const int SigeWaittingTime = 1000 * 30;
        ExecuteCommand _currentCommand;

        public IoTMeter()
        {
            this._reportWatch = new Stopwatch();
        }

        CY.IoTM.DataService.Business.TaskManageService tms = new DataService.Business.TaskManageService();
        /// <summary>
        /// 任务调度线程
        /// </summary>
        private Thread _taskThread;

        public string Initi(string mac, MeterLink link, byte control, byte[] data, IotProtocolType protocolType = IotProtocolType.RanQiBiao)
        {
            if (this._meterLink != null) return "IsExit";

            this._meterLink = link;
            this._meterLink.OnConnectClosed += _meterLink_OnConnectClosed;  
            try 
            {
                //读取当前表的信息并初始化     
                //Stopwatch sw = new Stopwatch();
                //sw.Start();
                 meter = tms.GetMeter(mac);

                this._meterLink.OnReviced += _meterLink_OnReviced;
               
                //应答并上报数据
                ReportData(mac,control, data, protocolType);

                if(meter == null)
                {
                    return "";
                }
                //读取当前表任务   
                List<Task> taskList = tms.GetTaskList(mac); 

                //Log.getInstance().Write(MsgType.Information, string.Format("处理MAC上报数据:{0} Time:{1}", mac, sw.ElapsedMilliseconds / 1000.00));
                //Log.getInstance().Write(new OneMeterDataLogMsg(mac, string.Format("处理MAC上报数据:{0} Time:{1}; 时间：{2}", mac, sw.ElapsedMilliseconds / 1000.00, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))));
                
                
                this._reportWatch.Start();
                //创建并启动任务执行线程
                this._taskThread = new Thread(new ParameterizedThreadStart(DoTask));
                this._reportWatch.Restart();
                this._taskThread.Start(taskList);
            }
            catch (Exception e)
            {
                Log.getInstance().Write(MsgType.Error, "创建IoTMetre对象失败,原因：" + e.Message);
                Log.getInstance().Write( new OneMeterDataLogMsg(this.MAC,"创建IoTMetre对象失败,原因：" + e.Message + e.ToString ()));
                Log.getInstance().Write(e,MsgType.Error); 
                return ("创建IoTMetre对象失败,原因：" + e.Message);
            }
            return "";
        }

        public string MAC
        {
            get
            {
                if (meter == null) return "";
                return this.meter.Mac;
            }
        }

        void _meterLink_OnConnectClosed(MeterLink e)
        {
            this.meter = null;
            this.isRunning = false;
        }

        void _meterLink_OnReviced(Meter_DATA data)
        {
            //处理接收到应答数据
            if (this.meter == null) return;
            if (this._currentCommand != null)
            {
                if (this._currentCommand.Command.Identification == "A013" && data.Control== 0xc4)
                {
                    
                    this.meter.LastTopUpSer++;
                    this._currentCommand.IsReSend = true;
                    this._semaphore.Release();
                    return;
                }
                this.meter.LastTopUpSer++;
                //if(this.meter.MeterType == "01" && this.meter.IsDianHuo)
                //    this.tms.UpdateMeter(this.meter);

                this._semaphore.Release();
                //判断表回复广告文件
                if (_currentCommand.Task.TaskType == TaskType.TaskType_发布广告)
                {
                    this._currentCommand.Dowith(data, MKey);
                    //Log.getInstance().Write(new OneMeterDataLogMsg(this.MAC, "接收到表【" + this.MAC + "】发送广告回复指令：" + MyDataConvert.BytesToHexStr(data.m_buffer) + "; 时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                }
                if (_currentCommand.Task.TaskType != TaskType.TaskType_发布广告)
                {
                    this._currentCommand.Dowith(data, MKey);

                    if (this._currentCommand.Command.Identification == "A011")
                    {
                        //设置通讯密钥
                        this.meter.IsDianHuo = true; 
                    }
                    //this._semaphore.Release();
                }
                //TODO:需要处理确认


            }
        }
        List<Task> _failTaskList = new List<Task>();

        bool isRunning = true;
        /// <summary>
        /// 处理任务线程
        /// </summary>
        private void DoTask(object parObject)
        {
            //while (this._reportWatch.ElapsedMilliseconds < 1000 * 5)
                Thread.Sleep(50);
            if (this.meter == null) return;
            List<Task> taskList = null;

            //检查是否要校时
            if (this.meter != null && this.meter.IsJiaoShi(JiaoShiDays))
            {                
                //处理对时操作
                taskList = new List<Task>();  
                taskList.Add(GetDuiShiTask(this.tms));  
                this.tms.UpdateMeter(this.meter);  

                if (taskList != null)
                    this.Execute(taskList);
            }
            //检查是否有校正任务
            if (this._xZTaskList != null)
            {
                Log.getInstance().Write(new OneMeterDataLogMsg(this.MAC, $" 有{this._xZTaskList.Count()}条修正记录需要发送"));

                this.Execute(this._xZTaskList);
                this._xZTaskList = null;
            }
            //开始执行任务
            taskList = parObject as List<Task>;
            if(taskList != null)
                this.Execute(taskList);

            while (this.meter != null && isRunning)
            {
                try
                {
                    
                    taskList = this.tms.GetTaskList(this.meter.Mac);
                    if (taskList != null && taskList.Count >0)
                        this.Execute(taskList);
                    Thread.Sleep(1000 * 5);
                }
                catch(Exception ex)
                {
                    Log.getInstance().Write(MsgType.Error, "表：" + this.MAC + " 连接执行任务失败，原因：" + ex.Message);
                    Log.getInstance().Write(new OneMeterDataLogMsg(this.MAC," 连接执行任务失败，原因：" + ex.Message));     
                     
                }
            }
        }

        private Task GetDuiShiTask(ITaskManage iTaskManage)
        {
            Task task = new Task();

            task.MeterMac = this.MAC;
            task.TaskDate = DateTime.Now;
            task.TaskID = Guid.NewGuid().ToString();//用于和指令进行进行关联
            task.TaskState = TaskState.Waitting;
            task.TaskType = TaskType.TaskType_校时;//点火任务(DH)，换表登记(HB)、开阀（KF)、关阀(GF)、充值(CZ)、调整价格(TJ)
           
            Command cmd = new Command();
            //1.校时
            byte ser = this.meter.LastTopUpSer;// Convert.ToByte(new Random().Next(0, 255));
            DataItem_A015 item_A015 = new DataItem_A015(ser);//
            cmd.TaskID = task.TaskID;
            cmd.Identification = ((UInt16)item_A015.IdentityCode).ToString("X2");
            cmd.ControlCode = (byte)ControlCode.WriteData;//写操作
            cmd.DataLength = Convert.ToByte(item_A015.Length);
            cmd.DataCommand = MyDataConvert.BytesToHexStr(item_A015.GetBytes());
            cmd.Order = 1;
            task.CommandList.Add(cmd);
            iTaskManage.SetDuiShiTask(task, cmd);
            return task;
        }

        private Task GetFailTask(string taskID)
        {
            var task = (from p in this._failTaskList where p.TaskID == taskID select p).SingleOrDefault();
            return task;
        }

        private void Execute(List<Task> taskList)
        {
            Task tmp = null;
            if (taskList != null)
            {
                int iTimes = 0;
                foreach (Task task in taskList)
                {
                    //检查任务是否在失败队列中，在失败队列中超过6次，不再发送
                    tmp = GetFailTask(task.TaskID);
                    if (tmp != null && tmp.Counter > 6) continue;

                    if (task.TaskState == TaskState.Waitting)
                    {
                        List<Command> commandList = null;
                        if (task.CommandList.Count == 0)
                        {
                            continue;
                        }

                        commandList = task.CommandList;
                        for (int i = 0; i < commandList.Count; i++)
                        {
                            Command cmd = commandList[i];
                            if (isRunning == false) return;
                            Thread.Sleep(1000);
                            #region 发送命令
                            if (cmd.CommandState == CommandState.Waitting)
                            {
                                iTimes = 0;
                                ReStart:
                                if (IsReSend == true)
                                {
                                    i = CurCommandIndex;
                                    cmd = commandList[i];
                                    IsReSend = false;
                                }
                                this._currentCommand = new ExecuteCommand(cmd, task, this.tms);


                                //创建发送帧
                                #region  广告命令 不加密 透明传输
                                if (task.TaskType == TaskType.TaskType_发布广告)
                                {
                                    //广告命令 不加密 透明传输
                                    TaskArge taskArge = new TaskArge(this.meter.Mac, _currentCommand.getDataItem(this.meter.LastTopUpSer), (ControlCode)cmd.ControlCode,IotProtocolType.LCD, null);
                                    this._meterLink.Send(new Protocol.Frame(taskArge).GetBytes());//发送请求指令

                                    Log.getInstance().Write(DateTime.Now.ToString() + " 主站向表【" + this.MAC + "】发送广告指令：" + i.ToString() + " " + taskArge.Data.IdentityCode + " 控制码：" + ((byte)taskArge.ControlCode).ToString("X2"), MsgType.Information);
                                    Log.getInstance().Write(new OneMeterDataLogMsg(this.MAC, "主站向表【" + this.MAC + "】发送广告指令：" + i.ToString() + " " + taskArge.Data.IdentityCode + " 控制码：" + ((byte)taskArge.ControlCode).ToString("X2") + "; 时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                                }
                                #endregion

                                #region   其他命令 加密 传输
                                else
                                {
                                    TaskArge taskArge = new TaskArge(this.meter.Mac, _currentCommand.getDataItem(this.meter.LastTopUpSer), (ControlCode)cmd.ControlCode, MKey);
                                    this._meterLink.Send(new Protocol.Frame(taskArge).GetBytes());//发送请求指令
                                    Log.getInstance().Write(DateTime.Now.ToString() + " 主站向表【" + this.MAC + "】发送指令：" + taskArge.Data.IdentityCode + " 控制码：" + ((byte)taskArge.ControlCode).ToString("X2"), MsgType.Information);
                                    Log.getInstance().Write(new OneMeterDataLogMsg(this.MAC, "主站向表【" + this.MAC + "】发送指令：" + taskArge.Data.IdentityCode + " 控制码：" + ((byte)taskArge.ControlCode).ToString("X2") + "; 时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                                }
                                #endregion   其他命令 加密 传输
                                

                                if (!this._currentCommand.IsFinished && !_semaphore.WaitOne(SigeWaittingTime, false))
                                {
                                    iTimes++;
                                    if (iTimes > 2)
                                    {
                                        Log.getInstance().Write("向表【" + this.MAC + "】发送的任务执行失败.", MsgType.Information);
                                        Log.getInstance().Write(new OneMeterDataLogMsg(this.MAC, "向表【" + this.MAC + "】发送的任务执行失败."));
                                        if (tmp == null)
                                        {
                                            task.Counter++;
                                            this._failTaskList.Add(task);
                                        }
                                        else
                                        {
                                            tmp.Counter++;
                                        }

                                        goto End;
                                    }
                                    else
                                    {
                                        goto ReStart;
                                    }
                                }
                                if (this.IsReSend)
                                {
                                    goto ReStart;
                                }
                            }
                            Thread.Sleep(100);
                            #endregion 发送命令
                            if (cmd.CommandState == CommandState.Failed)
                            {
                                task.TaskState = TaskState.Failed;
                                break;
                            }
                        } 
                    }
                    //处理任务,只要有指令执行失败，本次任务不能完成。
                    if (task.TaskState != TaskState.Failed)
                    {
                        task.TaskState = TaskState.Finished;
                    }
                    else
                    {
                        if (tmp == null)
                        {
                            task.Counter++;
                            this._failTaskList.Add(task);
                        }
                        else
                        {
                            tmp.Counter++;
                        }
                    }

                    TaskCompletes(task);
                }
            End:
                this._reportWatch.Restart();
            }
        }


        /// <summary>
        /// 上报数据线程
        /// </summary>
        private void ReportData(object ParObject)
        {
            //*********该函数要求2秒内必须返回*****************//
            DataItem_C001 item_C001 = ParObject as  DataItem_C001;
            if (item_C001 == null || this.meter == null) return;
            //编写调用上报数据中心抄表数据处理程序
            //Console.WriteLine(string.Format("于：{1} 接收到表{0}上报的数据:累计气量={2},累计金额={3},剩余金额={4},上次结算累计量={5}", this.meter.Mac,DateTime .Now,
            //    item_C001.LJGas,item_C001.LJMoney,item_C001.SYMoney,item_C001.LastLJGas));
            Log.getInstance().Write(string.Format("于：{1} 接收到表{0}上报的数据:累计气量={2},累计金额={3},剩余金额={4},上次结算累计量={5}", this.meter.Mac, DateTime.Now,
                item_C001.LJGas, item_C001.LJMoney, item_C001.SYMoney, item_C001.LastLJGas), MsgType.Information);
            //Log.getInstance().Write(new OneMeterDataLogMsg(this.meter.Mac, string.Format("接收到表{0}上报的数据:累计气量={2},累计金额={3},剩余金额={4},上次结算累计量={5}; 时间：{1} ", this.meter.Mac, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            //    item_C001.LJGas, item_C001.LJMoney, item_C001.SYMoney, item_C001.LastLJGas)));

            //WCFServiceProxy<IReportData> _iReportDataProxy = new WCFServiceProxy<IReportData>();
            this.meter = this.tms.GetMeter(meter.Mac);
            this.meter.TotalAmount = item_C001.LJGas;

            if (!meter.IsDianHuo && meter.LastGasPoint>item_C001.LJGas && meter.TotalTopUp ==0)
            {
                //this.meter.LastGasPoint = item_C001.LJGas;
            }

            CY.IoTM.DataService.Business.ReportDataService rds = new DataService.Business.ReportDataService();
            //SubmitResult result = _iReportDataProxy.getChannel
            SubmitResult result = rds.Submit(this.meter,
                new SubmitData()
                {
                    JSDay = item_C001.JSDay,
                    LastLJGas = item_C001.LastLJGas,
                    LJGas = item_C001.LJGas,
                    LJMoney = item_C001.LJMoney,
                    ReadDate = item_C001.ReadDate,
                    ST1 = item_C001.ST1,
                    ST2 = item_C001.ST2,
                    SYMoney = item_C001.SYMoney
                });
           

           // _iReportDataProxy.CloseChannel();
            //判断是否需要重新加载Meter对象
            if (result.IsReLoadMeter)//上报数据后，后台已更新Meter对象数据，当前meter对象已过失，需要重新加载。
            {
                //WCFServiceProxy<ITaskManage> _iTaskManageProxy = new WCFServiceProxy<ITaskManage>();
                try
                {
                    //Meter _m = _iTaskManageProxy.getChannel.GetMeter(meter.Mac);
                    Meter _m =this.tms.GetMeter(meter.Mac);
                    //_iTaskManageProxy.CloseChannel();
                    this.meter = _m;
                }
                catch(Exception er)
                {
                    Log.getInstance().Write(MsgType.Error, "重新加载表：" + meter.Mac + " 信息失败，原因：" + er.Message);
                    Log.getInstance().Write(new OneMeterDataLogMsg(meter.Mac,"重新加载表：" + meter.Mac + " 信息失败，原因：" + er.Message));
                }
            }
            if (result.IsCalibration && result.Calibrations != null )
            {
                //执行校准
                //
                this._xZTaskList = result.Calibrations;
            }
        }
        List<Task> _xZTaskList;

        private DataItem_C001 _lastReportData;//燃气表协议

        private bool _lastDowith = false;//上一条上报数据是否处理完成，已处理完成则丢弃
        /// <summary>
        /// 处理主动上报数据
        /// </summary>
        /// <param name="control"></param>
        /// <param name="data"></param>
        public void ReportData(string mac, byte control, byte[] data, IotProtocolType protocolType)
        {
            lock (this._meterLink)
            {
                this._reportWatch.Restart();
                CY.IoTM.Protocol.Frame frame = null;
                if (protocolType == IotProtocolType.RanQiBiao)
                {
                    //燃气表协议
                    //解密数据
                    byte[] desData = Encryption.Decry(data, MKey);
                    DataItem_C001 item_C001 = new DataItem_C001(desData);
                    if (this._lastReportData == null || this._lastReportData.ReadDate != item_C001.ReadDate)
                    {
                        this._lastReportData = item_C001;
                        _lastDowith = false;
                    }
                    //else if (_lastDowith)
                    //{
                    //    //上报的数据重复，并上条记录已处理过
                    //    return;
                    //}

                    //解析并应答
                    DataItem_C001_Answer itemAnswer = new DataItem_C001_Answer(item_C001.SER);
                    TaskArge taskArge = new TaskArge(mac, itemAnswer, ControlCode.CTR_7, MKey);
                    frame = new Protocol.Frame(taskArge);
                    if (this._reportWatch.ElapsedMilliseconds < Answer_YanShi)
                        Thread.Sleep(Answer_YanShi - (int)this._reportWatch.ElapsedMilliseconds);
                    this._meterLink.Send(frame.GetBytes());//应答数据
                    //检查上线的表是否登记到系统中
                    if (this.meter == null)
                    {
                        Log.getInstance().Write(MsgType.Information, "表：" + mac + " 在系统中未登记。");
                        Log.getInstance().Write(new OneMeterDataLogMsg(mac, "表：" + mac + "  在系统中未登记。"));
                        return;
                    }
                    Thread.Sleep(300);

                    //将接收到抄表数据提交给数据中心处理
                    Stopwatch _watch = new Stopwatch();
                    _watch.Start();
                    ReportData(item_C001);
                    //new Thread(new ParameterizedThreadStart(ReportData)).Start(item_C001);
                    Console.WriteLine("处理表：{0} 上报数据总用时：{1} 毫秒", mac, _watch.ElapsedMilliseconds);

                    //Log.getInstance().Write(MsgType.Information, "处理表：" + this.MAC + " 上报数据总用时：" + _watch.ElapsedMilliseconds + " 毫秒");
                    //Log.getInstance().Write(new OneMeterDataLogMsg(this.MAC, "处理表：" + this.MAC + " 上报数据总用时：" + _watch.ElapsedMilliseconds + " 毫秒" + "; 时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                    _lastDowith = true;
                    //_watch.Stop();
                }
                else if (protocolType == IotProtocolType.LCD)
                {
                    //LCD屏协议
                    if (data[0] == 0xc0 && data[1] == 0x02)
                    {

                        //下载
                        DataItem_C002 item_C002 = new DataItem_C002(data);
                        Log.getInstance().Write(new OneMeterDataLogMsg(mac, $"[{this._meterLink.socketHandle}]接收到LCD下载请求,文件名：{item_C002.FileName} 文件长度：{item_C002.FileLength} 请求段号：{item_C002.CurrentSegmentsIndex}"));

                        MemoryStream stream = new MemoryStream();
                        string str = ADFileCacheService.getInstance().ReadFileSeg(stream, item_C002.FileName.Substring(0, 4), item_C002.FileName, item_C002.FileLength, item_C002.TotalSegments, item_C002.CurrentSegmentsIndex, item_C002.DataLength);


                        DataItem answerItem = null;
                        TaskArge answerArge = null;
                        if (str != "")
                        {
                            //没有读取到要下载的文件,返回异常应答信息
                            answerItem = new DataItem_C002_Answer_Err(item_C002.SER);
                            answerArge = new TaskArge(mac, answerItem, ControlCode.CTR_8);
                            //TODO:记录异常应答记录
                        }
                        else
                        {
                            byte[] tmp = new byte[stream.Length];
                            stream.Position = 0;
                            stream.Read(tmp, 0, tmp.Length);
                            answerItem = new DataItem_C002_Answer(item_C002.SER, item_C002.FileName, item_C002.FileLength, item_C002.TotalSegments, item_C002.CurrentSegmentsIndex, tmp);
                            answerArge = new TaskArge(mac, answerItem, ControlCode.CTR_7, IotProtocolType.LCD);
                        }
                        //发送应答数据
                        frame = new Protocol.Frame(answerArge);
                        //if (this._reportWatch.ElapsedMilliseconds < Answer_YanShi)
                        //    Thread.Sleep(Answer_YanShi - (int)this._reportWatch.ElapsedMilliseconds);

                        this._meterLink.Send(frame.GetBytes());//应答数据
                        if (this.meter == null)
                        {
                            Log.getInstance().Write(MsgType.Information, "表：" + mac + " 在系统中未登记。");
                            Log.getInstance().Write(new OneMeterDataLogMsg(mac, "表：" + mac + "  在系统中未登记。"));
                            return;
                        }
                    }
                    else
                    {
                        Log.getInstance().Write(new OneMeterDataLogMsg(mac, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}接收到错误的请求指令：{data[0]} {data[1]} "));
                    }
                }
            }
        }

        /// <summary>
        /// 完成任务
        /// </summary>
        /// <param name="task"></param>
        private void TaskCompletes(Task task)
        {
            //WCFServiceProxy<ITaskManage> _iTaskManageProxy = new WCFServiceProxy<ITaskManage>();
            try
            {

                task.Finished = DateTime.Now;
                if (task.TaskType == TaskType.TaskType_充值 && task.TaskState == TaskState.Failed) return;//充值失败允许多次操作，所以不更新充值任务的队列状态
                this.meter = this.tms.GetMeter(meter.Mac);

                string result = this.tms.TaskCompletes(task,this._lastReportData.LJGas);
                //完成下列任务时需要重新加载表对象数据
                if (task.TaskType == TaskType.TaskType_点火)
                {
                    this.meter.MeterState = "0";
                    this.meter.IsDianHuo = true;
                    this.meter.MeterType = "01";
                    this.meter.NextSettlementPointGas = this.meter.LastTotal + this.meter.Gas1;
                    this.meter.SetNextSettlementDateTime();
                    this.tms.UpdateMeter(this.meter);
                }
                else if (task.TaskType == TaskType.TaskType_充值)
                {
                    DataItem_A013 a013 = this._currentCommand.getDataItem(this.meter.LastTopUpSer) as DataItem_A013;
                    this.meter.TotalTopUp += a013.BuyMoney;
                    this.meter.LastSettlementAmount += a013.BuyMoney;
                    this.meter.CurrentBalance += a013.BuyMoney;
                    if (result != "")
                    {
                        this.tms.UpdateMeter(this.meter);
                    }
                }
                else if (task.TaskType == TaskType.TaskType_设置结算日期)
                {
                    meter = this.tms.GetMeter(this.MAC);
                }
            }
            catch (Exception er)
            {
                Log.getInstance().Write(MsgType.Error, "任务完成状态更新失败，原因：" + er.Message);
                Log.getInstance().Write(new OneMeterDataLogMsg(this.MAC, "任务完成状态更新失败，原因：" + er.Message));
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            if (isRunning)
            {
                this.isRunning = false;
                this._semaphore.Close();
            }
        }

        #endregion         
    }
}
