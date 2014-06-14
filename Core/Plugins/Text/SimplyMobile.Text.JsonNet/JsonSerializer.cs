//
//  Copyright 2013, Sami M. Kallio
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
using Newtonsoft.Json;
using System.IO;

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
        /// Gets the format.
        /// </summary>
        public Format Format
        {
            get { return Format.Json; }
        }

        /// <summary>
        /// Serializes object to a string
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>Serialized string of the object</returns>
        public string Serialize<T>(T obj)
        {
            JsonConvert.DefaultSettings().ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Serializes object to a stream
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <param name="stream">Stream to serialize to</param>
        public void Serialize<T>(T obj, Stream stream)
        {
            this.SerializeToStream(obj, stream);
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


        public object Deserialize(string data, System.Type type)
        {
            return JsonConvert.DeserializeObject(data, type);
        }

        /// <summary>
        /// Deserializes stream into an object
        /// </summary>
        /// <typeparam name="T">Type of object to serialize to</typeparam>
        /// <param name="stream">Stream to deserialize from</param>
        /// <returns>Object of type T</returns>
        public T Deserialize<T>(Stream stream) where T : class
        {
            return this.DeserializeFromStream<T>(stream);
        }
    }
}
