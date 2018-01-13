using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace CY.IotM.Common
{
    /// <summary>
    /// 系统日志管理入口
    /// </summary>
    [ServiceContract]
    public interface ISystemLogManage
    {
        [OperationContract]
        Message AddSystemLog(SystemLog info);
    }
}
