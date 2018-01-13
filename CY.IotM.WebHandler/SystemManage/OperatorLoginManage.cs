using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CY.IotM.Common;
using CY.IotM.Common.Tool;

namespace CY.IotM.WebHandler.SystemManage
{
    public class OperatorLoginManage : BaseHandler
    {
        public override void DoLoginedHandlerWork(HttpContext context)
        {
            DoNoLoginHandlerWork(context);
        }
        public override void DoNoLoginHandlerWork(HttpContext context)
        {
            Message jsonMessage=new Message();
            //获取操作类型AType:ADD,EDIT,DELETE,QUERY
            WCFServiceProxy<ILoginerManage> proxyLoginer;
            string AjaxType = context.Request.QueryString["AType"] == null ? string.Empty : context.Request.QueryString["AType"].ToString().ToUpper();
            switch (AjaxType)
            {

                case "USERLOGIN":
                    jsonMessage = UserLogin(context);
                    break;
                case "USERLOGINOUT":
                    proxyLoginer = new WCFServiceProxy<ILoginerManage>();
                    if (context.Session["LoginCompanyOperator"] != null)
                    {
                        context.Session.Remove("LoginCompanyOperator");
                    }
                    string webCookie = context.Session.SessionID.ToString();
                    try
                    {
                        jsonMessage = proxyLoginer.getChannel.UnLRegisterClientByMd5Cookie(Md5.GetMd5(webCookie));
                    }
                    catch
                    { }
                    finally
                    {
                        proxyLoginer.CloseChannel();
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
            context.Response.Write(JSon.TToJson<Message>(jsonMessage));
        }
        #region 用户登陆
        private Message UserLogin(HttpContext context)
        {
            Message jMessage = new Message();
            string LoginID = string.Empty;
            string LoginPsw = string.Empty;
            string CompanyID = string.Empty;
            string OperID = string.Empty;
            bool IsPhoneLogin = false;
            if (context.Request.Form["LoginID"] != null && context.Request.Form["LoginID"].ToString().Trim() != string.Empty)
            {
                LoginID = context.Request.Form["LoginID"].ToString().Trim();
                if (LoginID.Split('@').Length == 2)
                {
                    CompanyID = LoginID.Split('@')[1];
                    OperID = LoginID.Split('@')[0];
                }
                else
                {
                    if (LoginID.Length == 11)
                    {
                        IsPhoneLogin = true;
                    }
                }

            }
            if (context.Request.Form["LoginPsw"] != null)
            {
                LoginPsw = context.Request.Form["LoginPsw"].ToString().Trim();
            }
            if (LoginID == string.Empty)
            {
                jMessage = new Message()
                {
                    Result = false,
                    TxtMessage = "登录账号格式不正确。"
                };
            }
            else
            {
                CommonSearch<CompanyOperator> userInfoSearch = new CommonSearch<CompanyOperator>();
                string Where = "1=1 ";
                if (IsPhoneLogin)
                {
                    Where += " AND PhoneLogin=1 and Phone='" + LoginID + "'";
                }
                else
                {
                    Where += " AND OperID='" + OperID + "' AND CompanyID='" + CompanyID + "'";
                }
                SearchCondition sCondition = new SearchCondition() { TBName = "S_CompanyOperator", TFieldKey = "OperID", TTotalCount = -1, TWhere = Where };
                CompanyOperator Loginer = userInfoSearch.GetFirstTModel(ref sCondition);
                //服务器错误
                if (sCondition.TTotalCount == -1)
                {
                    jMessage = new Message()
                    {
                        Result = false,
                        TxtMessage = "数据中心未返回信息，请稍候再试。"
                    };
                }
                else if (Loginer != null && Loginer.OperID != string.Empty)
                {
                    if (Loginer.State != null && Loginer.State.ToString() == "1")
                    {
                        jMessage = new Message()
                        {
                            Result = false,
                            TxtMessage = string.Format("账号{0}已停用。", LoginID)
                        };
                    }
                    else if (Loginer.Pwd == Md5.GetMd5(Loginer.CompanyID + LoginPsw))
                    {
                        jMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = "登录成功。"
                        };
                        //向数据中心记录登录信息
                        WCFServiceProxy<ILoginerManage> proxy = new WCFServiceProxy<ILoginerManage>();
                        string webCookie = context.Session.SessionID.ToString();
                        try
                        {
                            proxy.getChannel.RegisterClient(Md5.GetMd5(webCookie), Loginer.OperID, Loginer.CompanyID);
                        }
                        catch
                        { }
                        finally
                        {
                            proxy.CloseChannel();
                        }
                        Loginer.Pwd = string.Empty;
                        context.Session["LoginCompanyOperator"] = Loginer;
                    }
                    else
                    {
                        jMessage = new Message()
                        {
                            Result = false,
                            TxtMessage = "密码错误。"
                        };
                    }

                }
                else
                {
                    jMessage = new Message()
                    {
                        Result = false,
                        TxtMessage = IsPhoneLogin ? "该手机号未绑定，请联系管理员操作[系统管理]->[操作员管理]界面进行绑定。" : "登录账号不存在。"
                    };
                }
            }
            return jMessage;
        }
        #endregion
    }
}
