using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CY.IotM.WebClient
{

    [DataContract]
    public class OlbCompany
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        [DataMember]
        public string Id { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

    }



    [DataContract]
    public class OlbGasUser
    {
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string CompanyID { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public decimal Balance { get; set; }
        [DataMember]
        public string MeterNo { get; set; }
        [DataMember]
        public int MeterType { get; set; }
    }



    [DataContract]
    public class OlbGasFeeBill
    {
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public DateTime ChaoBiaoTime { get; set; }
        [DataMember]
        public decimal LastNum { get; set; }
        [DataMember]
        public decimal ThisNum { get; set; }
        [DataMember]
        public decimal GasNum { get; set; }
        [DataMember]
        public decimal GasFee { get; set; }


    }

}