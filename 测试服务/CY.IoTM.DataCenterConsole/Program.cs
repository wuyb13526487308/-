using CY.IotM.WCFServiceRegister;
using CY.IoTM.DataTransmitHelper.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.DataCenterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
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
