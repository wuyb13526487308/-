using CY.IotM.WCFServiceRegister;
using CY.IoTM.DataTransmitHelper.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace CY.IoTM.BusinessCenterService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length==0)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new IoTMBCService() 
                };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
               int port = 9000;
                port = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["WCFServicePort"]);

                RegisterWCFService wcf = new RegisterWCFService("localhost", 9000);
                wcf.RunWCFService();
                DataRecordQueueFactory.getInstance().StartService();
                while (true)
                {
                    System.Console.Read();
                }
            }
        }
    }
}
