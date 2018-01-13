using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace CY.IotM.Common.Tool
{
    public class JsToJson
    {
        /// <summary>
        /// 序列化对象。
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        public static string SerializeToJsonString(object objectToSerialize)
        {
            using (var memory = new MemoryStream())
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new DataContractJsonSerializer(objectToSerialize.GetType());
                serializer.WriteObject(memory, objectToSerialize);
                memory.Position = 0;
                using (var reader = new StreamReader(memory))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// Json反序列化，泛型操作。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonString)
        {
            using (var memory = new MemoryStream(Encoding.Unicode.GetBytes(jsonString.Replace("'", "\""))))
            {
                var serialize = new DataContractJsonSerializer(typeof(T));
                return (T)serialize.ReadObject(memory);
            }
        }
    }
}
