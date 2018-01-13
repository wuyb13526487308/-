using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using CY.IoTM.Channel;
using CY.IoTM.DataCollectionService;

namespace CY.IoTM.MeterService
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
                new IotMService() 
            };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                System.Console.WriteLine(DataChannelFactoryService.getInstance().StartChannelService());
                DDService.getInstance();

                while (true)
                {
                    System.Console.Read();
                } 

            }
        }
    }
}
