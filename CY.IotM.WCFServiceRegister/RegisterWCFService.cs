using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using CY.IotM.Common.Tool;
using CY.IoTM.Common.Log;


namespace CY.IotM.WCFServiceRegister
{
    /// <summary>
    /// 注册WCF服务（基于TCP.Net）
    /// </summary>
    public class RegisterWCFService
    {
        NetTcpBinding _binding;
        int port;
        string server;
        int MaxStringContentLength = 2147483647;
        int MaxArrayLength = 1024 * 1024 * 1024;
        List<ServiceHost> _listHost = new List<ServiceHost>();
        //Thread myThread;
        //bool isRunning = true;

        public RegisterWCFService(string server, int port)
        {
            this.server = server;
            this.port = port;
            this._binding = new NetTcpBinding();
            this._binding.Security.Mode = SecurityMode.None;
            this._binding.MaxReceivedMessageSize = 1024 * 1024 * 1024;
            this._binding.ListenBacklog = 500;
            this._binding.MaxConnections = 500;
            //所以，如果想在客户端断开或异常断开的时候立即触发事件，
            //则将inactivityTimout设置的尽量小，例如5秒，
            //this._binding.ReliableSession.InactivityTimeout = new TimeSpan(24, 0, 0, 0);
            //如果想让客户端和服务器端保持长连接时，
            //则将receiveTimeout设置的尽量大，例如1个小时。
            //this._binding.ReceiveTimeout = new TimeSpan(24, 0, 0, 0);

            //this._binding.PortSharingEnabled = true;
            Console.WriteLine("当前链接数:{0}", this._binding.MaxConnections);

            this._binding.ReaderQuotas = new XmlDictionaryReaderQuotas()
            {
                MaxStringContentLength = MaxStringContentLength,
                MaxArrayLength = MaxArrayLength
            };
        }
        /// <summary>
        /// 测试数据库服务启动情况
        /// </summary>
        /// <returns></returns>
        public bool TestSqlConnect()
        {
            bool result = false;
            string sqlText = "SELECT top 1 * FROM Frame_RemotingObject";
            SqlParameter[] parms = null;
            try
            {
                using (DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.SchuleConnection, CommandType.Text, sqlText, parms))
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                Log.getInstance().Write(e,MsgType.Error);
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 启动服务器
        /// </summary>
        public void RunWCFService()
        {
            try
            {
                string sqlText = "SELECT * FROM Frame_RemotingObject";
                SqlParameter[] parms = null;
                using (DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.SchuleConnection, CommandType.Text, sqlText, parms))
                {
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        //Console.WriteLine("Beginning Run WCF Service.");
                        Log.getInstance().Write(MsgType.Information, "Beginning Run WCF Service");
                        if (dt != null)
                        {
                            //Console.WriteLine("Service count :{0}", dt.Rows.Count);
                            Log.getInstance().Write(MsgType.Information, string.Format("Service count :{0}", dt.Rows.Count));
                            foreach (DataRow r in dt.Rows)
                            {
                                try
                                {
                                    Type _t = getType(r["objecttype"].ToString(), r["objectDll"].ToString().Trim());
                                    string[] types = r["interfaceType"].ToString().Split(new char[] { '|' });
                                    Type[] listTypes = new Type[types.Length];
                                    for (int ii = 0; ii < listTypes.Length; ii++)
                                    {
                                        Type _interface = getType(types[ii], r["interfaceDll"].ToString().Trim());
                                        listTypes[ii] = _interface;
                                    }
                                    if (_t == null || r["uri"].ToString().Trim() == "" || listTypes.Length == 0)
                                    {
                                        Console.WriteLine(string.Format("SERVICE {0}-{1}注册失败。", r["objectDll"].ToString(), r["objecttype"].ToString()));
                                        Log.getInstance().Write(MsgType.Error, string.Format("SERVICE {0}-{1}注册失败。", r["objectDll"].ToString(), r["objecttype"].ToString()));
                                        continue;
                                    }
                                    this.RunWCF(_t, listTypes, r["uri"].ToString().Trim());
                                }
                                catch(Exception e)
                                {
                                    Log.getInstance().Write(MsgType.Error, string.Format("SERVICE {0}-{1}注册失败。", r["objectDll"].ToString(), r["objecttype"].ToString()));
                                    Log.getInstance().Write(e,MsgType.Error);
                                    //Console.WriteLine(string.Format("SERVICE {0}-{1}注册失败。", r["objectDll"].ToString(), r["objecttype"].ToString()));

                                   // Console.WriteLine("{0} {1} 原因：{2}",r["objecttype"].ToString(), r["objectDll"].ToString(),e.Message);
                                }
                            }
                        }
                        else
                        {
                            //Console.WriteLine("dt == null RunRemotingService.");
                            Log.getInstance().Write(MsgType.Information, "Frame_RemotingObject==null");
                        }
                        Console.WriteLine("End Run WCF Service.");
                        Log.getInstance().Write(MsgType.Information, "End Run WCF Service.");
                    }

                }

            }
            catch (Exception ex)
            {
                string result = ex.Message;
                //加载数据错误。//在此需要记录系统日志。
                Console.WriteLine(result);
                Log.getInstance().Write(ex,MsgType.Error);
            }

        }

        public void Close()
        {
            foreach (ServiceHost host in _listHost)
                host.Close();
        }

        private void RunWCF(Type serviceType, Type[] interfaceType, string ui)
        {
            string uri = string.Format("net.tcp://{2}:{0}/{1}", this.port, ui, this.server);
            ServiceHost host = new ServiceHost(serviceType, new Uri(uri));

            ServiceThrottlingBehavior throttle = host.Description.Behaviors.Find<ServiceThrottlingBehavior>();
            if (throttle == null)
            {
                throttle = new ServiceThrottlingBehavior();
                throttle.MaxConcurrentCalls = 1000;
                throttle.MaxConcurrentSessions = 1200;
                throttle.MaxConcurrentInstances = 1000;
                host.Description.
                Behaviors.Add(throttle);
            }

            int i = 0;
            foreach (Type type in interfaceType)
            {
                if (type == null)
                    continue;
                host.AddServiceEndpoint(type, this._binding, "");
                i++;
            }
            if (i > 0)
            {
                host.Open();
                _listHost.Add(host);
#if DEBUG
                 Console.WriteLine(string.Format("SERVICE【{0}】注册成功。", uri));
                Log.getInstance().Write(MsgType.Information, string.Format("SERVICE【{0}】注册成功。", uri));
#endif

            }
        }

        private Type getType(string type, string dll)
        {
            Type _t = null;
            //type = "CY.SFXT.CommonBankCharge.Interface.IBankCharge<CY.SFXT.CommonBankCharge.dll,CY.SFXT.CommonBankCharge.Dictionary>";
            //dll = "CY.SFXT.CommonBankCharge.dll";
            Regex isGType = new Regex("<(.*dll,.*)>");//泛型参数正则验证
            string rType = string.Empty;
            Type[] typeArgs = { };
            try
            {
                Assembly a = null;
                if (isGType.IsMatch(type))
                {

                    //动态解析，将type转换成
                    //type = "CY.SFXT.CommonBankCharge.Interface.IBankCharge`1";
                    rType = isGType.Replace(type, "`1");
                    Match m = isGType.Match(type);
                    string gDll = m.Value.Split(',')[0].Replace("<", "");
                    string gClass = m.Value.Split(',')[1].Replace(">", "");
                    string gPath = string.Format("{0}\\{1}", System.Windows.Forms.Application.StartupPath, gDll);
                    if (!System.IO.File.Exists(gPath))
                    {
                        gPath = string.Format("{0}\\{1}", System.Configuration.ConfigurationManager.AppSettings["SystemPath"], dll);
                        if (!System.IO.File.Exists(gPath))
                            return null;

                    }
                    a = Assembly.LoadFile(gPath);
                    Type _g = a.GetType(gClass);
                    typeArgs = new Type[] { _g };
                }
                else
                {
                    rType = type;
                }
                string path = string.Format("{0}\\{1}", System.Windows.Forms.Application.StartupPath, dll);
                if (!System.IO.File.Exists(path))
                    path = string.Format("{0}\\{1}", System.Configuration.ConfigurationManager.AppSettings["SystemPath"], dll);
                if (!System.IO.File.Exists(path))
                    return null;

                a = Assembly.LoadFile(path);
                _t = a.GetType(rType);
                if (_t.IsGenericType)
                    _t = _t.MakeGenericType(typeArgs);

            }
            catch (Exception e)
            {
                Log.getInstance().Write(e,MsgType.Error);
            }
            return _t;
        }
    }
}
