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


namespace CY.IotM.WebClient.Handler
{
    /// <summary>
    /// YingYeTingCZHandler 的摘要说明
    /// </summary>
    public class YingYeTingCZHandler : BaseHandler
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
            //IoT_User Info; View_UserMeter viewInfo;
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
                    //实现营业厅充值动作
                    case "CHONGZHI":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "CHONGZHI"))
                        {
                            IoT_MeterTopUp Info;
                            WCFServiceProxy<IChongzhiManage> proxy1 = null;
                            Info = new CommonModelFactory<IoT_MeterTopUp>().GetModelFromContext(context);
                            List<IoT_MeterTopUp> lstIoT_MeterTopUp = new List<IoT_MeterTopUp>();
                            string MeterNo = string.IsNullOrEmpty(context.Request["MeterNo"]) == true ? "" : context.Request["MeterNo"].ToString();
                            string Amount = string.IsNullOrEmpty(context.Request["Amount"]) == true ? "0" : context.Request["Amount"].ToString();
                            string MeterID = string.IsNullOrEmpty(context.Request["MeterID"]) == true ? "" : context.Request["MeterID"].ToString();
                            string UserID = string.IsNullOrEmpty(context.Request["UserID"]) == true ? "" : context.Request["UserID"].ToString();
                            string OperID = string.IsNullOrEmpty(context.Request["OperId"]) ? "" : context.Request["OperId"].ToString();
                            string OperName = string.IsNullOrEmpty(context.Request["OperName"]) ? "" : context.Request["OperName"].ToString();
                            string PayType = string.IsNullOrEmpty(context.Request["PayType"]) ? "0" : context.Request["PayType"].ToString();

                            if (Amount == "0")//后台验证输入的金额是否为空
                            {
                                jsonMessage = new Message()
                                 {
                                     Result = false,
                                     TxtMessage = "请输入正确的充值金额。"
                                 };
                                context.Response.Write(JSon.TToJson<Message>(jsonMessage));
                                return;
                            }
                            Info.Amount = decimal.Parse(Amount); //充值金额
                            Info.MeterID = int.Parse(MeterID);   //充值的表ID
                            Info.MeterNo = MeterNo;              //充值表号
                            Info.Oper = base.loginOperator.Name; //操作员
                            Info.CompanyID = base.loginOperator.CompanyID;//公司
                            Info.TopUpDate = DateTime.Now;       //充值时间，后面会被写到表上的时间覆盖，用来表示写到表上的时间
                            Info.PayDate = DateTime.Now;//充值时间，用户支付完成的时间
                            Info.UserID = UserID;                //充值户号
                            Info.TopUpType = '0';                //充值类型为"本地营业厅"
                            Info.State = '0';                    //等待充值状态
                            Info.PayType = Convert.ToChar(PayType);//付款类型：0 现金 1 支付宝 2 微信
                            Info.SFOperID = OperID;
                            Info.SFOperName = OperName;
                            proxy1 = new WCFServiceProxy<IChongzhiManage>();
                            jsonMessage = proxy1.getChannel.Add(Info);
                            proxy1.CloseChannel();
                        }
                        break;
                    case "PRINT":
                        //打印票据（修改打印状态为已打印)
                        WCFServiceProxy<IChongzhiManage> proxy2 = new WCFServiceProxy<IChongzhiManage>();
                        string id = string.IsNullOrEmpty(context.Request["id"]) ? "" : context.Request["id"].ToString();
                        if(id == "")
                        {
                            jsonMessage = new Message()
                            {
                                Result = false,
                                TxtMessage = "票据不存在"
                            };
                        }
                        else
                        {
                            string result = proxy2.getChannel.PrintTicket(id);
                            if (result == "")
                            {
                                jsonMessage = new Message()
                                {
                                    Result = true,
                                    TxtMessage = ""
                                };
                            }

                        }
                        proxy2.CloseChannel();
                        
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