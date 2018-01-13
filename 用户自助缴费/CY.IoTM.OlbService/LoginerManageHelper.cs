using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CY.IoTM.OlbCommon;
using System.Threading;

namespace CY.IoTM.OlbService
{
    public class LoginerManageHelper
    {
        private List<LoginerInfo> _LoginerList = new List<LoginerInfo>();
        public List<LoginerInfo> List
        {
            get { return _LoginerList; }
            set { _LoginerList = value; }
        }
        private static LoginerManageHelper objInstance = null;
        private static readonly object _object = new object();
        Semaphore _semaphore = new Semaphore(1, 1);
        public Semaphore Semaphore
        {
            get { return _semaphore; }
        }

        /// <summary>
        /// 入口
        /// </summary>
        /// <returns></returns>
        public static LoginerManageHelper getInstance()
        {
            if (objInstance == null)
            {
                lock (_object)
                {
                    if (objInstance == null)
                    {
                        objInstance = new LoginerManageHelper();
                    }
                }
            }
            return objInstance;
        }
    }
}
