using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using CY.IotM.Common;
using CY.IotM.Common.Tool;
using CY.IotM.WebHandler;
using CY.IoTM.Common.Business;

namespace CY.IotM.WebClient.Handler
{
    /// <summary>
    /// IgnitionVentilationHandler 的摘要说明
    /// </summary>
    public class DianHuoHandler : BaseHandler
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
            IoT_Meter Info;
            WCFServiceProxy<IMeterManage> proxyMeter = null;
            WCFServiceProxy<IDianHuo> proxy = null;
            try
            {
                switch (AjaxType)
                {
                    //撤销点火
                    case "REVOKE":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "dhtq_cx"))
                        {
                            Info = new CommonModelFactory<IoT_Meter>().GetModelFromContext(context);
                            proxy = new WCFServiceProxy<IDianHuo>();
                            string reslut = proxy.getChannel.Undo(Info);

                            if (reslut != "")
                            {
                                jsonMessage = new Message { Result = false, TxtMessage = reslut };
                            }
                            else
                            {
                                jsonMessage = new Message { Result = true, TxtMessage = "操作成功" };
                            }
                        }
                        break;

                    //点火 
                    case "REGISTRATION":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "dhtq_dh"))
                        {
                            proxy = new WCFServiceProxy<IDianHuo>();
                            proxyMeter = new WCFServiceProxy<IMeterManage>();

                            Int64 PriceType = 0;
                            string EnableMeterOper = string.Empty;
                            List<String> meterNoList = null;
                            List<String> lstUserID = null;
                            DateTime EnableDate = DateTime.Now;
                            string strPriceType = string.Empty;
                            string meterType = context.Request.Form["MType"].ToString();
                            if (context.Request.Form["PriceType"] != null && context.Request.Form["PriceType"].ToString().Trim() != string.Empty)
                            {
                                PriceType = Int64.Parse(context.Request.Form["PriceType"].ToString().Trim());
                                strPriceType = context.Request.Form["PriceType"].ToString().Trim();
                            }
                            if (!string.IsNullOrEmpty(context.Request.Form["MType"]) && context.Request.Form["MType"] =="01" && strPriceType == "")
                            {
                                jsonMessage = new Message()
                                {
                                    Result = false,
                                    TxtMessage = "请选择价格类型！"
                                };
                                context.Response.Write(JSon.TToJson<Message>(jsonMessage));
                                return;
                            }
                            if (context.Request.Form["strNo"] != null && context.Request.Form["strNo"].ToString().Trim() != string.Empty)
                            {
                                string strNo = context.Request.Form["strNo"];
                                string[] arrNo = strNo.Split(',');
                                meterNoList = arrNo.ToList();
                            }
                            if (context.Request.Form["UserID"] != null && context.Request.Form["UserID"].ToString().Trim() != string.Empty)
                            {
                                string strNo = context.Request.Form["UserID"];
                                string[] arrNo = strNo.Split(',');
                                lstUserID = arrNo.ToList();
                            }
                            if (context.Request.Form["EnableDate"] != null && context.Request.Form["EnableDate"].ToString().Trim() != string.Empty)
                            {
                                EnableDate = Convert.ToDateTime(context.Request.Form["EnableDate"].ToString().Trim());
                            }
                            EnableMeterOper = string.IsNullOrEmpty(context.Request.Form["EnableOper"]) == true ? "" : context.Request.Form["EnableOper"];
                            //if (context.Request.Form["EnableOper"] != null && context.Request.Form["EnableOper"].ToString().Trim() != string.Empty)
                            //{
                            //    EnableMeterOper = context.Request.Form["EnableOper"].ToString().Trim();
                            //}
                            jsonMessage = proxy.getChannel.DianHuo(meterNoList, meterType, PriceType, this.loginOperator.CompanyID, EnableDate, lstUserID, EnableMeterOper);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {

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