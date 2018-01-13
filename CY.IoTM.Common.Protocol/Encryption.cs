
//可以在该项目的编译条件中增加 JiMi 条件
#define JiaMi

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Protocol
{

    public class Encryption
    {
        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="data">明文数据</param>
        /// <param name="key">密钥</param>
        /// <returns>返回密文</returns>
        public static byte[] Encry(byte[] data, byte[] key)
        {
#if JiaMi
            MemoryStream memStr = new MemoryStream();
            int ilength = data.Length / 8;
            for (int i = 0; i < ilength; i++)
                memStr.Write(YiHuoYunSuan(data, i * 8, key), 0, 8);

            int syLength = data.Length % 8;
            if (syLength > 0)
            {
                byte[] tmp = new byte[8];
                for (int i = 0; i < syLength; i++)
                    tmp[i] = data[(ilength) * 8 + i];

                memStr.Write(YiHuoYunSuan(tmp, 0, key), 0, syLength);
            }
            byte[] buffer = new byte[data.Length];
            memStr.Position = 0;
            memStr.Read(buffer, 0, data.Length);
            return buffer;
#else
            return data;
#endif

        }

        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>返回明文数据</returns>
        public static byte[] Decry(byte[] data, byte[] key)
        {
#if JiaMi

            return Encry(data, key);
#else
            return data;
#endif
        }
        private static byte[] YiHuoYunSuan(byte[] data, int offset, byte[] key)
        {
            byte[] tmp = new byte[8];
            {
                for (int i = 0; i < 8; i++)
                    tmp[i] = (byte)(data[i + offset] ^ key[i]);
            }
            return tmp;
        }
    }
}
