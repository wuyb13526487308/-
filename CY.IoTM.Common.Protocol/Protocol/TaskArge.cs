using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CY.IoTM.Common
{
    /// <summary>
    /// 对表读写指令的参数类
    /// </summary>
    public class TaskArge
    {
        DataItem _data;
        ControlCode _controlcode;    
        string _lotMac;
        byte[] _MKey;
        IotProtocolType _protocolType = IotProtocolType.RanQiBiao;
        public TaskArge(string mac, DataItem data, ControlCode controlcode, byte[] key=null)
        {
            this._data = data;
            this._controlcode = controlcode;
            this._MKey = key;
            if (mac.Length > 14)
                this._lotMac = mac.Substring(0, 14);
            else if (mac.Length < 14)
                this._lotMac = mac.PadLeft(14, '0');
            else
                this._lotMac = mac;
        }

        public TaskArge(string mac, DataItem data, ControlCode controlcode, IotProtocolType protocolType ,byte[] key = null)
        {
            this._data = data;
            this._controlcode = controlcode;
            this._protocolType = protocolType;
            this._MKey = key;
            if (mac.Length > 14)
                this._lotMac = mac.Substring(0, 14);
            else if (mac.Length < 14)
                this._lotMac = mac.PadLeft(14, '0');
            else
                this._lotMac = mac;
        }

        /// <summary>
        /// 数据指令对象
        /// </summary>
        public DataItem Data
        {
            get
            {
                return this._data;
            }
        }
        /// <summary>
        /// 控制码
        /// </summary>
        public ControlCode ControlCode
        {
            get
            {
                return _controlcode;
            }
        }     
        /// <summary>
        /// 
        /// </summary>
        public String IoTMac 
        { 
            get
            { 
                return _lotMac; 
            }
        }
        /// <summary>
        /// 获取数据加密密钥
        /// </summary>
        public byte[] MKey
        {
            get { return _MKey; }
        }
        /// <summary>
        /// 获取协议类型
        /// </summary>
        public IotProtocolType IotProtocolType
        {
            get { return this._protocolType; }
        }
    }
}
