using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using CY.IoTM.Common.Classes;
using CY.IoTM.Common.Log;
//using CY.DTU.Log;

namespace CY.IoTM.Service.Common
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IDCSClient))]
    public interface IDCSService
    {
        [OperationContract]
        void Register(string dscID, string dscName, string dscIP, string dscPort);

        [OperationContract]
        void Heart(string dscID);

        [OperationContract]
        void UnRegister(string dscID);  
        
        [OperationContract]
        void InsertDataRecord(DataType type, DataArge e); 
    }

    /// <summary>
    /// 数据采集服务为数据中心提供的回调访问接口
    /// </summary>
    [ServiceContract]
    public interface IDCSClient
    {
        //[OperationContract]
        //GPRS_APN_SET GetApnSet(string dataChannelType);

        //[OperationContract(IsOneWay = true)]
        //void AddDataPoint(string zdID, string dpID);

        //[OperationContract(IsOneWay = true)]
        //void AddZD(DTUDevice dtuInfo);
        //[OperationContract(IsOneWay = true)]
        //void RemoveDataPoint(string zdID, string dpID);
        //[OperationContract(IsOneWay = true)]
        //void RemoveZD(string zdID);
        //[OperationContract]
        //List<CY.DTU.Common.Terminal.DTU> getZDList();
        //[OperationContract]
        //List<CY.DTU.Common.Terminal.DP> getDataPoint(string zdID);
        //[OperationContract]
        //LogCollection getDCSLog(string zdID, string dpID, DateTime date, int pageNum, int pageSize, ReadLogDataType lType);
        [OperationContract]
        void Test();

        /// <summary>
        /// 获取监视情况
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataArge GetMonitorInfo();

        /// <summary>
        /// 获取日志文件
        /// </summary>
        /// <param name="cjdID"></param>
        /// <param name="mac"></param>
        /// <param name="date"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="lType"></param>
        /// <returns></returns>
        [OperationContract]
        LogCollection getDCSLog(string cjdID, string mac, DateTime date, int pageNum, int pageSize, ReadLogDataType lType);

    }  
}
