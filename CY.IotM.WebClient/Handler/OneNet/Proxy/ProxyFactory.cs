using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace OneNETDataReceiver.Proxy
{
    public class ProxyFactory
    {
        private static ProxyFactory _proxy;
        private static object _lock = new object();
        private List<ProxyService> list;
        private Semaphore _semaphore = new Semaphore(1, 1);

        private bool isLock = false;

        public bool IsLock { get => isLock; set => isLock = value; }

        private ProxyFactory()
        {
            this.list = new List<ProxyService>();
        }

        public static ProxyFactory getInstance()
        {
            if (_proxy == null)
            {
                lock (_lock)
                {
                    if (_proxy == null)
                    {
                        _proxy = new ProxyFactory();
                    }
                }
            }
            return _proxy;
        }

        public void Login(int deviceId)
        {
            _semaphore.WaitOne();
            this.list.Add(new ProxyService(deviceId));
            _semaphore.Release();
        }
        /// <summary>
        /// 接收来自OneNet的数据
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="data"></param>
        public void RevData(int deviceId,string data)
        {
            if (isLock) return;
           // _semaphore.WaitOne();
            var ps = this.list.Where(p => p.DeviceId == deviceId).SingleOrDefault();
            if (ps == null)
            {
                ps = new ProxyService(deviceId);
                this.list.Add(ps);
            }
            //_semaphore.Release();

            WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, 
                ($"->接收到oneNet平台数据：{data}"),deviceId.ToString ()));

            if (!ps.IsOnline)
            {
                WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, 
                    ($"->上线处理"),deviceId.ToString ()));

                ps.Login(deviceId, data);
            }
            else
            {
                ps.Revice(data);
            }            
            
        }

        public void Logout(int deviceId)
        {
            _semaphore.WaitOne();
            var ps = this.list.Where(p => p.DeviceId == deviceId).SingleOrDefault();
            if (ps != null)
            {
                ps.Close();
                this.list.Remove(ps);
            }            
            _semaphore.Release();
        }

    }
}