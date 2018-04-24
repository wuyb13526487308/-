using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneNETDataReceiver.Proxy
{
    public class Param
    {
        public static String token = "123456789";//在OneNET配置的token
        public static String aeskey = "ew8tYG6cMXcCsBGShj4NmA2mtbJh6eA53cAHVfCaVLn";//在OneNET配置时“消息加解密方式”选择“安全模式”下的EncodingAESKey，安全模式下必填
        public static bool isJiaMi = false;
        public static string url = "http://api.heclouds.com/";
        public static string appkey = "tqdK8g8NzfQQ4=JCVHMaprtfhCw=";//您在OneNET平台的APIKey
    }
}