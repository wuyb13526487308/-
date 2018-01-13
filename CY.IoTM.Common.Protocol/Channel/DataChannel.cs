using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Channel
{
    public abstract class DataChannel:IDataChannel
    {

        #region IDataChannel 成员

        public event Channel_InfoDelegate OnCreateChannel;

        public event Channel_InfoDelegate OnCloseChannel;

        public event Channel_DATA_Delegate OnReceiveData;

        public string MAC
        {
            get { return _mac; }
        }

        public string ChannelType
        {
            get { return _channelType; }
        }


        protected string _mac;


        protected string _channelType;


        public Common.Link ChannelLink
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public virtual void Send(byte[] data)
        {

            throw new NotImplementedException();
        }
        #endregion

        public DataChannel(string mac, string channelType)
        {
            this._channelType = channelType;
            this._mac = mac;

        }
    }
}
