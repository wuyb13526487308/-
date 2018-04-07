using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CY.IoTM.Common
{
    /// <summary>
    /// 数据转换类
    /// </summary>
    public class MyDataConvert
    {
        /// <summary>
        /// 将BCD码（低位在前，高位在后)表示4字节数组转换为decimal类型数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static decimal FourBCDToDecimal(byte[] buffer, int index)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("{0:X2}", buffer[index + 3]));
            sb.Append(string.Format("{0:X2}", buffer[index + 2]));
            sb.Append(string.Format("{0:X2}.", buffer[index + 1]));
            sb.Append(string.Format("{0:X2}", buffer[index + 0]));
            Log.Log.getInstance().Write(sb.ToString(),Log.MsgType.Information);
            return Convert.ToDecimal(sb.ToString());
        }

        /// <summary>
        /// 将带符号的BCD码 格式：SN NN NN.NN 其中S为符号位，S = 0 表示正数 S不等于 0 表示负数（低位在前，高位在后)表示4字节数组转换为decimal类型数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static decimal FourBCDToDecimal_Symbol(byte[] buffer, int index)
        {
            StringBuilder sb = new StringBuilder();
            string str = string.Format("{0:X2}", buffer[index + 3]);
            sb.Append(str == "AA" ? "-" : str);
            //sb.Append(str.Substring(1, 1));

            sb.Append(string.Format("{0:X2}", buffer[index + 2]));
            sb.Append(string.Format("{0:X2}.", buffer[index + 1]));
            sb.Append(string.Format("{0:X2}", buffer[index + 0]));
            return Convert.ToDecimal(sb.ToString());
        }

        /// <summary>
        /// 将decimal数据转换为4个字节的BCD码数据（低位在前，高位在后)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] DecimalToFourBCD(decimal value)
        {
            value = Convert.ToDecimal(Math.Abs(value).ToString("0.00"));

            byte[] dataUnits = new byte[4];
            string strLJGas = Convert.ToInt64(Math.Abs(value) * 100).ToString().PadLeft(8, '0');
            dataUnits[0] = (byte)Convert.ToInt32(strLJGas.Substring(6, 2), 16);//
            dataUnits[1] = (byte)Convert.ToInt32(strLJGas.Substring(4, 2), 16);//
            dataUnits[2] = (byte)Convert.ToInt32(strLJGas.Substring(2, 2), 16);//
            dataUnits[3] = (byte)Convert.ToInt32(strLJGas.Substring(0, 2), 16);//
            return dataUnits;
        }

        /// <summary>
        /// 将decimal转换为4字节BCD，格式：AA NN NN.NN 其中S为符号位，AA 表示负数 其他表示正数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] DecimalToFourBCD_Symbol(decimal value)
        {
            value = Convert.ToDecimal(value.ToString("0.00"));

            byte[] dataUnits = new byte[4];
            int symbol = 0;
            if (value < 0) symbol = -1;
            StringBuilder sb = new StringBuilder();
            string strLJGas = Convert.ToInt64(Math.Abs(value) * 100).ToString();
            if (symbol >= 0)
            {
                //正数
                if (strLJGas.Length > 8)
                    sb.Append(strLJGas.Substring(strLJGas.Length - 8, 8));
                else if (strLJGas.Length < 8)
                    sb.Append(strLJGas.PadLeft(8, '0'));
                else
                    sb.Append(strLJGas);
                strLJGas = sb.ToString();
                dataUnits[3] = (byte)Convert.ToInt32(strLJGas.Substring(0, 2), 16);//
            }
            else
            {
                //负数情况
                sb.Append("AA");
                if (strLJGas.Length > 6)
                    sb.Append(strLJGas.Substring(strLJGas.Length - 6, 6));
                else if (strLJGas.Length < 6)
                    sb.Append(strLJGas.PadLeft(6, '0'));
                else
                    sb.Append(strLJGas);
                strLJGas = sb.ToString();
                dataUnits[3] = 0xAA;// (byte)Convert.ToInt32(strLJGas.Substring(0, 2), 16);//
            }

            dataUnits[0] = (byte)Convert.ToInt32(strLJGas.Substring(6, 2), 16);//
            dataUnits[1] = (byte)Convert.ToInt32(strLJGas.Substring(4, 2), 16);//
            dataUnits[2] = (byte)Convert.ToInt32(strLJGas.Substring(2, 2), 16);//
            return dataUnits;
        }


        /// <summary>
        /// 将decimal数据转换为4个字节的BCD码数据（低位在前，高位在后)并写入指定数组中
        /// </summary>
        /// <param name="value"></param>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        public static void DecimalToFourBCD(decimal value, byte[] buffer, int index)
        {
            value = Convert.ToDecimal(Math.Abs (value).ToString("0.00"));
            //if (value > 999999.99m) throw new Exception("数值大于999999.99");

            string str = Convert.ToInt64(value * 100).ToString().PadLeft(8, '0');
            if (str.Length > 8) str = str.Substring(str.Length - 8, 8);
            buffer[index + 0] = (byte)Convert.ToInt32(str.Substring(6, 2), 16);//
            buffer[index + 1] = (byte)Convert.ToInt32(str.Substring(4, 2), 16);//
            buffer[index + 2] = (byte)Convert.ToInt32(str.Substring(2, 2), 16);//
            buffer[index + 3] = (byte)Convert.ToInt32(str.Substring(0, 2), 16);//
        }
        /// <summary>
        /// 将decimal数据转换为4个字节的BCD码数据 格式：SN NN NN.NN 其中S为符号位，S = 0 表示正数 S不等于 0 表示负数（低位在前，高位在后)并写入指定数组中
        /// </summary>
        /// <param name="value"></param>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        public static void DecimalToFourBCD_Symbol(decimal value, byte[] buffer, int index)
        {
            value = Convert.ToDecimal(value.ToString("0.00"));

            //if (value > 999999.99m) throw new Exception("数值大于999999.99");
            int symbol = 0;
            if (value < 0) symbol = -1;
            StringBuilder sb = new StringBuilder();
            string strLJGas = Convert.ToInt64(Math.Abs(value) * 100).ToString();
            if (symbol >= 0)
            {
                //正数
                if (strLJGas.Length > 8)
                    sb.Append(strLJGas.Substring(strLJGas.Length - 8, 8));
                else if (strLJGas.Length < 8)
                    sb.Append(strLJGas.PadLeft(8, '0'));
                else
                    sb.Append(strLJGas);
                strLJGas = sb.ToString();
                buffer[index + 3] = (byte)Convert.ToInt32(strLJGas.Substring(0, 2), 16);//
            }
            else
            {
                //负数情况
                sb.Append("AA");
                if (strLJGas.Length > 6)
                    sb.Append(strLJGas.Substring(strLJGas.Length - 6, 6));
                else if (strLJGas.Length < 6)
                    sb.Append(strLJGas.PadLeft(6, '0'));
                else
                    sb.Append(strLJGas);
                strLJGas = sb.ToString();
                buffer[index + 3] = 0xAA;// (byte)Convert.ToInt32(str.Substring(0, 2), 16);//
            }

            buffer[index + 0] = (byte)Convert.ToInt32(strLJGas.Substring(6, 2), 16);//
            buffer[index + 1] = (byte)Convert.ToInt32(strLJGas.Substring(4, 2), 16);//
            buffer[index + 2] = (byte)Convert.ToInt32(strLJGas.Substring(2, 2), 16);//
        }

        /// <summary>
        /// 将DateTime类型数据转换为7字节BCD码数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] DateTimeToSevenBCD(DateTime value)
        {
            byte[] dataUnits = new byte[7];
            dataUnits[6] = (byte)Convert.ToInt32(value.Year.ToString().Substring(0, 2), 16);//年高2位
            dataUnits[5] = (byte)Convert.ToInt32(value.Year.ToString().Substring(2, 2), 16);//年低2位
            dataUnits[4] = (byte)Convert.ToInt32(value.Month.ToString().PadLeft(2, '0').Substring(0, 2), 16);//月份
            dataUnits[3] = (byte)Convert.ToInt32(value.Day.ToString().PadLeft(2, '0').Substring(0, 2), 16);//日
            dataUnits[2] = (byte)Convert.ToInt32(value.Hour.ToString().PadLeft(2, '0').Substring(0, 2), 16);//小时
            dataUnits[1] = (byte)Convert.ToInt32(value.Minute.ToString().PadLeft(2, '0').Substring(0, 2), 16);//分钟
            dataUnits[0] = (byte)Convert.ToInt32(value.Second.ToString().PadLeft(2, '0').Substring(0, 2), 16);//秒
            return dataUnits;
        }
        /// <summary>
        /// 将dateTime类型转换为7字节BCD码并写入字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        public static void DateTimeToSevenBCD(DateTime value, byte[] buffer, int index)
        {
            buffer[index + 6] = (byte)Convert.ToInt32(value.Year.ToString().Substring(0, 2), 16);//年高2位
            buffer[index + 5] = (byte)Convert.ToInt32(value.Year.ToString().Substring(2, 2), 16);//年低2位
            buffer[index + 4] = (byte)Convert.ToInt32(value.Month.ToString().PadLeft(2, '0').Substring(0, 2), 16);//月份
            buffer[index + 3] = (byte)Convert.ToInt32(value.Day.ToString().PadLeft(2, '0').Substring(0, 2), 16);//日
            buffer[index + 2] = (byte)Convert.ToInt32(value.Hour.ToString().PadLeft(2, '0').Substring(0, 2), 16);//小时
            buffer[index + 1] = (byte)Convert.ToInt32(value.Minute.ToString().PadLeft(2, '0').Substring(0, 2), 16);//分钟
            buffer[index + 0] = (byte)Convert.ToInt32(value.Second.ToString().PadLeft(2, '0').Substring(0, 2), 16);//秒
        }
        /// <summary>
        /// 将7字节数字表示的日期转换为DateTime数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static DateTime SevenBCDToDateTime(byte[] buffer, int index)
        {
            int year = Convert.ToInt32(string.Format("{0:X2}{1:X2}", buffer[index + 6], buffer[index + 5]));
            int month = Convert.ToInt32(string.Format("{0:X2}", buffer[index + 4]));
            int day = Convert.ToInt32(string.Format("{0:X2}", buffer[index + 3]));
            int hour = Convert.ToInt32(string.Format("{0:X2}", buffer[index + 2]));
            int minute = Convert.ToInt32(string.Format("{0:X2}", buffer[index + 1]));
            int second = Convert.ToInt32(string.Format("{0:X2}", buffer[index + 0]));
            return new DateTime(year, month, day, hour, minute, second);
        }
        /// <summary>
        /// 将decimal数据转换为3字节BCD码并写入字节数组指定位置
        /// </summary>
        /// <param name="value"></param>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        public static void DecimalToThreeBDC(decimal value, byte[] buffer, int index)
        {
            value = Convert.ToDecimal(Math.Abs(value).ToString("0.00"));
            //if (value > 9999.9900m) throw new Exception("数值大于9999.99");
            string str = Convert.ToInt64(value * 100).ToString().PadLeft(6, '0');
            if (str.Length > 6) str = str.Substring(str.Length - 6, 6);

            buffer[index + 0] = (byte)Convert.ToInt32(str.Substring(4, 2), 16);//
            buffer[index + 1] = (byte)Convert.ToInt32(str.Substring(2, 2), 16);//
            buffer[index + 2] = (byte)Convert.ToInt32(str.Substring(0, 2), 16);//
        }

        public static decimal ThreeBCDToDecimal(byte[] buffer, int index)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("{0:X2}", buffer[index + 2]));
            sb.Append(string.Format("{0:X2}.", buffer[index + 1]));
            sb.Append(string.Format("{0:X2}", buffer[index + 0]));
            return Convert.ToDecimal(sb.ToString());
        }

        /// <summary>
        /// 将decimal数据转换为2字节BCD码并写入字节数组指定位置
        /// </summary>
        /// <param name="value"></param>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        public static void DecimalToTwoBDC(decimal value, byte[] buffer, int index)
        {
            value = Convert.ToDecimal(Math.Abs(value).ToString("0.00"));
            //if (value > 99.9900m) throw new Exception("数值大于99.99");
            string str = Convert.ToInt64(value * 100).ToString().PadLeft(4, '0');
            if (str.Length > 4) str = str.Substring(str.Length - 4, 4);

            buffer[index + 0] = (byte)Convert.ToInt32(str.Substring(2, 2), 16);//
            buffer[index + 1] = (byte)Convert.ToInt32(str.Substring(0, 2), 16);//
        }


        public static decimal TwoBCDToDecimal(byte[] buffer, int index)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("{0:X2}.", buffer[index + 1]));
            sb.Append(string.Format("{0:X2}", buffer[index + 0]));
            return Convert.ToDecimal(sb.ToString());
        }

        public static decimal TwoBCDStrToDecimal(string bcd)
        {
            byte[] buffer = new byte[2];
            if (bcd.Length < 4)
                bcd = bcd.PadLeft(4, '0');
            buffer[1] = BCD.S2B(bcd.Substring(0, 2));
            buffer[0] = BCD.S2B(bcd.Substring(2, 2));
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("{0:X2}.", buffer[1]));
            sb.Append(string.Format("{0:X2}", buffer[0]));
            return Convert.ToDecimal(sb.ToString());

        }
        public static IdentityCode get数据表示符(byte[] buffer)
        {
            IdentityCode ident = IdentityCode.出厂启用;
            byte[] b = { buffer[1], buffer[0] };
            ident = (IdentityCode)BitConverter.ToUInt16(b, 0);
            return ident;
        }

        /// <summary>
        /// 16进制字符串转字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] StrToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static string BytesToHexStr(byte[] buffer)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
                sb.Append(string.Format("{0:X2} ", buffer[i]));
            return sb.ToString();
        }
    }
}
