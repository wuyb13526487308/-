using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IotM.WebHandler;
using CY.IoTM.Common.Business;


namespace CY.IotM.WebClient.Handler
{
    /// <summary>
    /// UserHandler 的摘要说明
    /// </summary>
    public class UserHandler : BaseHandler
    {

        public override void DoLoginedHandlerWork(HttpContext context)
        {
            Message jsonMessage;
            jsonMessage = new Message()
            {
                Result = false,
                TxtMessage = "权限验证失败，可能原因:\n1、数据中心通讯失败。\n2、系统管理员未与您分配对应操作权限。"
            };
            //获取操作类型AType:ADD,EDIT,DELETE
            string AjaxType = context.Request.QueryString["AType"] == null ? string.Empty : context.Request.QueryString["AType"].ToString().ToUpper();
            IoT_User Info; View_UserMeter viewInfo;
            WCFServiceProxy<IUserManage> proxy=null;
            try
            {
                switch (AjaxType)
                {

                    //查询用户
                    case "QUERY":

                    CommonSearch<IoT_User> InfoSearch = new CommonSearch<IoT_User>();
                    string Where = "1=1 ";
                    Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                    if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                    {
                        Where += context.Request.Form["TWhere"].ToString();
                    }
                    SearchCondition sCondition = new SearchCondition() { TBName = "IoT_User", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "UserID ASC", TWhere = Where };
                    List<IoT_User> list = InfoSearch.GetList(ref sCondition, context);
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<IoT_User>(list, sCondition.TTotalCount)
                    };
                    break;
                   //查询用户表具视图
                    case "QUERYVIEW":

                    CommonSearch<View_UserMeter> InfoSearchView = new CommonSearch<View_UserMeter>();
                    Where = "1=1 ";
                    Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                    if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                    {
                        Where += context.Request.Form["TWhere"].ToString();
                    }
                    sCondition = new SearchCondition() { TBName = "View_UserMeter", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "UserID ASC", TWhere = Where };
                    List<View_UserMeter> listView = InfoSearchView.GetList(ref sCondition, context);
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<View_UserMeter>(listView, sCondition.TTotalCount)
                    };
                    break;

                    //查询临时用户
                    case "QUERYTEMP":

                    CommonSearch<IoT_UserTemp> InfoSearchTemp = new CommonSearch<IoT_UserTemp>();
                    Where = "1=1 ";
                    Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                    if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                    {
                        Where += context.Request.Form["TWhere"].ToString();
                    }
                    sCondition = new SearchCondition() { TBName = "IoT_UserTemp", TFieldKey = "MeterNo", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "MeterNo ASC", TWhere = Where };
                    List<IoT_UserTemp> listTemp = InfoSearchTemp.GetList(ref sCondition, context);
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<IoT_UserTemp>(listTemp, sCondition.TTotalCount)
                    };
                    break;
                    case "QUERYALARMPARM":
                        //TODO:查询用户当前设置的报警参数
                        CommonSearch<Iot_MeterAlarmPara> meterAlarmPara = new CommonSearch<Iot_MeterAlarmPara>();
                        viewInfo = new CommonModelFactory<View_UserMeter>().GetModelFromContext(context);

                        if (viewInfo.MeterNo != null && viewInfo.MeterNo.ToString().Trim() != string.Empty)
                        {
                            Where = $"MeterNo = '{viewInfo.MeterNo.ToString().Trim()}'";
                            sCondition = new SearchCondition() { TBName = "Iot_MeterAlarmPara", TFieldKey = "MeterNo", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "MeterNo ASC", TWhere = Where };
                            List<Iot_MeterAlarmPara> listMAP = meterAlarmPara.GetList(ref sCondition, context);
                            if (listMAP.Count == 1)
                            {
                                jsonMessage = new Message()
                                {
                                    Result = true,
                                    TxtMessage = JsToJson.SerializeToJsonString(listMAP[0])
                                };
                            }
                            else
                            {
                                jsonMessage = new Message()
                                {
                                    Result = false,
                                    TxtMessage = "没有找到配置数据。"
                                };
                            }                          
                        }
                        break;

                    //单户创建
                    case "ADD":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "cjyh_dhcj"))
                        {
                            Info = new CommonModelFactory<IoT_User>().GetModelFromContext(context);
                            Info.CompanyID = base.loginOperator.CompanyID;
                            proxy = new WCFServiceProxy<IUserManage>();
                            jsonMessage = proxy.getChannel.Add(Info);
                        }
                        break;
                 

                    //编辑用户信息和表信息 
                    case "EDITUSERMETER":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "dagl_bjyh"))
                        {
                            viewInfo = new CommonModelFactory<View_UserMeter>().GetModelFromContext(context);
                            proxy = new WCFServiceProxy<IUserManage>();
                            jsonMessage = proxy.getChannel.EditUserMeter(viewInfo);
                        }
                        break;

                    //删除用户信息和表信息 
                    case "DELETEUSERMETER":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "dagl_scyh"))
                        {
                            viewInfo = new CommonModelFactory<View_UserMeter>().GetModelFromContext(context);
                            proxy = new WCFServiceProxy<IUserManage>();
                            jsonMessage = proxy.getChannel.DeleteUserMeter(viewInfo);

                        }
                        break;

                    //批量删除用户 
                    case "BATCHDELETE":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "dagl_scyh"))
                        {
                            viewInfo = new CommonModelFactory<View_UserMeter>().GetModelFromContext(context);
                            proxy = new WCFServiceProxy<IUserManage>();

                            if (context.Request.Form["strNo"] != null && context.Request.Form["strNo"].ToString().Trim() != string.Empty)
                            {
                                string strNo = context.Request.Form["strNo"];

                                string[] arrNo = strNo.Split(',');

                                for (int i = 0; i < arrNo.Length; i++)
                                {
                                    proxy.getChannel.BatchDeleteUserMeter(arrNo[i]);
                                }
                            }
                            jsonMessage = new Message()
                            {
                                Result = true,
                                TxtMessage = "删除用户成功"
                            };

                        }
                        break;
                    //批量excel导入
                    case "BATCHIMPORT":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "cjyh_dhcj"))
                        {
                            viewInfo = new CommonModelFactory<View_UserMeter>().GetModelFromContext(context);
                            proxy = new WCFServiceProxy<IUserManage>();

                            if (context.Request.Form["strNo"] != null && context.Request.Form["strNo"].ToString().Trim() != string.Empty)
                            {
                                string strNo = context.Request.Form["strNo"];

                                string[] arrNo = strNo.Split(',');

                                for (int i = 0; i < arrNo.Length; i++)
                                {
                                    proxy.getChannel.BatchImport(arrNo[i]);
                                }
                            }
                            jsonMessage = new Message()
                            {
                                Result = true,
                                TxtMessage = "导入用户成功"
                            };

                        }
                        break;

                    //批量添加用户 
                    case "BATCHADD":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "cjyh_dhcj"))
                        {
                            viewInfo = new CommonModelFactory<View_UserMeter>().GetModelFromContext(context);
                            proxy = new WCFServiceProxy<IUserManage>();

                            string street = "";
                            if (context.Request.Form["Street"] != null && context.Request.Form["Street"].ToString().Trim() != string.Empty)
                            {
                                street = context.Request.Form["Street"].ToString().Trim();
                            }
                            string community = "";
                            if (context.Request.Form["Community"] != null && context.Request.Form["Community"].ToString().Trim() != string.Empty)
                            {
                                community = context.Request.Form["Community"].ToString().Trim();
                            }
                            string meterType = "";
                            if (context.Request.Form["MeterType"] != null && context.Request.Form["MeterType"].ToString().Trim() != string.Empty)
                            {
                                meterType = context.Request.Form["MeterType"].ToString().Trim();
                            }

                            if (context.Request.Form["Rows"] != null && context.Request.Form["Rows"].ToString().Trim() != string.Empty)
                            {
                                UserRows rows = JsToJson.Deserialize<UserRows>(context.Request.Form["Rows"].ToString());
                                List<User> userlist = rows.Rows;

                                int count = 0; string error = "";
                                Message tempM = null;
                                foreach (User u in userlist) {

                                    if (u.MeterNo.Trim() == "")
                                    {
                                        tempM = new Message() { Result = false, TxtMessage = string.Format("UserID={0}的表号未填写\r", u.UserID) };
                                    }
                                    else
                                    {
                                        IoT_User tempUser = new IoT_User();

                                        tempUser.UserID = u.UserID;
                                        tempUser.UserName = u.UserName;
                                        tempUser.Address = u.Address;
                                        tempUser.Community = community;
                                        tempUser.Street = street;
                                        tempUser.CompanyID = loginOperator.CompanyID;
                                        tempUser.State = '1';

                                        IoT_Meter tempMeter = new IoT_Meter();
                                        tempMeter.MeterType = meterType;
                                        tempMeter.MeterNo = u.MeterNo;
                                        tempMeter.CompanyID = loginOperator.CompanyID;
                                        tempM = proxy.getChannel.BatchAddUserMeter(tempUser, tempMeter);
                                    }
                                    if (tempM.Result)
                                    {
                                        count++;
                                    }
                                    else {
                                        error = tempM.TxtMessage;
                                    }

                                }
                                if (count > 0)
                                {

                                    jsonMessage = new Message()
                                    {
                                        Result = true,
                                        TxtMessage = "批量添加用户" + count + "个成功," + (userlist.Count - count) + "个失败，失败原因:" + error
                                    };
                                }
                                else {

                                    jsonMessage = new Message()
                                    {
                                        Result = false,
                                        TxtMessage = "批量添加用户失败"
                                    };
                                
                                }

                            }
                         
                        }
                        break;

                    default:
                        jsonMessage = new Message()
                        {
                            Result = false,
                            TxtMessage = "操作未定义。"
                        };
                        break;
                }
            }
            catch (Exception  ex){
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
    }
}