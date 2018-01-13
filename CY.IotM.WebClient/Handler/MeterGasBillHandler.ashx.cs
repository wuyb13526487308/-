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
    /// IoT_MeterGasBillHandler 的摘要说明
    /// </summary>
    public class MeterGasBillHandler : BaseHandler
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
           
            WCFServiceProxy<IMeterGasBill> proxy = null;
            try
            {
                switch (AjaxType)
                {


                    case "QUERY":

                    CommonSearch<View_MeterGasBill> InfoSearch = new CommonSearch<View_MeterGasBill>();
                    string Where = "1=1 ";
                    Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                    if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                    {
                        Where += context.Request.Form["TWhere"].ToString();
                    }
                    SearchCondition sCondition = new SearchCondition() { TBName = "View_MeterGasBill", TFieldKey = "BillID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "BillID ASC", TWhere = Where };
                    List<View_MeterGasBill> list = InfoSearch.GetList(ref sCondition, context);
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<View_MeterGasBill>(list, sCondition.TTotalCount)
                    };
                    break;
                    case "SETTLE":
                    {
                        int priceId = 0; string month = ""; string meterNo = "";
                        if (context.Request.Form["PriceType"] != null && context.Request.Form["PriceType"].ToString().Trim() != string.Empty)
                        {
                            priceId = int.Parse(context.Request.Form["PriceType"].ToString().Trim());
                        }
                        if (context.Request.Form["Month"] != null && context.Request.Form["Month"].ToString().Trim() != string.Empty)
                        {
                            month = context.Request.Form["Month"].ToString().Trim();
                        }
                        if (context.Request.Form["MeterNo"] != null && context.Request.Form["MeterNo"].ToString().Trim() != string.Empty)
                        {
                            meterNo = context.Request.Form["MeterNo"].ToString().Trim();
                        }

                        proxy = new WCFServiceProxy<IMeterGasBill>();
                        jsonMessage = proxy.getChannel.SettleMeterGas(meterNo, month);
                        
                        break;


                    }
                    case "GETSETTLE":
                        {
                            int priceId = 0;
                            if (context.Request.Form["PriceType"] != null && context.Request.Form["PriceType"].ToString().Trim() != string.Empty)
                            {
                                priceId = int.Parse(context.Request.Form["PriceType"].ToString().Trim());
                            }

                            proxy = new WCFServiceProxy<IMeterGasBill>();
                            List<IoT_Meter> meterList = proxy.getChannel.GetMeterByPriceId(loginOperator.CompanyID, priceId);
                            jsonMessage = new Message()
                            {
                                Result = true,
                                TxtMessage = JSon.ListToJson<IoT_Meter>(meterList, meterList.Count)
                            };
                            break;
                        }
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