using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using CY.IoTM.Common;
using CY.IoTM.Common.Log;
using System.Diagnostics;

namespace CY.IoTM.Channel.MeterTCP
{
    /// <summary>
    /// 向表发送数据、接收表发送的数据
    /// </summary>
    public class MeterLink : Link
    {
        /// <summary>
        /// 连接对应的燃气表对象
        /// </summary>
        IoTMeter _ioTMeter = new IoTMeter ();

        Socket _socket;
        Thread _clientThread = null;
        /// <summary>
        /// 表示接收数据的线程是否在运行，true 在运行 ，false 已关闭。
        /// </summary>
        private bool isRun = true;

        /// <summary>
        /// 测试表上线到下线耗用时间
        /// </summary>
        private Stopwatch _meterWatchTest = new Stopwatch();
        /// <summary>
        /// 接收到应答数据
        /// </summary>
        public event Meter_DATA_Delegate OnReviced;
        public event ConnectionMessageHandle OnConnectClosed;

        public string MAC
        {
            get
            {
                if (_ioTMeter != null)
                    return _ioTMeter.MAC;
                else
                    return "";
            }
        }
        internal int socketHandle;
        bool isLogin = false;//用于标记设备登录（收到第1帧数据）

        public MeterLink(Socket socket)
        {
            this._socket = socket;
            this.socketHandle = this._socket.Handle.ToInt32();
            ThreadStart ts = new ThreadStart(ReceiveData);
            _clientThread = new Thread(ts);
            _clientThread.IsBackground = true;
            //开始线程。   
            _clientThread.Start();

            //测试表上线到下线耗用时间  开始
            _meterWatchTest.Start();
           
        }
        public string IP
        {
            get
            {
                return this._socket.RemoteEndPoint.AddressFamily.ToString();
            }
        }

        /// <summary>
        /// 接收数据的线程函数
        /// </summary>
        void ReceiveData()
        {
            //提供用于数据解析的内存流
            //System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //FE FE 68 30 39 61 51 23 35 51 04 21 1E C0 01 20 40 14 16 22 04 15 20 18 56 42 37 18 56 02 00 18 56 02 00 18 56 02 00 23 00 00 00 6F 16
            byte[] b = new byte[1];
            b[0] = 0x01;
            while (isRun)
            {
                if (this._socket == null) break;
                try
                {
                    System.Threading.Thread.Sleep(50);
                    if (!isRun) break; //
                    if (_socket.Poll(10, SelectMode.SelectRead))
                    {
                        int dLength = this._socket.Available;
                        if (dLength <= 0)
                        {
                            isRun = false;
                            this.Close();
                            break;
                        }
                        byte[] buffer = null;
                        if (dLength > 0)
                        {
                            if (dLength != this._socket.Available)
                            {
                                System.Threading.Thread.Sleep(100);
                                dLength = this._socket.Available;
                            }

                            buffer = new byte[dLength];
                            this._socket.Receive(buffer);
                            int index = 0;//查找报文头
                            for (int i = 0; i < buffer.Length - 2; i++)
                            {
                                if (buffer[i] == 0XFE && buffer[i + 1] == 0X68)
                                {
                                    index = ++i;
                                    break;
                                }
                            }
                            //检查仪表类型是否为燃气表或LCD屏协议，否则抛弃
                            IotProtocolType pType = (IotProtocolType)buffer[index + 1];
                            int iOffset = 11; //定义从包头（0x68）到数据区域字节数

                            if (pType != IotProtocolType.LCD && pType != IotProtocolType.RanQiBiao) continue;

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
                                continue;
                            }
                            //取出数据部分（该数据为加密数据）注：LCD协议不加密
                            byte[] DATA = new byte[DataLength];
                            for (int i = 0;i<DataLength ; i++)
                            {
                                DATA[i] = buffer[i + index + iOffset];
                            }

                            StringBuilder sb = new StringBuilder();
                            for (int i = index + 8; i > index + 1; i--)
                            {
                                sb.Append(string.Format("{0:X2}", buffer[i]));
                            }
                            this._MAC = sb.ToString();
                            //
                            if (!this.isLogin)
                            {
                                this.isLogin = true;
                                Log.getInstance().Write(new OneMeterDataLogMsg(this._MAC, string.Format("上线-> [{0}] ", socketHandle) + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                                RecordRevDataToLog(this._MAC, buffer, socketHandle.ToString());//添加日志
                                this._ioTMeter.Initi(this._MAC, this, buffer[index + 9], DATA, pType);
                            }
                            else
                            {
                                RecordRevDataToLog(this._MAC, buffer, socketHandle.ToString());//添加日志
                                //在此需要判端是下位机请求
                                if (buffer[index + 9] == 0xA1)
                                {
                                    //下位机请求，（包括：上报燃气表数据、请求LCD文件、请求交易等）
                                    this._ioTMeter.ReportData(this._MAC, buffer[index + 9], DATA,pType);
                                }
                                else if (this.OnReviced != null)
                                {
                                    this.OnReviced(new Meter_DATA() { Control = buffer[index + 9], DataLength = DataLength, m_buffer = DATA ,ProtocolType = pType });
                                }
                            }
                            //else if(result != "")
                            //{
                            //    //Console.WriteLine(result);
                            //    Log.getInstance().Write(MsgType.Information,result);
                            //}
                        }
                        else
                        {
                            //检查网络连接有效性
                            if (((this._socket.Poll(1000, SelectMode.SelectRead) && (this._socket.Available == 0)) || !this._socket.Connected))
                            {
                                isRun = false;
                                this.Close();
                            }
                            continue;
                        }
                    }
                    else
                    {
                        //检查网络连接有效性
                        if (((this._socket.Poll(1000, SelectMode.SelectRead) && (this._socket.Available == 0)) || !this._socket.Connected))
                        {
                            isRun = false;
                            this.Close();
                        }
                        continue;
                    }
                }
                catch (ObjectDisposedException e)
                {
                    Log.getInstance().Write(MsgType.Information, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} 在MeterLink.ReceiveData产生ObjectDisposedException异常，详细如下：\r\nMAC:{this._MAC}[{this.socketHandle}]\r\n  {e} ");

                    //Socket已关闭
                    isRun = false;
                    this.Close();
                    break;
                }
                catch (SocketException e1)
                {
                    Log.getInstance().Write(MsgType.Information, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} 在MeterLink.ReceiveData产生SocketException异常ErrorCode={e1.ErrorCode}，详细如下：\r\nMAC:{this._MAC}[{this.socketHandle}]\r\n {e1} ");

                    if (e1.ErrorCode != 10035)
                    {
                        //当前链接已断开或连接已关闭。
                        isRun = false;
                        this.Close();
                        break;
                    }
                    //Log.getInstance().Write(e1, MsgType.Error);
                }
                catch (OutOfMemoryException e2)
                {
                    //Log.getInstance().Write(e2, MsgType.Error);
                    Log.getInstance().Write(MsgType.Information, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} 在MeterLink.ReceiveData产生OutOfMemoryException异常，详细如下：\r\nMAC:{this._MAC}[{this.socketHandle}]\r\n  {e2} ");
                    GC.Collect();
                }
                catch (Exception e3)
                {
                    Log.getInstance().Write(MsgType.Information, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} 在MeterLink.ReceiveData产生Exception异常，详细如下：\r\nMAC:{this._MAC}[{this.socketHandle}]\r\n  {e3} ");
                    //Log.getInstance().Write(e3, MsgType.Error);
                }
            }
            //ms.Close();
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public override void Close()
        {
            //Console.WriteLine("表:{0}连接已关闭，时间：{1}", this.MAC, DateTime.Now.ToString());
            Log.getInstance().Write(new MeterLogMsg(IP + string.Format("表[{0}]-[{2}]下线,在线时长：{1} 秒 ", this.MAC, (_meterWatchTest.ElapsedMilliseconds / 1000.00).ToString(),this.socketHandle)));

            Log.getInstance().Write(new OneMeterDataLogMsg(this.MAC, string.Format ("下线->[{0}]-[{2}],在线时长：{1} 秒 \r\n------------------------", this.MAC , (_meterWatchTest.ElapsedMilliseconds / 1000.00).ToString(), this.socketHandle)));
            _meterWatchTest.Stop();
            
            isRun = false;  
            if (this.OnConnectClosed != null)
                this.OnConnectClosed(this);
            base.Close();
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public override bool Send(byte[] buffer)
        {
            if (this._socket == null) return false;


            bool isResult = false;
            try
            {
                while (!this._socket.Poll(1000 * 2, SelectMode.SelectWrite)) ;
                this._socket.Send(buffer);
                RecordSendDataToLog(this._MAC,buffer,this.socketHandle.ToString ());
                isResult = true;
            }
            catch (ObjectDisposedException e)
            {
                //Socket已关闭
                Console.Out.Write(e);
                this.Close();
                Log.getInstance().Write(e, MsgType.Error);
                Log.getInstance().Write(MsgType.Information, "MeterLink.Send.e");
            }
            catch (SocketException e1)
            {
                if (e1.ErrorCode != 10035)
                {
                    //当前链接已断开或连接已关闭。
                    this.Close();
                }
                Log.getInstance().Write(e1, MsgType.Error);
                Log.getInstance().Write(MsgType.Information, "MeterLink.Send.e1");
            }
            catch (Exception e2)
            {                
                Log.getInstance().Write(e2, MsgType.Error);
                Log.getInstance().Write(MsgType.Information, "MeterLink.Send.e2");
            }
            return isResult;
        }
    }

    public delegate void Meter_DATA_Delegate(Meter_DATA data);

    /// <summary>
    /// 传输数据体定义
    /// </summary>
    public struct Meter_DATA
    {
        /// <summary>
        /// 控制码
        /// </summary>
        public byte Control;	//控制码	
        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLength;	//数据长度
        /// <summary>
        /// 数据
        /// </summary>
        public byte[] m_buffer; //数据
        /// <summary>
        /// 数据协议类型
        /// </summary>
        public IotProtocolType ProtocolType;
    }
}
