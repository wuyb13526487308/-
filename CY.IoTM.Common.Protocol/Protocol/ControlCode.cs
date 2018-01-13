using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common
{
    /// <summary>
    /// 指令控制字
    /// </summary>
    public enum ControlCode : byte
    {
        /// <summary>
        /// 读数据
        /// </summary>
        ReadData = 0x01,
        /// <summary>
        /// 读数据正常应答码
        /// </summary>
        ReadData_Success = 0x81,
        /// <summary>
        /// 读数据异常应答码
        /// </summary>
        ReadData_Failed = 0xc1,

        /// <summary>
        /// 写数据 CODE = 0x04
        /// </summary>
        WriteData = 0x04,
        /// <summary>
        /// 写数据应答码 0x84 （正常应答）
        /// </summary>
        WriteData_Answer= 0x84,
        /// <summary>
        /// 写数据应答码 0xC4 （异常应答）
        /// </summary>
        WriteData_Failed = 0xC4,

        /// <summary>
        /// 读密钥版本号
        /// </summary>
        ReadKeyVersion = 0x09,

        /// <summary>
        /// 读表号地址
        /// </summary>
        ReadMeterAdress = 0x03,


        /// <summary>
        /// 写表号地址
        /// </summary>
        WriteMeterAdress = 0x15,


        /// <summary>
        /// 写表底数
        /// </summary>
        WriteMeterNum = 0x16,


        /// <summary>
        /// 创源读参数
        /// </summary>
        CYReadData = 0x20,


        /// <summary>
        /// 创源设置参数
        /// </summary>
        CYWriteData = 0x24,

        /// <summary>
        /// 创源设置参数正常应答
        /// </summary>
        CYWriteData_Answer = 0xA4,

        /// <summary>
        /// 主动上报燃气表数据
        /// </summary>
        CTR_6 = 0xA1,
        /// <summary>
        /// 主动上报燃气表数据正常应答控制码CTR_7
        /// </summary>
        CTR_7 = 0x21,
        /// <summary>
        ///  主动上报燃气表数据异常应答控制码
        /// </summary>
        CTR_8 = 0x61,

        /// <summary>
        /// 广告发布请求控制码
        /// </summary>
        CTR_9 = 0x24,
        /// <summary>
        /// 广告发布应答控制码
        /// </summary>
        CTR_10 = 0XA4,
        /// <summary>
        /// 广告发布数据异常应答控制码
        /// </summary>
        CTR_11 = 0XE4,

    }


    public enum IdentityCode
    {

        //读操作

        读计量数据 = 0x901F,
        历史计量数据1 = 0xD120,
        历史计量数据2 = 0xD121,
        历史计量数据3 = 0xD122,
        历史计量数据4 = 0xD123,
        历史计量数据5 = 0xD124,
        历史计量数据6 = 0xD125,
        历史计量数据7 = 0xD126,
        历史计量数据8 = 0xD127,
        历史计量数据9 = 0xD128,
        历史计量数据10 = 0xD129,
        历史计量数据11 = 0xD12A,
        历史计量数据12 = 0xD12B,
        读价格表 = 0x8102,
        读结算日 = 0x8103,
        读抄表日 = 0x8104,
        读购入金额 = 0x8105,
        读密钥版本号 = 0x8106,
        读地址 = 0x810A,



        //写操作
        /// <summary>
        /// 0xA010
        /// </summary>
        写价格表 = 0xA010,
        /// <summary>
        /// 0xA011
        /// </summary>
        写结算日 = 0xA011,
        写抄表日 = 0xA012,
        /// <summary>
        /// 0xA013
        /// </summary>
        写购入金额 = 0xA013,
        /// <summary>
        /// 0xA014
        /// </summary>
        写新密钥 = 0xA014,
        /// <summary>
        /// 0xA015
        /// </summary>
        写标准时间 = 0xA015,
        /// <summary>
        /// 0xA017
        /// </summary>
        写阀门控制 = 0xA017,
        /// <summary>
        /// 0xA019
        /// </summary>
        出厂启用 = 0xA019,
        /// <summary>
        /// 0xA018
        /// </summary>
        写地址 = 0xA018,
        /// <summary>
        /// 0xA016
        /// </summary>
        写表底数 = 0xA016,
        //上报数据
        /// <summary>
        /// 0xC001
        /// </summary>
        主动上报燃气表数据 = 0xC001,


        //创源读操作

        读时钟 = 0xC200,
        读切断报警参数 = 0xC201,
        读服务器信息 = 0xC202,
        读上传周期 = 0xC203,
        读公称流量 = 0xC204,



        //创源写操作

        设置公称流量 = 0xC101,
        修正表数据 = 0xC102,
        设置切断报警参数 = 0xC103,
        设置服务器信息 = 0xC104,
        设置上传周期 = 0xC105,
        恢复初始设置 = 0xC106,
        换表         = 0xC107,
        //发送广告文件 = 0xC108,
        发送广告 = 0xC109

    }

    /// <summary>
    /// 广告发布操作码
    /// </summary>
    public enum ADPublishOperatorCode : byte
    {
        /// <summary>
        /// 重新定义广告列表
        /// </summary>
        ReDefineList = 1,
        /// <summary>
        /// 添加广告列表
        /// </summary>
        AddList = 2,
    }
}
