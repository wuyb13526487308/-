using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IotM.WebHandler;
using CY.IoTM.Common.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace CY.IotM.WebClient.WebService
{
    /// <summary>
    /// UserManageWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class UserManageWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string AddIotUser(string account,string pwd, string userInfoJson, string meterInfoJson)
        {
            string LoginPsw = pwd;
            string CompanyID = string.Empty;
            string OperID = string.Empty;
            IotUser _iotUser = new IotUser();

            try
            {
                string[] acc = account.Replace("-","").Split('@');
                if (acc.Length != 2) return "账号错误";
                //验证账号密码
                string result = Login(account, pwd);
                if (result != "") return result;
                OperID = acc[0];
                CompanyID = acc[1];

                IoT_User user = Newtonsoft.Json.JsonConvert.DeserializeObject<IoT_User>(userInfoJson);
                IoT_Meter meter = Newtonsoft.Json.JsonConvert.DeserializeObject<IoT_Meter>(meterInfoJson);
                user.CompanyID = CompanyID;
                meter.CompanyID = CompanyID;
                meter.UserID = user.UserID;
                user.State = '1';//设置为等待点火状态

                WCFServiceProxy<IUserManage> proxy  = new WCFServiceProxy<IUserManage>();
                Message jsonMessage = proxy.getChannel.Add(user);
                if (!jsonMessage.Result)
                {
                    proxy.CloseChannel();
                    return Newtonsoft.Json.JsonConvert.SerializeObject(jsonMessage); 
                }
                IoT_User resultUser = Newtonsoft.Json.JsonConvert.DeserializeObject<IoT_User>(jsonMessage.TxtMessage);
                meter.UserID = resultUser.UserID;
                meter.MeterType = "00";
                _iotUser.User = resultUser;

                //表安装
                WCFServiceProxy<IMeterManage> _proxy = new WCFServiceProxy<IMeterManage>();
                jsonMessage = _proxy.getChannel.Add(meter);
                if (!jsonMessage.Result)
                {
                    //删除用户档案
                    proxy.getChannel.Delete(user);
                    proxy.CloseChannel();
                    _proxy.CloseChannel();
                    return Newtonsoft.Json.JsonConvert.SerializeObject(jsonMessage); ;
                }
                proxy.CloseChannel();
                _proxy.CloseChannel();
                IoT_Meter resultMeter = Newtonsoft.Json.JsonConvert.DeserializeObject<IoT_Meter>(jsonMessage.TxtMessage);
                _iotUser.Meter = resultMeter;

                return Newtonsoft.Json.JsonConvert.SerializeObject(new Message()
                {
                    Result = true,
                    TxtMessage = Newtonsoft.Json.JsonConvert.SerializeObject(_iotUser)
                }); 
            }
            catch (Exception e)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new Message()
                {
                    Result = false,
                    TxtMessage = e.Message
                });
            }

        }

        [WebMethod]
        public string UpdateIotUser(string account,string pwd, string userInfoJson, string meterInfoJson)
        {
            string LoginPsw = pwd;
            string CompanyID = string.Empty;
            string OperID = string.Empty;
            try
            {
                string[] acc = account.Replace("-", "").Split('@');
                if (acc.Length != 2) return "账号错误";
                OperID = acc[0];
                CompanyID = acc[1];

                //验证账号密码
                string result = Login(account, pwd);
                if (result != "") return result;
                IoT_User user = Newtonsoft.Json.JsonConvert.DeserializeObject<IoT_User>(userInfoJson);
                IoT_Meter meter = Newtonsoft.Json.JsonConvert.DeserializeObject<IoT_Meter>(meterInfoJson);
                user.CompanyID = CompanyID;
                meter.CompanyID = CompanyID;
                meter.UserID = user.UserID;

                View_UserMeter viewInfo = new View_UserMeter()
                {
                    CompanyID = user.CompanyID,
                    Address = user.Address,
                    Community = user.Community,
                    Door = user.Door,
                    BGL = user.BGL,
                    BWGCD = user.BWGCD,
                    BXGMRQ = user.BXGMRQ,
                    BXGMRSFZ = user.BXGMRSFZ,
                    BXYXQ = user.BXYXQ,
                    BZCZYBH = user.BZCZYBH,
                    BZFY = user.BZFY,
                    BZRQ = user.BZRQ,
                    DY = user.DY,
                    LD = user.LD,
                    Phone= user.Phone,
                    FYQHTR = user.FYQHTR,
                    UserID = user.UserID,
                    UserName= user.UserName,
                    UserType = user.UserType,
                    SFZH = user.SFZH,
                    Street = user.Street,
                    SYBWG = user.SYBWG,
                    YGBX = user.YGBX,
                    QYQHTR = user.QYQHTR,
                    YJBZFY =user.YJBZFY,
                    YQHTBH = user.YQHTBH,
                    YQHTQD= user.YQHTQD,
                    YQHTQDRQ = user.YQHTQDRQ,
                    ZCZJE = user.ZCZJE,
                    ZQF = user.ZQF,
                    ZS = user.ZS,
                    ZYQL = user.ZYQL,

                    MeterID= meter.ID,
                    Direction=meter.Direction,
                    Installer = meter.Installer,
                    MeterNo = meter.MeterNo,
                    IotPhone = meter.IotPhone,
                    FDKH1 = meter.FDKH1,
                    InstallDate = meter.InstallDate,
                    InstallPlace = meter.InstallPlace,
                    InstallType = meter.InstallType,
                    MeterRange = meter.MeterRange
                };

                WCFServiceProxy<IUserManage>  proxy = new WCFServiceProxy<IUserManage>();
                Message jsonMessage = proxy.getChannel.EditUserMeter(viewInfo);
                if (!jsonMessage.Result)
                {
                    return jsonMessage.TxtMessage;
                }
            }
            catch (Exception e)
            {
            }

            return "";
        }
        [WebMethod]
        public string ReadIotUser(string account, string pwd, int index, int count)
        {
            IotUser user = new IotUser();
            //Query

            return Newtonsoft.Json.JsonConvert.SerializeObject(user);
        }

        private string Login(string account,string pwd)
        {
            string LoginPsw = pwd;
            string CompanyID = string.Empty;
            string OperID = string.Empty;

            string[] acc = account.Replace("-", "").Split('@');
            if (acc.Length != 2) return "账号错误";
            OperID = acc[0];
            CompanyID = acc[1];
            //验证账号密码
            CommonSearch<CompanyOperator> userInfoSearch = new CommonSearch<CompanyOperator>();
            string Where = "1=1 ";
            Where += " AND OperID='" + OperID + "' AND CompanyID='" + CompanyID + "'";

            SearchCondition sCondition = new SearchCondition() { TBName = "S_CompanyOperator", TFieldKey = "OperID", TTotalCount = -1, TWhere = Where };
            CompanyOperator Loginer = userInfoSearch.GetFirstTModel(ref sCondition);
            if (sCondition.TTotalCount == -1)
            {
                return "账号错误";
            }
            if (Loginer.Pwd != Md5.GetMd5(Loginer.CompanyID + LoginPsw))
            {
                return "密码错误";
            }

            return "";
        }

    }
}
