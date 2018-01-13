using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Memcached.ClientLibrary;

namespace CY.IotM.Common.Memcached
{
    /// <summary>
    /// 数据中心缓存权限配置
    /// </summary>
    public class CompanyRightCached
    {
        private static CompanyRightCached _objInstance = null;
        private static readonly object _object = new object();

        private static string[] _serverlist = { "192.168.1.18:11211" };
        private static string _Key = "CompanyRight";
        private static MemcachedClient _mc = null;
        /// <summary>
        /// 入口
        /// </summary>
        /// <returns></returns>
        public static CompanyRightCached getInstance()
        {
            if (_objInstance == null)
            {
                lock (_object)
                {
                    if (_objInstance == null)
                    {

                        _objInstance = new CompanyRightCached();
                        //initialize the pool for memcache servers
                        SockIOPool pool = SockIOPool.GetInstance("CompanyRightCached");

                        string cServerList = System.Configuration.ConfigurationManager.AppSettings["MemcachedServerList"] == null ?
                            string.Empty : System.Configuration.ConfigurationManager.AppSettings["MemcachedServerList"];
                        if (cServerList != string.Empty)
                            _serverlist = cServerList.Split(',');
                        pool.SetServers(_serverlist);
                        pool.Initialize();

                        _mc = new MemcachedClient();
                        _mc.PoolName = "CompanyRightCached";
                        _mc.EnableCompression = false;
                    }
                }
            }
            return _objInstance;
        }
        #region 获取权限配置
        public List<DefineMenu> GetDefineMenu(string companyID)
        {
            try
            {
                return (List<DefineMenu>)_mc.Get(_Key + companyID.Trim() + "_DefineMenu");

            }
            catch
            {

                return null;
            }
        }
        public List<DefineRight> GetDefineRight(string companyID)
        {
            try
            {
                return (List<DefineRight>)_mc.Get(_Key + companyID.Trim() + "_DefineRight");
            }
            catch
            {

                return null;
            }
        }
        public List<DefineRightMenu> GetDefineRightMenu(string companyID)
        {
            try
            {
                return (List<DefineRightMenu>)_mc.Get(_Key + companyID.Trim() + "_DefineRightMenu");

            }
            catch
            {

                return null;
            }
        }
        public List<DefineOperRight> GetDefineOperRight(string companyID)
        {
            try
            {
                return (List<DefineOperRight>)_mc.Get(_Key + companyID.Trim() + "_DefineOperRight");

            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region 设置权限配置
        public bool SetDefineMenu(string companyID, List<DefineMenu> list)
        {
            bool result = true;
            try
            {
                _mc.Set(_Key + companyID.Trim() + "_DefineMenu", list);

            }
            catch
            {
                result = false;
            }
            return result;
        }
        public bool RemoveDefineMenu(string companyID)
        {
            bool result = true;
            try
            {
                _mc.Delete(_Key + companyID.Trim() + "_DefineMenu");
            }
            catch
            {

                result = false;
            }
            return result;
        }
        public bool SetDefineRight(string companyID, List<DefineRight> list)
        {
            bool result = true;
            try
            {
                _mc.Set(_Key + companyID.Trim() + "_DefineRight", list);

            }
            catch
            {
                result = false;
            }
            return result;
        }
        public bool RemoveDefineRight(string companyID)
        {
            bool result = true;
            try
            {
                _mc.Delete(_Key + companyID.Trim() + "_DefineRight");
            }
            catch
            {

                result = false;
            }
            return result;
        }
        public bool SetDefineRightMenu(string companyID, List<DefineRightMenu> list)
        {
            bool result = true;
            try
            {
                _mc.Set(_Key + companyID.Trim() + "_DefineRightMenu", list);

            }
            catch
            {
                result = false;
            }
            return result;
        }
        public bool RemoveDefineRightMenu(string companyID)
        {
            bool result = true;
            try
            {
                _mc.Delete(_Key + companyID.Trim() + "_DefineRightMenu");
            }
            catch
            {

                result = false;
            }
            return result;
        }
        public bool SetDefineOperRight(string companyID, List<DefineOperRight> list)
        {
            bool result = true;
            try
            {
                _mc.Set(_Key + companyID.Trim() + "_DefineOperRight", list);

            }
            catch
            {
                result = false;
            }
            return result;
        }
        public bool RemoveDefineOperRight(string companyID)
        {
            bool result = true;
            try
            {
                _mc.Delete(_Key + companyID.Trim() + "_DefineOperRight");
            }
            catch
            {

                result = false;
            }
            return result;
        }
        #endregion
    }
}
