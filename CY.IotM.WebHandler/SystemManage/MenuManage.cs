using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CY.IotM.Common;
using CY.IotM.Common.Tool;

namespace CY.IotM.WebHandler.SystemManage
{
    public class MenuManage : BaseHandler
    {
        public override void DoLoginedHandlerWork(HttpContext context)
        {
            Message jsonMessage;
            //获取操作类型AType:ADD,EDIT,DELETE,QUERY
            string AjaxType = context.Request.QueryString["AType"] == null ? string.Empty : context.Request.QueryString["AType"].ToString().ToUpper();
            MenuInfo Info;
            jsonMessage = new Message()
            {
                Result = false,
                TxtMessage = "权限验证失败，可能原因:\n1、数据中心通讯失败。\n2、系统管理员未与您分配对应操作权限。"
            };
            switch (AjaxType)
            {
                case "QUERY":
                    string Where = "1=1 ";
                    if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                    {
                        Where += "AND " + context.Request.Form["TWhere"].ToString();
                    }

                    if (context.Request.QueryString["Code"] != null && context.Request.QueryString["Code"].ToString().Trim() != string.Empty)
                    {
                        Where += "AND  FatherCode= '" + context.Request.QueryString["Code"].ToString()+"'";
                    }
                    else {
                        Where += "AND (  Type= '00' or  Type= '01' or  Type= '03' )";
                    }
                    CommonSearch<MenuInfo> userInfoSearch = new CommonSearch<MenuInfo>();
                    SearchCondition sCondition = new SearchCondition() { TBName = "S_DefineMenu", TFieldKey = "MenuCode", TTotalCount = -1, TPageCurrent = 1, TPageCount = 1, TFieldOrder = "Type,FatherCode,OrderNum ASC", TWhere = Where };
                    List<MenuInfo> list = userInfoSearch.GetList(ref sCondition, context);
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<MenuInfo>(list, sCondition.TTotalCount)
                    };
                    break;
                case "ADD":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "tjcd"))
                    {
                        Info = new CommonModelFactory<MenuInfo>().GetModelFromContext(context);
                        jsonMessage = new WCFServiceProxy<IMenuManage>().getChannel.AddMenu(Info);

                        if (jsonMessage.Result) { new WCFServiceProxy<IMenuManage>().getChannel.ReSetCompany(base.loginOperator.CompanyID); }

                       
                    }
                    break;
                case "EDIT":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "bjcd"))
                    {
                        Info = new CommonModelFactory<MenuInfo>().GetModelFromContext(context);
                        jsonMessage = new WCFServiceProxy<IMenuManage>().getChannel.EditMenu(Info);
                        if (jsonMessage.Result) { new WCFServiceProxy<IMenuManage>().getChannel.ReSetCompany(base.loginOperator.CompanyID); }
                    }
                    break;
                case "DELETE":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "sccd"))
                    {
                        Info = new CommonModelFactory<MenuInfo>().GetModelFromContext(context);
                        jsonMessage = new WCFServiceProxy<IMenuManage>().getChannel.DeleteMenu(Info);
                        if (jsonMessage.Result) { new WCFServiceProxy<IMenuManage>().getChannel.ReSetCompany(base.loginOperator.CompanyID); }
                    }
                    break;
                case "QUERYFATHER":
                    Where = "1=1 ";

                    if (context.Request.QueryString["MenuType"] != null && context.Request.QueryString["MenuType"].ToString().Trim() != string.Empty)
                    {
                        string MenuType = context.Request.QueryString["MenuType"].ToString();
                        Where += "AND  Type= '" + MenuType + "'";
                    }
                  
                    userInfoSearch = new CommonSearch<MenuInfo>();
                    sCondition = new SearchCondition() { TBName = "S_DefineMenu", TFieldKey = "MenuCode", TTotalCount = -1, TPageCurrent = 1, TPageCount = 1, TFieldOrder = "Type,FatherCode,OrderNum ASC", TWhere = Where };
                    list = userInfoSearch.GetList(ref sCondition, context);
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<MenuInfo>(list, sCondition.TTotalCount)
                    };
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
    }
}
