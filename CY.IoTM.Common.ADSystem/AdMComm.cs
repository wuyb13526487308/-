using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.ADSystem
{
    public class AdMComm
    {
        //******************************进制转换补零*******************************
        public static string GetAddZero(int num,int strLen)
        {
            string writeStr = num.ToString();
            for (int i = 0; i < strLen - num.ToString().Length; i++)
            {

                writeStr = "0" + writeStr;
               
            }
            return writeStr;
        }


        /// <summary>
        /// 字段串是否为Null或为""(空)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StrIsNullOrEmpty(string str)
        {
            if (str == null || str.Trim() == string.Empty)
                return true;

            return false;
        }

        /// <summary>
        /// 模糊判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayID_like(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    //if (strSearch.ToLower() == stringArray[i].ToLower())
                    if (stringArray[i].ToLower().IndexOf(strSearch.ToLower()) >= 0)
                        return i;
                }
                else if (stringArray[i].IndexOf(strSearch) >= 0)
                    return i;
            }
            return -1;
        }
    }

    public static class SysCookie {

        /// <summary>
        /// 类似网页版Url中?号后面参数值,规则:para1?value|para2?value;
        /// </summary>
        private static string urlParaStr;
        public static string UrlParaStr
        {
            get { return urlParaStr; }
            set { urlParaStr = value; }
        }
        //-------------------------------------------------------------------
        //设定UrlParaStr对应几个方法,后期可单独提取
        /// <summary>
        /// 获得某一个参数值;
        /// </summary>
        /// <param name="paraStr"></param>
        /// <returns></returns>
        public static string RequestParaValue(string paraStr)
        {
            //提取Url字符串
            string urlParaStr = SysCookie.UrlParaStr;
            string reStr = "";
            //确认是否为空和是否存在传入的提取变量名称
            if (!AdMComm.StrIsNullOrEmpty(urlParaStr) && urlParaStr.IndexOf(paraStr) >= 0)
            {
                //对字符串进行解析
                string[] proArray = urlParaStr.Split('|');
                //得到字符串所在组ID
                int proID = AdMComm.GetInArrayID_like(paraStr, proArray, false);
                //二次分拆
                if (proID >= 0)
                {
                    reStr = proArray[proID].Split('?')[1];
                }
                else
                {
                    reStr = "NoInfo";
                }
            }
            else { reStr = "NoInfo"; }

            return reStr;
        }


    }

    //定义一个广告用户统计的实体
    public class ADUserSC
    {
        //街道
        private string street;
        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        //小区
        private string  community;
        /// <summary>
        /// 起始日期
        /// </summary>
        public string Community
        {
            get { return community; }
            set { community = value; }
        }

        //用户数
        private int num;
        /// <summary>
        /// 起始日期
        /// </summary>
        public int Num
        {
            get { return num; }
            set { num = value; }
        }
    }




}
