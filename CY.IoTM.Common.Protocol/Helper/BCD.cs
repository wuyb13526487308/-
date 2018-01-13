using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common
{
    public static class BCD
    {
        /// <summary>
        /// string转bcd
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte S2B(string str)
        {
            try
            {
                return (byte)System.Convert.ToInt32(str, 16);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// string转bcd数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] S2BArr(string asc)
        {
            int len = asc.Length;
            int mod = len % 2;

            if (mod != 0)
            {
                asc = "0" + asc;
                len = asc.Length;
            }

            byte[] abt = new byte[len];
            if (len >= 2)
            {
                len = len / 2;
            }

            byte[] bbt = new byte[len];
            abt = System.Text.Encoding.ASCII.GetBytes(asc);
            int j, k;

            for (int p = 0; p < asc.Length / 2; p++)
            {
                if ((abt[2 * p] >= '0') && (abt[2 * p] <= '9'))
                {
                    j = abt[2 * p] - '0';
                }
                else if ((abt[2 * p] >= 'a') && (abt[2 * p] <= 'z'))
                {
                    j = abt[2 * p] - 'a' + 0x0a;
                }
                else
                {
                    j = abt[2 * p] - 'A' + 0x0a;
                }

                if ((abt[2 * p + 1] >= '0') && (abt[2 * p + 1] <= '9'))
                {
                    k = abt[2 * p + 1] - '0';
                }
                else if ((abt[2 * p + 1] >= 'a') && (abt[2 * p + 1] <= 'z'))
                {
                    k = abt[2 * p + 1] - 'a' + 0x0a;
                }
                else
                {
                    k = abt[2 * p + 1] - 'A' + 0x0a;
                }

                int a = (j << 4) + k;
                byte b = (byte)a;
                bbt[p] = b;
            }
            return bbt;
        }
        /// <summary>
        /// bcd转string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string B2S(byte data)
        {
            //转16进制数
            return data.ToString("X2");
        }
        
        /// <summary>
        /// bcd数组转string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string B2S(byte[] data)
        {

            string str = "";
            try
            {
                for (int i = 0; i < data.Length; i++)
                {
                    str += B2S(data[i]);
                }
                return str;
            }
            catch
            {
                return str;
            }
        }
        
        public static byte I2B(int data)
        {
            data &= 0xff;//防止数据溢出

            return (byte)System.Convert.ToInt32(data.ToString(), 16);
        }

        public static byte[] I2B(int data,int length)
        {

            string str = data.ToString();

            str = str.PadLeft(2 * length, '0');

            byte[] tempArray = new byte[length];

            for (int i = 0; i < length; i++)
            {
                tempArray[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
            }
            return tempArray;
        }
        
        public static int B2I(byte data)
        {
            if (((data & 0x0f) > 0x09) || (data & 0xf0) > 0x90) return 0;
            return int.Parse(data.ToString("X2"));
        }
        
        public static int B2I(byte[] data)
        {
            string str = "";
            try
            {
                for (int i = 0; i < data.Length; i++)
                {
                    str += B2I(data[i]);
                }
                return int.Parse(str);
            }
            catch
            {
                return 0;
            }
        }
        
        public static string H2B(int value)
        {
            string tmp = h2b(value);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < tmp.Length; i++)
                sb.Append(tmp.Substring(tmp.Length - i - 1, 1));

            return sb.ToString();
        }

        private static string h2b(int value)
        {
            string tmp = "";
            tmp += (value % 2).ToString();
            value = (value / 2);
            if (value == 0)
                return tmp;
            else
                return tmp + h2b(value);
        }

        public static string PadLeft(string str, int len, char ch)
        {
            int iLength = len - str.Length;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < iLength; i++)
                sb.Append(ch);
            sb.Append(str);
            return sb.ToString();
        }
    }

   



}
