using System;
using System.Collections.Generic;
using System.Text;

namespace CY.IoTM.SystemInterface
{
    public class User
    {
        string companyId;
        string pwd;

        public User()
        {
            this.companyId = System.Configuration.ConfigurationSettings.AppSettings["CompanyId"];
            this.pwd = System.Configuration.ConfigurationSettings.AppSettings["Pwd"];

        }
        public string AddIotUser(IoT_User userInfo, IoT_Meter meterinfo)
        {
            try
            {

                UM.UserManageWebServiceSoapClient iumWebService = new UM.UserManageWebServiceSoapClient();
                string user = Newtonsoft.Json.JsonConvert.SerializeObject(userInfo);
                string meter = Newtonsoft.Json.JsonConvert.SerializeObject(meterinfo);

                string result = iumWebService.AddIotUser(this.companyId, pwd, user, meter);
                Message msg = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(result);
                if (msg.Result)
                {
                    IotUser _meter = Newtonsoft.Json.JsonConvert.DeserializeObject<IotUser>(msg.TxtMessage);
                    userInfo.UserID = _meter.User.UserID;
                    meterinfo.ID = _meter.Meter.ID;
                }
                else
                {
                    return msg.TxtMessage;
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "";
        }
        public string UpdateIotUser(IoT_User userInfo, IoT_Meter meterinfo)
        {
            try
            {
                UM.UserManageWebServiceSoapClient iumWebService = new UM.UserManageWebServiceSoapClient();
                string user = Newtonsoft.Json.JsonConvert.SerializeObject(userInfo);
                string meter = Newtonsoft.Json.JsonConvert.SerializeObject(meterinfo);
                return iumWebService.UpdateIotUser(this.companyId, pwd, user, meter);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }

    class Message
    {
        public bool Result
        {
            get;
            set;
        }
        public string TxtMessage
        {
            get;
            set;
        }
    }

    class IotUser
    {
        public IoT_User User { get; set; }
        public IoT_Meter Meter { get; set; }
    }
}
