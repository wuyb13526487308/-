using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace CY.IotM.Common
{
    /// <summary>
    /// 系统登录用户信息
    /// </summary>
    public class LoginerInfo
    {


        string _Md5Key;
        /// <summary>
        /// 登录标识
        /// </summary>
        public string Md5Key
        {
            get { return _Md5Key; }
            set { _Md5Key = value; }
        }

        DateTime _AddTime;
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime AddTime
        {
            get { return _AddTime; }
            set { _AddTime = value; }
        }       

        DateTime _ExpiredDate;
        /// <summary>
        /// Key有效期
        /// </summary>
        public DateTime ExpiredDate
        {
            get { return _ExpiredDate; }
            set { _ExpiredDate = value; }
        }

        string _Company;
        /// <summary>
        /// 用户所在单位编码
        /// </summary>
        public string Company
        {
            get { return _Company; }
            set { _Company = value; }
        }

        string _OperID;
        /// <summary>
        /// 用户编号
        /// </summary>
        public string OperID
        {
            get { return _OperID; }
            set { _OperID = value; }
        }

        int _ClientType;
        /// <summary>
        /// 客户端类型:1、webClient 2、window客户端
        /// </summary>
        public int ClientType
        {
            get { return _ClientType; }
            set { _ClientType = value; }
        }

        private OperationContext _ClientContext;
        /// <summary>
        /// 客户端对象
        /// </summary>
        public OperationContext ClientContext
        {
            get { return _ClientContext; }
            set { _ClientContext = value; }
        }

    }
}
