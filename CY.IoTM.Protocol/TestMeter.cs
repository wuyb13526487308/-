using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IoTM.Common;
using CY.IoTM.Common.Item;
using CY.IoTM.Channel;


namespace CY.IoTM.Protocol
{

    /// <summary>
    /// 模拟物联网表虚拟设备  
    /// </summary>
    public class TestMeter
    {


        public IDataChannel Channel { get { return this._channel; } }

        private IDataChannel _channel;

        private LoTMeter _protocol;

        private string _meterId;


        public TestMeter(string meterId, IDataChannel channel)
        {

            this._channel = channel;
            this._meterId = meterId;
            this._protocol = new LoTMeter(this._channel);

            this._channel.OnReceiveData += this._protocol.ReceiveData;
            this._protocol.OnReceiveData += ReviceData;

            System.Console.WriteLine(this._meterId + "加载通道完成...");

        }



        public void ReviceData(RecordData data)
        {

            System.Console.WriteLine(this._meterId + "收到数据");
            //System.Console.WriteLine(((DataItem_8103)data.Data).JieSuanDay);

        }


        public string SendData(TaskArge data)
        {
            //System.Console.WriteLine(this._meterId + "发送数据");
            return this._protocol.SendData(data);

        }




    }
}
