using CY.IotM.WCFServiceRegister;
using CY.IoTM.DataTransmitHelper.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace CY.IoTM.BusinessCenterService
{
    public partial class IoTMBCService : ServiceBase
    {
        public IoTMBCService()
        {
            InitializeComponent();
        }
        RegisterWCFService wcf;
        protected override void OnStart(string[] args)
        {
            int port = 9000;
            port = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["WCFServicePort"]);
            wcf = new RegisterWCFService("localhost", port);
            wcf.RunWCFService();
            DataRecordQueueFactory.getInstance().StartService();
        }

        protected override void OnStop()
        {
            if (wcf != null)
            {
                wcf.Close();
            }
            DataRecordQueueFactory.getInstance().StopService();
        }

        protected override void OnShutdown()
        {
            if (wcf != null)
            {
                wcf.Close();
            }
            DataRecordQueueFactory.getInstance().StopService();
        }
    }
}
