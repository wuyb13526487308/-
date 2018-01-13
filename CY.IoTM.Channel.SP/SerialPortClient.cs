using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Threading;
using CY.IoTM.Common;


namespace CY.IoTM.Channel.SP
{
    public class SerialPortClient:Link
    {
        /// <summary>
        /// 串行端口
        /// </summary>
        SerialPort com;

        /// <summary>
        /// 能否发送下一帧
        /// </summary>
        Boolean CanSendNext = false;

        /// <summary>
        /// 发送请求线程
        /// </summary>
        Thread threadsend;

        /// <summary>
        /// 接收线程
        /// </summary>
        Thread threadrev;


        private byte[] _data;

        private string _mac;



        public event ByteArr_Delegate OnReceiveData;



        public SerialPortClient(string mac, SerialPortConfig config) {

            this._mac = mac;

            com = new SerialPort();
            com.PortName = config.PortName;
            com.BaudRate = config.BaudRate;
            com.DataBits = config.DataBits;
            com.Parity = config.Parity;
            com.StopBits = config.StopBits;

        
        }




        public string Send(byte[] data)
        {

            this._data = data;
            return "";
        
        }


        /// <summary>
        /// 向串口发送数据
        /// </summary>
        private void Send()
        {

            while (true)
            {

                if (_data!=null&&_data.Length > 0)
                {
                    if (com.IsOpen)
                    {

                        //清空缓存区
                        com.DiscardOutBuffer();
                        com.DiscardInBuffer();

                        com.Write(_data, 0, _data.Length);

                        //Console.WriteLine(this._meterId + "发送:" + ConvertHelper.GetHex(_data));

                        this._data = null;
                    }
                } 
                Thread.Sleep(1000);
            }

        }





        /// <summary>
        /// 接收串口数据
        /// </summary>
        private void Receive()
        {


            //接收回复数据给字段列表赋值
            while (true)
            {
                Thread.Sleep(100);
                if (com.IsOpen)
                {
                    try
                    {

                        byte[] readBuffer = new byte[com.BytesToRead];

                        if (readBuffer.Length == 0) { continue; }

                        int count = com.Read(readBuffer, 0, readBuffer.Length);

                        //Console.WriteLine(this._meterId + "接收:" + ConvertHelper.GetHex(readBuffer));


                        ////解密
                        //readBuffer = EncryptHelper.Decrypt(readBuffer);

                        //判断包头 仪表类型  校验码 结束符 地址

                        //if (!Frame.CheckFrame(readBuffer,this._meterId))
                        //{ 
                        //    continue; 
                        //}

                        this.OnReceiveData(readBuffer);

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    TimeSpan waitTime = new TimeSpan(0, 0, 0, 0, 50);
                    Thread.Sleep(waitTime);
                }

            }
        }



        /// <summary>
        /// 激活端口
        /// </summary>
        /// <returns></returns>
        public string Activate() {

            try
            {
                com.Open();

                threadsend = new Thread(new ThreadStart(Send));
                threadsend.IsBackground = true;
                threadsend.Start();

                threadrev = new Thread(new ThreadStart(Receive));
                threadrev.IsBackground = true;
                threadrev.Start();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return "";
        
        
        
        }

    }



    public delegate void ByteArr_Delegate(byte[] data);
}




