using CY.IoTM.Common.Business;
using CY.IoTM.Common.Log;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.DataService.Business
{
    public class OneNetChannelService : IOneNetService
    {
        private string url = "http://api.heclouds.com/";
        private string appkey = "tqdK8g8NzfQQ4=JCVHMaprtfhCw=";//您在OneNET平台的APIKey

        public OneNetChannelService()
        {
            this.url = $"{System.Configuration.ConfigurationManager.AppSettings["oneNetUrl"]}";
            this.appkey = $"{System.Configuration.ConfigurationManager.AppSettings["appkey"]}";
        }

        public void OutPutLog(string mac, string info)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} -> 表：{mac} info:{info}");
            Log.getInstance().Write(new OneMeterDataLogMsg(mac, $"表：{mac} info:{info}"));
        }

        public string PostToOneNet(string mac, string hex)
        {
            Msg msg = new Msg();
            msg.data.Add(new _Data() { val = hex.Replace(" ", ""), res_id = 5505 });
            var client = new RestClient(url);
            var request = new RestRequest($"nbiot?imei={mac}&obj_id=3200&obj_inst_id=0&mode=2", Method.POST);
            request.AddHeader("api-key", appkey);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(msg);

            IRestResponse response = client.Execute(request);
            string result = response.Content; // raw content as string
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} ->向oneNet平台发送数据完成，返回结果：{result}");
            return result;           
        }
    }

    class Msg
    {
        public List<_Data> data { get; set; } = new List<_Data>();

    }

    class _Data
    {
        public string val { get; set; }
        public int res_id { get; set; }
    }
}
