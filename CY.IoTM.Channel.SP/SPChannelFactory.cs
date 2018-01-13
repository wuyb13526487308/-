using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;


namespace CY.IoTM.Channel.SP
{
    /// <summary>
    /// 串口通道工厂
    /// </summary>
    [Export(typeof(ICreateDataChannel))]
    public class SPChannelFactory : ICreateDataChannel
    {


        protected string _channelType = "SerialPort";

        /// <summary>
        /// 获取数据通道类型编码
        /// </summary>
        public string ChannelType
        {
            get { return _channelType; }
        }
   

        /// <summary>
        /// 创建数据通道
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns></returns>
        public IDataChannel CreateDataChannel(String meterId) {


            return new SP_DataChannel(meterId,this._channelType);
        
        }


        #region ICreateDataChannel 成员


        public string StartService()
        {
            throw new NotImplementedException();
        }

        public void StopService()
        {
            throw new NotImplementedException();
        }

        #endregion


        public int GetConectionCount()
        {
            return 0;
        }
    }
}
