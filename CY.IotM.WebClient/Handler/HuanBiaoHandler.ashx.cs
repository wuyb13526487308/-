using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IotM.WebHandler;
using CY.IoTM.Common.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CY.IotM.WebClient.Handler
{
    /// <summary>
    /// HuanBiaoHandler 的摘要说明
    /// </summary>
    public class HuanBiaoHandler : BaseHandler
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
            IoT_ChangeMeter Info;
            WCFServiceProxy<IHuanBiao> Iproxy = null;

            try
            {
                switch (AjaxType)
                {
                        //查询换标记录
                    case "QUERY":
                        CommonSearch<View_HuanBiao> InfoSearch = new CommonSearch<View_HuanBiao>();
                        string Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        SearchCondition sCondition = new SearchCondition() { TBName = "View_HuanBiao", TFieldKey = "HID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "FinishedDate DESC", TWhere = Where };
                        List<View_HuanBiao> list = InfoSearch.GetList(ref sCondition, context);

                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_HuanBiao>(list, sCondition.TTotalCount)
                        };
                        break;
                        //查询换标历史记录
                    case "HISTORYMETER":
                        CommonSearch<View_HistoryUserMeter> InfoSearch1 = new CommonSearch<View_HistoryUserMeter>();

                        Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        sCondition = new SearchCondition() { TBName = "View_HistoryUserMeter", TFieldKey = "MeterNo", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "MeterNo DESC", TWhere = Where };
                         List<View_HistoryUserMeter> list1 = InfoSearch1.GetList(ref sCondition, context);

                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_HistoryUserMeter>(list1, sCondition.TTotalCount)
                        };
                        break;
                        //新增换标申请
                    case "ADD":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "szbjcs"))
                        {
                            Info = new CommonModelFactory<IoT_ChangeMeter>().GetModelFromContext(context);
                            List<IoT_ChangeMeter> lstIoT_ChangeMeter = new List<IoT_ChangeMeter>();
                            View_UserMeter View_UserMeters = new View_UserMeter();
                            string MeterNo = string.IsNullOrEmpty(context.Request["MeterNo"]) == true ? "" : context.Request["MeterNo"].ToString();
                            string UserID = string.IsNullOrEmpty(context.Request["UserID"]) == true ? "" : context.Request["UserID"].ToString();
                            string Reason = string.IsNullOrEmpty(context.Request["Reason"]) == true ? "" : context.Request["Reason"].ToString();
                            Iproxy = new WCFServiceProxy<IHuanBiao>();
                            View_UserMeters = Iproxy.getChannel.getView_UserMeterList(UserID, MeterNo, loginOperator.CompanyID);
                            Info.CompanyID = View_UserMeters.CompanyID;
                            //Info.OldGasSum = View_UserMeters.LastTotal;这里不太清楚
                            Info.OldMeterNo = View_UserMeters.MeterNo;
                            Info.Reason = Reason;
                            Info.State = '1';
                            Info.UserID = View_UserMeters.UserID;
                            //Info.ChangeUseSum = View_UserMeters.CompanyID;
                            //Info.ChangeGasSum = View_UserMeters.CompanyID;
                            //Info.p = View_UserMeters.CompanyID;
                            Info.RegisterDate = DateTime.Now;
                            jsonMessage = Iproxy.getChannel.AddShenQing(Info);
                        }
                        break;
                        //修改换标申请
                    case "EDIT":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "szbjcs"))
                        {
                            Info = new CommonModelFactory<IoT_ChangeMeter>().GetModelFromContext(context);
                            string Reason = string.IsNullOrEmpty(context.Request["Reason"]) == true ? "" : context.Request["Reason"].ToString();
                            string HID = string.IsNullOrEmpty(context.Request["HID"]) == true ? "" : context.Request["HID"].ToString();
                            Info.ID = int.Parse(HID);
                            Info.Reason = Reason;
                            Info.CompanyID = loginOperator.CompanyID;
                            Iproxy = new WCFServiceProxy<IHuanBiao>();
                            jsonMessage = Iproxy.getChannel.Edit(Info);
                        }
                        break;
                        //撤销换表申请
                    case "REVOKE":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "SQREVOKE"))
                        {
                            Iproxy = new WCFServiceProxy<IHuanBiao>();
                            string ID = string.Empty;
                            if (context.Request["HID"] != null && context.Request["HID"].ToString().Trim() != string.Empty)
                            {
                                ID = context.Request["HID"].ToString() == "" ? "0" : context.Request["HID"].ToString();
                            }
                            jsonMessage = Iproxy.getChannel.revoke(ID, loginOperator.CompanyID);
                        }
                        break;
                        //换表登记
                    case "DENGJI":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "DENGJI"))
                        {
                            string dayGas = string.Empty;          //上期结算底数
                            string NewMeterType_DJ = string.Empty; //新表类型
                            string TotalAmountS = string.Empty;    //本期用量
                            string MeterType_DJ = string.Empty;    //原表类型
                            string Direction_DJ = string.Empty;    //原表进气方向
                            string ID = string.Empty;    //原表进气方向
                            if (context.Request.Form["dayGas"] != null && context.Request.Form["dayGas"].ToString().Trim() != string.Empty)
                            {
                                dayGas = context.Request.Form["dayGas"].ToString() == "" ? "0" : context.Request.Form["dayGas"].ToString();
                            }
                            if (context.Request.Form["NewMeterType_DJ"] != null && context.Request.Form["dayGas"].ToString().Trim() != string.Empty)
                            {
                                NewMeterType_DJ = context.Request.Form["NewMeterType_DJ"].ToString() == "" ? "" : context.Request.Form["NewMeterType_DJ"].ToString();
                            }
                            if (context.Request.Form["TotalAmountS"] != null && context.Request.Form["TotalAmountS"].ToString().Trim() != string.Empty)
                            {
                                TotalAmountS = context.Request.Form["TotalAmountS"].ToString() == "" ? "0" : context.Request.Form["TotalAmountS"].ToString();
                            }
                            if (context.Request.Form["MeterType_DJ"] != null && context.Request.Form["MeterType_DJ"].ToString().Trim() != string.Empty)
                            {
                                MeterType_DJ = context.Request.Form["MeterType_DJ"].ToString() == "" ? "" : context.Request.Form["MeterType_DJ"].ToString();
                            }
                            if (context.Request.Form["Direction_DJ"] != null && context.Request.Form["Direction_DJ"].ToString().Trim() != string.Empty)
                            {
                                Direction_DJ = context.Request.Form["Direction_DJ"].ToString() == "" ? "" : context.Request.Form["Direction_DJ"].ToString();
                            }
                            if (context.Request.Form["ID"] != null && context.Request.Form["ID"].ToString().Trim() != string.Empty)
                            {
                                ID = context.Request.Form["ID"].ToString() == "" ? "0" : context.Request.Form["ID"].ToString();
                            }
                            Info = new CommonModelFactory<IoT_ChangeMeter>().GetModelFromContext(context);
                            Info.CompanyID = loginOperator.CompanyID;
                            Info.State = '2';
                            Info.ID = int.Parse(ID);
                            Iproxy = new WCFServiceProxy<IHuanBiao>();
                            if (MeterType_DJ!="01")//不是金额表则将换表时剩余金额改为0
                            {
                                Info.RemainingAmount = 0;
                            }
                            //注意换表时剩余金额（仅针对金额表）
                            jsonMessage = Iproxy.getChannel.Dengji(Info);
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
                if (Iproxy != null)
                    Iproxy.CloseChannel();
            }
            context.Response.Write(JSon.TToJson<Message>(jsonMessage));
        }
    }
}