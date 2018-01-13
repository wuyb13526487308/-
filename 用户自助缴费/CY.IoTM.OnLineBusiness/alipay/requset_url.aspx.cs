using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Com.Alipay;



using CY.IoTM.OlbCommon;
using CY.IoTM.OlbCommon.Tool;
using CY.IoTM.OlbService;


namespace CY.IoTM.OnLineBusiness.alipay
{
    public partial class requset_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack) {

                string companyId = ""; string userId = ""; decimal money = 0; string account = "";
                if (Request.QueryString["companyId"] != null && Request.QueryString["companyId"].ToString().Trim() != string.Empty)
                {
                    companyId = Request.QueryString["companyId"].ToString().Trim();
                }
                if (Request.QueryString["userId"] != null && Request.QueryString["userId"].ToString().Trim() != string.Empty)
                {
                    userId = Request.QueryString["userId"].ToString().Trim();
                }
                if (Request.QueryString["money"] != null && Request.QueryString["money"].ToString().Trim() != string.Empty)
                {
                    money = decimal.Parse(Request.QueryString["money"].ToString().Trim());
                }
                if (Request.QueryString["account"] != null && Request.QueryString["account"].ToString().Trim() != string.Empty)
                {
                    account = Request.QueryString["account"].ToString().Trim();
                }



                string out_trade_no = Guid.NewGuid().ToString();





                ChargeOrderService.GetInstance().Add(new Olb_ChargeOrder()
                {
                    CompanyID = companyId,
                    GasUserID = userId,
                    Account = account,
                    OrderMoney = money,
                    ID = out_trade_no,
                    OrderTime = DateTime.Now,
                    Status = 0


                });



                WIDout_trade_no.Text = out_trade_no;
                WIDsubject.Text = "燃气账户充值";
                WIDtotal_fee.Text = money.ToString();
            
            
            
            }


        


        }

        protected void BtnAlipay_Click(object sender, EventArgs e)
        {

            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            //支付类型
            string payment_type = "1";
            //必填，不能修改
            //服务器异步通知页面路径
            string notify_url = "http://yyt.zzcyc.cn/alipay/notify_url.aspx";
            //需http://格式的完整路径，不能加?id=123这类自定义参数

            //页面跳转同步通知页面路径
            string return_url = "http://yyt.zzcyc.cn/alipay/return_url.aspx";
            //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/


            //商户订单号
            string out_trade_no = WIDout_trade_no.Text.Trim();
            //商户网站订单系统中唯一订单号，必填

            //订单名称
            string subject = WIDsubject.Text.Trim();
            //必填

            //付款金额
            string total_fee = WIDtotal_fee.Text.Trim();
            //必填

            //订单描述

            string body = WIDbody.Text.Trim();
            //商品展示地址
            string show_url = WIDshow_url.Text.Trim();
            //需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html

            //防钓鱼时间戳
            string anti_phishing_key = "";
            //若要使用请调用类文件submit中的query_timestamp函数

            //客户端的IP地址
            string exter_invoke_ip = "";
            //非局域网的外网IP地址，如：221.0.0.1


            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("seller_email", Config.Seller_email);
            sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
            sParaTemp.Add("service", "create_direct_pay_by_user");
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
            Response.Write(sHtmlText);

        }
    }
}