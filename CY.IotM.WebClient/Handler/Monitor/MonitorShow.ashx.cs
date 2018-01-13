using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.Business;
using CY.IoTM.Common.Classes;
using CY.IoTM.Common.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CY.IotM.WebClient.Handler.Monitor
{
    /// <summary>
    /// MonitorShow 的摘要说明
    /// </summary>
    public class MonitorShow : IHttpHandler
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
                        string str = "[{\"id\":0,\"text\":\"采集服务器列表\",\"children\":["; 
                        foreach (CJDInfo one in list)
                        {
                            str += "{\"id\":\""+one.ID+"\",\"text\":\""+one.ID+one.Name+"\"},";
                        }
                        str=str.Trim(',');
                        str += "]}]";
                        context.Response.Write(str);
                        return;
                      //jsonMessage = new Message()
                      //  {
                      //      Result = true,
                      //      TxtMessage = JSon.ListToJson<CJDInfo>(list, list.Count)
                      //  };
                        break;
                    case "ONEINFO"://获取某一台采集服务器 监视信息
                        proxy = new WCFServiceProxy<IGetMonitorInfo>();
                        string dscId = context.Request.Form["dscId"] == null ? string.Empty : context.Request.Form["dscId"].ToString();
                        DataArge arg= proxy.getChannel.GetMonitorInfo(dscId);
                        MonitorInfo monitorinfo = (MonitorInfo)arg.Data;
                         

                        ////是否保存监视信息
                        //string isSaveData = context.Request.Form["saveData"] == null ? "false" : context.Request.Form["saveData"].ToString();
                        //if (isSaveData == "true")
                        //{  
                        //    Log.getInstance().FileSpace = 5;
                        //    Log.getInstance().Write(new MonitorLogMsg(dscId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + monitorinfo.Cpu.ToString() + "," + monitorinfo.LinkCount.ToString() + "," + monitorinfo.Memory.ToString()));

                        //}
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.TToJson<MonitorInfo>(monitorinfo)
                        };
                        break;
                    case "FILELIST"://获取某一台采集服务器 监视信息
                        string datafileDir = System.Configuration.ConfigurationManager.AppSettings["SystemPath"].ToString()+"\\Data";
                        string[] dirstring = Directory.GetDirectories(datafileDir);
                        List<DataFileList> dataFilelist = new List<DataFileList>();
                        DataFileList oneDataFile ;
                        foreach (string  onedir in dirstring)
                        {
                              oneDataFile = new DataFileList();
                              oneDataFile.FileFolder = onedir.Substring(onedir.LastIndexOf("\\")+1);
                              string[] filestring= Directory.GetFiles(onedir);
                               string  strlist = "";
                              foreach (string onefile in filestring)
                              {
                                  strlist+=Path.GetFileNameWithoutExtension(onefile)+";";
                              }
                              oneDataFile.fileName = strlist;
                              dataFilelist.Add(oneDataFile);

                        }
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<DataFileList>(dataFilelist,dataFilelist.Count)
                        };
                        break;
                    case "ONEHISDATA"://读取一个数据文件的数据
                        #region           读取一个数据文件的数据
                        //0001_201510092110.txt
                        string filename = context.Request.Form["filename"] == null ? string.Empty : context.Request.Form["filename"].ToString();
                        string fileFolder = filename.Substring(filename.IndexOf('_') + 1, 8);

                        string datafileDirq = System.Configuration.ConfigurationManager.AppSettings["SystemPath"].ToString() + "\\Data";
                        string filepath = datafileDirq + "\\" + fileFolder + "\\" + filename + ".txt";
                        if (File.Exists(filepath))
                        {
                            FileStream fs = File.Open(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            // 创建一个数据流读入器，和打开的文件关联 
                            StreamReader srMyfile = new StreamReader(fs);
                            List<DataFormat> datalist = new List<DataFormat>();
                            DataFormat onedata;
                            try
                            {

                                // 把文件指针重新定位到文件的开始 
                                srMyfile.BaseStream.Seek(0, SeekOrigin.Begin);
                                string s1;
                                while ((s1 = srMyfile.ReadLine()) != null)
                                {
                                    if (s1 != "")
                                    {
                                        onedata = new DataFormat();
                                        string[] datas = s1.Split(',');
                                        onedata.DataDateTime = Convert.ToDateTime(datas[0]);
                                        onedata.CPU = float.Parse(datas[1]);
                                        onedata.Link = float.Parse(datas[2]);
                                        onedata.Memory = float.Parse(datas[3]);
                                        datalist.Add(onedata);
                                    }
                                }

                            }
                            catch { }
                            finally
                            {
                                srMyfile.Close();
                                fs.Close();
                            }
                            jsonMessage = new Message()
                            {
                                Result = true,
                                TxtMessage = JSon.ListToJson<DataFormat>(datalist, datalist.Count)
                            };
                            break;
                        }
                         #endregion           读取一个数据文件的数据    
                        break;
                    case "SAVEHISDATA"://存储采集服务器 监视信息
                        #region 存储采集服务器 监视信息
                        string dscIdList = context.Request.Form["dscIdList"] == null ? string.Empty : context.Request.Form["dscIdList"].ToString();
                        if (dscIdList.Length > 0)
                        {
                            string[] idArrary = dscIdList.Split(',');
                            proxy = new WCFServiceProxy<IGetMonitorInfo>();
                            for (int i = 0; i < idArrary.Length; i++)
                            {
                                DataArge argsave = proxy.getChannel.GetMonitorInfo(idArrary[i]);
                                MonitorInfo savemonitorinfo = (MonitorInfo)argsave.Data;
                                Log.getInstance().FileSpace = 5;
                                Log.getInstance().Write(new MonitorLogMsg(idArrary[i], DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + savemonitorinfo.Cpu.ToString() + "," + savemonitorinfo.LinkCount.ToString() + "," + savemonitorinfo.Memory.ToString()));

                            }
                        }
                        
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = ""
                        }; 
                        #endregion 存储采集服务器 监视信息
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

    public class DataFileList
    {
        public string FileFolder{get;set;}
        public string fileName { get; set; }
        public DataFileList()
		{
			 
		}
    }
    public class DataFormat
    {
        public DateTime DataDateTime { get; set; }
        public float CPU { get; set; }
        public float Link { get; set; }
        public float Memory { get; set; }
    }
}