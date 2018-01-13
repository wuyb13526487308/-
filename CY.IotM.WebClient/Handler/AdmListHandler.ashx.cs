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
using System.IO;

namespace CY.IotM.WebClient.Handler
{
    /// <summary>
    /// AdmListHandler 的摘要说明
    /// </summary>
    public class AdmListHandler : BaseHandler
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
            ADItem Info = new ADItem();
            WCFServiceProxy<IADItemDAL> proxy = null;
            Info = new CommonModelFactory<ADItem>().GetModelFromContext(context);
            proxy = new WCFServiceProxy<IADItemDAL>();
            //文件控制接口
            WCFServiceProxy<IADFileService>  fileContrl = new WCFServiceProxy<IADFileService>();
            try
            {
                switch (AjaxType)
                {//查询用户
                    case "QUERY":

                        CommonSearch<ADItem> InfoSearch = new CommonSearch<ADItem>();
                        string Where = "1=1 ";
                        
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        SearchCondition sCondition = new SearchCondition() { TBName = "ADItem", TFieldKey = "AI_ID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = "OrderID asc", TWhere = Where };
                        List<ADItem> list = InfoSearch.GetList(ref sCondition, context);
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<ADItem>(list, sCondition.TTotalCount)
                        };
                        break;
                    //列表
                    case "QUERYVIEW":

                        CommonSearch<ADItem> InfoSearchView = new CommonSearch<ADItem>();
                        Where = "1=1 ";
                        if (context.Request.Form["TWhere"] != null && context.Request.Form["TWhere"].ToString().Trim() != string.Empty)
                        {
                            Where += context.Request.Form["TWhere"].ToString();
                        }
                        sCondition = new SearchCondition() { TBName = "ADItem", TFieldKey = "AI_ID", TTotalCount = -1, TPageCurrent = 1, TFieldOrder = " OrderID asc", TWhere = Where };

                        List<ADItem> listView = InfoSearchView.GetList(ref sCondition, context);
                        //SysCookie.UrlParaStr = "InfoCount?" + listView.Count;//取得条数据,方便生成序列号
                        jsonMessage = new Message()
                        {
                            Result = true,
                            TxtMessage = JSon.ListToJson<ADItem>(listView, sCondition.TTotalCount)
                        };
                        break;

                    //添加广告内容
                    case "ADD":
                        
                        //提取上传地址配置值
                         string ADFilePath = System.Configuration.ConfigurationManager.AppSettings["ADFilePath"].ToString();
                         HttpPostedFile postedFile = context.Request.Files[0];
                         
                         //提取扩展名
                         string fileExtendName = Info.FileName.Substring(Info.FileName.IndexOf("."), Info.FileName.Length - Info.FileName.IndexOf("."));
                         //将文件存到服务器上
                         postedFile.SaveAs(ADFilePath + @"\" + Info.FileName);
                         //将生成文件流
                         System.IO.Stream stream = postedFile.InputStream;
                         
                         //将文件转换成文件流并存入二进制数组
                         byte[] data = new byte[stream.Length];
                         stream.Read(data, 0, data.Length);
                         stream.Close();

                        Info.FileLength = data.Length;
                        Info.StorePath = postedFile.FileName;
                        Info.StoreName = loginOperator.CompanyID + string.Format("{0:yyMMdd}", DateTime.Now) + AdMComm.GetAddZero(proxy.getChannel.userPuFileNum(loginOperator.CompanyID) + 1, 5) + fileExtendName;
                        //调用共用文件上传接口
                        string fileRetrue = fileContrl.getChannel.UpLoad(loginOperator.CompanyID, Info.StoreName, data);
                        jsonMessage = proxy.getChannel.Add(Info);
                        break;
                    case "EDIT":
                        
                        HttpFileCollection files = HttpContext.Current.Request.Files;
                        
                        if (files.Count > 0)
                        {
                            //提取上传地址配置值
                            string eADFilePath = System.Configuration.ConfigurationManager.AppSettings["ADFilePath"].ToString();
                            HttpPostedFile epostedFile = context.Request.Files[0];
                            //提取扩展名
                            string efileExtendName = Info.FileName.Substring(Info.FileName.IndexOf("."), Info.FileName.Length - Info.FileName.IndexOf("."));
                            //将文件存到服务器上
                            epostedFile.SaveAs(eADFilePath + @"\" + Info.FileName);
                            //将生成文件流
                            System.IO.Stream estream = epostedFile.InputStream;

                            //将文件转换成文件流并存入二进制数组
                            byte[] edata = new byte[estream.Length];
                            estream.Read(edata, 0, edata.Length);
                            estream.Close();

                            Info.FileLength = edata.Length;
                            Info.StorePath = epostedFile.FileName;
                            Info.StoreName = loginOperator.CompanyID + string.Format("{0:yyMMdd}", DateTime.Now) + AdMComm.GetAddZero(proxy.getChannel.userPuFileNum(loginOperator.CompanyID) + 1, 5) + efileExtendName;
                            //调用共用文件上传接口
                            string efileRetrue = fileContrl.getChannel.UpLoad(loginOperator.CompanyID, Info.StoreName, edata);
                        }
                        
                        jsonMessage = proxy.getChannel.Edit(Info);
                        break;

                    case "UPORDER":
                        
                        //if (Info.OrderID != null) orderIDre = short.Parse(Info.OrderID.ToString());
                        jsonMessage = proxy.getChannel.upOrder(Info);
                        break;

                    case "DOWNORDER":
                        //if (Info.OrderID != null) orderIDre = short.Parse(Info.OrderID.ToString());
                        jsonMessage = proxy.getChannel.downOrder(Info);
                        break;
                    //删除信息 
                    case "DELFILE":

                        jsonMessage = proxy.getChannel.Delete(Info.AI_ID);
                        //调用共用文件接口,删除文件
                        string dfileRetrue = fileContrl.getChannel.Delete(loginOperator.CompanyID, Info.StoreName);
                        break;
                    default:
                        jsonMessage = new Message()
                        {
                            Result = false,
                            TxtMessage = "1.操作未定义!"
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