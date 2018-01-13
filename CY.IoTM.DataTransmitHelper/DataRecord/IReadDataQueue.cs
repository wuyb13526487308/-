using CY.IoTM.Common.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.DataTransmitHelper.DataRecord
{
    public interface IReadDataQueue
    {

        int getServiceNo { get; }
        string getServiceName { get; }
        WorkQueue<ReadDataInfo> getWorkQueue { get; }
        void workQueue_AddWork(ReadDataInfo work);
        void Start();
        void Stop();
    }
}
