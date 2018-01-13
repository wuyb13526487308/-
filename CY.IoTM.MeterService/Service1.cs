using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using CY.IoTM.Channel;
using CY.IoTM.DataCollectionService;

namespace CY.IoTM.MeterService
{
    public partial class IotMService : ServiceBase
    {
        public IotMService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            DataChannelFactoryService.getInstance().StartChannelService();
            DDService.getInstance();
        }

        protected override void OnStop()
        {
            DataChannelFactoryService.getInstance().StopChannelService();
            DDService.getInstance().UnRegister();
        }

        protected override void OnShutdown()
        {
            DataChannelFactoryService.getInstance().StopChannelService();
            DDService.getInstance().UnRegister();
        }
    }
}
