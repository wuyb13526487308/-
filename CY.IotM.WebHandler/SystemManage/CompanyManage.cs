using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CY.IotM.Common;
using CY.IotM.Common.Tool;

namespace CY.IotM.WebHandler.SystemManage
{
    public class CompanyManage : BaseHandler
    {
        public override void DoLoginedHandlerWork(HttpContext context)
        {
            Message jsonMessage;
            //获取操作类型AType:ADD,EDIT,DELETE,QUERY
            string AjaxType = context.Request.QueryString["AType"] == null ? string.Empty : context.Request.QueryString["AType"].ToString().ToUpper();
            CompanyInfo Info;
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
                    CommonSearch<CompanyInfo> userInfoSearch = new CommonSearch<CompanyInfo>();
                    SearchCondition sCondition = new SearchCondition() { TBName = "S_CompanyInfo", TFieldKey = "CompanyID", TTotalCount = -1, TPageCurrent = 1, TPageCount = 1, TFieldOrder = "CompanyName ASC", TWhere = Where };
                    List<CompanyInfo> list = userInfoSearch.GetList(ref sCondition, context);
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<CompanyInfo>(list, sCondition.TTotalCount)
                    };
                    break;
                case "ADD":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "tjzcqy"))
                    {
                        Info = new CommonModelFactory<CompanyInfo>().GetModelFromContext(context);
                        jsonMessage = new WCFServiceProxy<ICompanyManage>().getChannel.AddCompany(Info);
                    }
                    break;
                case "EDIT":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "bjzcqy"))
                    {
                        Info = new CommonModelFactory<CompanyInfo>().GetModelFromContext(context);
                        jsonMessage = new WCFServiceProxy<ICompanyManage>().getChannel.EditCompany(Info);

                    }
                    break;
                case "REMOVECACHE":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "qcqyqxhc"))
                    {
                        Info = new CommonModelFactory<CompanyInfo>().GetModelFromContext(context);
                        jsonMessage = new WCFServiceProxy<IOperRightManage>().getChannel.RemoveCompanyRightCache(Info.CompanyID);
                    }
                    break;
                case "RESETCOMPANYADMIN":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "qyzzhcsh"))
                    {
                        Info = new CommonModelFactory<CompanyInfo>().GetModelFromContext(context);
                        if (Info.CompanyID == base.loginOperator.CompanyID)
                        {
                            jsonMessage = new Message() {  Result=false, TxtMessage=string.Format( "您没有权限对当前企业管理账户{0}做初始化操作。",base.loginOperator.CompanyID)};
                        }
                        else
                        {
                            jsonMessage = new WCFServiceProxy<ICompanyManage>().getChannel.ResetCompanyAdmin(Info);
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
            context.Response.Write(JSon.TToJson<Message>(jsonMessage));
        }
    }
}
