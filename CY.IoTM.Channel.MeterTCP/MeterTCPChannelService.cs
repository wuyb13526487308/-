using CY.IoTM.Common.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;

namespace CY.IoTM.Channel.MeterTCP
{
    [Export(typeof(ICreateDataChannel))]
    public class MeterTCPChannelService : ICreateDataChannel
    {
        /// <summary>
        /// 创源RTU第1版协议数据通道表示
        /// </summary>
        private const string CreaterName = "TCP";
        /// <summary>
        /// 连接监听对象
        /// </summary>
        private MeterTCPChannelLister _listener;
        /// <summary>
        /// 监听线程
        /// </summary>
        private Thread _listenThread;
        /// <summary>
        /// 服务器端IP
        /// </summary>
        string ip = "127.0.0.1";
        /// <summary>
        /// 服务器端Port
        /// </summary>
        int port = 8600;
        private List<IDataChannel> _channelList = new List<IDataChannel>();

        public MeterTCPChannelService()
        {
            try
            {
                this.ip = System.Configuration.ConfigurationManager.AppSettings["TCPServer"];
                this.port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TCPServerPort"]);
            }
            catch { 
                //从配置中读取失败
            }
            if (this.ip == null || this.ip == "")
            {
                string strHostName = Dns.GetHostName();
                foreach (IPAddress ip in System.Net.Dns.GetHostAddresses(strHostName))
                {
                    this.ip = ip.ToString();
                }
            }
        }
        #region ICreateDataChannel 成员

        public string ChannelType
        {
            get { throw new NotImplementedException(); }
        }

        public IDataChannel CreateDataChannel(string meterId)
        {
            throw new NotImplementedException();
        }

        public string StartService()
        {
            if (this._listener != null) return "服务已启动"; //防止重复启动侦听

            //参数ip 和port 将来从配置文件中读取。            
            _listener = new MeterTCPChannelLister(ip, port);
            _listener.OnReceviceConnected += new ConnectionMessageHandle(OnReceviceConnected);
            //_listener.OnConnectClosed += _listener_OnConnectClosed;
            _listener.OnTcpListenerStoped += new EventHandler(OnTcpListenerStoped);
            this._listenThread = new Thread(new ThreadStart(Start));

            this._listenThread.Start();
            //添加日志
            Log.getInstance().Write(MsgType.Information,"物联网表后台服务启动成功，服务器IP：" + this.ip + " 端口号：" + this.port.ToString());
            return "物联网表后台服务启动成功，服务器IP：" + this.ip + " 端口号：" + this.port.ToString();
        }

        void _listener_OnConnectClosed(MeterLink e)
        {
            //throw new NotImplementedException();
            //一个链接关闭了

        }

        /// <summary>
        /// 接收到一个连接，建立和终端通讯链接对象。
        /// </summary>
        /// <param name="e"></param>
        private void OnReceviceConnected(MeterLink e)
        {
            //e.DataChannelList = this._channelList;

        }

        /// <summary>
        /// 关闭通道服务
        /// </summary>
        public void StopService()
        {
            if (this._listener != null)
            {
                this._listener.Stop();
                this._listener = null;
            }             
            //foreach (IDataChannel dc in this._channelList)
            //    dc.Close();
        }

        /// <summary>
        /// 侦听器连接已停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTcpListenerStoped(object sender, EventArgs e)
        {
        }

        private void Start()
        {
            //启动侦听
            _listener.Start();
        }

        #endregion

        /// <summary>
        /// 获取表连接数量
        /// </summary>
        /// <returns></returns>
        public int GetConectionCount()
        {
            if (_listener != null)
            {
                return _listener.iConnectionCount;
            }
            else
            {
                return 0;
            } 
        }
    }
}
