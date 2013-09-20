using System.IO;
using Newtonsoft.Json;

namespace SimplyMobile.Text.JsonNet
{
    /// <summary>
    /// JSON serializer using Newtonsoft.Json library
    /// </summary>
    /// <remarks>
    /// 
    /// Newtonsoft.Json copyright information
    /// 
    /// The MIT License (MIT)
    /// Copyright (c) 2007 James Newton-King
    /// 
    /// https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md
    /// 
    /// </remarks>
    public class JsonSerializer : IJsonSerializer
    {
        /// <summary>
        /// Serializes object to a string
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>Serialized string of the object</returns>
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Deserializes string into an object
        /// </summary>
        /// <typeparam name="T">Type of object to serialize to</typeparam>
        /// <param name="data">Serialized object</param>
        /// <returns>Object of type T</returns>
        public T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public T DeserializeFromStream<T>(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                var text = streamReader.ReadToEnd();
                return Deserialize<T>(text);
            }
        }
    }
}