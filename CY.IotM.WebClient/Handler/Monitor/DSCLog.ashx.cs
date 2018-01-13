using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.Business;
using CY.IoTM.Common.Classes;
using CY.IoTM.Common.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 

namespace CY.IotM.WebClient.Handler.Monitor
{
    /// <summary>
    /// DSCLog 的摘要说明
    /// </summary>
    public class DSCLog : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //获取操作类型AType:list,oneinfo
            string AjaxType = context.Request.QueryString["AType"] == null ? string.Empty : context.Request.QueryString["AType"].ToString().ToUpper();
            Message jsonMessage;
            jsonMessage = new Message()
            {          
                Result = false,
                TxtMessage = "调用失败。"
            };
            WCFServiceProxy<IGetMonitorInfo> proxy = null;
            try
            {
                switch (AjaxType)
                {
                    case "LIST"://获取采集端服务器列表
                        proxy = new WCFServiceProxy<IGetMonitorInfo>();
                        DataArge arglist = proxy.getChannel.GetMonitorInfo("");
                        List<CJDInfo> list = (List<CJDInfo>)arglist.Data;
                        jsonMessage = new Message()
                          {
                              Result = true,
                              TxtMessage = JSon.ListToJson<CJDInfo>(list, list.Count)
                          };
                        break;
                    case "ONEINFO"://获取某一台采集服务器 监视信息
                        proxy = new WCFServiceProxy<IGetMonitorInfo>();
                        string dscId = context.Request.Form["dscId"] == null ? string.Empty : context.Request.Form["dscId"].ToString();
                        string mac = context.Request.Form["mac"] == null ? string.Empty : context.Request.Form["mac"].ToString();
                        string datetime = context.Request.Form["datetime"] == null ? string.Empty : context.Request.Form["datetime"].ToString();
                        string pageIndex = context.Request.Form["page"] == null ? string.Empty : context.Request.Form["page"].ToString(); 
                        string pageSize = context.Request.Form["rows"] == null ? string.Empty : context.Request.Form["rows"].ToString(); 
                        //获取日志
                        LogCollection logCollection= proxy.getChannel.GetDCSLog(dscId,mac,Convert.ToDateTime(datetime),Convert.ToInt32(pageIndex),Convert.ToInt32(pageSize),IoTM.Common.Log.ReadLogDataType.OneMeterData);
                        //LogCollection logCollection = new LogCollection();
                        //logCollection.Rows = 1;
                        //List<TxtMessage> listTxtMessage = new List<TxtMessage>();
                        //TxtMessage txtmsg;
                        //for (int j = 0; j < 20; j++)
                        //{
                        //    txtmsg = new TxtMessage();
                        //    txtmsg.Message =DateTime.Now.ToString("j: "+"yyyy-MM-dd HH:mm:ss.fff");
                        //    listTxtMessage.Add(txtmsg);
                        //}
                        //logCollection.ListTxtMessage = listTxtMessage;

                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<TxtMessage>(logCollection.ListTxtMessage, logCollection.Rows)
                        };
                         //context.Response.Write(JSon.ListToJson<TxtMessage>(logCollection.ListTxtMessage, logCollection.ListTxtMessage.Count));
                         //return;
                         break;
                     
                   
                }
            }
            catch (Exception ex)
            {
                jsonMessage = new Message()
                {
                    Result = false,
                    TxtMessage = ex.Message
                };
            }
            finally
            {
                if (proxy != null)
                    proxy.CloseChannel();
            }
            context.Response.Write(JSon.TToJson<Message>(jsonMessage));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}