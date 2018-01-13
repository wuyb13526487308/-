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
    /// AdMHandler 的摘要说明
    /// </summary>
    public class AdMHandler : BaseHandler
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
            //context.Response.Write(AjaxType);
            //context.Response.End();
            ADContext Info = new ADContext() ;
            WCFServiceProxy<IADContextDAL> proxy = null;
            Info = new CommonModelFactory<ADContext>().GetModelFromContext(context);
            proxy = new WCFServiceProxy<IADContextDAL>();
            
            try {
                switch (AjaxType)
                {//查询用户
                    case "QUERY":

                        CommonSearch<ADContext> InfoSearch = new CommonSearch<ADContext>();
                        string Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        SearchCondition sCondition = new SearchCondition() { TBName = "ADContext", TFieldKey = "AC_ID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "AC_ID desc", TWhere = Where };
                        List<ADContext> list = InfoSearch.GetList(ref sCondition, context);
                       
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<ADContext>(list, sCondition.TTotalCount)
                        };
                        break;
                    //广告主题列表
                    case "QUERYVIEW":

                    CommonSearch<ADContext> InfoSearchView = new CommonSearch<ADContext>();
                    Where = "1=1 ";
                    Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                    if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                    {
                        Where += context.Request.Form["TWhere"].ToString();
                    }
                    sCondition = new SearchCondition() { TBName = "ADContext", TFieldKey = "AC_ID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = " AC_ID desc", TWhere = Where };

                    List<ADContext> listView = InfoSearchView.GetList(ref sCondition, context);
                   
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<ADContext>(listView, sCondition.TTotalCount)
                    };
                    break;
                    //广告主题列表
                    case "QUERYVIEWLIST":

                    CommonSearch<ADContext> InfoSearchView2 = new CommonSearch<ADContext>();
                    Where = "1=1 and State != 0 ";
                    Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                    if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                    {
                        Where += context.Request.Form["TWhere"].ToString();
                    }
                    sCondition = new SearchCondition() { TBName = "ADContext", TFieldKey = "AC_ID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = " AC_ID desc", TWhere = Where };

                    List<ADContext> listView2 = InfoSearchView2.GetList(ref sCondition, context);

                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<ADContext>(listView2, sCondition.TTotalCount)
                    };
                    break;
                    //添加主题广告
                    case "ADD":
                        Info.CompanyID = base.loginOperator.CompanyID;
                        Info.State = 0;
                        Info.CreateDate = DateTime.Now;
                        jsonMessage = proxy.getChannel.Add(Info);

                    break;
                    case "EDIT":
                        Info.CompanyID = base.loginOperator.CompanyID;
                        jsonMessage = proxy.getChannel.Edit(Info);
                    break;

                    //删除信息 
                    case "DELCONTENT":
                        jsonMessage = proxy.getChannel.Delete((int)Info.AC_ID);
                    break;

                    //草稿->可发布 
                    case "UPDATEOK":
                    jsonMessage = proxy.getChannel.UpadteAdStatus(Info.AC_ID,1);
                    break;

                    //可发布-> 草稿
                    case "UPDATENO":
                    jsonMessage = proxy.getChannel.UpadteAdStatus(Info.AC_ID, 0);
                    break;

                    default:
                    jsonMessage = new Message()
                    {
                        Result = false,
                        TxtMessage = "1.操作未定义!" + AjaxType
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

















        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}