using CY.IoTM.Common.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.DataTransmitHelper.DataRecord
{
    public interface IWarningDataQueue
    {
        int getServiceNo { get; }
        string getServiceName { get; }
        WorkQueue<WarningInfo> getWorkQueue { get; }
        void workQueue_AddWork(WarningInfo work);
        void Start();
        void Stop();
    }
}
