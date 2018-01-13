﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace CY.IoTM.Common
{
    public class WCFServiceProxy<TChannel> : ClientBase<TChannel> where TChannel : class
    {       
        public WCFServiceProxy()
            : base()
        {
        }
        public WCFServiceProxy(InstanceContext callbackInstance)
            : base(callbackInstance)
        {
        }

        public TChannel getChannel
        {
            get
            {
                return base.Channel;
            }
        }
        protected override TChannel CreateChannel()
        {
            return base.CreateChannel();
        }
        public void CloseChannel()
        {
            try
            {
                if (this.State != System.ServiceModel.CommunicationState.Faulted)
                {
                    this.Close();
                }
            }
            catch
            {
                this.Abort();
            }
        }
    }
}
