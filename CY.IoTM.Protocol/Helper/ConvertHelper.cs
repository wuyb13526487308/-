using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.LoTM.Protocol
{
    internal static class ConvertHelper
    {

        public static Byte[] Reverse(Byte[] data)
        {
            Array.Reverse(data);
            return data;
        }

        public static string GetHex(byte[] buffer)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
                sb.Append(string.Format(" {0:X2}", buffer[i]));
            return sb.ToString();
        }



    }
}
