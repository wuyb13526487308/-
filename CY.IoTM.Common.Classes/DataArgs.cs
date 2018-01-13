using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CY.IoTM.Common.Classes
{
    /// <summary>
    /// 数据处理中心和监控客户端之间的数据传递对象。
    /// </summary>
    [DataContract]
    [KnownType(typeof(MonitorInfo))]
    [KnownType(typeof(ReadDataInfo))]
    [KnownType(typeof(WarningInfo))]
    [KnownType(typeof(List<CJDInfo>))]
    [Serializable]
    public class DataArge
    {
        private DataType _dataType;
        private object _data;
        public DataArge(DataType type, object data)
        {
            this._dataType = type;
            this._data = data;
        }
        [DataMember]
        public DataType DataType
        {
            get
            {
                return _dataType;
            }
            set
            {
                this._dataType = value;
            }
        }
        [DataMember]
        public object Data
        {
            get { return this._data; }
            set { this._data = value; }
        }
    }
}
