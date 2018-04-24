using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneNETDataReceiver.Entity
{
    public class OneNetMessage
    {
        public MessageBody msg { get; set; }
        public string msg_signature { get; set; }
        public string nonce { get; set; }
    }

    public class MessageBody
    {
        /*
            "type": 1,
            "dev_id": 2016617,
            "ds_id": "datastream_id",
            "at": 1466133706841,
            "value": 42
            ---------------------
                "type": 2,
            "dev_id": 2016617,
            "status": 0,
            "login_type": 1,
            "at": 1466133706841,             
             
        */

        public int type { get; set; }
        public int dev_id { get; set; }
        public string ds_id { get; set; }
        public string at { get; set; }
        public string value { get; set; }
        public int login_type { get; set; }
        public int status { get; set; }

    }
}