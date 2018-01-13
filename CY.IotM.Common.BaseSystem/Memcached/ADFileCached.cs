using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IotM.Common.Memcached
{
    public class ADFileCached
    {
        private static readonly object _object = new object();

        private static string[] _serverlist = { "127.0.0.1:11211" };
        private static string _Key = "ADFile_";
        private static MemcachedClient _mc = null;

        private static ADFileCached _aDFileCached;

        private ADFileCached()
        {
        }

        public static ADFileCached getInstance()
        {
            if (_aDFileCached == null)
            {
                lock (_object)
                {
                    if (_aDFileCached == null)
                    {

                        _aDFileCached = new ADFileCached();
                        //initialize the pool for memcache servers
                        SockIOPool pool = SockIOPool.GetInstance("ADFileCached");
                        //将来可以将缓存服务分布到不同的服务器
                        string cServerList = System.Configuration.ConfigurationManager.AppSettings["MemcachedServerList"] == null ?
                            string.Empty : System.Configuration.ConfigurationManager.AppSettings["MemcachedServerList"];
                        if (cServerList != string.Empty)
                            _serverlist = cServerList.Split(',');
                        pool.SetServers(_serverlist);
                        pool.Initialize();

                        _mc = new MemcachedClient();
                        _mc.PoolName = "ADFileCached";
                        _mc.EnableCompression = false;
                    }
                }
            }
            return _aDFileCached;
        }


        public byte[] ReadFile(string companyID, string filename)
        {
            try
            {
                object value = _mc.Get(_Key + companyID + "_" + filename);
                byte[] buffer = (byte[])value;
                return buffer;               
            }
            catch
            {
                return null;
            }
        }

        public byte[] ReadFileSegment(string companyID, string fileName, int fileLength, int totalSegments, int currentSegmentsIndex)
        {
            string segKey = string.Format("{0}_{1}_{2}_{3}_{4}_{5}", _Key, companyID, fileName, fileLength, totalSegments, currentSegmentsIndex);
            try
            {
                object value = _mc.Get(segKey);
                byte[] buffer = (byte[])value;
                return buffer;
            }
            catch
            {
                return null;
            }
        }

        public string SaveFileSegment(byte[] buffer,string companyID, string fileName, int fileLength, int totalSegments, int currentSegmentsIndex)
        {
            string segKey = string.Format("{0}_{1}_{2}_{3}_{4}_{5}", _Key, companyID, fileName, fileLength, totalSegments, currentSegmentsIndex);
            try
            {
                _mc.Set(segKey, buffer,DateTime.Now .AddHours(1));
                return "";
            }
            catch(Exception e)
            {
                return e.Message ;
            }
        }


        public bool SaveFile(byte[] buffer, string companyID, string filename)
        {
            bool result =true;
            try
            {
                _mc.Set(_Key + companyID + "_" + filename, buffer, DateTime.Now.AddHours(1));               
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
