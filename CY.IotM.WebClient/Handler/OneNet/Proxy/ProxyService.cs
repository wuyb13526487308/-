using CY.IotM.Common.Tool;
using CY.IoTM.Common;
using CY.IoTM.Common.Business;
using CY.IoTM.Common.Item;
using CY.IoTM.Common.Item.上报数据;
using CY.IoTM.Common.Item.读操作;
using CY.IoTM.Common.Protocol;
using CY.IoTM.Protocol;
using OneNETDataReceiver.Entity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;

namespace OneNETDataReceiver.Proxy
{
    public class ProxyService
    {
        /// <summary>
        /// 等待onenet应答数据
        /// </summary>
        private bool bWaitOneNetResp = false;
        /// <summary>
        /// 等待物联网表平台应答
        /// </summary>
        private bool bWaitIotSeviceResp = false;
        /// <summary>
        /// 物联网表平台的请求指令队列（需要发送到onenet平台的指令队列）
        /// </summary>
        private Queue<ClientTask> requestQueue = new Queue<ClientTask>();
        private System.Net.Sockets.TcpClient tcpClient;
        private Meter meter;
        private string hostname = "127.0.0.1";
        private int port = 15555;
        private int deviceId;
        //private Semaphore _semaphore = new Semaphore(1, 1);


        public ProxyService(int deviceId)
        {
            this.meter = new Meter();
            this.DeviceId = deviceId;
            this.hostname = $"{System.Configuration.ConfigurationManager.AppSettings["MeterServer"]}";
            this.port = Convert.ToInt32($"{System.Configuration.ConfigurationManager.AppSettings["MeterServerPort"]}");
        }

        private byte[] MKey
        {
            get
            {
                return strToToHexByte(this.meter.Key);
            }
        }
        /// <summary>
        /// nb表是否在线
        /// </summary>
        public bool IsOnline { get; private set; }
        public int DeviceId { get => deviceId; private set => deviceId = value; }

        public bool Login(int deviceId,string data)
        {
            string message;
            TaskArge taskArge = JieXi(strToToHexByte(data), out message);
            if (taskArge == null || taskArge.ControlCode != ControlCode.CTR_6)
            {
                //接收到数据不上上报数据
                WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now,($"->表：{this.meter.Mac} 上线失败。"), this.meter.Mac));
                return false;
            }
            Stopwatch watch = new Stopwatch();
            watch.Start();
            this.meter.Mac = taskArge.IoTMac;

            //将数据发送给服务器
            try
            {
                this.tcpClient = new System.Net.Sockets.TcpClient();
                this.tcpClient.Connect(this.hostname, this.port);
                Socket socket = tcpClient.Client;
                //TODO:根据deviceId 读取表信息，包括：mac，key
                byte[] buffer = strToToHexByte(data);
                this.IsOnline = true;
                bWaitIotSeviceResp = true;
                //启动工作线程
                new Thread(t=>
                {
                    try
                    {
                        while (IsOnline)
                        {
                            Thread.Sleep(100);
                            //检查数据
                            if (socket!=null && socket.Poll(10, SelectMode.SelectRead))
                            {
                                int dLength = socket.Available;
                                Thread.Sleep(100);
                                if (dLength != socket.Available)
                                    dLength = socket.Available;
                                buffer = new byte[dLength];
                                socket.Receive(buffer);
                                //解析接收到的数据
                                this.SendResp(JieXi(buffer, out message));
                                watch.Restart();

                            }
                            else
                            {
                                if (((socket.Poll(1000, SelectMode.SelectRead) && (socket.Available == 0)) || !socket.Connected))
                                {
                                    IsOnline = false;
                                }
                            }
                            if (watch.ElapsedMilliseconds > 1000 * 60 * 5)
                            {
                                IsOnline = false;//5分钟没有任何数据交换，关闭tcp连接
                                this.tcpClient.Close();
                                socket = null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now,($"->代理线程TCP连接异常，错误信息：" + ex.Message), this.meter.Mac));
                        IsOnline = false;
                    }
                    WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now,  ($"->代理线程TCP连接已关闭。"),this.meter.Mac));

                }).Start();

                if (!ReportToZhuZhan(socket, buffer))
                {
                    WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, ($"->表：{this.meter.Mac} 上线失败，没有收到主站应答"), this.meter.Mac));
                    return false;
                }
                WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, ($"->表：{this.meter.Mac} 上线成功。"), this.meter.Mac));
                IsOnline = true;
                return true;
            }
            catch(Exception ee)
            {
                IsOnline = false;
                WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, 
                    ($"->代理线程启动失败，原因："+ee.Message),this.meter.Mac));
                return false;
            }            
        }
        /// <summary>
        /// 接收到来自oneNet平台的数据
        /// </summary>
        /// <param name="data"></param>
        public void Revice(string data)
        {
            string message;
            TaskArge taskArge = JieXi(strToToHexByte(data), out message);
            if (taskArge == null)
            {
                //接收到数据不上上报数据
                WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, 
                    ($"->解析数据:[{data}]失败,原因："+message),this.meter.Mac));
                return;
            }

            if (this.SendToMS(data))
            {
                if (bWaitOneNetResp)
                {
                    //oneNet应答数据给表平台
                    this.bWaitOneNetResp = false;
                }
                else
                {
                    //nb表上报的数据，需要后台应答
                    this.bWaitIotSeviceResp = true;
                }
            }
        }

        /// <summary>
        /// 发送数据给oneNet平台
        /// </summary>
        /// <param name="arge"></param>
        void SendReq(TaskArge arge)
        {
            this.bWaitOneNetResp = true;
            Frame frame = new Frame(arge);
            byte[] buffer = frame.GetBytes();
            ResponseData respData = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseData>(PostToOneNet(BytesToHexStr(buffer),frame.Adress));
            if (respData.errno == 0)
            {
                WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now,
                ($"->主站请求的数据已发送给onenet平台发送成功，表号:{arge.IoTMac}"), this.meter.Mac));
            }
            else
            {
                WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now,
               ($"->主站请求的数据已发送给onenet平台失败，表号:{arge.IoTMac}，原因："+respData.error), this.meter.Mac));

                this.IsOnline = false;
                if (this.tcpClient != null)
                {
                    this.tcpClient.Close();
                    this.tcpClient = null;
                }
            }
        }

        /// <summary>
        /// post数据给onenet平台
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        string PostToOneNet(string hex,string mac)
        {
            string result = "";
            WCFServiceProxy<IOneNetService> proxy = null;
            try
            {
                Msg msg = new Msg();
                msg.data.Add(new _Data() { val = hex.Replace(" ", ""), res_id = 5505 });
                var client = new RestClient(Param.url);
                var request = new RestRequest($"nbiot?imei={mac}&obj_id=3200&obj_inst_id=0&mode=2", Method.POST);
                request.AddHeader("api-key", Param.appkey);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(msg);

                IRestResponse response = client.Execute(request);
                result = response.Content; // raw content as string
                proxy = new WCFServiceProxy<IOneNetService>();

                string tmp = $"nbiot?imei={mac}&obj_id=3200&obj_inst_id=0&mode=2";
                proxy.getChannel.OutPutLog(mac, $"->向oneNet平台发送数据,URI PARAM:{tmp}");                
                proxy.getChannel.OutPutLog(mac, $"->向oneNet平台发送数据完成，返回结果：{result}");
            }
            catch
            {
            }
            finally
            {
                if (proxy != null)
                {
                    proxy.CloseChannel();
                }
            }
            return result;          

        }
        /// <summary>
        /// 发送应答数据给oneNet平台
        /// </summary>
        /// <param name="taskArge"></param>
        void SendResp(TaskArge taskArge)
        {
            Frame frame = new Frame(taskArge);
            byte[] buffer = frame.GetBytes();
            ResponseData respData = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseData>(PostToOneNet(BytesToHexStr(buffer),taskArge.IoTMac));
            if (respData.errno == 0)
            {
                if (taskArge != null && (((byte)taskArge.ControlCode) & 0x80) == 0x00)
                {
                    if (taskArge.ControlCode != ControlCode.CTR_7 && taskArge.ControlCode != ControlCode.CTR_8)
                    {
                        //主站指令
                        //接收到主站请求数据
                        WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now,
                        ($"->主站发向表：{this.meter.Mac}的请求指令成功，控制码：{taskArge.ControlCode}"), this.meter.Mac));
                        this.bWaitOneNetResp = true;
                    }
                    else
                    {
                        //主站应答
                        WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now,
                            ($"->主站应答表：{this.meter.Mac} 数据成功。"), this.meter.Mac));
                        this.bWaitIotSeviceResp = false;
                    }
                }

                WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, $"->发送应答给OneNet平台成功", this.meter.Mac));
            }
            else
            {
                WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now,
                    ($"->主站向OneNet平台发送数据失败，表号:{taskArge.IoTMac}，原因：" + respData.error), this.meter.Mac));

                this.IsOnline = false;
                if (this.tcpClient != null)
                {
                    this.tcpClient.Close();
                    this.tcpClient = null;
                }
            }
            //需要判断是否成功
            this.bWaitIotSeviceResp = false;
        }

        public void Close()
        {
            this.IsOnline = false;
            if (this.tcpClient != null)
            {
                this.tcpClient.Close();
                this.tcpClient = null;
            }
        }

        /// <summary>
        /// 发送数据给物联网表平台
        /// </summary>
        /// <param name="data"></param>
        private bool SendToMS(string data)
        {
            bool isResult = false;
            if (this.tcpClient != null && this.tcpClient.Connected)
            {
                Socket _socket = tcpClient.Client;
                try
                {
                    byte[] buffer = strToToHexByte(data);
                    while (!_socket.Poll(1000 * 2, SelectMode.SelectWrite)) ;
                    _socket.Send(buffer);
                    if (bWaitOneNetResp)
                    {
                        //oneNet应答数据给表平台
                        WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now,
                        $"->表：{this.meter.Mac}应答主站请求完成。", this.meter.Mac));

                    }
                    else
                    {
                        //nb表上报的数据，需要后台应答
                        WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now,
                         $"->表：{this.meter.Mac}向主站上报数据完成。", this.meter.Mac));
                    }
    

                    isResult = true;
                }
                catch (ObjectDisposedException e)
                {
                    //Socket已关闭
                    Console.Out.Write(e);
                }
                catch (SocketException e1)
                {
                    if (e1.ErrorCode != 10035)
                    {
                        //当前链接已断开或连接已关闭。
                        //this.Close();
                    }
                }
                catch (Exception e2)
                {
                }
                if (!isResult)
                {
                    WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now,$"->表：{this.meter.Mac}向主站发送数据失败。", this.meter.Mac));
                }
                return isResult;
            }
            else
            {
                this.IsOnline = false;
            }
            return isResult;
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
                    //item = DowithReadData(e.Data.IdentityCode, e.Data.SER);
                    //answerCtrCode = ControlCode.ReadData_Success;
                    break;
                case ControlCode.WriteData:
                    //主站请求的写数据操作
                    //if (this.AnswerType == null || this.AnswerType == "" || this.AnswerType == "正常应答")
                    //{
                    //    item = DowithWriteData(e.Data.IdentityCode, e.Data.SER, e.Data);
                    //    answerCtrCode = ControlCode.WriteData_Answer;//正常应答
                    //    if (e.Data.IdentityCode == IdentityCode.写新密钥)
                    //    {
                    //        for (int i = 0; i < 8; i++)
                    //            currentKey[i] = 0x88;
                    //    }
                    //}
                    //else
                    //{
                    //    answerCtrCode = ControlCode.WriteData_Failed;
                    //    item = new DataItem_Answer(e.Data.SER, e.Data.IdentityCode);
                    //}
                    break;
                case ControlCode.ReadKeyVersion:
                    //主站读从站密钥版本
                    //item = new DataItem_8106(e.Data.SER, this.MKeyVer);
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
                    //if (this.AnswerType == null || this.AnswerType == "" || this.AnswerType == "正常应答")
                    //{
                    //    item = DowithCYWriteData(e.Data.IdentityCode, e.Data.SER, e.Data);
                    //    answerCtrCode = ControlCode.CYWriteData_Answer;
                    //}
                    //else
                    //{
                    //    answerCtrCode = ControlCode.CTR_11;
                    //    item = new DataItem_Answer(e.Data.SER, e.Data.IdentityCode);
                    //}
                    break;
                default:
                    break;
            }
            return new TaskArge(this.meter.Mac, item, answerCtrCode, e.IotProtocolType, currentKey);

        }

        private bool ReportToZhuZhan(Socket socket,byte[] buffer)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int iTime = 1;
            WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now,
                ($"->开始登录"),this.meter.Mac));

            while (socket.Poll(100, SelectMode.SelectWrite))
            {
                socket.Send(buffer);
                break;
            }
            //Notice(string.Format("{1:HH:mm:ss} 第{0}次发送上报数据请求帧\r{2}\r", iTime, DateTime.Now, MyDataConvert.BytesToHexStr(buffer)));

            bWaitIotSeviceResp = true;
            WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, 
                ($"->等待主站应答"),this.meter.Mac));

            //等待应答
            while (bWaitIotSeviceResp)
            {
                Thread.Sleep(100);
                if (watch.ElapsedMilliseconds >= 1000 * 10)
                {
                    if (iTime >= 3)
                    {
                        //接收不到主站应答，本次上报结束
                        this.tcpClient.Close();
                        this.tcpClient = null;
                        this.IsOnline = false;
                        WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, 
                            ($"->等待应答超时，登录失败"),this.meter.Mac));
                        return false;
                    }
                    iTime++;
                    socket.Send(buffer);
                    watch.Restart();
                }
            }

            return true;
        }

        private TaskArge JieXi(byte[] buffer, out string message)
        {
            message = "";
            WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, 
                ($"->开始解析数据..."),this.meter.Mac));

            try
            {
                string _MAC = "";
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

                StringBuilder sb = new StringBuilder();
                for (int i = index + 8; i > index + 1; i--)
                {
                    sb.Append(string.Format("{0:X2}", buffer[i]));
                }
                _MAC = sb.ToString();
                this.meter.Mac = _MAC;

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
                    WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, 
                        ($"{DateTime.Now.ToString("yy-MM-dd HH:mm:dd.fff")}->解析数据失败，原因："+ message),this.meter.Mac));

                    return null;
                }
                //取出数据部分（该数据为加密数据）
                byte[] DATA = new byte[DataLength];
                for (int i = 0; i < DataLength; i++)
                {
                    DATA[i] = buffer[i + index + iOffset];
                }


                sb = new StringBuilder();
                for (int i = index + 8; i > index + 1; i--)
                {
                    sb.Append(string.Format("{0:X2}", buffer[i]));
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
                WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, 
                    ($"->解析数据完成，表号：{_MAC} 控制码:{ctrCode}"),_MAC));

                return new TaskArge(_MAC, item, (ControlCode)buffer[index + 9], pType, this.MKey);
            }
            catch(Exception e)
            {
                WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, 
                    ($"->解析数据失败，原因：" + e.Message),""));
                return null;
            }
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
                    //this.LCD.SaveADList(this.Mac, (DataItem_C109)item);
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

        private static string BytesToHexStr(byte[] buffer)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
                sb.Append(string.Format("{0:X2} ", buffer[i]));
            return sb.ToString();
        }
    }
}