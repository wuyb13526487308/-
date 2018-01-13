using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CY.IoTM.Common;
using CY.IoTM.Common.Business;
using CY.IoTM.Common.Item;
using CY.IoTM.Common.Item.读操作;
using CY.IoTM.Common.Protocol;
using CY.IoTM.MongoDataHelper;
using CY.IoTM.Protocol;
using MongoDB.Driver;
using CY.IoTM.Common.Item.上报数据;
using System.IO;

namespace CY.IoTM.MeterSimulator
{
    public class MSimulator:Meter,IDisposable
    {
        public string 下发指令 { get; set; }
        public string 应答指令 { get; set; }
        private System.Net.Sockets.TcpClient tcpClient;
        private bool IsRunning = true;
        private byte ser;
        private LCD _lcd;

        /// <summary>
        /// 下载文件队列
        /// </summary>
        private Queue<File> _downQueue = new Queue<File>();

        public void AddDownLoadTask(File file)
        {
            this._downQueue.Enqueue(file);
        }


        #region 表状态参数定义
        public ST1 _st1 = new ST1();
        public ST2 _st2 = new ST2();
        #endregion
        #region 计费的中间参数定义

        public bool 出厂状态 = true;

        /// <summary>
        /// 上次充值时的SER
        /// </summary>
        public byte lastBuySer = 0;

        public decimal syMoney;

        public bool IsOnline { get; set; }

        private decimal hll;
        /// <summary>
        /// 小时流量
        /// </summary>
        public decimal hourLiuLiang
        {
            get
            {
                if (this.hll == -1)
                {
                    return new Random(DateTime.Now.Millisecond).Next(0, 10);
                }
                else
                {
                    return this.hll;
                }
            }
            set
            {
                this.hll = value;
                if (this.OnJiliang != null)
                    this.OnJiliang(this);
            }
        }
        #endregion
        #region 历史记录
        public DataItem_D120 _item_01 = new DataItem_D120();
        public DataItem_D121 _item_02 = new DataItem_D121();
        public DataItem_D122 _item_03 = new DataItem_D122();
        public DataItem_D123 _item_04 = new DataItem_D123();
        public DataItem_D124 _item_05 = new DataItem_D124();
        public DataItem_D125 _item_06 = new DataItem_D125();
        public DataItem_D126 _item_07 = new DataItem_D126();
        public DataItem_D127 _item_08 = new DataItem_D127();
        public DataItem_D128 _item_09 = new DataItem_D128();
        public DataItem_D129 _item_10 = new DataItem_D129();
        public DataItem_D12A _item_11 = new DataItem_D12A();
        public DataItem_D12B _item_12 = new DataItem_D12B();
        #endregion
        #region 通知事件
        public event AlertsHandle OnNoticed;
        public event AlertsHandle OnJiliang;
        #endregion

        private Stopwatch _reportWatch;
        private string _message;
        public string Message
        {
            get
            {
                return this._message;
            }
        }
        private byte[] MKey
        {
            get
            {
                return strToToHexByte(this.Key);
            }
        }

        public MSimulator()
        {
            this._lcd = new LCD(this);
        }

        public LCD LCD { get { return this._lcd; } }

        public string AnswerType
        {
            get;
            set;
        }


        public static MSimulator Create(string mac,string host,int port,int zhouqi = 5)
        {
            MSimulator _simulator = Query(mac);
            if (_simulator == null)
            {
                _simulator = new MSimulator();
                _simulator.hostname = host;
                _simulator.port = port;
                _simulator.周期 = zhouqi;
                _simulator.Mac = mac;
                _simulator.Key = "8888888888888888";
                _simulator.MeterType = "00";
                _simulator.MKeyVer = 0;
                _simulator.PriceCheck = "0";
                _simulator.SettlementDay = 28;
                _simulator.SettlementType = "00";
                _simulator.TotalAmount = 0;
                _simulator.TotalTopUp = 0;
                _simulator.ValveState = "0";
                _simulator.SettlementDateTime = "2015-01-01";
                _simulator.Insert();
            }
            return _simulator;
        }
        private static MSimulator Query(string mac)
        {
            MongoDBHelper<MSimulator> mongo = new MongoDBHelper<MSimulator>();
            QueryDocument query = new QueryDocument();
            query.Add("Mac", mac);
            MongoCursor<MSimulator> mongoCursor = mongo.Query(TaskCollectionName, query);
            var dataList = mongoCursor.ToList();
            if (dataList == null || dataList.Count == 0)
                return null;
            else
                return dataList[0];
        }

        public void Start()
        {
            this._reportWatch = new Stopwatch();
            this._reportWatch.Start();
            if (this.OnJiliang != null)
                this.OnJiliang(this);

            new Thread(Dowith).Start();

            new Thread(JiLiang).Start();
            
        }

        //计量线程
        private void JiLiang()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (IsRunning)
            {
                if (watch.ElapsedMilliseconds >= 1000 * 10)
                {
                    _semaphore.WaitOne();

                    if(this.ValveState =="0")
                        this.TotalAmount += Convert.ToDecimal ((this.hourLiuLiang /120).ToString ("0.00"));
                    //如果为金额表则进行计量
                    if (this.MeterType == "01")
                    {
                        Calculate(this);

                        if (this.CurrentBalance <= 0)
                        {
                            this.ValveState = "1";
                            this._st1.ValveStatu = false;

                        }
                        else
                        {
                            this.ValveState = "0";
                            this._st1.ValveStatu = true;

                        }

                        //判断是否到结算日 
                        if (Jiange(this.GetSettlementTimePoint()) >= 0)
                        {
                            IsAtOnce = true;
                            this.SetNextSettlementDateTime();// = NextSettlementDateTime(this.SettlementType, this.SettlementDay, this.SettlementMonth).AddHours(8);
                            this.LastTotal = TotalAmount;
                            this.LastSettlementAmount = this.CurrentBalance;
                            this.CurrentLadder = 1;
                            this.CurrentPrice = this.Price1;
                            if (this.IsUsedLadder && this.CurrentLadder < this.Ladder)
                            {
                                //设置第1个节点的结算点气量
                                this.NextSettlementPointGas = this.LastTotal + this.Gas1;
                            }
                            else
                            {
                                //下一次结算点气量为无穷大
                                this.NextSettlementPointGas = -1;
                            }
                        }
                        //处理调价计划
                        TiaoJiaDowith();
                    }
                    this.Update();
                    _semaphore.Release();

                    watch.Restart();
                    if (this.OnJiliang != null)
                        this.OnJiliang(this);
                }
                Thread.Sleep(100);
            }
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


        //计费
        private decimal Calculate(Meter meter)
        {
            decimal amount = 0;
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
            
            //计算当前阶段用气量
            ReCalulate:
            decimal JieDuanYongQiLiang = this.TotalAmount - this.LastGasPoint;
            if (this.NextSettlementPointGas != -1 && this.TotalAmount >= this.NextSettlementPointGas)
            {
                //已到达阶梯结算点
                JieDuanYongQiLiang = this.NextSettlementPointGas - this.LastGasPoint;
                this.CurrentBalance -= JieDuanYongQiLiang * this.CurrentPrice;
                this.LastGasPoint = this.NextSettlementPointGas;

                if (this.CurrentLadder < this.Ladder)
                    this.CurrentLadder++;

                this.CurrentPrice = prices[this.CurrentLadder - 1];
                //重新设置下一个结算点
                if (this.CurrentLadder == this.Ladder)
                {
                    //已到达最大阶梯
                    this.NextSettlementPointGas = -1;
                }
                else
                {
                    this.NextSettlementPointGas += gas[this.CurrentLadder - 1];
                }
                goto ReCalulate;
            }
            else
            {
                //未到达结算点或已超过最后阶梯
                this.CurrentBalance -= JieDuanYongQiLiang * this.CurrentPrice;
                this.LastGasPoint = this.TotalAmount;
            }        
            
            return Convert.ToDecimal(amount.ToString("0.00"));
        }

        /// <summary>
        /// 调价处理，如存在调价则处理
        /// </summary>
        private void TiaoJiaDowith()
        {
            //检查是否存在调价计划， 如存在并调价启动时间已到达，则将新的价格写入执行体系中（即修改价格对应的变量值），设置成功，删除调价计划数据．
            this.PricingPlan = this.QueryPricingPlan(this.Mac);

            if (this.PricingPlan != null)
            {
                decimal[] prices = new decimal[5];
                decimal[] gas = new decimal[5];
                //存在调价计划
                DateTime pricingDate = Convert.ToDateTime(this.PricingPlan.UseDate);
                if (Jiange(pricingDate) >= 0)
                {
                    IsAtOnce = true;
                    //调价启用时间到
                    this.IsUsedLadder = this.PricingPlan.IsUsedLadder;
                    this.Ladder = this.PricingPlan.Ladder;
                    this.Price1 = this.PricingPlan.Price1;
                    this.Gas1 = this.PricingPlan.Gas1;
                    this.Price2 = this.PricingPlan.Price2;
                    this.Gas2 = this.PricingPlan.Gas2;
                    this.Price3 = this.PricingPlan.Price3;
                    this.Gas3 = this.PricingPlan.Gas3;
                    this.Price4 = this.PricingPlan.Price4;
                    this.Gas4 = this.PricingPlan.Gas4;
                    this.Price5 = this.PricingPlan.Price5;
                    prices[0] = this.Price1;
                    prices[1] = this.Price2;
                    prices[2] = this.Price3;
                    prices[3] = this.Price4;
                    prices[4] = this.Price5;
                    gas[0] = this.Gas1;
                    gas[1] = this.Gas2;
                    gas[2] = this.Gas3;
                    gas[3] = this.Gas4;
                    gas[4] = -1;

                    //结算周期
                    this.SettlementType = this.PricingPlan.SettlementType;

                    //设置当前计费价格
                    if (this.IsUsedLadder)
                    {
                        this.NextSettlementPointGas = this.LastTotal;

                        for (int i = 0; i < this.Ladder; i++)
                        {
                            this.NextSettlementPointGas += gas[i];
                            this.CurrentPrice = prices[i];
                            this.CurrentLadder = i + 1;
                            if (this.NextSettlementPointGas > this.TotalAmount)
                            {
                                break;
                            }
                        }

                        //this.CurrentPrice = prices[this.CurrentLadder - 1];
                        //this.NextSettlementPointGas = this.LastTotal;
                        //if (this.CurrentLadder < this.Ladder)
                        //{
                        //    for (int i = 0; i < this.CurrentLadder; i++)
                        //        this.NextSettlementPointGas += gas[this.CurrentLadder - 1];
                        //}
                        //else
                        //{
                        //    this.NextSettlementPointGas = -1;
                        //}
                        this.SetNextSettlementDateTime();
                    }
                    else
                    {
                        //新价格未启用阶梯价
                        this.CurrentPrice = this.Price1;
                        this.NextSettlementPointGas = -1;
                    }
                    //删除调价计划
                    this.DeletePlan(this.Mac);
                }
                this.PricingPlan = null;
            }   
        }

        /// <summary>
        /// 换表时初次阶梯计算
        /// </summary>
        /// <param name="meter"></param>
        /// <returns></returns>
        private decimal ChangeCalculate(Meter meter,decimal syMoney)
        {
            //计算当前阶段用气量
            decimal JieDuanYongQiLiang = this.TotalAmount - this.LastTotal;
            decimal[] prices = new decimal[5];
            prices[0] = meter.Price1;
            prices[1] = meter.Price2;
            prices[2] = meter.Price3;
            prices[3] = meter.Price4;
            prices[4] = meter.Price5;
            decimal[] gas = new decimal[5];
            gas[0] = meter.Gas1;
            gas[1] = meter.Gas2;
            gas[2] = meter.Gas3;
            gas[3] = meter.Gas4;
            gas[4] = -1;
            decimal currentPirce = meter.Price1;
            decimal currentGas = meter.Gas1;
            decimal amount = meter.LastSettlementAmount;

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
                        amount -= currentGas * currentPirce;
                        JieDuanYongQiLiang -= currentGas;
                        iLadder++;
                    }
                    else if (JieDuanYongQiLiang > 0)
                    {
                        amount -= JieDuanYongQiLiang * currentPirce;
                        JieDuanYongQiLiang = 0;
                        break;
                    }
                }
                if (JieDuanYongQiLiang > 0)
                {
                    //计算最后一个阶梯
                    currentPirce = prices[iLadder - 1];
                    amount -= JieDuanYongQiLiang * currentPirce;
                }
                this.CurrentLadder = iLadder;
                this.CurrentPrice = currentPirce;
                meter.NextSettlementPointGas = this.TotalAmount + this.LastTotal;
                //计算下一个阶梯的的计算点
                if (meter.CurrentLadder < meter.Ladder)
                {
                    for (int i = 0; i < meter.CurrentLadder; i++)
                    {
                        meter.NextSettlementPointGas += gas[i];
                    }
                }
                else
                {
                    meter.NextSettlementPointGas = -1;
                }
                meter.SetNextSettlementDateTime();
            }
            else
            {
                //未使用阶梯价
                amount -= JieDuanYongQiLiang * currentPirce;
                this.CurrentLadder = 1;
                this.CurrentPrice = currentPirce;

            }
            meter.CurrentBalance = syMoney;
            return meter.CurrentBalance;
        }

        /// <summary>
        /// 立即上报标记，用于调价点、结算点
        /// </summary>
        private bool IsAtOnce = false;
        private void Dowith()
        {
            ReportData();
            while (IsRunning)
            {
                if (this._reportWatch.ElapsedMilliseconds >= this.周期 * 60 * 1000 || IsAtOnce)
                {
                    //到达上报周期
                    ReportData();
                    this.IsAtOnce = false;
                }
                Thread.Sleep(1000);
            }
        }
        Semaphore _semaphore = new Semaphore(1, 1);

        private bool LoadFile(Stopwatch watch, Socket socket)
        {
            NextFile:
            if (this._downQueue.Count > 0)
            {
                File file = this._downQueue.Dequeue();
                if (file.State == FileState.Normal) goto NextFile;
                Notice(string.Format("{1:HH:mm:ss.fff} 开始下载文件【{0}】\r", file.FileName, DateTime.Now));

                MemoryStream ms = new MemoryStream();
                byte[] tmp = file.Data;
                if (tmp != null) ms.Write(tmp, 0, tmp.Length);
                NextFileSeg:
                watch.Restart();

                DataItem_C002 item_C002 = new DataItem_C002(ser++, file.FileName, file.Length, file.TotalSeg, file.segIndex + 1, 1024);

                TaskArge taskArge = new TaskArge(this.Mac, item_C002, ControlCode.CTR_6,IotProtocolType.LCD, MKey);
                Frame frame = new Frame(taskArge);
                int iTime = 1;
                byte[] buffer = frame.GetBytes();
                while (socket.Poll(100, SelectMode.SelectWrite))
                {
                    socket.Send(buffer);
                    break;
                }
                Notice(string.Format("{1:HH:mm:ss.fff} 第{0}次发送下载文件第[{2}]段请求帧\r", iTime, DateTime.Now, file.segIndex + 1));
                string message;
                //等待应答
                while (true)
                {
                    if (socket.Poll(1000, SelectMode.SelectRead))
                    {
                        int dLength = socket.Available;
                        if (dLength != socket.Available)
                            dLength = socket.Available;
                        buffer = new byte[dLength];
                        socket.Receive(buffer);
                        Notice(string.Format("{1:HH:mm:ss.fff} 接收到主站传送的第[{2}]段文件数据：{0}\r", MyDataConvert.BytesToHexStr(buffer), DateTime.Now, file.segIndex + 1));

                        taskArge = JieXi(buffer, out message);

                        if (taskArge != null && taskArge.ControlCode == ControlCode.CTR_7)
                        {
                            DataItem_C002_Answer c002 = (taskArge.Data as DataItem_C002_Answer);
                            if (c002.CurrentSegmentsIndex == file.segIndex + 1)
                            {
                                tmp = c002.getFileSegment;
                                ms.Write(tmp, 0, tmp.Length);
                                file.segIndex++;
                            }
                            else
                            {
                                //文件传送失败
                                Notice(string.Format ("=============================\r{2:HH:mm:ss.fff} 接收到错误的数据段,Rev SegIndex={0} Request SegIndex = {1}\r==================================\r", c002.CurrentSegmentsIndex.ToString(),file.segIndex + 1));
                            }
                            if (file.segIndex == file.TotalSeg)
                            {
                                //文件传送完成
                                tmp = new byte[ms.Length];
                                ms.Position = 0;
                                ms.Read(tmp, 0, tmp.Length);
                                ms.Close();
                                file.State = FileState.Normal;
                                file.Data = tmp;
                                file.Update();
                                Notice(string.Format("文件{0}下载完成。\r", file.FileName));
                                FileStream fs = new FileStream(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\" + file.FileName, FileMode.Create);
                                fs.Write(tmp, 0, tmp.Length);
                                fs.Close();
                                goto NextFile;
                            }
                            else
                            {
                                goto NextFileSeg;
                            }
                        }

                        this._reportWatch.Restart();
                        break;
                    }

                    if (watch.ElapsedMilliseconds >= 1000 * 10)
                    {
                        if (iTime >= 3)
                        {
                            //接收不到主站应答，本次上报结束
                            tmp = new byte[ms.Length];
                            ms.Position = 0;
                            ms.Read(tmp, 0, tmp.Length);
                            ms.Close();
                            file.State = FileState.Loading;
                            if (tmp.Length > 0)
                            {
                                file.Data = tmp;
                                file.Update();
                            }
                            Notice(string.Format("文件{0}下载中断，已完成{1}%\r", file.FileName,tmp.Length/(file.Length *1.0)));

                            this.tcpClient.Close();
                            this.tcpClient = null;
                            this.IsOnline = false;
                            return false;
                        }
                        iTime++;
                        buffer = frame.GetBytes();
                        socket.Send(buffer);
                        Notice(string.Format("{1:HH:mm:ss.fff} 第{0}次发送下载文件第[{2}]段请求帧\r", iTime, DateTime.Now, file.segIndex + 1));
                        watch.Restart();
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 上报数据并接收主站指令
        /// </summary>
        private void ReportData()
        {
            //随机延时
            Thread.Sleep(new Random().Next(500,1000) * 10);

            //FE FE 68 30 39 61 51 23 35 51 04 21 1E C0 01 20 40 14 16 22 04 15 20 18 56 42 37 18 56 02 00 18 56 02 00 18 56 02 00 23 00 00 00 6F 16
            DataItem_C001 item_C001 = new DataItem_C001(ser++, DateTime.Now, this.TotalAmount, LJMoney, CurrentBalance, this.LastTotal, _st1, _st2);
            TaskArge taskArge = new TaskArge(this.Mac, item_C001, ControlCode.CTR_6, MKey);
            //连接服务器
            Stopwatch watch = new Stopwatch();
            watch.Start();
            byte[] buffer = null;
            try
            {
                this.tcpClient = new System.Net.Sockets.TcpClient();
                this.tcpClient.Connect(this.hostname, this.port);
                Socket socket = tcpClient.Client;
                Notice(string.Format("{0} 表连接服务器完成.\r", DateTime.Now.ToString("MM-dd HH:mm:ss.fff")));
                this.IsOnline = true;

                if (!ReportToZhuZhan(watch,socket))
                {
                    return;
                }

                Frame frame = new Frame(taskArge);
                string message;
                //等待主站指令
                Notice(string.Format("===等待主站指令===\r"));

                watch.Restart();
                while (true)
                {
                    if (watch.ElapsedMilliseconds >= 1000 * 30)
                    {
                        //20秒内没有收到任何主站指令，关闭连接并结束本次通信
                        this.tcpClient.Close();
                        this.IsOnline = false;
                        Notice(string.Format("{0} 关闭表连接\r\n",DateTime.Now.ToString ("MM-dd HH:mm:ss.fff")));
                        this._reportWatch.Restart();
                        break;
                    }

                    if (watch.ElapsedMilliseconds >= 1000 * 5 && this._downQueue.Count >0)
                    {
                        //如何在5秒内没有上位机指令，检查文件下载队列
                        if(!this.LoadFile(watch, socket))
                            return;
                    }
                    //if (this._reportWatch.ElapsedMilliseconds >= this.周期 * 60 * 1000)
                    //{
                    //    //上报数据周期到
                    //    watch.Restart();
                    //    if (!ReportToZhuZhan(watch,socket))
                    //    {
                    //        return;
                    //    }
                    //}
                    //检查数据
                    if (socket.Poll(10, SelectMode.SelectRead))
                    {
                        int dLength = socket.Available;
                        Thread.Sleep(100);
                        if (dLength != socket.Available)
                            dLength = socket.Available;
                        buffer = new byte[dLength];
                        socket.Receive(buffer);
                        Notice(string.Format("{1:HH:mm:ss.fff} 接收到主站数据：{0}\r", MyDataConvert.BytesToHexStr(buffer),DateTime.Now));
                        //_semaphore.WaitOne();
                        //解析接收到的数据
                        try
                        {
                            taskArge = JieXi(buffer, out message);
                            //_semaphore.Release();

                            if (taskArge != null && (((byte)taskArge.ControlCode) & 0x80) == 0x00 && taskArge.ControlCode != ControlCode.CTR_7 && taskArge.ControlCode != ControlCode.CTR_8)
                            {
                                Notice(string.Format("控制码：{0} 指令：{1}\r", taskArge.ControlCode, taskArge.Data.IdentityCode));

                                //接收到主站请求数据
                                taskArge = CreateAnswerData(taskArge);
                                frame = new Frame(taskArge);
                                buffer = frame.GetBytes();
                                socket.Send(buffer);
                                Notice(string.Format("{0:HH:mm:ss.fff} 从站应答完成。\r", DateTime.Now));
                                watch.Restart();

                            }
                        }
                        catch (Exception ex)
                        {
                            //_semaphore.Release();
                            throw ex;
                        }

                    }
                }
            }
            catch(Exception e)
            {
                Notice(string.Format("{1:HH:mm:ss.fff} 程序异常：{0}\r", e.Message,DateTime.Now));

                Debug.Print(e.Message);
            }
        }

        private bool ReportToZhuZhan(Stopwatch watch, Socket socket)
        {
            watch.Start();
            DataItem_C001 item_C001 = new DataItem_C001(ser++, DateTime.Now, this.TotalAmount, this.TotalTopUp - this.CurrentBalance, this.CurrentBalance, this.LastTotal,this._st1, this._st2);
            TaskArge taskArge = new TaskArge(this.Mac, item_C001, ControlCode.CTR_6, MKey);
            Frame frame = new Frame(taskArge);
            int iTime = 1;
            byte[] buffer = frame.GetBytes();
            while (socket.Poll(100, SelectMode.SelectWrite))
            {
                socket.Send(buffer);
                break;
            }
            Notice(string.Format("{1:HH:mm:ss} 第{0}次发送上报数据请求帧\r{2}\r", iTime,DateTime.Now, MyDataConvert.BytesToHexStr(buffer)));

            string message;
            //等待应答
            while (true)
            {
                Thread.Sleep(100);
                if (socket.Poll(10, SelectMode.SelectRead) && socket.Available >0)
                {
                    int dLength = socket.Available;
                    Thread.Sleep(100);
                    if (dLength != socket.Available)
                        dLength = socket.Available;
                    buffer = new byte[dLength];
                    socket.Receive(buffer);
                    Notice(string.Format("{1:HH:mm:ss}接收到主站应答数据：{0}\r", MyDataConvert.BytesToHexStr(buffer), DateTime.Now));

                    taskArge = JieXi(buffer, out message);
                    if (taskArge != null && taskArge.ControlCode == ControlCode.CTR_7)
                        Notice(string.Format("第{0}次发送上报数据请求完成。\r", iTime));
                    Notice(string.Format("本次上报数据总用时：{0}毫秒\r", watch.ElapsedMilliseconds));
                    this._reportWatch.Restart();
                    break;
                }
                else if(socket.Poll(100,SelectMode.SelectError))
                {
                }
                if (watch.ElapsedMilliseconds >= 1000 * 10)
                {
                    if (iTime >= 3)
                    {
                        //接收不到主站应答，本次上报结束
                        this.tcpClient.Close();
                        this.tcpClient = null;
                        this.IsOnline = false;
                        Notice(string.Format("等待应答超时，关闭连接\r\n"));
                        return false;
                    }
                    iTime++;
                    buffer = frame.GetBytes();
                    socket.Send(buffer);
                    Notice(string.Format("{1:HH:mm:ss}接收到主站应答数据：{0}\r", MyDataConvert.BytesToHexStr(buffer), DateTime.Now));
                    watch.Restart();
                }
            }

            return true;
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="message"></param>
        private void Notice(string message)
        {
            this._message = message;
            if (this.OnNoticed != null)
                this.OnNoticed(this);
        }

        /// <summary>
        /// 处理主站的读数据请求，并返回应答数据
        /// </summary>
        /// <param name="identityCode"></param>
        /// <param name="ser"></param>
        /// <returns></returns>
        private DataItem DowithReadData(IdentityCode identityCode, byte ser)
        {
            DataItem item = null;
            switch (identityCode)
            {
                case IdentityCode.读计量数据:
                    item = new DataItem_901F_ANSWER(ser, DateTime.Now, this.TotalAmount, this.LJMoney, this.CurrentBalance, this.LastTotal, _st1, _st2);
                    break;
                case IdentityCode.历史计量数据1:
                    item = this._item_01;
                    item.SER = ser;
                    break;
                case IdentityCode.历史计量数据2:
                    item = this._item_02;
                    item.SER = ser;
                    break;
                case IdentityCode.历史计量数据3:
                    item = this._item_03;
                    item.SER = ser;
                    break;
                case IdentityCode.历史计量数据4:
                    item = this._item_04;
                    item.SER = ser;
                    break;
                case IdentityCode.历史计量数据5:
                    item = this._item_05;
                    item.SER = ser;
                    break;
                case IdentityCode.历史计量数据6:
                    item = this._item_06;
                    item.SER = ser;
                    break;
                case IdentityCode.历史计量数据7:
                    item = this._item_07;
                    item.SER = ser;
                    break;
                case IdentityCode.历史计量数据8:
                    item = this._item_08;
                    item.SER = ser;
                    break;
                case IdentityCode.历史计量数据9:
                    item = this._item_09;
                    item.SER = ser;
                    break;
                case IdentityCode.历史计量数据10:
                    item = this._item_10;
                    item.SER = ser;
                    break;
                case IdentityCode.历史计量数据11:
                    item = this._item_11;
                    item.SER = ser;
                    break;
                case IdentityCode.历史计量数据12:
                    item = this._item_12;
                    item.SER = ser;
                    break;
                default:
                    break;
            }
            return item;
        }

        /// <summary>
        /// 处理主站请求的写操作，并返回应答数据对象
        /// </summary>
        /// <param name="identityCode"></param>
        /// <param name="ser"></param>
        /// <param name="oldItem"></param>
        /// <returns></returns>
        private DataItem DowithWriteData(IdentityCode identityCode, byte ser,DataItem oldItem)
        {
            //在该方法中将写入数据设置到响应变量中
            DataItem item = null;
            switch (identityCode)
            {
                case IdentityCode.写价格表:
                    DataItem_A010 item_10 = oldItem as DataItem_A010;
                    this.MeterType = ((byte)item_10.CT.CTMeterType).ToString().PadLeft(2, '0');
                    if (出厂状态 && item_10.CT.CTMeterType == Common.Item.MeterType.金额表)
                    {
                        this.出厂状态 = false;
                        this.IsUsedLadder = item_10.CT.CTIsLadder;
                        this.Ladder = item_10.CT.CTLadderNum;
                        this.Price1 = item_10.Price1;
                        this.Gas1 = item_10.UseGas1;
                        this.Price2 = item_10.Price2;
                        this.Gas2 = item_10.UseGas2;
                        this.Price3 = item_10.Price3;
                        this.Gas3 = item_10.UseGas3;
                        this.Price4 = item_10.Price4;
                        this.Gas4 = item_10.UseGas4;
                        this.Price5 = item_10.Price5;
                        this.SettlementType = ((byte)item_10.CT.CTJieSuanType).ToString().PadLeft(2, '0');//结算类型
                        //设置初次转换为金额表的当前计价参数
                        this.CurrentLadder = 1;
                        this.CurrentPrice = this.Price1;
                        this.LastGasPoint = this.TotalAmount;//上次结算时表底
                        this.LastTotal = this.TotalAmount;
                        this.NextSettlementPointGas = this.TotalAmount;
                        if (this.IsUsedLadder && this.Ladder > this.CurrentLadder)
                        {
                            //计算下一个结算点
                            this.NextSettlementPointGas = this.Gas1;
                            this.SetNextSettlementDateTime();
                        }
                        else
                        {
                            //非阶梯价或无阶梯,-1表示正无穷大
                            this.NextSettlementPointGas = -1;
                        }
                    }
                    else if (!出厂状态 && item_10.CT.CTMeterType == Common.Item.MeterType.金额表)
                    {
                        //调价
                        PricingPlan plan = new PricingPlan();

                        plan.IsUsedLadder = item_10.CT.CTIsLadder;
                        plan.Ladder = item_10.CT.CTLadderNum;
                        plan.Price1 = item_10.Price1;
                        plan.Gas1 = item_10.UseGas1;
                        plan.Price2 = item_10.Price2;
                        plan.Gas2 = item_10.UseGas2;
                        plan.Price3 = item_10.Price3;
                        plan.Gas3 = item_10.UseGas3;
                        plan.Price4 = item_10.Price4;
                        plan.Gas4 = item_10.UseGas4;
                        plan.Price5 = item_10.Price5;
                        plan.UseDate = item_10.StartDate.ToString();
                        plan.MeterType = ((byte)item_10.CT.CTMeterType).ToString();
                        plan.SettlementType = ((byte)item_10.CT.CTJieSuanType).ToString().PadLeft(2, '0');//结算类型
                        plan.MeterNo = this.Mac;
                        this.AddPlan(plan);
                    }
                    item = new DataItem_A010_Answer(ser, _st1);
                    break;
                case IdentityCode.写结算日:
                    DataItem_A011 item_11 = oldItem as DataItem_A011;

                    this.SettlementDay = item_11.JieSuanDay;
                    this.SettlementMonth = item_11.JieSuanMonth;
                    SetNextSettlementDateTime();
                    item = new DataItem_A011_Answer(ser);
                    break;
                case IdentityCode.写购入金额:
                    DataItem_A013 item_13 = oldItem as DataItem_A013;
                    Thread.Sleep(8000);
                    if (this.lastBuySer != ser)
                    {
                        if (this.lastBuySer != ser)
                        {
                            this.LastSettlementAmount += item_13.BuyMoney;//写入购买金额
                            this.TotalTopUp += item_13.BuyMoney;
                            this.CurrentBalance += item_13.BuyMoney;
                            this.lastBuySer = ser;
                        }
                    }
                    item = new DataItem_A013_ASK(ser, item_13.BuyIndex, item_13.BuyMoney);
                    break;
                case IdentityCode.写新密钥:
                    DataItem_A014 item_14 = oldItem as DataItem_A014;
                    this.Key = item_14.Key;
                    this.MKeyVer = item_14.Version;
                    item = new DataItem_A014_ASK(ser, item_14.Version);
                    break;
                case IdentityCode.写标准时间:
                    item = new DataItem_A015_ASK(ser);
                    break;
                case IdentityCode.写阀门控制:
                    DataItem_A017 item_17 = oldItem as DataItem_A017;
                    this.ValveState = item_17.IsOpen == true ? "0" : "1";
                    this._st1.ValveStatu = item_17.IsOpen;
                    item = new DataItem_A017_ASK(ser, this._st1);
                    break;
                case IdentityCode.出厂启用:
                    item = new DataItem_A019(ser);
                    break;
                case IdentityCode.写地址:
                    item = new DataItem_A018_Answer(ser);
                    break;
                default:
                    break;
            }
            Update();
            return item;
        }
        /// <summary>
        /// 处理主站请求的创源参数设置操作并返回应答数据
        /// </summary>
        /// <param name="identityCode"></param>
        /// <param name="ser"></param>
        /// <param name="oldItem"></param>
        /// <returns></returns>
        private DataItem DowithCYWriteData(IdentityCode identityCode, byte ser, DataItem oldItem)
        {
            DataItem item = null;
            switch (identityCode)
            {
                case IdentityCode.设置上传周期:
                    item = new DataItem_C105_Answer(ser);
                    break;
                case IdentityCode.设置切断报警参数:
                    item = new DataItem_C103_Answer(ser, this._st1, this._st2);
                    break;
                case IdentityCode.设置服务器信息:
                    item = new DataItem_C104_Answer(ser);
                    break;
                case IdentityCode.修正表数据:
                    item = new DataItem_C102_Answer(ser);
                    DataItem_C102 item_C102 = oldItem as DataItem_C102;

                    this.TotalTopUp = item_C102.CurrentDaySYMoney + item_C102.CurrentLJMoney;//总充值金额
                    this.LJMoney = item_C102.CurrentLJMoney;//累计金额
                    this.LastTotal = item_C102.SettlementDayLJGas;//上次结算日表底
                    this.CurrentBalance = item_C102.CurrentDaySYMoney;
                    break;
                case IdentityCode.换表:
                    item = new DataItem_C107_Answer(ser);
                    DataItem_C107 item_C107 = oldItem as DataItem_C107;
                    //本阶梯周期已使用累计用量
                    //当前剩余金额
                    this.LastTotal = this.TotalAmount - item_C107.LJGas;
                    //计算气费，将价格调整的合适的阶梯
                    this.ChangeCalculate(this, item_C107.SYMoney);
                    this.TotalTopUp = item_C107.SYMoney;
                    this.CurrentBalance = item_C107.SYMoney;
                    break;
                case IdentityCode.发送广告:
                    item = new DataItem_C109_Answer(ser,"1.00");

                    break;
            }
            Update();
            return item;
        }


        /// <summary>
        /// 根据主站请求数据，从站创建应答数据对象
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private TaskArge CreateAnswerData(TaskArge e)
        {
            ControlCode ctrCode = e.ControlCode;
            DataItem item = null;
            ControlCode answerCtrCode = ControlCode.ReadData_Success;
            byte[] currentKey = this.MKey;//本次发送数据使用的密钥
            switch (ctrCode)
            {
                case ControlCode.ReadData:
                    //主站请求的读数据操作
                    item = DowithReadData(e.Data.IdentityCode, e.Data.SER);
                    answerCtrCode = ControlCode.ReadData_Success;
                    break;
                case ControlCode.WriteData:
                    //主站请求的写数据操作
                    if (this.AnswerType == null || this.AnswerType == "" || this.AnswerType == "正常应答")
                    {
                        item = DowithWriteData(e.Data.IdentityCode, e.Data.SER, e.Data);
                        answerCtrCode = ControlCode.WriteData_Answer;//正常应答
                        if (e.Data.IdentityCode == IdentityCode.写新密钥)
                        {
                            for (int i = 0; i < 8; i++)
                                currentKey[i] = 0x88;
                        }
                    }
                    else
                    {
                        answerCtrCode = ControlCode.WriteData_Failed;
                        item = new DataItem_Answer(e.Data.SER, e.Data.IdentityCode);
                    }
                    break;
                case ControlCode.ReadKeyVersion:
                    //主站读从站密钥版本
                    item = new DataItem_8106(e.Data.SER, this.MKeyVer);
                    break;
                case ControlCode.ReadMeterAdress:
                    //读地址（仅用于单机）
                    break;
                case ControlCode.WriteMeterAdress:
                    break;
                case ControlCode.WriteMeterNum:
                    break;
                case ControlCode.CYReadData:
                    //item = this.DowithCYWriteData(e.Data.IdentityCode, e.Data.SER, e.Data);
                    //answerCtrCode = ControlCode.CYWriteData_Answer;//正常应答
                    break;
                case ControlCode.CYWriteData:
                    //处理创源写数据
                    if (this.AnswerType == null || this.AnswerType == "" || this.AnswerType == "正常应答")
                    {
                        item = DowithCYWriteData(e.Data.IdentityCode, e.Data.SER, e.Data);
                        answerCtrCode = ControlCode.CYWriteData_Answer;
                    }
                    else
                    {
                        answerCtrCode = ControlCode.CTR_11;
                        item = new DataItem_Answer(e.Data.SER, e.Data.IdentityCode);
                    }
                    break;
                default:
                    break;
            }
            return new TaskArge(this.Mac, item, answerCtrCode,e.IotProtocolType, currentKey);

        }

        private TaskArge JieXi(byte[] buffer,out string message)
        {
            message = "";
            int index = 0;//查找报文头
            for (int i = 0; i < buffer.Length - 2; i++)
            {
                if (buffer[i] == 0XFE && buffer[i + 1] == 0X68)
                {
                    index = ++i;
                    break;
                }
            }
            //检查仪表类型是否为燃气表，不是燃气表，抛弃
            IotProtocolType pType = (IotProtocolType)buffer[index + 1];
            ControlCode ctrCode = (ControlCode)buffer[index + 9];

            int iOffset = 11; //定义从包头（0x68）到数据区域字节数

            if (pType != IotProtocolType.LCD && pType != IotProtocolType.RanQiBiao)
            {
                message = "表类型错误";
                return null;
            };

            int DataLength = DataLength = buffer[index + 10];
            if (pType == IotProtocolType.RanQiBiao)
            {
                DataLength = buffer[index + 10];
            }
            else if (pType == IotProtocolType.LCD)
            {
                iOffset = 12;//LCD协议中，数据长度字段修改为2个字节，所以偏移量为12
                DataLength += buffer[index + 11] << 8;
            }

            byte checkSum = 0;

            //检查校验码
            for (int i = index; i < index + DataLength + iOffset; i++)
            {
                checkSum += Convert.ToByte(buffer[i]);
            }
            if (checkSum != buffer[index + iOffset + DataLength])
            {
                //校验和错误
                message = "校验和错误";
                return null;
            }
            //取出数据部分（该数据为加密数据）
            byte[] DATA = new byte[DataLength];
            for (int i = 0; i < DataLength; i++)
            {
                DATA[i] = buffer[i + index + iOffset];
            }


            StringBuilder sb = new StringBuilder();
            for (int i = index + 8; i > index + 1; i--)
            {
                sb.Append(string.Format("{0:X2}", buffer[i]));
            }
            if (this.Mac != sb.ToString())
            {
                 message = string.Format("接收到数据不是给表：{0}的应答数据或请求指令。", this.Mac);
                return null;
            }

            // 解密数据
            byte[] desData = null;// Encryption.Decry(DATA, MKey);
            if (ctrCode == ControlCode.ReadKeyVersion || pType == IotProtocolType.LCD)
            {
                //读取密钥为明文传输
                desData = DATA;
            }
            else
            {
                desData = Encryption.Decry(DATA, MKey);
            }


            //创建数据对象
            DataItem item = null;
            switch (ctrCode)
            {
                case ControlCode.ReadData:
                    //主站请求的读数据操作
                    item = new DataItem_ReadData_ASK(desData);
                    break;
                case ControlCode.WriteData:
                    //主站请求的写数据操作
                    item = getWriteDataAskItem(desData);
                    break;
                case ControlCode.ReadKeyVersion:
                    break;
                case ControlCode.ReadMeterAdress:
                    break;
                case ControlCode.WriteMeterAdress:
                    break;
                case ControlCode.WriteMeterNum:
                    break;
                case ControlCode.CYReadData:
                    break;
                case ControlCode.CYWriteData:
                    item = getCYWriteDataAskItem(desData);
                    break;
                case ControlCode.CTR_6:
                    //主动上报数据

                    break;
                case ControlCode.CTR_7:
                    //
                    item = new DataItem_C002_Answer(desData);
                    break;
                case ControlCode.CTR_8:
                    break;
                default:
                    break;
            }
            return new TaskArge(this.Mac, item, (ControlCode)buffer[index + 9],pType, this.MKey);            
        }

        private DataItem getCYReadDataAskItem(byte[] buffer)
        {
            IdentityCode identityCode = MyDataConvert.get数据表示符(buffer);
            DataItem item = null;
            switch (identityCode)
            {
                case IdentityCode.读时钟:
                    item = new DataItem_C200(buffer);
                    break;
                case IdentityCode.读切断报警参数:
                    item = new DataItem_C201(buffer);
                    break;
                case IdentityCode.读服务器信息:
                    item = new DataItem_C202(buffer);
                    break;
                case IdentityCode.读上传周期:
                    item = new DataItem_C203(buffer);
                    break;
                case IdentityCode.读公称流量:
                    item = new DataItem_C204(buffer);
                    break;
            }
            return item;
        }

        private DataItem getCYWriteDataAskItem(byte[] buffer)
        {
            IdentityCode identityCode = MyDataConvert.get数据表示符(buffer);
            DataItem item = null;
            switch (identityCode)
            {
                case IdentityCode.设置服务器信息:
                    item = new DataItem_C104(buffer);
                    break;
                case IdentityCode.设置上传周期:
                    item = new DataItem_C105(buffer);
                    break;
                case IdentityCode.设置切断报警参数:
                    item = new DataItem_C103(buffer);
                    break;
                case IdentityCode.修正表数据:
                    item = new DataItem_C102(buffer);
                    break;
                case IdentityCode.设置公称流量:
                    item = new DataItem_C101(buffer);
                    break;
                case IdentityCode.换表:
                    item = new DataItem_C107(buffer);
                    break;
                case IdentityCode.发送广告:
                    item = new DataItem_C109(buffer);
                    //存储广告列表
                    this.LCD.SaveADList(this.Mac, (DataItem_C109)item);
                    break;
            }
            return item;
        }

        /// <summary>
        /// 主站请求写数据对象
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private DataItem getWriteDataAskItem(byte[] buffer)
        {
            IdentityCode identityCode = MyDataConvert.get数据表示符(buffer);
            DataItem item = null;
            switch (identityCode)
            {
                case IdentityCode.写价格表:
                    item = new DataItem_A010(buffer);
                    break;
                case IdentityCode.写结算日:
                    item = new DataItem_A011(buffer);
                    break;
                case IdentityCode.写购入金额:
                    item = new DataItem_A013(buffer);
                    break;
                case IdentityCode.写新密钥:
                    item = new DataItem_A014(buffer);
                    break;
                case IdentityCode.写标准时间:
                    item = new DataItem_A015(buffer);
                    break;
                case IdentityCode.写阀门控制:
                    item = new DataItem_A017(buffer);
                    break;
                case IdentityCode.出厂启用:
                    item = new DataItem_A019(buffer);
                    break;
                case IdentityCode.写地址:
                    item = new DataItem_A018(buffer);
                    break;
                case IdentityCode.写表底数:
                    item = new DataItem_A016(buffer);
                    break;
                case IdentityCode.发送广告:
                    item = new DataItem_C109(buffer);
                    break;
                default:
                    break;
            }

            return item;

        }

        private const string TaskCollectionName = "MSimulator";

        public void Update()
        {
            MSimulator simulator = this;
            MongoDBHelper<MSimulator> mongo = new MongoDBHelper<MSimulator>();
            var query = new QueryDocument();
            query.Add("_id", simulator._id);
            var update = new UpdateDocument();
            update.Add("Mac", this.Mac);
            update.Add("Key", simulator.Key);
            update.Add("MKeyVer", simulator.MKeyVer);
            update.Add("周期", simulator.周期);
            update.Add("PriceCheck", simulator.PriceCheck);
            update.Add("ValveState", simulator.ValveState);
            update.Add("TotalAmount", simulator.TotalAmount.ToString ("0.0000"));
            update.Add("MeterState", simulator.MeterState == null ? "0" : simulator.MeterState);
            update.Add("MeterType", simulator.MeterType);
            update.Add("TotalTopUp", (double)simulator.TotalTopUp);
            update.Add("hostname", simulator.hostname);
            update.Add("port", simulator.port);
            update.Add("SettlementType", simulator.SettlementType == null ? "00" : simulator.SettlementType);
            update.Add("SettlementDay",  simulator.SettlementDay);
            update.Add("SettlementMonth", simulator.SettlementMonth);

            update.Add("IsUsedLadder", simulator.IsUsedLadder);
            update.Add("Ladder", simulator.Ladder);
            update.Add("Price1",(double)simulator.Price1);
            update.Add("Gas1",  (double)simulator.Gas1);
            update.Add("Price2", (double)simulator.Price2);
            update.Add("Gas2",  (double)simulator.Gas2);
            update.Add("Price3", (double)simulator.Price3);
            update.Add("Gas3",  (double)simulator.Gas3);
            update.Add("Price4", (double)simulator.Price4);
            update.Add("Gas4",  (double)simulator.Gas4);
            update.Add("Price5", (double)simulator.Price5);

            update.Add("LastSettlementAmount",(double) simulator.LastSettlementAmount);
            update.Add("LastTotal", (double)simulator.LastTotal);
            update.Add("CurrentBalance", (double)simulator.CurrentBalance);
            update.Add("出厂状态", simulator.出厂状态);

            update.Add("NextSettlementPointGas", (double)simulator.NextSettlementPointGas);//下一个结算点气量
            update.Add("CurrentLadder", simulator.CurrentLadder);
            update.Add("CurrentPrice", (double)simulator.CurrentPrice);
            update.Add("LastGasPoint", (double)simulator.LastGasPoint);
            update.Add("SettlementDateTime", simulator.SettlementDateTime);



            //if (this.PricingPlan != null)
            //{                
            //    MongoDB.Bson.BsonDocument b = new MongoDB.Bson.BsonDocument();
            //    b.Add("IsUsedLadder", this.PricingPlan.IsUsedLadder);
            //    b.Add("Ladder", this.PricingPlan.Ladder);
            //    b.Add("Price1", (double)this.PricingPlan.Price1);
            //    b.Add("Gas1", (double)this.PricingPlan.Gas1);
            //    b.Add("Price2", (double)this.PricingPlan.Price2);
            //    b.Add("Gas2", (double)this.PricingPlan.Gas2);
            //    b.Add("Price3", (double)this.PricingPlan.Price3);
            //    b.Add("Gas3", (double)this.PricingPlan.Gas3);
            //    b.Add("Price4", (double)this.PricingPlan.Price4);
            //    b.Add("Gas4", (double)this.PricingPlan.Gas4);
            //    b.Add("Price5", (double)this.PricingPlan.Price5);
            //    b.Add("MeterType", this.PricingPlan.MeterType);
            //    b.Add("UseDate", this.PricingPlan.UseDate);

            //    update.Add("PricingPlan", b);
            //}

            mongo.Update(TaskCollectionName, query, update);
            if(this.OnJiliang != null)
                this.OnJiliang(this);
        }

        public void Insert()
        {
            MongoDBHelper<MSimulator> mongo = new MongoDBHelper<MSimulator>();

            mongo.Insert(TaskCollectionName, this);
        }
        int _zhouqi = 10;
        /// <summary>
        /// 上报周期,单位：分钟
        /// </summary>
        public int 周期 { 
            get
            {
                return _zhouqi;}
             set{
                 _zhouqi = value;
                 if (this.OnJiliang != null)
                     this.OnJiliang(this);


             } }

        public string hostname { get; set; }

        public int port { get; set; }

        public void Stop()
        {
            this.IsRunning = false;
            Update();
            if (this.tcpClient !=null)
            {
                this.tcpClient.Close();
            }
        }


        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        #region IDisposable 成员
        bool isDispose = false;
        public void Dispose()
        {
            if (isDispose) return;
            isDispose = true;
            if (this.tcpClient != null)
                this.tcpClient.Close();
            if (_semaphore != null)
                _semaphore.Close();
        }

        #endregion

        private PricingPlan QueryPricingPlan(string meterNo)
        {
            MongoDBHelper<PricingPlan> mongo_plan = new MongoDBHelper<PricingPlan>();
            QueryDocument query = new QueryDocument();
            query.Add("MeterNo", meterNo);
            MongoCursor<PricingPlan> mongoCursor = mongo_plan.Query(PPlan, query);
            if (mongoCursor == null) return null;
            var dataList = mongoCursor.ToList();
            if (dataList != null && dataList.Count >= 1)
                return dataList[0];
            else
                return null;
        }

        private void AddPlan(PricingPlan plan)
        {
            string result = "";
            try
            {
                MongoDBHelper<PricingPlan> mongo_plan = new MongoDBHelper<PricingPlan>();
                DeletePlan(plan.MeterNo);
                mongo_plan.Insert(PPlan, plan);
            }
            catch (Exception e)
            {
                result = e.Message;
            }
        }

        private void DeletePlan(string mac)
        {
            MongoDBHelper<PricingPlan> mongo_plan = new MongoDBHelper<PricingPlan>();
            var iDelete = new QueryDocument();
            iDelete.Add("MeterNo", mac);
            //删除老计划
            mongo_plan.Delete(PPlan, iDelete);
        }

        private const string PPlan = "SimulatorPricingPlan";
    }


    public delegate void AlertsHandle(MSimulator simulator);
}
