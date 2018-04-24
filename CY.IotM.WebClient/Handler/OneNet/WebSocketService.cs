using CY.IotM.Common.Tool;
using CY.IoTM.Common.Business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Web;

namespace OneNETDataReceiver
{
    public class WebSocketService
    {
        private static WebSocketService _webSocket;
        private static object _object = new object();
        private WebSocketService()
        {
        }

        public static WebSocketService getInstance()
        {
            if(_webSocket == null)
            {
                lock (_object)
                {
                    if (_webSocket == null)
                    {
                        _webSocket = new WebSocketService();
                    }
                }
            }
            return _webSocket;
        }


        public void SendMessage(MessageInfo message)
        {
            WCFServiceProxy<IOneNetService> proxy = null;
            try
            {
                proxy = new WCFServiceProxy<IOneNetService>();
                proxy.getChannel.OutPutLog(message.Mac, message.MsgContent); // raw content as string
                proxy.CloseChannel();
            }
            catch(Exception e)
            {
                Debug.Print(e.Message);
            }
        }

    }

    public class MessageInfo
    {
        public MessageInfo(DateTime _MsgTime, string _MsgContent,string mac)
        {
            MsgTime = _MsgTime;
            MsgContent = _MsgContent;
            Mac = mac;
        }
        public DateTime MsgTime { get; set; }
        public string MsgContent { get; set; }
        public string Mac { get; set; }
    }
}