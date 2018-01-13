using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CY.IoTM.Channel
{
    /// <summary>
    /// 数据通道类型加载接口
    /// </summary>
    public interface ICreateDataChannel
    {
        /// <summary>
        /// 获取数据通道类型编码
        /// </summary>
        string ChannelType
        {
            get;
        }
   

        /// <summary>
        /// 创建数据通道
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns></returns>
        IDataChannel CreateDataChannel(String meterId);

        /// <summary>
        /// 开始通道数据服务
        /// </summary>
        /// <returns></returns>
        string StartService();
        /// <summary>
        /// 停止通道数据服务
        /// </summary>
        /// <returns></returns>
        void StopService();

        /// <summary>
        /// zyh获取连接数量
        /// </summary>
        /// <returns></returns>
        int GetConectionCount();

    }
}
