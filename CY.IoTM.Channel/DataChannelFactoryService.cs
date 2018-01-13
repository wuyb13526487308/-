using CY.IoTM.Common.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data;
using System.Linq;
using System.Text;


namespace CY.IoTM.Channel
{
    /// <summary>
    /// 数据通道工厂服务类，采用MEF技术实现了实现IChannelTypeFactory接口的类由Composite组件自动装载。
    /// </summary>
    public class DataChannelFactoryService
    {
        [ImportMany(AllowRecomposition = true)]
        public ICreateDataChannel[] DataChannelFactory{ get; set; }
        private static DataChannelFactoryService _ProFactory;
        private static object _object = new object();

        public static DataChannelFactoryService getInstance()
        {
            if (_ProFactory == null)
            {
                lock (_object)
                {
                    if (_ProFactory == null)
                        _ProFactory = new DataChannelFactoryService();
                }
            }
            return _ProFactory;
        }

        /// <summary>
        /// 数据通道列表
        /// </summary>
        private List<IDataChannel> _listDataChannel = new List<IDataChannel>();

        #region MEF基本代码
        private readonly string _extensionDir = ".\\Channel";
        private DataChannelFactoryService()
        {
            this.Compose();
        }
        private CompositionContainer _container;
        private CompositionContainer GetContainerFromDirectory()
        {
            var catalog = new AggregateCatalog();
            var thisAssembly =
                new AssemblyCatalog(
                    System.Reflection.Assembly.GetExecutingAssembly());
            catalog.Catalogs.Add(thisAssembly);

            catalog.Catalogs.Add(
                new DirectoryCatalog(_extensionDir));

            var container = new CompositionContainer(catalog);
            return container;
        }
        private bool Compose()
        {
            _container = GetContainerFromDirectory();

            try
            {
                _container.ComposeParts(this);
            }
            catch (CompositionException compException)
            {
                Log.getInstance().Write(compException,MsgType.Error);
                //Console.WriteLine(compException.ToString());
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 获取数据通道,如指定的通道类型不存在时，返回null
        /// </summary>
        /// <param name="servicePort"></param>
        /// <param name="cellID"></param>
        /// <param name="channelType"></param>
        /// <returns></returns>
        public IDataChannel getDataChannel(string mac,string channelType)
        {
            var query = from p in _listDataChannel
                        where p.MAC.ToString() == mac && p.ChannelType == channelType
                        select p;
            List<IDataChannel> list = query.ToList();
            if (list != null && list.Count == 1)
            {
                return list[0];
            }
            else
            {              
              
            
                //加载数据通道（服务端口-数据单元-数据通道）
                var queryChannel = from p in DataChannelFactory
                                    where p.ChannelType == channelType
                                    select p;
                List<ICreateDataChannel> cList = queryChannel.ToList();
                if (cList != null && cList.Count > 0)
                {
                    IDataChannel channel = cList[0].CreateDataChannel(mac);
                    this._listDataChannel.Add(channel);
                    return channel;
                }
                else
                {
                    //找不到指定编码的通道组件

                }
            }

            
            return null;
        }


     
        public void Remove(IDataChannel channel)
        {
            this._listDataChannel.Remove(channel);
        }

        /// <summary>
        /// 启动通道服务
        /// </summary>
        /// <returns></returns>
        public string StartChannelService()
        {
            if (DataChannelFactory == null) return "没有注册任何服务。";
            StringBuilder sb = new StringBuilder();
            foreach (ICreateDataChannel dc in DataChannelFactory)
            {
                sb.AppendLine(dc.StartService());
            }
            return sb.ToString();
        }
        public void StopChannelService()
        {
            if (DataChannelFactory == null) return;
            foreach (ICreateDataChannel dc in DataChannelFactory)
            {
                dc.StopService();
            }
        }
       
    }
}
