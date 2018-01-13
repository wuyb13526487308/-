using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 用户信息数据结构
    /// </summary>
    [DataContract]
    public class User//:BaseEntity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [DataMember]
        public string UserID { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// 表号
        /// </summary>
        [DataMember]
        public string MeterNo { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [DataMember]
        public string Address { get; set; }

    }



    public class UserRows {

        public List<User> Rows { get; set; }
 
    
    }





}
