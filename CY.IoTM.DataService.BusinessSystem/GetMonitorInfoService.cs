using CY.IoTM.Common.Business;
using CY.IoTM.Common.Classes;
using CY.IoTM.Common.Log;
using CY.IoTM.DataCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.DataService.Business
{
    public class GetMonitorInfoService : IGetMonitorInfo
    {
        /// <summary>
        /// 根据dscID获取采集服务器监视情况
        /// </summary>
        /// <param name="dcsID"></param>
        /// <returns></returns>
        private DataArge GetMonitorInfoBy(string dcsID)
        {
            DCSService dscService = DCSRegister.getInstance().getDcsSevice(dcsID);
            if (dscService != null)
            {                 
                return dscService.getIDCSClient.GetMonitorInfo();
            }
            else
            {
                return new DataArge(DataType.MonitorData,new  MonitorInfo { LinkCount = 0, Cpu = 0, Memory = 0 });
            } 
        }

        /// <summary>
        /// 获取数据采集服务器集合
        /// </summary>
        /// <returns></returns>
        private List<CJDInfo> GetCJDList()
        {
            List<CJDInfo> list = new List<CJDInfo>();
            if (DCSRegister.getInstance()._dcsList != null)
            {
                CJDInfo one;
                foreach (DCSInfo item in DCSRegister.getInstance()._dcsList)
                {
                    one = new CJDInfo();
                    one.ID = item.ID;
                    one.IP = item.IP;
                    one.Name = item.Name;
                    one.Port = item.Port;
                    list.Add(one);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取采集端监视信息
        /// </summary>
        /// <param name="dcsID"></param>
        /// <returns></returns>
        public DataArge GetMonitorInfo(string dcsID)
        {
            if (dcsID == "")
            {
                return new DataArge(DataType.CJDList, GetCJDList());
            }
            else
            {
                return GetMonitorInfoBy(dcsID);
            }
        }
        /// <summary>
        /// 获取采集端日志信息
        /// </summary>
        /// <param name="cjdID"></param>
        /// <param name="mac"></param>
        /// <param name="date"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="lType"></param>
        /// <returns></returns>
        public LogCollection GetDCSLog(string cjdID, string mac, DateTime date, int pageNum, int pageSize, ReadLogDataType lType)
        {
            DCSService dscService = DCSRegister.getInstance().getDcsSevice(cjdID);
            if (dscService != null)
            {
                return dscService.getIDCSClient.getDCSLog(cjdID, mac, date, pageNum, pageSize, lType);
            } 
            else
            {
                return new LogCollection() {  ListTxtMessage=new List<TxtMessage>(), Rows=0};
            }
        }
    }
}
