using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CY.IoTM.Common;

namespace CY.IoTM.Channel
{
    public interface IDataChannel
    {
        /// <summary>
        /// 创建物联网表数据通道事件
        /// </summary>
        event Channel_InfoDelegate OnCreateChannel;
        /// <summary>
        /// 关闭物联网表数据通道事件
        /// </summary>
        event Channel_InfoDelegate OnCloseChannel;
        /// <summary>
        /// 接收到通道数据事件
        /// </summary>
        event Channel_DATA_Delegate OnReceiveData;
        /// <summary>
        /// 通道对应表的MAC
        /// </summary>
        string MAC { get; }

        /// <summary>
        /// 通道类型
        /// </summary>
        string ChannelType {get;}

        /// <summary>
        /// 获取或设置数据物理通道
        /// </summary>
        Link ChannelLink { get; set; }
        /// <summary>
        /// 关闭数据通道
        /// </summary>
        void Close();
		void Send(byte[] data);	
    }

    /// <summary>
    /// 终端注册登录或注销
    /// </summary>
    public struct Channel_INFO
    {
        public string m_MAC;				//表的MAC	
        public DateTime m_Link_date;		//表物理连接创建时间
        public byte m_status;				//终端模块状态, 1 在线 0 不在线
    }

    /// <summary>
    /// 终端数据信息
    /// </summary>
    public struct Channel_DATA
    {
        public string m_MAC;				//表的MAC	
        public DateTime m_recv_date;		//接收数据时间
        public byte[] m_buffer;
    }

    /// <summary>
    /// 创建或关闭物理通道代理方法
    /// </summary>
    /// <param name="info"></param>
    public delegate void Channel_InfoDelegate(Channel_INFO info);
    /// <summary>
    /// 终端数据代理
    /// </summary>
    /// <param name="data"></param>
    public delegate void Channel_DATA_Delegate(Channel_DATA data);

}
