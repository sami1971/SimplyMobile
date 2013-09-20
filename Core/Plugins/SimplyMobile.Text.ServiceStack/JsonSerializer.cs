
using System.IO;
using Serializer = ServiceStack.Text.JsonSerializer;

namespace SimplyMobile.Text.ServiceStack
{
    /// <summary>
    /// JSON serializer using ServiceStack.Text library
    /// </summary>
    /// <remarks>
    /// 
    /// ServiceStack.Text copyright information
    /// 
    /// Copyright (c) 2007-2011, Demis Bellot, ServiceStack.
    /// http://www.servicestack.net
    /// All rights reserved.
    /// 
    /// https://github.com/ServiceStack/ServiceStack.Text/blob/master/LICENSE
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
            return Serializer.SerializeToString(obj);
        }

        /// <summary>
        /// Deserializes string into an object
        /// </summary>
        /// <typeparam name="T">Type of object to serialize to</typeparam>
        /// <param name="data">Serialized object</param>
        /// <returns>Object of type T</returns>
        public T Deserialize<T>(string data)
        {
            return Serializer.DeserializeFromString<T>(data);
        }

        public T DeserializeFromStream<T>(Stream stream)
        {
            return Serializer.DeserializeFromStream<T>(stream);
        }
    }
}