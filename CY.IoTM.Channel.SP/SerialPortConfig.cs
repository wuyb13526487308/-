using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;

namespace CY.IoTM.Channel.SP
{
    public class SerialPortConfig
    {


        private string _portName;
       /// <summary>
        /// 端口名
       /// </summary>
        public string PortName { get { return _portName; } }

        private int _baudRate;
        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get { return _baudRate; } }


        private int _dataBits;
        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get { return _dataBits; } }


        private Parity _parity;
        /// <summary>
        /// 奇偶校验位
        /// </summary>
        public Parity Parity { get { return _parity; } }

        private StopBits _stopBits;
        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits StopBits { get { return _stopBits; } }





        public SerialPortConfig(string portName, int baudRate, int dataBits, string parity, string stopBits)
        {
             
            //端口名
            this._portName = portName;
            //波特率
            this._baudRate = baudRate;
            //数据位
            this._dataBits = dataBits;


            //奇偶校验位
            this._parity = Parity.None;
            switch (parity)
            {

                case "NONE":
                    _parity = Parity.None;
                    break;
                case "EVEN":
                    _parity = Parity.Even;
                    break;
                case "ODD":
                    _parity = Parity.Odd;
                    break;
                case "MARK":
                    _parity = Parity.Mark;
                    break;
                case "SPACE":
                    _parity = Parity.Space;
                    break;
                default:
                    _parity = Parity.None;
                    break;

            }
            //停止位
            this._stopBits = StopBits.One;
            switch (stopBits)
            {

                case "1":
                    _stopBits = StopBits.One;
                    break;
                case "1.5":
                    _stopBits = StopBits.OnePointFive;
                    break;
                case "2":
                    _stopBits = StopBits.Two;
                    break;
                default:
                    _stopBits = StopBits.None;
                    break;

            }
         
        
        }




    }
}
