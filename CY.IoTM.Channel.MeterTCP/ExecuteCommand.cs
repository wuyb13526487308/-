using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IoTM.Common;
using CY.IoTM.Common.Business;
using CY.IoTM.Common.Item;
using CY.IoTM.Common.Item.读操作;
using CY.IoTM.Common.Protocol;
using CY.IoTM.Common.Log;

namespace CY.IoTM.Channel.MeterTCP
{
    /// <summary>
    /// 指令执行后状态更新
    /// </summary>
    class ExecuteCommand
    {
        Command _cmd;
        Task _task;
        ITaskManage _iTaskManage;
        private bool isFinished = false;
        public bool IsFinished
        {
            get { return this.isFinished; }
        }


        public ExecuteCommand(Command cmd, Task task, ITaskManage iTaskManage)
        {
            this._cmd = cmd;
            this._task = task;
            this._iTaskManage = iTaskManage;
        }

        /// <summary>
        /// 根据指令创建指定数据项对象（代码未完成）
        /// </summary>
        /// <returns></returns>
        public DataItem getDataItem()
        {
            byte[] cmdData = strToToHexByte(_cmd.DataCommand);
            DataItem item = null;
            switch ((ControlCode)_cmd.ControlCode)
            {
                case ControlCode.ReadData:
                    //读数据指令
                    item = new DataItem_ReadData_ASK(cmdData);
                    break;
                case ControlCode.WriteData:
                    //写数据
                    item = getWriteDataAskItem(cmdData);
                    break;
                case ControlCode.ReadKeyVersion:
                    //读Key版本号
                    break;
                case ControlCode.ReadMeterAdress:
                    //读地址
                    break;
                case ControlCode.WriteMeterAdress:
                    //写地址
                    break;
                case ControlCode.WriteMeterNum:
                    //写表底
                    break;
                case ControlCode.CYReadData:
                    //读参数
                    item = getCYReadDataAskItem(cmdData);
                    break;
                case ControlCode.CYWriteData:
                    //创源内部写指令（参数设置）
                    item = getCYWriteDataAskItem(cmdData);
                    break;
                case ControlCode.CTR_6://主动上报数据中
                    item = new DataItem_C001(cmdData);
                    break;
                default:
                    break;
            }
            return item;
        }

        public Command Command { get { return this._cmd; } }
        public Task Task { get { return this._task; } }

        /// <summary>
        /// 主站请求的创源读指令
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private DataItem getCYReadDataAskItem(byte[] buffer)
        {
            IdentityCode identityCode = MyDataConvert.get数据表示符(buffer);
            DataItem item = null;
            switch (identityCode)
            {
                case IdentityCode.读时钟:
                    item = new DataItem_C200(buffer);
                    break;
                case IdentityCode.读切断报警参数:
                    item = new DataItem_C201(buffer);
                    break;
                case IdentityCode.读服务器信息:
                    item = new DataItem_C202(buffer);
                    break;
                case IdentityCode.读上传周期:
                    item = new DataItem_C203(buffer);
                    break;
                case IdentityCode.读公称流量:
                    item = new DataItem_C204(buffer);
                    break;
            }
            return item;
        }


        /// <summary>
        /// 主站请求的创源写指令请求对象
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private DataItem getCYWriteDataAskItem(byte[] buffer)
        {
            IdentityCode identityCode = MyDataConvert.get数据表示符(buffer);
            DataItem item = null;
            switch (identityCode)
            {
                case IdentityCode.设置服务器信息:
                    item = new DataItem_C104(buffer);
                    break;
                case IdentityCode.设置上传周期:
                    item = new DataItem_C105(buffer);
                    break;
                case IdentityCode.设置切断报警参数:
                    item = new DataItem_C103(buffer);
                    break;
                case IdentityCode.修正表数据:
                    item = new DataItem_C102(buffer);
                    break;
                case IdentityCode.设置公称流量:
                    item = new DataItem_C101(buffer);
                    break;
                case IdentityCode.换表:
                    item = new DataItem_C107(buffer);
                    break;
                //case IdentityCode.发送广告文件:
                //    item = new DataItem_C108(buffer);
                //    break;
                case IdentityCode.发送广告:
                    item = new DataItem_C109(buffer);
                    break;
            }
            return item;
        }



        /// <summary>
        /// 主站请求写数据对象
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private DataItem getWriteDataAskItem(byte[] buffer)
        {
            IdentityCode identityCode = MyDataConvert.get数据表示符(buffer);
            DataItem item = null;
            switch (identityCode)
            {
                case IdentityCode.写价格表:
                    item = new DataItem_A010(buffer);
                    break;
                case IdentityCode.写结算日:
                    item = new DataItem_A011(buffer);
                    break;
                case IdentityCode.写购入金额:
                    item = new DataItem_A013(buffer);
                    break;
                case IdentityCode.写新密钥:
                    item = new DataItem_A014(buffer);
                    break;
                case IdentityCode.写标准时间:
                    item = new DataItem_A015(buffer);
                    break;
                case IdentityCode.写阀门控制:
                    item = new DataItem_A017(buffer);
                    break;
                case IdentityCode.出厂启用:
                    item = new DataItem_A019(buffer);
                    break;
                case IdentityCode.写地址:
                    item = new DataItem_A018(buffer);
                    break;
                case IdentityCode.写表底数:
                    item = new DataItem_A016(buffer);
                    break;
                default:
                    break;
            }

            return item;

        }

        public bool Dowith(Meter_DATA data,byte[] key)
        {            
            byte[] desData = null;
            if (data.ProtocolType == IotProtocolType.RanQiBiao && key != null)
                desData = Encryption.Decry(data.m_buffer, key);//解密数据
            else
                desData = data.m_buffer;


            StringBuilder sb = new StringBuilder();
            for(int i=0;i<desData.Length;i++)
                sb.Append (string.Format ("{0:X2} ",desData[i]));

            this._cmd.AnswerDate = DateTime.Now;//.AddHours(8) ;
            this._cmd.AnswerData = sb.ToString();
            bool bCmdExecSucced = true;
            try
            {
                if (!((this._task.TaskType == TaskType.TaskType_发布广告) && (data.ProtocolType == IotProtocolType.LCD) && (data.Control == (byte)ControlCode.CTR_10)) &&
                    !((this._task.TaskType != TaskType.TaskType_发布广告) && (data.ProtocolType == IotProtocolType.RanQiBiao) && (this._cmd.ControlCode == (byte)ControlCode.WriteData) && (data.Control == (byte)ControlCode.WriteData_Answer)) &&
                    !((this._task.TaskType != TaskType.TaskType_发布广告) && (data.ProtocolType == IotProtocolType.RanQiBiao) && (this._cmd.ControlCode == (byte)ControlCode.CTR_9) && (data.Control == (byte)ControlCode.CTR_10)))
                {
                    //从mongodb数据库中读取command，在更新  
                    bCmdExecSucced = false;
                }

                if (bCmdExecSucced)
                {
                    //指令执行成功
                    this._cmd.CommandState = CommandState.Finished;
                }
                else
                {
                    //TODO：如何处理呢？(从站应答异常码）
                    this._cmd.CommandState = CommandState.Failed;
                    this._task.TaskState = TaskState.Failed;
                }
                if(!(this._task.TaskType == TaskType.TaskType_充值 && this._cmd.CommandState == CommandState.Failed))
                    this._iTaskManage.CommandCompletes(this._cmd, this._task);
            }
            catch (Exception e)
            {                
                Log.getInstance().Write(e, MsgType.Error);
                return false;
            }
            isFinished = true;
            return true;
        }


        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }


        /// <summary>
        /// 从汉字转换到16进制
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charset">编码,如"utf-8","gb2312"</param>
        /// <param name="fenge">是否每字符用逗号分隔</param>
        /// <returns></returns>
        public static string ToHex(string s, string charset, bool fenge)
        {
            if ((s.Length % 2) != 0)
            {
                s += " ";//空格
                //throw new ArgumentException("s is not valid chinese string!");
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            byte[] bytes = chs.GetBytes(s);
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str += string.Format("{0:X}", bytes[i]);
                if (fenge && (i != bytes.Length - 1))
                {
                    str += string.Format("{0}", ",");
                }
            }
            return str.ToLower();
        }

        ///<summary>
        /// 从16进制转换成汉字
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="charset">编码,如"utf-8","gb2312"</param>
        /// <returns></returns>
        public static string UnHex(string hex, string charset)
        {
            if (hex == null)
                throw new ArgumentNullException("hex");
            hex = hex.Replace(",", "");
            hex = hex.Replace("\n", "");
            hex = hex.Replace("\\", "");
            hex = hex.Replace(" ", "");
            if (hex.Length % 2 != 0)
            {
                hex += "20";//空格
            }
            // 需要将 hex 转换成 byte 数组。 
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // 每两个字符是一个 byte。 
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                    System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    // Rethrow an exception with custom message. 
                    throw new ArgumentException("hex is not a valid hex number!", "hex");
                }
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            return chs.GetString(bytes);
        }
    }
}
