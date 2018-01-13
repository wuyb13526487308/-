using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IotM.WCFServiceRegister;
using CY.IoTM.Channel;
using CY.IoTM.DataCollectionService;
using CY.IoTM.DataTransmitHelper.Factory;
using System.Diagnostics;

namespace CY.IoTM.CollectionService.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //string hexstr = "43 8C E8 F5";
            string hexstr = "01 03 04 42 40 66 66 44 15";
            /*01 03 1E 
            3 00 02 
            5 43 8F A8 F5 
            9 00 00 
            11 02 9A 45 D0

            15 55 48 43 3A
            19 80 00 44 D0 
            23 50 00 41 D9 
            
            99 9A 00 03 00 20 71 89
            **/

            byte[] data = strToToHexByte(hexstr);
            int iIndex = 0;
            //float WenDu = BitConverter.ToSingle(getIEEE(data, iIndex + 3 + 2 + 4 + 6 + 4 + 4), 0);

            //float v = BitConverter.ToSingle(data, 0);
            //System.Console.WriteLine("{0}", WenDu);

            float ZongYongLiang = BytesTouInt16(data, iIndex + 3) * 10000;
            System.Console.WriteLine("{0}", ZongYongLiang);
            ZongYongLiang = BitConverter.ToSingle(getIEEE(data, iIndex + 3), 0);
            System.Console.WriteLine("{0}", ZongYongLiang);

            //int i = 0;
            //while (true)
            //{
            //    System.Console.WriteLine("{0}", Convert.ToByte(new Random().Next(0, 255)));

            //    System.Threading.Thread.Sleep(1000 * 3);
            //    i++;
            //    if (i > 10)
            //        break;
            //}

            int port = 9000;
            port = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["WCFServicePort"]);

            RegisterWCFService wcf = new RegisterWCFService("localhost", port);
            wcf.RunWCFService();

            DataRecordQueueFactory.getInstance().StartService();

            System.Console.WriteLine(DataChannelFactoryService.getInstance().StartChannelService());
            DDService.getInstance();

             

            while (true)
            {
                System.Console.Read();
            }
        }

        private static int BytesTouInt16(byte[] buf, int iIndex)
        {
            int iValue = 0;
            iValue = buf[iIndex + 1];
            iValue += buf[iIndex] << 8;
            return iValue;
        }

        private static byte[] getIEEE(byte[] buf, int iIndex)
        {
            byte[] buffer = new byte[4];
            //byte[] aa = {0xB8, 0xD5, 0xCB,   0x41 };

            //41 CB D5 B8
            buffer[0] = buf[iIndex + 3];
            buffer[1] = buf[iIndex + 2];
            buffer[2] = buf[iIndex + 1];
            buffer[3] = buf[iIndex + 0];

            buffer[0] = buf[iIndex + 1];
            buffer[1] = buf[iIndex + 0];
            buffer[2] = buf[iIndex + 3];
            buffer[3] = buf[iIndex + 2];

            buffer[0] = buf[iIndex + 3];
            buffer[1] = buf[iIndex + 2];
            buffer[2] = buf[iIndex + 1];
            buffer[3] = buf[iIndex + 0];

            return buffer;
        }
        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

    }
}
