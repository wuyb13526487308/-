using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.ADSystem
{
    [ServiceContract]
    public interface IADPublishManager
    {
        [OperationContract]
        string ADPublish(long ap_id, PublishType type = PublishType.NewPublish);
        [OperationContract]
        List<ADItem> ReadADItems(long ac_id);
        ADPublish ReadADPublish(long ap_id);
        List<ADPublisMeter> ReadPublishUser(long ap_id);
        string UnADPublish(long ap_id);
    }
}
