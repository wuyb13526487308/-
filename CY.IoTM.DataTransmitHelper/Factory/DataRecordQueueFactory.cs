using CY.IoTM.Common.Classes;
using CY.IoTM.Common.Log;
using CY.IoTM.DataTransmitHelper.DataRecord;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CY.IoTM.DataTransmitHelper.Factory
{
    public class DataRecordQueueFactory
    {
        private static DataRecordQueueFactory _ProFactory;
        private static object _object = new object();
        private readonly string _extensionDir = ".\\Channel";// @"D:\Project\大客户平台V2.0\DataSource\dtu_test\bin\Debug";
        public Thread thread;
        /// <summary>
        /// 数据队列处理接口
        /// </summary>
        [ImportMany(AllowRecomposition = true)]
        public IReadDataQueue[] DataRecordQueueServices { get; set; }

        [ImportMany(AllowRecomposition = true)]
        public IWarningDataQueue[] WarningDataQueueServices { get; set; }

        private DataRecordQueueFactory()
        {
            this.Compose();
        }
        private CompositionContainer _container;

        /// <summary>
        /// 对dll进行组装
        /// </summary>
        /// <returns></returns>
        private CompositionContainer GetContainerFromDirectory()
        {
            var catalog = new AggregateCatalog();
            var thisAssembly =
                new AssemblyCatalog(
                    System.Reflection.Assembly.GetExecutingAssembly());
            catalog.Catalogs.Add(thisAssembly);

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
                Log.getInstance().Write(compException, MsgType.Error);    
                return false;
            }
            catch (Exception e)
            {
                 Log.getInstance().Write(e, MsgType.Error);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取当前实例
        /// </summary>
        /// <returns></returns>
        public static DataRecordQueueFactory getInstance()
        {
            if (_ProFactory == null)
            {
                lock (_object)
                {
                    if (_ProFactory == null)
                        _ProFactory = new DataRecordQueueFactory();
                }
            }
            return _ProFactory;
        }
        /// <summary>
        /// 添加数据队列处理任务   燃气上报信息
        /// </summary>
        /// <returns></returns>
        public void AddWork(ReadDataInfo work)
        {
            if (DataRecordQueueServices == null) return;
            foreach (IReadDataQueue dc in DataRecordQueueServices)
            {
                dc.workQueue_AddWork(work);
            }
        }
        /// <summary>
        /// 添加数据队列处理任务  报警信息
        /// </summary>
        /// <returns></returns>
        public void AddWork(WarningInfo work)
        {
            if (WarningDataQueueServices == null) return;
            foreach (IWarningDataQueue dc in WarningDataQueueServices)
            {
                dc.workQueue_AddWork(work);
            }
        }

        /// <summary>
        /// 获取数据队列
        /// </summary>
        /// <param name="serviceNo"></param>
        /// <returns></returns>
        public IReadDataQueue GetIDataRecordQueue(int serviceNo)
        {
            if (DataRecordQueueServices == null) return null;
            foreach (IReadDataQueue dc in DataRecordQueueServices)
            {
                if (dc.getServiceNo == serviceNo)
                    return dc;
            }
            return null;
        }
        /// <summary>
        /// 启动数据队列服务
        /// </summary>
        /// <returns></returns>
        public void StartService()
        {

            if (DataRecordQueueServices != null)
            {
                foreach (IReadDataQueue dc in DataRecordQueueServices)
                {
                    thread = new Thread(new ThreadStart(dc.Start));
                    thread.Start();
                }
            }
            if (WarningDataQueueServices != null)
            {
                foreach (IWarningDataQueue dt in WarningDataQueueServices)
                {
                    thread = new Thread(new ThreadStart(dt.Start));
                    thread.Start();
                }
            }

        }
        /// <summary>
        /// 停止数据队列服务
        /// </summary>
        public void StopService()
        {

            if (DataRecordQueueServices != null)
            {
                foreach (IReadDataQueue dt in DataRecordQueueServices)
                {
                    dt.Stop();
                }
            }

            if (WarningDataQueueServices != null)
            {
                foreach (IWarningDataQueue dt in WarningDataQueueServices)
                {
                    dt.Stop();
                }
            }
        }
    }
}
