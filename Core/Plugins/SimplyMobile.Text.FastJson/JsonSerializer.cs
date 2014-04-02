using System;
using System.IO;
using System.Text.Json;

namespace SimplyMobile.Text.FastJson
{
    public class JsonSerializer : JsonParser, IJsonSerializer
    {
        private IJsonSerializer serializer;

        public JsonSerializer(IJsonSerializer serializer)
        {
            this.serializer = serializer;
        }

        #region ITextSerializer implementation
        public Format Format 
        {
            get
            {
                return Format.Json;
            }
        }

        public string Serialize<T>(T obj)
        {

            return serializer.Serialize(obj);
        }

        /// <summary>
        /// Serializes object to a stream
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <param name="stream">Stream to serialize to</param>
        public void Serialize<T>(T obj, Stream stream)
        {
            serializer.Serialize(obj, stream);
        }

        //public string Serialize(object obj)
        //{
        //    return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        //}

        public T Deserialize<T> (string data)
        {
            return Parse<T> (data);
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

        public object Deserialize(string data, Type type)
        {
            return serializer.Deserialize (data, type);
        }
        #endregion
    }
}

