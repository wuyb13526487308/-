using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace CY.IotM.Common
{

    /// <summary>
    /// 公共查询接口
    /// </summary>
    [ServiceContract]
    public interface ICommonSearch<T> where T : class,new()　
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        [OperationContract]
        List<T> getListBySearchCondition(ref SearchCondition sCondition);
        [OperationContract]
        int Test();
    }
}

