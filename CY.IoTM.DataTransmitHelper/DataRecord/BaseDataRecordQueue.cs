using CY.IoTM.Common.Classes;
using CY.IoTM.Common.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.DataTransmitHelper.DataRecord
{
    public class BaseDataRecordQueue : IReadDataQueue
    {
        private bool _isRunning = false;
        /// <summary>
        /// 服务队列编号
        /// </summary>
        protected int serviceNo = 0;
        /// <summary>
        /// 服务队列名称
        /// </summary>
        protected string serviceName = string.Empty;
        private WorkQueue<ReadDataInfo> Queue = new WorkQueue<ReadDataInfo>();

        /// <summary>
        /// 获取服务队列编号
        /// </summary>
        public int getServiceNo
        {
            get { return this.serviceNo; }
        }        
        /// <summary>
        /// 获取服务队列名称
        /// </summary>
        public string getServiceName
        {
            get { return this.serviceName; }
        }

        /// <summary>
        /// 返回服务队列实例
        /// </summary>
        public WorkQueue<ReadDataInfo> getWorkQueue
        {
            get { return this.Queue; }
        }
        /// <summary>
        /// 向队列中添加对象
        /// </summary>
        /// <param name="item"></param>
        public void workQueue_AddWork(ReadDataInfo item)
        {
            this.Queue.EnqueueItem(item);
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        public void Start()
        {
            if (!_isRunning)
            {
                //Console.WriteLine(string.Format("{0}-启动【{1}】服务", System.DateTime.Now, this.serviceName));
                Log.getInstance().Write(MsgType.Information, string.Format("{0}-启动【{1}】服务", System.DateTime.Now, this.serviceName));
                _isRunning = true;
                Queue.UserWork += new UserWorkEventHandler<ReadDataInfo>(workQueue_UserWork);
            }
            else
            {
                //Console.WriteLine(string.Format("【{0}】服务正在运行中。不能重复进行启动操作", this.serviceName));
                Log.getInstance().Write(MsgType.Information, string.Format("【{0}】服务正在运行中。不能重复进行启动操作", this.serviceName));
            }
        }
        /// <summary>
        /// 结束服务
        /// </summary>
        public void Stop()
        {
            if (_isRunning)
            {
                //Console.WriteLine(string.Format("{0}-停止【{1}】服务", System.DateTime.Now, this.serviceName));
                Log.getInstance().Write(MsgType.Information, string.Format("{0}-停止【{1}】服务", System.DateTime.Now, this.serviceName));
                _isRunning = false;
                Queue.UserWork -= new UserWorkEventHandler<ReadDataInfo>(workQueue_UserWork);
            }
            else
            {
                //Console.WriteLine(string.Format("【{0}】服务已经停止。不能重复进行停止操作", this.serviceName));
                Log.getInstance().Write(MsgType.Information, string.Format("【{0}】服务已经停止。不能重复进行停止操作", this.serviceName));
            }
        }
        private void workQueue_UserWork(object sender, WorkQueue<ReadDataInfo>.EnqueueEventArgs e)
        {
            try
            {
                DoWork(e);
            }
            catch (Exception ex)
            {
                //异常记录
                Log.getInstance().Write(ex, MsgType.Error);
            }

        }
        protected virtual void DoWork(WorkQueue<ReadDataInfo>.EnqueueEventArgs e)
        {

        }

    }
}
