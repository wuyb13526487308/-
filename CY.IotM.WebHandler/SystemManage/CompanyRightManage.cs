using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CY.IotM.Common;
using CY.IotM.Common.Tool;

namespace CY.IotM.WebHandler.SystemManage
{
    public class CompanyRightManage : BaseHandler
    {
        public override void DoLoginedHandlerWork(HttpContext context)
        {
            Message jsonMessage;
            //获取操作类型AType:ADD,EDIT,DELETE,QUERY
            string AjaxType = context.Request.QueryString["AType"] == null ? string.Empty : context.Request.QueryString["AType"].ToString().ToUpper();
            DefineRight Info;
            jsonMessage = new Message()
            {
                Result = false,
                TxtMessage = "权限验证失败，可能原因:\n1、数据中心通讯失败。\n2、系统管理员未与您分配对应操作权限。"
            };
            switch (AjaxType)
            {
                case "LOADLEFTMENU":
                    jsonMessage = new Message() { Result = true, TxtMessage = CommonOperRightHelper.MenuListToJson(base.loginOperator) };
                    break;

                    //新菜单栏加载菜单
                case "LOADNEWLEFTMENU":
                    jsonMessage = new Message() { Result = true, TxtMessage = CommonOperRightHelper.GetLeftMenu(base.loginOperator) };
                    break;
                case "LOADHIDEMENUCODE":
                    jsonMessage = new Message() { Result = true, TxtMessage = CommonOperRightHelper.GetHidMenuCode(base.loginOperator) };
                    break;
                case "LOADCOMPANYRIGHT":
                    List<DefineRight> list = new WCFServiceProxy<IOperRightManage>().getChannel.LoadCompanyDefineRight(base.loginOperator.CompanyID);
                    jsonMessage = new Message() { Result = true, TxtMessage = JSon.ListToJson<DefineRight>(list, list.Count) };
                    break;
                case "LOADCOMPANYMENU":
                    jsonMessage = new Message() { Result = true, TxtMessage = CommonOperRightHelper.CompanyMenuListToJson(base.loginOperator.CompanyID) };
                    break;
                    //加载两级菜单树(zzcy)
                case "LOADNEWCOMPANYMENU":
                    jsonMessage = new Message() { Result = true, TxtMessage = CommonOperRightHelper.MenuListToJson(base.loginOperator) };
                    break;

                    //获取公司菜单
                case "LOADMENUBYCOMPANY":
                    string companyId = context.Request.Form["CompanyID"] == null ? string.Empty : context.Request.Form["CompanyID"].ToString().Trim();
                    jsonMessage = new Message() { Result = true, TxtMessage = CommonOperRightHelper.GetMenuListToJson(companyId) };

                    break;
                case "LOADRIGHTMENU":
                    string rightCode = context.Request.Form["rightCode"] == null ? string.Empty : context.Request.Form["rightCode"].ToString().Trim();
                    jsonMessage = new Message() { Result = true, TxtMessage = CommonOperRightHelper.GetCompanyRightMenuCode(base.loginOperator.CompanyID, rightCode) };
                    break;
                case "LOADCOMPANYOPERRIGHT":
                    string operID = context.Request.Form["OperID"] == null ? string.Empty : context.Request.Form["OperID"].ToString();
                    WCFServiceProxy<IOperRightManage> proxy = new WCFServiceProxy<IOperRightManage>();
                    try
                    {
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = proxy.getChannel.LoadCompanyOperDefineRight(base.loginOperator.CompanyID, operID)
                        };
                    }
                    catch
                    {
                    }
                    finally
                    {
                        proxy.CloseChannel();
                    }
                    break;
                //编辑公司菜单(分配菜单)
                case "EDITCOMPANYMENU":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "fpqycd"))
                    {
                    
                        companyId = context.Request.Form["CompanyID"] == null ? string.Empty : context.Request.Form["CompanyID"].ToString().Trim();

                        List<String> List = new List<String>();
                        string CompanyMenuCode = context.Request.Form["CompanyMenuCode"] == null ? string.Empty : context.Request.Form["CompanyMenuCode"].ToString();
                        foreach (string menu in CompanyMenuCode.Split(','))
                        {
                            List.Add(menu);
                        }
                        jsonMessage = new WCFServiceProxy<IOperRightManage>().getChannel.EditCompanyMenu(companyId, List);
                    }
                    break;



                case "ADDQXZ":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "tjqxz"))
                    {
                        Info = new CommonModelFactory<DefineRight>().GetModelFromContext(context);
                        Info.CompanyID = base.loginOperator.CompanyID;
                        string RightMenuCode = context.Request.Form["RightMenuCode"] == null ? string.Empty : context.Request.Form["RightMenuCode"].ToString();
                        List<DefineRightMenu> List = new List<DefineRightMenu>();
                        foreach (string menu in RightMenuCode.Split(','))
                        {
                            List.Add(new DefineRightMenu() { CompanyID = base.loginOperator.CompanyID, MenuCode = menu, RightCode = Info.RightCode });
                        }
                        jsonMessage = new WCFServiceProxy<IOperRightManage>().getChannel.AddCompanyDefineRight(Info, List.Distinct().ToList());

                    }
                    break;
                case "EDITQXZ":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "bjqxz"))
                    {
                        Info = new CommonModelFactory<DefineRight>().GetModelFromContext(context);
                        Info.CompanyID = base.loginOperator.CompanyID;
                        List<DefineRightMenu> List = new List<DefineRightMenu>();
                        string RightMenuCode = context.Request.Form["RightMenuCode"] == null ? string.Empty : context.Request.Form["RightMenuCode"].ToString();
                        foreach (string menu in RightMenuCode.Split(','))
                        {
                            List.Add(new DefineRightMenu() { CompanyID = base.loginOperator.CompanyID, MenuCode = menu, RightCode = Info.RightCode });
                        }
                        jsonMessage = new WCFServiceProxy<IOperRightManage>().getChannel.AddCompanyDefineRight(Info, List.Distinct().ToList());
                    }
                    break;
                case "DELETEQXZ":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "scqxz"))
                    {
                        Info = new CommonModelFactory<DefineRight>().GetModelFromContext(context);
                        Info.CompanyID = base.loginOperator.CompanyID;
                        jsonMessage = new WCFServiceProxy<IOperRightManage>().getChannel.DelCompanyDefineRight(Info);
                    }
                    break;
                case "EDITOPERRIGHT":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "fpqxz"))
                    {
                        string OperID = context.Request.Form["OperID"] == null ? string.Empty : context.Request.Form["OperID"].ToString();
                        string RightCode = context.Request.Form["RightCode"] == null ? string.Empty : context.Request.Form["RightCode"].ToString();
                        List<DefineRight> lDefineRight = new List<DefineRight>();
                        foreach (string tmp in RightCode.Split(','))
                        {
                            lDefineRight.Add(new DefineRight() { CompanyID = base.loginOperator.CompanyID, RightCode = tmp });
                        }
                        if (OperID == string.Empty)
                        {
                            jsonMessage = new Message()
                            {
                                Result = false,
                                TxtMessage = "请选择您要分配权限组的操作员。"
                            };
                        }
                        else
                        {
                            jsonMessage = new WCFServiceProxy<IOperRightManage>().getChannel.EditCompanyOperRight(base.loginOperator.CompanyID, OperID, lDefineRight);
                        }

                    }
                    break;
                case "REMOVECACHE":
                    jsonMessage = new WCFServiceProxy<IOperRightManage>().getChannel.RemoveCompanyRightCache(base.loginOperator.CompanyID);
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
