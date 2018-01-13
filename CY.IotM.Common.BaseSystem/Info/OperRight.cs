using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CY.IotM.Common
{

    [DataContract]
    [Serializable]
    public class DefineMenu
    {
        [DataMember]
        public string MenuCode { get; set; }
        [DataMember]
        public string CompanyID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string UrlClass { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public short OrderNum { get; set; }
        [DataMember]
        public string FatherCode { get; set; }
    }
    [DataContract]
    [Serializable]
    public class DefineOperRight
    {
        [DataMember]
        public string CompanyID { get; set; }
        [DataMember]
        public string OperID { get; set; }
        [DataMember]
        public string RightCode { get; set; }
    }
    [DataContract]
    [Serializable]
    public class DefineRight
    {
        [DataMember]
        public string RightCode { get; set; }
        [DataMember]
        public string CompanyID { get; set; }
        [DataMember]
        public string RightName { get; set; }
        [DataMember]
        public string Context { get; set; }
    }
    [DataContract]
    [Serializable]
    public class DefineRightMenu
    {
        [DataMember]
        public string CompanyID { get; set; }
        [DataMember]
        public string RightCode { get; set; }
        [DataMember]
        public string MenuCode { get; set; }
    }
}
