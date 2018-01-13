using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IoTM.Common;
using CY.IoTM.Channel;

namespace CY.IoTM.Protocol
{
    /// <summary>
    /// 物联网表处理类
    /// </summary>
    public class LoTMeter
    {
        private string _meterId;
        private IDataChannel  _channel;
        private Frame _frame;

        public event RecordData_Delegate OnReceiveData;

        public LoTMeter(IDataChannel channel){        
             this._channel=channel;
             this._meterId = this._channel.MAC;
        }

     
        /// <summary>
        /// 控制表（读取、设置表相关数据）
        /// </summary>
        /// <param name="arge"></param>
        /// <returns></returns>
        public RecordData Control(TaskArge arge)
        {
            this._frame = new Frame(arge);
            this._channel.Send(this._frame.GetBytes());
            return null;
        }


        public string SendData(TaskArge arge)
        {
            this._frame = new Frame(arge);
            byte[] data = this._frame.GetBytes();
            this._channel.Send(data);

            return ConvertHelper.GetHex(data);
        }


        public void ReceiveData(Channel_DATA data)
        {
            RecordData recordData = new RecordData();
            //this._frame = new Frame(data.m_buffer);

            recordData.LoTMac = _frame.Adress;
            //recordData.ControlCode = _frame.ControlCode;

            //recordData.IdentityCode = _frame.IdentityCode;
            //recordData.RevTime = data.m_recv_date;

            this.OnReceiveData(recordData);

        }
    }

    public delegate void RecordData_Delegate(RecordData data);
}
