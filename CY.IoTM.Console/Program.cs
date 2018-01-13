using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IotM.WCFServiceRegister;

namespace CY.IoTM.Console
{
    class Program
    {
        static void Main(string[] args)
        {


            //string meterId="39615123355104";

            //string meterType="SerialPort";

            //Channel.IDataChannel  channel= Channel.DataChannelFactoryService.getInstance().getDataChannel(meterId,meterType);

            //TestMeter meter = new TestMeter(meterId, channel);



            ////读操作
            //DataItem item=new DataItem_8103();
            //TaskArge data = new TaskArge(meterId, item,ControlCode.ReadData);
            //meter.SendData(data);


            ////写操作
            //CT ct = new CT(MeterType.气量表, true, JieSuanType.全年度, 5);
            //DataItem item_A010 = new DataItem_A010(ct,2.3m,100,3.1m,200,4.7m,300,5.3m,400,6.8m,21);

            //TaskArge data_A010 = new TaskArge(meterId, item_A010, ControlCode.WriteData);

            //meter.SendData(data_A010);




            RegisterWCFService wcf = new RegisterWCFService("localhost", 9000);
            wcf.RunWCFService();
 

            System.Console.Read();


        }
    }
}
