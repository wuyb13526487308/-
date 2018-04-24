using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Business
{
    [ServiceContract]
    public interface IOneNetService
    {
        [OperationContract]
        string PostToOneNet(string mac, string hex);
        [OperationContract]
        void OutPutLog(string mac, string info);
    }
}
