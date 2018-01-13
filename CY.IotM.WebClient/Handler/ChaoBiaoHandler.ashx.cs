using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IotM.WebClient.IotM;
using CY.IotM.WebHandler;
using CY.IoTM.Common.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace CY.IotM.WebClient
{
    /// <summary>
    /// ChaoBiaoHandler 的摘要说明
    /// </summary>
    public class ChaoBiaoHandler : BaseHandler
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
            WCFServiceProxy<IUserManage> proxy = null;
            try
            {
                switch (AjaxType)
                {

                    //查询用户表讯息
                    case "QUERY":
                        CommonSearch<View_UserMeter> InfoSearch = new CommonSearch<View_UserMeter>();
                        string Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        SearchCondition sCondition = new SearchCondition() { TBName = "View_UserMeter", TFieldKey = "UserID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "UserID ASC", TWhere = Where };
                        List<View_UserMeter> list = InfoSearch.GetList(ref sCondition, context);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_UserMeter>(list, sCondition.TTotalCount)
                        };
                        break;
                    //查询用户表具视图
                    case "QUERYHISTORY":
                        CommonSearch<View_UserMeterHistory> InfoSearchView = new CommonSearch<View_UserMeterHistory>();
                        Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request["UserID"] != null && context.Request["UserID"].ToString().Trim() != string.Empty)
                        {
                            Where += "and UserID='" + context.Request["UserID"].ToString() + "'";
                        }
                        sCondition = new SearchCondition() { TBName = "View_UserMeterHistory", TFieldKey = "ReadDate", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "ReadDate DESC", TWhere = Where };
                        List<View_UserMeterHistory> listView = InfoSearchView.GetList(ref sCondition, context);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_UserMeterHistory>(listView, sCondition.TTotalCount)
                        };
                        break;
                    //查询街道
                    case "QUERYSTREET":
                        //CommonSearch<IoT_Street> InfoSearch = new CommonSearch<IoT_Street>();
                        Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        sCondition = new SearchCondition()
                        {
                            TBName = "View_Community",
                            TFieldKey = "ID",
                            TTotalCount = -1,
                            TPageCurrent = 1,
                            TFieldOrder = "ID ASC",
                            TWhere = Where
                        };
                        CommonSearch<View_Community> InfoSearchStreet = new CommonSearch<View_Community>();
                        List<View_Community> listStreet = InfoSearchStreet.GetList(ref sCondition, context);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_Community>(listStreet, sCondition.TTotalCount)
                        };
                        break;
                    //查询历史资料笔数
                    case "GETHISTORYCOUNT":
                        string TimeKind = string.Empty;//按照时间类型
                        string UserKind = string.Empty;//按照人员类型
                        string CompanyID = string.Empty;//公司ID
                        Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        //CommonSearch<IoT_Street> InfoSearch = new CommonSearch<IoT_Street>();
                        try
                        {
                            if (context.Request["Time"] != null && context.Request["Time"].ToString().Trim() != string.Empty)
                            {
                                TimeKind = context.Request["Time"];
                            }
                            if (context.Request["User"] != null && context.Request["User"].ToString().Trim() != string.Empty)
                            {
                                UserKind = context.Request["User"];
                            }
                            if (UserKind != "*" && UserKind != "")
                            {
                                Where += "AND charindex(','+CAST(Community AS NVARCHAR(10))+',',','+'" + UserKind + "'+',')>=1";
                            }
                            if (TimeKind != "*")
                            {
                                Where += "and CONVERT(varchar(100), ReadDate, 23)='" + TimeKind + "'";
                            }
                            WCFServiceProxy<IChaoBiao> proxy1 = new WCFServiceProxy<IChaoBiao>();
                            List<View_UserMeterHistory> listAll = proxy1.getChannel.GetModelList(Where);
                            if (listAll == null || listAll.Count <= 0)
                            {//如果查询出笔数大于0，返回True
                                jsonMessage = new Message()
                                {
                                    Result = true,
                                    TxtMessage = ""
                                };
                            }
                            else
                            {//否则返回False
                                jsonMessage = new Message()
                                {
                                    Result = false,
                                    TxtMessage = ""
                                };
                            }

                        }
                        catch (Exception)
                        {
                            jsonMessage = new Message()
                            {
                                Result = false,
                                TxtMessage = ""
                            };
                        }
                        break;
                    //获取当前的公司
                    case "GETCOMPANY":
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = loginOperator.CompanyID
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