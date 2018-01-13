using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.ADSystem
{
    [ServiceContract]
    public interface IADFileService
    {
        [OperationContract]
        byte[] DownLoad(string companyId, string remoteFileName);
        [OperationContract]
        string UpLoad(string companyid, string filename, byte[] data);
        [OperationContract]
        string Delete(string companyid, string filename);
        [OperationContract]
        bool Exists(string companyid, string filename);
        [OperationContract]
        int FileLength(string companyid, string filename);

    }
}
