using OneNETDataReceiver;
using OneNETDataReceiver.Entity;
using OneNETDataReceiver.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CY.IotM.WebClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public String receive()
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string body = new StreamReader(req).ReadToEnd();

            try
            {
                var obj = Util.resolveBody(body, Param.isJiaMi);
                if (obj != null)
                {
                    var dataRight = Util.checkSignature(obj, Param.token);
                    dataRight = true;
                    if (dataRight)
                    {
                        OneNetMessage msg = Newtonsoft.Json.JsonConvert.DeserializeObject<OneNetMessage>(body);
                        if (msg != null)
                        {
                            if (msg.msg.type == 2)
                            {
                                //上下线消息
                                if (msg.msg.status == 0)
                                {
                                    //上线
                                    WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now,
                                        ($"->设备{msg.msg.dev_id} 上线了"), ""));
                                    //ProxyFactory.getInstance().Login(msg.msg.dev_id);
                                }
                                else
                                {
                                    //下线
                                    WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now,
                                        ($"->设备{msg.msg.dev_id} 下线了"), ""));
                                    //ProxyFactory.getInstance().Logout(msg.msg.dev_id);
                                }
                            }
                            else if (msg.msg.type == 1 && msg.msg.value.Length >4)
                            {
                                //数据点消息
                                WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now,($"->接收到设备{msg.msg.dev_id} 上报的数据：" + msg.msg.value), ""));
                                ProxyFactory.getInstance().RevData(msg.msg.dev_id, msg.msg.value);
                            }
                        }
                        else
                        {
                            //WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, new ArraySegment<byte>(Encoding.UTF8.GetBytes($"{DateTime.Now.ToString("yy-MM-dd HH:mm:dd.fff")}->接收到OneNet的数据转换对象失败"))));

                        }

                        //WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, new ArraySegment<byte>(Encoding.UTF8.GetBytes("data receive: content" + obj.ToString()))));

                        Console.WriteLine("data receive: content" + obj.ToString());
                    }
                    else
                    {
                        //WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, new ArraySegment<byte>(Encoding.UTF8.GetBytes("data receive: signature error"))));

                        Console.WriteLine("data receive: signature error");
                    }
                }
                else
                {
                    //WebSocketService.getInstance().SendMessage(new MessageInfo(DateTime.Now, new ArraySegment<byte>(Encoding.UTF8.GetBytes("data receive: body empty error"))));

                    Console.WriteLine("data receive: body empty error");
                }

            }
            catch
            {
            }
            return "ok";
        }

        public String receive(string msg, string nonce, string signature)
        {
            if (string.IsNullOrEmpty(msg))
            {
                return "msg is null";
            }

            if (string.IsNullOrEmpty(nonce))
            {
                return "nonce is null";
            }

            if (string.IsNullOrEmpty(msg))
            {
                return "signature is null";
            }

            if (Util.VerifySignature(msg, nonce, signature, Param.token))
            {
                return msg;
            }
            else
            {
                return "error";
            }
        }
    }
}