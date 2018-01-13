using CY.IoTM.Common.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CY.IoTM.Channel.MeterTCP
{
    class MeterTCPChannelLister
    {
        private TcpListener _tcpLister;
        private string ipbound = "127.0.0.1";
        private int mPort = 8000;
        /// <summary>
        /// 连接端口数量
        /// </summary>
        public int iConnectionCount = 0;

        /// <summary>
        /// 接收到一个连接事件
        /// </summary>
        internal event ConnectionMessageHandle OnReceviceConnected;

        //internal event ConnectionMessageHandle OnConnectClosed;


        /// <summary>
        /// 连接侦听器已停止。
        /// </summary>
        internal event EventHandler OnTcpListenerStoped;


        /// <summary>
        /// 连接侦听器构造函数。
        /// </summary>
        /// <param name="serverIP">服务器IP</param>
        /// <param name="serverPort">服务器端口</param>
        public MeterTCPChannelLister(string serverIP, int serverPort)
        {
            this.ipbound = serverIP;
            this.mPort = serverPort;
        }
        /// <summary>
        /// 启动侦听。
        /// </summary>
        public void Start()
        {
            if (this._tcpLister != null) return;
            try
            {
                //IPAddress ipAddress = Dns.GetHostAddresses(ipbound)[0];//IPAddress.Parse(ipbound)
                _tcpLister = new TcpListener(IPAddress.Any, mPort);
                _tcpLister.Start(); 
#if DEBUG
                Log.getInstance().Write(MsgType.Information, "侦听已启动，等待连接.");
                //Console.WriteLine("侦听已启动，等待连接...");
#endif
            }
            catch (Exception ex)
            {
                Log.getInstance().Write("创建监听", MsgType.Information);
                Log.getInstance().Write(ex,MsgType.Error);
                return;
            }
            Stopwatch watch = new Stopwatch();
            watch.Start ();
            while (true)
            {
                if (this._tcpLister == null)
                {
                    break;
                }
                try
                {                    
                    Socket ServerSocketForClient = _tcpLister.AcceptSocket(); 
                    MeterLink clk = new MeterLink(ServerSocketForClient); 
                    if (this.OnReceviceConnected != null)
                        this.OnReceviceConnected(clk);
                    clk.OnConnectClosed += OnConnectionClosed;
                    this.iConnectionCount++; 

                    watch.Restart();

#if DEBUG

                    Log.getInstance().Write("已接受了一个连接，时间：" + DateTime.Now.ToString() + " IP:" + clk.IP, MsgType.Information);
                    Log.getInstance().Write("当前连接数量：" + iConnectionCount.ToString(), MsgType.Information);
                    
#endif
                }
                catch (InvalidOperationException ioe)//尚未通过调用 TcpListener.Start() 来启动该侦听器。
                {
                    Log.getInstance().Write(ioe, MsgType.Error);
                }
                catch (Exception e)
                {  
                    Log.getInstance().Write(e, MsgType.Error);
                }

            }
            watch.Stop();
            if (this.OnTcpListenerStoped != null)
                this.OnTcpListenerStoped(this, new EventArgs());

        } 
        /// <summary>
        /// 停止侦听
        /// </summary>
        public void Stop()
        {
            if (this._tcpLister != null)
            {
                try
                {
                    this._tcpLister.Stop();
                }
                catch (SocketException er)
                {
                    Log.getInstance().Write(er, MsgType.Error);
                }
                this._tcpLister = null;
            }
        }

        private void OnConnectionClosed(MeterLink e)
        {
            this.iConnectionCount--;
            try
            {
                Log.getInstance().Write("断开连接" + e.IP + e.MAC, MsgType.Information);
            }
            catch
            {
                Log.getInstance().Write("断开连接-OnConnectionClosed失败", MsgType.Information);
            }
            Log.getInstance().Write("当前连接数量：" + iConnectionCount.ToString(), MsgType.Information);
        }

    }

    /// <summary>   
    /// 连接消息代理   
    /// </summary>   
    /// <param name="e"></param>   
    public delegate void ConnectionMessageHandle(MeterLink e);  

}
