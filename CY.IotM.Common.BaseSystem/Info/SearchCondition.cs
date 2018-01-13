using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CY.IotM.Common
{

    [DataContract]
    public class SearchCondition
    {

        [DataMember]
        public string TBName
        {
            get;
            set;
        }
        [DataMember]
        public string TFieldKey
        {
            get;
            set;
        }
        [DataMember]
        public int TPageCurrent
        {
            get;
            set;
        }
        [DataMember]
        public int TPageSize
        {
            get;
            set;
        }
        [DataMember]
        public string TFieldShow
        {
            get;
            set;
        }       
        [DataMember]
        public string TWhere
        {
            get;
            set;
        }
        [DataMember]
        public string TFieldOrder
        {
            get;
            set;
        }
        [DataMember]
        public int TTotalCount
        {
            get;
            set;
        }
        [DataMember]
        public int TPageCount
        {
            get;
            set;
        }
    }
}
