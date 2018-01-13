using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IoTM.Common;
using CY.IoTM.Channel;


namespace CY.IoTM.Channel.SP
{
    public class SP_DataChannel : DataChannel
    {



        SerialPortClient _client;

        /// <summary>
        /// 创建物联网表数据通道事件
        /// </summary>
        public  new  event Channel_InfoDelegate OnCreateChannel;
        /// <summary>
        /// 关闭物联网表数据通道事件
        /// </summary>
        public new event Channel_InfoDelegate OnCloseChannel;
        /// <summary>
        /// 接收到通道数据事件
        /// </summary>
        public new event Channel_DATA_Delegate OnReceiveData;


        public SP_DataChannel(string mac, string channelType)
            : base(mac, channelType)
        {


            //从配置中获取  
            SerialPortConfig config = new SerialPortConfig("COM1", 9600, 8, "EVEN", "1");

            _client = new SerialPortClient(mac, config);
            _client.OnReceiveData += Receive;
            _client.Activate();

        }



        public override void Send(byte[] data)
        {

            this._client.Send(data);
            Console.WriteLine(this._mac+"发送:"+ ConvertHelper.GetHex(data));

        }

        public  void Receive(byte[] data)
        {

            Channel_DATA byteData = new Channel_DATA();

            byteData.m_MAC = this._mac;
            byteData.m_recv_date = DateTime.Now;
            byteData.m_buffer = data;

            this.OnReceiveData(byteData);
        }






    }
}
