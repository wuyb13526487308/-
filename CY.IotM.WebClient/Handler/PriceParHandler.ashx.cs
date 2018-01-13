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
    /// PriceParHandler 的摘要说明
    /// </summary>
    public class PriceParHandler : BaseHandler
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
            IoT_PricePar Info;
            WCFServiceProxy<IPriceParManage> proxy=null;
            try
            {
                switch (AjaxType)
                {


                    case "QUERY":

                    CommonSearch<IoT_PricePar> InfoSearch = new CommonSearch<IoT_PricePar>();
                    string Where = "1=1 ";
                    Where += "AND CompanyID='" + loginOperator.CompanyID + "' ";
                    if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                    {
                        Where += context.Request.Form["TWhere"].ToString();
                    }
                    SearchCondition sCondition = new SearchCondition()
                    {
                        TBName = "IoT_PricePar",
                        TFieldKey = "ID", 
                        TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "ID ASC", TWhere = Where };
                    List<IoT_PricePar> list = InfoSearch.GetList(ref sCondition, context);
                    jsonMessage = new Message()
                    {
                        Result = true,
                        TxtMessage = JSon.ListToJson<IoT_PricePar>(list, sCondition.TTotalCount)
                    };
                    break;
                    case "ADD":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "jggl_tjjg"))
                        {
                            Info = new CommonModelFactory<IoT_PricePar>().GetModelFromContext(context);
                            Info.CompanyID = loginOperator.CompanyID;
                            proxy = new WCFServiceProxy<IPriceParManage>();
                            jsonMessage = proxy.getChannel.Add(Info);
                        }
                        break;
                    case "EDIT":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "jggl_bjjg"))
                        {
                            Info = new CommonModelFactory<IoT_PricePar>().GetModelFromContext(context);
                            proxy = new WCFServiceProxy<IPriceParManage>();
                            jsonMessage = proxy.getChannel.Edit(Info); 
                        }
                        break;
                    case "DELETE":
                        if (CommonOperRightHelper.CheckMenuCode(base.loginOperator, "jggl_scjg"))
                        {
                            Info = new CommonModelFactory<IoT_PricePar>().GetModelFromContext(context);
                            proxy = new WCFServiceProxy<IPriceParManage>();
                            jsonMessage = proxy.getChannel.Delete(Info);
                            
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