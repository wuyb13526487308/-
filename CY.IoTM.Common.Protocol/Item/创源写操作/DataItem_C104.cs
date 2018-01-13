using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Item
{

    /// <summary>
    /// 设置服务器信息
    /// </summary>
    public class DataItem_C104 : DataItem
    {

        //服务器参数
        public ServerParamType ParamType { get; set; }
        public string GPRS_Adress { get; set; }
        public string GPRS_Port { get; set; }
        public string GSM { get; set; }
        public string APN { get; set; }
        public string APN_Name { get; set; }
        public string APN_Kay { get; set; }




        public DataItem_C104(ServerParamType ParamType, string GPRS_Adress, string GPRS_Port, string GSM, string APN, string APN_Name, string APN_Kay) 
        {

            this.ParamType = ParamType;

            this.GPRS_Adress = GPRS_Adress;
            this.GPRS_Port = GPRS_Port;
            this.GSM = GSM;

            this.APN = APN;
            this.APN_Name = APN_Name;
            this.APN_Kay = APN_Kay;



          
            //this.identityCode = IdentityCode.设置上传周期;
            //this.indexCode = 0x00;
            //this.controlCode = ControlCode.CYWriteData;
            //this.length = 89;

            this.dataUnits = new byte[86];

            dataUnits[0] = (byte)Convert.ToInt32(this.ParamType);



            //GPRS网络地址 25
            byte[] gprs_adress = Encoding.ASCII.GetBytes(GPRS_Adress);

            if (gprs_adress.Length>25)
            {
                Console.WriteLine("GPRS网络地址超长"); return;
            }

            for (int i = 0; i < gprs_adress.Length; i++) {

                dataUnits[1 + i] = gprs_adress[i];
            
            }

            //GPRS网络端口号 5
            byte[] gprs_port = Encoding.ASCII.GetBytes(GPRS_Port);

            for (int i = 0; i < gprs_port.Length; i++)
            {

                dataUnits[26 + i] = gprs_port[i];

            }


            //GSM网络地址（手机号) 15
            byte[] gsm = Encoding.ASCII.GetBytes(GSM);

            for (int i = 0; i < gsm.Length; i++)
            {

                dataUnits[31 + i] = gsm[i];

            }


            //APN接入点 20
            byte[] apn = Encoding.ASCII.GetBytes(APN);

            for (int i = 0; i < apn.Length; i++)
            {

                dataUnits[46 + i] = apn[i];

            }


            //APN用户名 10
            byte[] apn_name = Encoding.ASCII.GetBytes(APN_Name);

            for (int i = 0; i < apn_name.Length; i++)
            {

                dataUnits[66 + i] = apn_name[i];

            }



            //APN用户密码 10
            byte[] apn_key = Encoding.ASCII.GetBytes(APN_Kay);

            for (int i = 0; i < apn_key.Length; i++)
            {

                dataUnits[76 + i] = apn_key[i];

            }
          
        }



        /// <summary>
        ///从站应答
        /// </summary>
        public DataItem_C104(byte[] data):base(data)
        {
        }
    }

    public class DataItem_C104_Answer : DataItem_Answer
    {
        public DataItem_C104_Answer(byte ser) : base(ser) { }
        protected override void Init()
        {
            base.Init();
            this.dataUnits[0] = 0xC1;
            this.dataUnits[1] = 0x04;
        }
    }

    public enum ServerParamType 
    { 
    

        手机号=0,
        IP地址=1,
        域名地址=2    
    }

}
