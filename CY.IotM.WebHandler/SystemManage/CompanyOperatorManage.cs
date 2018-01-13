using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CY.IotM.Common;
using CY.IotM.Common.Tool;

namespace CY.IotM.WebHandler.SystemManage
{
    public class CompanyOperatorManage : BaseHandler
    {
        public override void DoLoginedHandlerWork(HttpContext context)
        {
            Message jsonMessage;
            //获取操作类型AType:ADD,EDIT,DELETE,QUERY
            string AjaxType = context.Request.QueryString["AType"] == null ? string.Empty : context.Request.QueryString["AType"].ToString().ToUpper();
            CompanyOperator Info;
            string webCookie = context.Session.SessionID.ToString();
            WCFServiceProxy<ICompanyOperatorManage> proxy;
            WCFServiceProxy<ILoginerManage> proxyLoginer;
            string Where = "1=1 ";
            SearchCondition sCondition;
            jsonMessage = new Message()
            {
                Result = false,
                TxtMessage = "权限验证失败，可能原因:\n1、数据中心通讯失败。\n2、系统管理员未与您分配对应操作权限。"
            };
            switch (AjaxType)
            {
                case "QUERY":
                    CommonSearch<CompanyOperator> CompanyOperatorSearch = new CommonSearch<CompanyOperator>();
                    Where += "AND CompanyID='" + base.loginOperator.CompanyID + "' ";
                    if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                    {
                        Where += context.Request.Form["TWhere"].ToString();
                    }
                    sCondition = new SearchCondition() { TBName = "S_CompanyOperator", TFieldKey = "OperID,CompanyID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "OperID ASC", TWhere = Where };
                    List<CompanyOperator> list = CompanyOperatorSearch.GetList(ref sCondition, context);
                    //将密码设置为空
                    list.ForEach(p => p.Pwd = string.Empty);
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<CompanyOperator>(list, sCondition.TTotalCount)
                    };
                    break;
                case "LOADLOGINER":
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.TToJson<CompanyOperator>(base.loginOperator)
                    };
                    break;
                case "HEARTCOOKIE":
                    //向数据中心更新登录信息
                    proxyLoginer = new WCFServiceProxy<ILoginerManage>();
                    try
                    {
                        proxyLoginer.getChannel.RegisterClient(Md5.GetMd5(webCookie), base.loginOperator.OperID, base.loginOperator.CompanyID);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = Md5.GetMd5(context.Session.SessionID.ToString())
                        };
                    }
                    catch { }
                    finally
                    {
                        proxyLoginer.CloseChannel();
                    }
                    break;
                case "ADD":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "tjczy"))
                    {
                        Info = new CommonModelFactory<CompanyOperator>().GetModelFromContext(context);
                        Info.CompanyID = base.loginOperator.CompanyID;
                        Info.Pwd = Md5.GetMd5(base.loginOperator.CompanyID + base.loginOperator.CompanyID);
                        Info.OperType = 0;//0一般操作员:1企业主账号（每个企业仅一个）；
                        if (Info.PhoneLogin == true && Info.Phone == string.Empty)
                        {
                            jsonMessage = new Message()
                            {
                                Result = false,
                                TxtMessage = string.Format("添加失败，请输入为操作员{0}绑定的手机号码。", Info.OperID)
                            };
                        }
                        else
                        {
                            WCFServiceProxy<ICompanyOperatorManage> proxyCompanyOperatorManage
                                = new WCFServiceProxy<ICompanyOperatorManage>();
                            try
                            {
                                jsonMessage = proxyCompanyOperatorManage.getChannel.AddCompanyOperator(Info);

                            }
                            catch { }
                            finally
                            {
                                proxyCompanyOperatorManage.CloseChannel();
                            }
                        }
                    }
                    break;
                case "EDIT":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "bjczy"))
                    {
                        Info = new CommonModelFactory<CompanyOperator>().GetModelFromContext(context);
                        Info.Pwd = null;
                        if (Info.CompanyID != base.loginOperator.CompanyID)
                        {
                            jsonMessage = new Message()
                            {
                                Result = false,
                                TxtMessage = "操作员所属企业与登录账号所属企业不一致，编辑失败。"
                            };
                        }
                        else
                        {
                            if (Info.PhoneLogin == true && Info.Phone == string.Empty)
                            {
                                jsonMessage = new Message()
                                {
                                    Result = false,
                                    TxtMessage = string.Format("编辑失败，请输入为操作员{0}绑定的手机号码。", Info.OperID)
                                };
                            }
                            else
                            {
                                proxy = new WCFServiceProxy<ICompanyOperatorManage>();
                                try
                                {
                                    jsonMessage = proxy.getChannel.EditCompanyOperator(Info);

                                }
                                catch
                                { }
                                finally
                                {
                                    proxy.CloseChannel();
                                }
                            }
                        }
                    }
                    break;
                //case "EDITOPERAREA":
                //    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "bjczy"))
                //    {
                //        Info = new CommonModelFactory<CompanyOperator>().GetModelFromContext(context);
                //        Info.Pwd = null;
                //        if (Info.CompanyID != base.loginOperator.CompanyID)
                //        {
                //            jsonMessage = new Message()
                //            {
                //                Result = false,
                //                TxtMessage = "操作员所属企业与登录账号所属企业不一致，编辑失败。"
                //            };
                //        }
                //        else
                //        {
                //            proxy = new WCFServiceProxy<ICompanyOperatorManage>();
                //            try
                //            {
                //                string nodes = context.Request.Form["nodes"].ToString();
                //                jsonMessage = proxy.getChannel.EditOperatorArea(Info, nodes.Split(','));
                //            }
                //            catch
                //            { }
                //            finally
                //            {
                //                proxy.CloseChannel();
                //            }
                //        }
                //    }
                //    break;
                case "DELETE":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "scczy"))
                    {
                        Info = new CommonModelFactory<CompanyOperator>().GetModelFromContext(context);
                        if (Info.CompanyID != base.loginOperator.CompanyID)
                        {
                            jsonMessage = new Message()
                            {
                                Result = false,
                                TxtMessage = "操作员所属企业与登录账号所属企业不一致，删除失败。"
                            };
                        }
                        else
                        {
                            proxy = new WCFServiceProxy<ICompanyOperatorManage>();
                            try
                            {
                                jsonMessage = proxy.getChannel.DeleteCompanyOperator(Info);

                            }
                            catch
                            {
                            }
                            finally
                            {
                                proxy.CloseChannel();
                            }
                        }
                    }
                    break;
                case "RESETPWD":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "czmm"))
                    {
                        Info = new CommonModelFactory<CompanyOperator>().GetModelFromContext(context);
                        if (Info.CompanyID != base.loginOperator.CompanyID)
                        {
                            jsonMessage = new Message()
                            {
                                Result = false,
                                TxtMessage = "操作员所属企业与登录账号所属企业不一致，删除失败。"
                            };
                        }
                        else
                        {
                            CompanyOperator infoEditPwd = new CompanyOperator()
                            {
                                OperID = Info.OperID,
                                CompanyID = Info.CompanyID,
                                Pwd = Md5.GetMd5(Info.CompanyID + Info.CompanyID)
                            };
                            proxy = new WCFServiceProxy<ICompanyOperatorManage>();
                            try
                            {
                                jsonMessage = proxy.getChannel.EditCompanyOperator(infoEditPwd);
                                if (jsonMessage.Result)
                                {
                                    jsonMessage.TxtMessage = "操作员密码已重置为初始密码：" + Info.CompanyID;
                                }
                            }
                            catch
                            {
                            }
                            finally
                            {
                                proxy.CloseChannel();
                            }

                        }
                    }
                    break;
                case "EDITPWD":
                    if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "xgmm"))
                    {
                        Info = new CommonModelFactory<CompanyOperator>().GetModelFromContext(context);
                        if (Info.CompanyID != base.loginOperator.CompanyID)
                        {
                            jsonMessage = new Message()
                            {
                                Result = false,
                                TxtMessage = "操作员所属企业与登录账号所属企业不一致，修改失败。"
                            };
                        }
                        else if (Info.OperID != base.loginOperator.OperID)
                        {
                            jsonMessage = new Message()
                            {
                                Result = false,
                                TxtMessage = "当前登录者与您修改的账号不一致，修改失败。"
                            };
                        }
                        else
                        {
                            //验证输入旧密码
                            CommonSearch<CompanyOperator> userInfoSearch = new CommonSearch<CompanyOperator>();
                            Where = " OperID='" + Info.OperID + "' AND CompanyID='" + Info.CompanyID + "'";
                            sCondition = new SearchCondition() { TBName = "S_CompanyOperator", TFieldKey = "OperID", TTotalCount = -1, TWhere = Where };
                            CompanyOperator dbInfo = userInfoSearch.GetFirstTModel(ref sCondition);
                            string OldPwd = context.Request.Form["OldPwd"] == null ? string.Empty : context.Request.Form["OldPwd"].ToString();
                            if (sCondition.TTotalCount == -1 || dbInfo == null)
                            {
                                jsonMessage = new Message()
                                {
                                    Result = false,
                                    TxtMessage = "数据中心未返回信息，请稍候再试。"
                                };

                            }
                            else if (Md5.GetMd5(dbInfo.CompanyID + OldPwd.Trim()) != dbInfo.Pwd)
                            {
                                jsonMessage = new Message()
                                {
                                    Result = false,
                                    TxtMessage = "原密码验证失败。"
                                };

                            }
                            else
                            {
                                CompanyOperator infoEditPwd = new CompanyOperator()
                                {
                                    OperID = dbInfo.OperID,
                                    CompanyID = dbInfo.CompanyID,
                                    Pwd = Md5.GetMd5(dbInfo.CompanyID + Info.Pwd)
                                };
                                proxy = new WCFServiceProxy<ICompanyOperatorManage>();
                                try
                                {
                                    jsonMessage = proxy.getChannel.EditCompanyOperator(infoEditPwd);
                                }
                                catch
                                {
                                }
                                finally
                                {
                                    proxy.CloseChannel();
                                }
                            }
                        }
                    }
                    break;

                //case "LOADAREATREE":

                //    string operID = context.Request.Form["OperID"].ToString();
                //    jsonMessage = new Message() { Result = true, TxtMessage = CreateAreaTreeToJson(base.loginOperator.CompanyID, operID) };
                //    break;

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
