using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IotM.WebHandler;
using CY.IoTM.Common.Business;
using CY.IoTM.Common.ADSystem;

namespace CY.IotM.WebClient.Handler
{
    /// <summary>
    /// ADUserHandler 的摘要说明
    /// </summary>
    public class ADUserHandler : BaseHandler
    {
        public override void DoLoginedHandlerWork(HttpContext context)
        {
            
            Message jsonMessage;
            jsonMessage = new Message()
            {
                Result = false,
                TxtMessage = "权限验证失败，可能原因:\n1、数据中心通讯失败。\n2、系统管理员未与您分配对应操作权限。"
            };

            string AjaxType = context.Request.QueryString["AType"] == null ? string.Empty : context.Request.QueryString["AType"].ToString().ToUpper();

            ADUser Info = new ADUser();
            WCFServiceProxy<IADUserDAL> proxy = null;
            Info = new CommonModelFactory<ADUser>().GetModelFromContext(context);
            proxy = new WCFServiceProxy<IADUserDAL>();

            try
            {
                switch (AjaxType)
                {//查询用户
                    case "QUERY":

                        CommonSearch<View_AdUser> InfoSearch = new CommonSearch<View_AdUser>();
                        string Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        SearchCondition sCondition = new SearchCondition() { TBName = "View_AdUser", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "UserID desc", TWhere = Where };
                        List<View_AdUser> list = InfoSearch.GetList(ref sCondition, context);

                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_AdUser>(list, sCondition.TTotalCount)
                        };
                        break;
                    //广告屏用户列表
                    case "QUERYVIEW":

                        CommonSearch<View_AdUser> InfoSearchView = new CommonSearch<View_AdUser>();
                        Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        sCondition = new SearchCondition() { TBName = "View_AdUser", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = " UserID desc", TWhere = Where };

                        List<View_AdUser> listView = InfoSearchView.GetList(ref sCondition, context);

                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_AdUser>(listView, sCondition.TTotalCount)
                        };
                        break;
                    //广告主题列表
                    case "QUERYVIEWSC":

                        List<ADUserSC> listSC = proxy.getChannel.getListSC();
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<ADUserSC>(listSC, listSC.Count)
                        };


                        break;
                    //用户列表
                    case "QUERYVIEWUSER":

                        CommonSearch<View_UserInfo> InfoSearchViewUser = new CommonSearch<View_UserInfo>();
                        Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        sCondition = new SearchCondition() { TBName = "View_UserInfo", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = " UserID desc", TWhere = Where };

                        List<View_UserInfo> listViewUser = InfoSearchViewUser.GetList(ref sCondition, context);

                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_UserInfo>(listViewUser, sCondition.TTotalCount)
                        };
                        break;
                    //用户列表
                    case "QUERYVIEWUSERADD":
                        //List<View_UserInfo> listViewUserADD = proxy.getChannel.getUserListShow(loginOperator.CompanyID);

                        CommonSearch<View_UserInfo> InfoSearchViewUserADD = new CommonSearch<View_UserInfo>();
                        Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        sCondition = new SearchCondition() { TBName = "View_UserInfoADDC", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = " UserID desc", TWhere = Where };

                        List<View_UserInfo> listViewUserADD = InfoSearchViewUserADD.GetList(ref sCondition, context);

                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_UserInfo>(listViewUserADD, sCondition.TTotalCount)
                        };
                        break;

                    //添加信息
                    case "GROUPADD":
                            if (context.Request.Form["strNo"] != null && context.Request.Form["strNo"].ToString().Trim() != string.Empty)
                            {
                                string strNo = context.Request.Form["strNo"];

                                string[] arrNo = strNo.Split(',');

                                for (int i = 0; i < arrNo.Length; i++)
                                {
                                    string[] userInfo = arrNo[i].Split('|');
                                    Info.UserID = userInfo[0].ToString();
                                    Info.CompanyID = userInfo[1].ToString();
                                    Info.Street = userInfo[2].ToString();
                                    Info.Community = userInfo[3].ToString();
                                    Info.Adress = userInfo[4].ToString();
                                    Info.AddTime = DateTime.Now;
                                    Info.Ver = "1.0";
                                    jsonMessage = proxy.getChannel.Add(Info);
                                    if (!jsonMessage.Result) {
                                        break;
                                    }
                                }
                            }

                        break;
                    case "DELETE":
                        jsonMessage = proxy.getChannel.Delete(Info.UserID,Info.CompanyID);
                        break;
                    //删除信息 
                    case "GROUPDEL":
                        if (context.Request.Form["strNo"] != null && context.Request.Form["strNo"].ToString().Trim() != string.Empty)
                        {
                            string strNo = context.Request.Form["strNo"];

                            string[] arrNo = strNo.Split(',');

                            for (int i = 0; i < arrNo.Length; i++)
                            {
                                string[] userInfo = arrNo[i].Split('|');
                                Info.UserID = userInfo[0].ToString();
                                Info.CompanyID = userInfo[1].ToString();
                                jsonMessage = proxy.getChannel.Delete(Info.UserID, Info.CompanyID);
                                if (!jsonMessage.Result)
                                {
                                    break;
                                }
                            }
                        }

                        //jsonMessage = proxy.getChannel.Delete(Info.UserID,Info.CompanyID);
                        break;

                    default:
                        jsonMessage = new Message()
                        {
                            Result = false,
                            TxtMessage = "操作未定义!" + AjaxType
                        };
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

    }
}