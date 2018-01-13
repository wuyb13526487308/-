using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using MongoDB.Bson;

namespace CY.IoTM.Common.Business
{
    /// <summary>
    /// 基础实体对象,该类主要用于MongoDB，作为对象的索引。
    /// </summary>
    [DataContract]
    public class BaseEntity
    {
        /// <summary>
        /// 对象ID
        /// </summary>
        [DataMember]
        public MongoDB.Bson.ObjectId _id;
        public BaseEntity()
        {
            this._id = ObjectId.GenerateNewId();
        }
    }
}
