using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CY.IoTM.DataCenter
{
    public class DCSRegister
    {
        private static DCSRegister _dcsRegister;

        private static readonly object _object = new object();
        public List<DCSInfo> _dcsList;
        private Thread myThread;
        private bool isChecked = true;

        private DCSRegister()
        {
            this._dcsList = new List<DCSInfo>();
            this.myThread = new Thread(TestDDS);
            this.myThread.IsBackground = true;
            this.myThread.Start();

        }
        public static DCSRegister getInstance()
        {
            if (_dcsRegister == null)
            {
                lock (_object)
                {
                    if (_dcsRegister == null)
                    {
                        _dcsRegister = new DCSRegister();
                    }
                }
            }
            return _dcsRegister;
        }

        private void TestDDS()
        {
            //用于检测DDS
            while (isChecked)
            {
                Thread.Sleep(1000 * 2);
                CheckDCSService();
            }
        }

        /// <summary>
        /// 添加数据采集服务器在数据中心的注册信息
        /// </summary>
        /// <param name="dcs"></param>
        /// <returns></returns>
        public string AddDCS(DCSService dcs)
        {
            lock (_object)
            {
                DCSService d = dcs;
                DCSInfo dcinfo = null;
                int iCount = this._dcsList.Where(p => p.ID == d.ID).Count();
                if (iCount == 0)
                {
                    dcinfo = new DCSInfo(dcs.ID) { DcsService = d, IP = d.IP, IsRegister = true, Name = d.Name, Port = d.Port };
                    dcinfo.IP = d.IP;
                    dcinfo.Name = d.Name;
                    dcinfo.Port = d.Port;
                    dcinfo.IsRegister = true;
                    dcinfo.DcsService = d;
                    this._dcsList.Add(dcinfo);
                }
                else
                {
                    dcinfo = this._dcsList.Where(p => p.ID == d.ID).Single();
                    if (!dcinfo.IsRegister)
                    {
                        dcinfo.IP = d.IP;
                        dcinfo.Name = d.Name;
                        dcinfo.Port = d.Port;
                        dcinfo.IsRegister = true;
                        dcinfo.DcsService = d;
                    }
                    else
                    {
                        return "服务器已注册";
                    }
                }
                Console.WriteLine("服务器：{0}-{1} {2}:{3}注册成功", d.ID, d.Name, d.IP, d.Port);
                return "";
            }
        }
        /// <summary>
        /// 根据数据采集服务器ID获取服务器通讯通道接口对象
        /// </summary>
        /// <param name="dcsID"></param>
        /// <returns></returns>
        public DCSService getDcsSevice(string dcsID)
        {
            lock (_object)
            {
                string id = dcsID;
                if (this._dcsList.Where(p => p.ID == id).ToList().Count == 0) return null;
                DCSInfo dcinfo = this._dcsList.Where(p => p.ID == id).Single();
                if (dcinfo == null || !dcinfo.IsRegister) return null;
                return dcinfo.DcsService;
            }
        }       
      
        /// <summary>
        /// 移除数据采集服务器注册信息
        /// </summary>
        /// <param name="dcsID"></param>
        /// <returns></returns>
        public string ReMoveDcsService(string dcsID)
        {
            lock (_object)
            {
                string id = dcsID;
                if (this._dcsList.Count == 0) return "";
                try
                {
                    this._dcsList.Remove(this._dcsList.Where(p => p.ID == id).Single());
                    return "";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
        }

        /// <summary>
        /// 检查注册服务
        /// </summary>
        public void CheckDCSService()
        {
            lock (_object)
            {
                if (_dcsList.Count > 0)
                {
                    for (int i = 0; i < _dcsList.Count; i++)
                    {
                        DCSInfo dcs = _dcsList[i];
                        try
                        {
                            dcs.DcsService.getIDCSClient.Test();
                        }
                        catch
                        {
                            this._dcsList.Remove(dcs);
                            dcs = null;
                        }
                    }
                }
                

                //foreach (DCSInfo dcs in _dcsList)
                //{
                //    try
                //    {
                //        if(dcs.IsRegister)
                //            dcs.DcsService.getIDCSClient.Test();
                //    }
                //    catch
                //    {
                //        dcs.DcsService = null;
                //        dcs.IsRegister = false;
                //    }
                //}
            }
        }

    }
}
