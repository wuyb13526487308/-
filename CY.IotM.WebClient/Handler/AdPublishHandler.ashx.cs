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
using System.Data.Linq;

namespace CY.IotM.WebClient.Handler
{
    /// <summary>
    /// AdPublishHandler 的摘要说明
    /// </summary>
    public class AdPublishHandler : BaseHandler
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
            ADPublish Info = new ADPublish();
            WCFServiceProxy<IADPublishDAL> proxy = new WCFServiceProxy<IADPublishDAL>();
            WCFServiceProxy<IADPublishUserDAL> proxyPU = new WCFServiceProxy<IADPublishUserDAL>();
            WCFServiceProxy<IADUserDAL> proxyAdU = new WCFServiceProxy<IADUserDAL>();
            WCFServiceProxy<IADContextDAL> proxyGGZT = new WCFServiceProxy<IADContextDAL>(); 
            Info = new CommonModelFactory<ADPublish>().GetModelFromContext(context);
            

            try
            {
                switch (AjaxType)
                {//查询用户
                    case "QUERY":

                        CommonSearch<View_AdPublish> InfoSearch = new CommonSearch<View_AdPublish>();
                        string Where = "1=1 and CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        SearchCondition sCondition = new SearchCondition() { TBName = "View_AdPublish", TFieldKey = "AP_ID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "AP_ID asc", TWhere = Where };
                        List<View_AdPublish> list = InfoSearch.GetList(ref sCondition, context);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_AdPublish>(list, sCondition.TTotalCount)
                        };
                        break;
                    //列表
                    case "QUERYVIEW":

                        CommonSearch<View_AdPublish> InfoSearchView = new CommonSearch<View_AdPublish>();
                        Where = "1=1 and CompanyID='"+ loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        sCondition = new SearchCondition() { TBName = "View_AdPublish", TFieldKey = "AP_ID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = " AP_ID asc", TWhere = Where };

                        List<View_AdPublish> listView = InfoSearchView.GetList(ref sCondition, context);

                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_AdPublish>(listView, sCondition.TTotalCount)
                        };
                        break;

                    //添加广告内容
                    case "ADD":

                        //1.添加到发布主表
                        Info.CompanyID = base.loginOperator.CompanyID;
                        jsonMessage = proxy.getChannel.Add(Info);
                        //当发布成功后再进行后续人员添加操作;
                        if (jsonMessage.Result )
                        {
                            //取得返回AP_ID
                            long recAP_ID = long.Parse(jsonMessage.TxtMessage);
                        //2.添加到发布用户表中;
                            if (context.Request.Form["strNo"] != null && context.Request.Form["strNo"].ToString().Trim() != string.Empty)
                            {
                                ADPublishUser adPUser = new ADPublishUser();
                                adPUser.AP_ID = recAP_ID;
                                adPUser.CompanyID = loginOperator.CompanyID;
                                //State和FinishDate两个字段由APP程序完成
                                adPUser.State = 0;
                                //adPUser.FinishedDate = DateTime.Now;
                                adPUser.Context = context.Request.Form["CNContext"] == null ? string.Empty : context.Request.Form["CNContext"].ToString();
                                string strNo = context.Request.Form["strNo"];
                                jsonMessage = proxyPU.getChannel.groupAdd(adPUser,strNo);
                                if (!jsonMessage.Result) { break; }
                            }
                            //当是发布状态时调用接口
                            if (Info.State == 1 || Info.State == 2)
                            {
                                //3.调用APP段发布接口
                                string pmApp = proxy.getChannel.ADPubManager(recAP_ID);
                                if (pmApp.IndexOf("APP接口") >= 0)
                                {
                                    jsonMessage.TxtMessage = pmApp;
                                    break;
                                }
                                //4.更新广告主题表中的状态;
                                if (Info.State == 1 || Info.State == 2)
                                {
                                    jsonMessage = proxyGGZT.getChannel.UpadteAdStatus(long.Parse(Info.AC_ID.ToString()), 2);

                                }
                            }
                        }

                        break;
                    case "EDIT":
                        Info.CompanyID = base.loginOperator.CompanyID;
                        jsonMessage = proxy.getChannel.Edit(Info);
                        if (jsonMessage.Result)
                        {
                            ADPublishUser adPUser = new ADPublishUser();
                            adPUser.AP_ID = Info.AP_ID;
                            adPUser.CompanyID = loginOperator.CompanyID;
                            //if (Info.State == 1 || Info.State == 2)
                            //{
                            //    adPUser.State = 1;
                            //}
                            //else
                            //{
                                adPUser.State = 0;
                            //}

                            adPUser.Context = context.Request.Form["CNContext"] == null ? string.Empty : context.Request.Form["CNContext"].ToString();
                            string strNo = context.Request.Form["strNo"];
                            jsonMessage = proxyPU.getChannel.groupAdd(adPUser, strNo);
                            if (!jsonMessage.Result) { break; }

                            if (Info.State == 1 || Info.State == 2){
                                //3.调用APP段发布接口
                                string pmApp = proxy.getChannel.ADPubManager(long.Parse(Info.AP_ID.ToString()));
                                if (pmApp.IndexOf("APP接口") >= 0)
                                {
                                    jsonMessage.TxtMessage = pmApp;
                                    break;
                                }

                                //2.更新广告主题表中的状态;
                                if (Info.State == 1 || Info.State == 2)
                                {
                                    jsonMessage = proxyGGZT.getChannel.UpadteAdStatus(long.Parse(Info.AC_ID.ToString()), 2);

                                }
                             }
                        }
                        
                        break;

                    //删除信息 
                    case "DELETINFO":

                        jsonMessage = proxy.getChannel.Delete(Info.AP_ID);

                        break;
                    //更新状态 
                    case "UPDATESTATE":
                        jsonMessage = proxy.getChannel.UpadteAdStatus(Info.AP_ID,1); 
                        //3.更新广告主题表中的状态;
                        if (jsonMessage.Result) {

                                //3.调用APP段发布接口
                            string pmApp = proxy.getChannel.ADPubManager(long.Parse(Info.AP_ID.ToString()));
                                if (pmApp.IndexOf("APP接口") >= 0)
                                {
                                    jsonMessage.TxtMessage = pmApp;
                                    break;
                                }
                                else
                                {
                                    jsonMessage = proxyGGZT.getChannel.UpadteAdStatus(long.Parse(Info.AC_ID.ToString()), 2);

                                }
                        }
                        break;

                    //发布人员信息
                    case "QUERYVIEWPUINFO":

                        CommonSearch<View_AdPublishUserInfo> InfoSearchView_pu = new CommonSearch<View_AdPublishUserInfo>();
                        Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        sCondition = new SearchCondition() { TBName = "View_AdPublishUserInfo", TFieldKey = " ID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "  ID asc", TWhere = Where };

                        List<View_AdPublishUserInfo> listView_pu = InfoSearchView_pu.GetList(ref sCondition, context);

                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_AdPublishUserInfo>(listView_pu, sCondition.TTotalCount)
                        };
                        break;
   
                    default:
                        jsonMessage = new Message()
                        {
                            Result = false,
                            TxtMessage = "操作未定义!"
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