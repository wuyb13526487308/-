using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Data.Linq;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IotM.WebHandler;
using CY.IoTM.Common.Business;


namespace CY.IotM.WebClient.Handler
{
    /// <summary>
    /// AdInfoHandler 的摘要说明
    /// </summary>
    public class AdInfoHandler : BaseHandler
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
            IoT_AdInfo Info;
            WCFServiceProxy<IAdInfoManage> proxy=null;
            try
            {
                switch (AjaxType)
                {
                    case "QUERY":
                    CommonSearch<IoT_AdInfo> InfoSearch = new CommonSearch<IoT_AdInfo>();
                    string Where = "1=1 ";
                    Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";

                    if (context.Request.Form["Date1"] != null && context.Request.Form["Date1"].ToString().Trim() != string.Empty)
                    {
                        Where += " AND StartDate>='" + context.Request.Form["Date1"].ToString() + "'"; ;
                    }
                    if (context.Request.Form["Date2"] != null && context.Request.Form["Date2"].ToString().Trim() != string.Empty)
                    {
                        Where += " AND EndDate<='" + context.Request.Form["Date2"].ToString() + "'"; ;
                    }

                    if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                    {
                        Where += context.Request.Form["TWhere"].ToString();
                    }
                    SearchCondition sCondition = new SearchCondition() { TBName = "IoT_AdInfo", TFieldKey = "FileIndex", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "FileIndex ASC", TWhere = Where };
                    List<IoT_AdInfo> list = InfoSearch.GetList(ref sCondition, context);
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<IoT_AdInfo>(list, sCondition.TTotalCount)
                    };
                    break;
                    case "ADD":
                        {

                            HttpPostedFile postedFile = context.Request.Files[0];
                            System.IO.Stream stream = postedFile.InputStream;
                            //将流转换为二进制数组
                            byte[] bytes = new byte[stream.Length];
                            stream.Read(bytes, 0, bytes.Length);
                            Binary binaryData = new Binary(bytes);
                            IoT_AdInfo adInfo = new IoT_AdInfo();

                            adInfo.FileData = binaryData;
                            adInfo.CompanyID = base.loginOperator.CompanyID;
                            adInfo.PublishStatus = 0;//未发布

                            if (context.Request.Form["CycleTime"] != null && context.Request.Form["CycleTime"].ToString().Trim() != string.Empty)
                            {
                                adInfo.CycleTime = int.Parse(context.Request.Form["CycleTime"].ToString());
                            }
                            if (context.Request.Form["EndDate"] != null && context.Request.Form["EndDate"].ToString().Trim() != string.Empty)
                            {
                                adInfo.EndDate = context.Request.Form["EndDate"].ToString();
                            }
                            if (context.Request.Form["StartDate"] != null && context.Request.Form["StartDate"].ToString().Trim() != string.Empty)
                            {
                                adInfo.StartDate = context.Request.Form["StartDate"].ToString();
                            }
                            if (context.Request.Form["ShowStatus"] != null && context.Request.Form["ShowStatus"].ToString().Trim() != string.Empty)
                            {
                                adInfo.ShowStatus = int.Parse(context.Request.Form["ShowStatus"].ToString());
                            }
                            if (context.Request.Form["PublishStatus"] != null && context.Request.Form["PublishStatus"].ToString().Trim() != string.Empty)
                            {
                                adInfo.PublishStatus = int.Parse(context.Request.Form["PublishStatus"].ToString());
                            }
                            if (context.Request.Form["FileIndex"] != null && context.Request.Form["FileIndex"].ToString().Trim() != string.Empty)
                            {
                                adInfo.FileIndex = int.Parse(context.Request.Form["FileIndex"].ToString());
                            }
                            if (context.Request.Form["FileName"] != null && context.Request.Form["FileName"].ToString().Trim() != string.Empty)
                            {
                                adInfo.FileName = context.Request.Form["FileName"].ToString();
                            }
                            adInfo.FileSize = postedFile.ContentLength;
                            proxy = new WCFServiceProxy<IAdInfoManage>();
                            jsonMessage = proxy.getChannel.Add(adInfo);
                        }
                        break;
                    case "EDIT":
                        {
                            Info = new CommonModelFactory<IoT_AdInfo>().GetModelFromContext(context);
                            proxy = new WCFServiceProxy<IAdInfoManage>();
                            jsonMessage = proxy.getChannel.Edit(Info); 
                        }
                        break;
                    case "DELETE":
                        {
                            Info = new CommonModelFactory<IoT_AdInfo>().GetModelFromContext(context);
                            proxy = new WCFServiceProxy<IAdInfoManage>();
                            jsonMessage = proxy.getChannel.Delete(Info);
                        }
                        break;
                    case "PUBLISH":
                        {
                            IoT_SetAdInfo setInfo = new CommonModelFactory<IoT_SetAdInfo>().GetModelFromContext(context);
                            setInfo.SetType = 0;//发布
                            setInfo.SendTime = DateTime.Now;
                            proxy = new WCFServiceProxy<IAdInfoManage>();
                            jsonMessage = proxy.getChannel.Publish(setInfo);
                        }
                        break;
                    case "UNPUBLISH":
                        {
                            IoT_SetAdInfo setInfo = new CommonModelFactory<IoT_SetAdInfo>().GetModelFromContext(context);
                            proxy = new WCFServiceProxy<IAdInfoManage>();
                            jsonMessage = proxy.getChannel.UnPublish(setInfo);
                        }
                        break;
                    case "EDITADINFO":
                        {
                            IoT_SetAdInfo setInfo = new CommonModelFactory<IoT_SetAdInfo>().GetModelFromContext(context);
                            setInfo.SetType = 1;//编辑
                            setInfo.DeleteStatus = 0;
                            setInfo.SendTime = DateTime.Now;
                            proxy = new WCFServiceProxy<IAdInfoManage>();
                            jsonMessage = proxy.getChannel.EditAdInfo(setInfo);
                        }
                        break;

                    case "DELETEADINFO":
                        {
                            IoT_SetAdInfo setInfo = new CommonModelFactory<IoT_SetAdInfo>().GetModelFromContext(context);
                            setInfo.SetType = 2;//删除
                            setInfo.DeleteStatus = 1;
                            setInfo.SendTime = DateTime.Now;
                            proxy = new WCFServiceProxy<IAdInfoManage>();
                            jsonMessage = proxy.getChannel.EditAdInfo(setInfo);
                        }
                        break;
               
                    case "QUERYCONTROL":
                        CommonSearch<IoT_SetAdInfo> setAdInfoSearch = new CommonSearch<IoT_SetAdInfo>();
                        Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        sCondition = new SearchCondition() { TBName = "IoT_SetAdInfo", TFieldKey = "SendTime", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "SendTime Desc", TWhere = Where };
                        List<IoT_SetAdInfo> setAdInfoList = setAdInfoSearch.GetList(ref sCondition, context);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<IoT_SetAdInfo>(setAdInfoList, sCondition.TTotalCount)
                        };
                        break;

                    case "QUERYMETERVIEW":
                        CommonSearch<View_AdInfoMeter> InfoSearch_User = new CommonSearch<View_AdInfoMeter>();
                        Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        if (context.Request.Form["ID"] != null && context.Request.Form["ID"].ToString().Trim() != string.Empty)
                        {
                            Where += " AND ID=" + context.Request.Form["ID"].ToString().Trim();
                        }
                        sCondition = new SearchCondition() { TBName = "View_AdInfoMeter", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "FinishedDate DESC", TWhere = Where };
                        List<View_AdInfoMeter> list_User = InfoSearch_User.GetList(ref sCondition, context);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_AdInfoMeter>(list_User, sCondition.TTotalCount)
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