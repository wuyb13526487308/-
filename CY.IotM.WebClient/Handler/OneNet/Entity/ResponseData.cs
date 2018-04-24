using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneNETDataReceiver.Entity
{
    public class ResponseData
    {
        /*
         {
                "errno": 0,
                "error":“succ”，
                "data":{
                //不超过64个字符字符串
                "cmd_uuid":“2302-312-FWs”
                }
            }        
*/

        public int errno { get; set; }
        public string error { get; set; }

        public string data { get; set; }
    }
}