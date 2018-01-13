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
    /// ChongZhiHandler 的摘要说明
    /// </summary>
    public class ChongZhiHandler : BaseHandler
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
            WCFServiceProxy<ISetUploadCycle> proxy = null;
            try
            {
                switch (AjaxType)
                {
                        //查询充值的历史资料
                    case "QUERY":
                        CommonSearch<View_ChongZhi> InfoSearch = new CommonSearch<View_ChongZhi>();
                        string Where = "1=1 ";
                        Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                        if (context.Request.Form["DateS"] != null && context.Request.Form["DateS"].ToString().Trim() != string.Empty)
                        {
                            Where += " AND convert(char(10),TopUpDate,120)>='" + context.Request.Form["DateS"].ToString() + "'"; ;
                        }
                        if (context.Request.Form["DateE"] != null && context.Request.Form["DateE"].ToString().Trim() != string.Empty)
                        {
                            Where += " AND convert(char(10),TopUpDate,120)<='" + context.Request.Form["DateE"].ToString() + "'"; ;
                        }
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        SearchCondition sCondition = new SearchCondition() { TBName = "View_ChongZhi", TFieldKey = "AID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "TopUpDate DESC", TWhere = Where };
                        List<View_ChongZhi> list = InfoSearch.GetList(ref sCondition, context);

                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<View_ChongZhi>(list, sCondition.TTotalCount)
                        };
                        break;
                        //执行充值动作
                    case "CXCHONGZHI":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "CZ_CXCZ"))//营业厅撤销充值
                        {
                            IoT_MeterTopUp Info;
                            WCFServiceProxy<IChongzhiManage> proxy1 = null;
                            Info = new CommonModelFactory<IoT_MeterTopUp>().GetModelFromContext(context);
                            List<IoT_MeterTopUp> lstIoT_MeterTopUp = new List<IoT_MeterTopUp>();
                            //表号
                            string MeterNo = string.IsNullOrEmpty(context.Request["MeterNo"]) == true ? "" : context.Request["MeterNo"].ToString();
                           //充值金额
                            string Amount = string.IsNullOrEmpty(context.Request["Amount"]) == true ? "" : context.Request["Amount"].ToString();
                            //表ID
                            string MeterID = string.IsNullOrEmpty(context.Request["MeterID"]) == true ? "" : context.Request["MeterID"].ToString();
                            //人员ID
                            string UserID = string.IsNullOrEmpty(context.Request["UserID"]) == true ? "" : context.Request["UserID"].ToString();
                            //PK
                            string ID = string.IsNullOrEmpty(context.Request["AID"]) == true ? "" : context.Request["AID"].ToString();
                            //任务编号
                            string TaskID = string.IsNullOrEmpty(context.Request["TaskID"]) == true ? "" : context.Request["TaskID"].ToString();
                           string Context = string.IsNullOrEmpty(context.Request["Context"]) == true ? "" : context.Request["Context"].ToString();
                            Info.Amount = decimal.Parse(Amount);
                            Info.MeterID = int.Parse(MeterID);
                            Info.MeterNo = MeterNo;
                            Info.ID = int.Parse(ID);
                            Info.Context = Context;
                            Info.Oper = base.loginOperator.Name;
                            Info.CompanyID = base.loginOperator.CompanyID;
                            Info.TopUpDate = DateTime.Now;
                            Info.UserID = UserID;
                            Info.TopUpType = '1';//充值类型目前未知
                            Info.State = '0';//等待充值状态
                            Info.TaskID = TaskID;//等待充值状态
                            proxy1 = new WCFServiceProxy<IChongzhiManage>();
                            jsonMessage = proxy1.getChannel.UPD(Info);
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
                if (proxy != null)
                    proxy.CloseChannel();
            }
            context.Response.Write(JSon.TToJson<Message>(jsonMessage));
        }
    }
}