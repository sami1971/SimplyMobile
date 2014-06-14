using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

namespace SimplyMobile.Text.RuntimeSerializer
{
    public class JsonSerializer : IJsonSerializer
    {
        private Lazy<List<Type>> types = new Lazy<List<Type>>();

        public Format Format
        {
            get { return Format.Json; }
        }

        public void AddKnownType<T>()
        {
            this.types.Value.Add(typeof(T));
        }

        public string Serialize<T>(T obj)
        {
            using (var memoryStream = new MemoryStream())
            using (var reader = new StreamReader(memoryStream))
            {
                var serializer = new DataContractJsonSerializer(obj.GetType(), this.types.Value);
                serializer.WriteObject(memoryStream, obj);
                memoryStream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Serializes object to a stream
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <param name="stream">Stream to serialize to</param>
        public void Serialize<T>(T obj, Stream stream)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            serializer.WriteObject(stream, obj);
        }

        public T Deserialize<T>(string data)
        {
            return (T)this.Deserialize(data, typeof(T));
        }

        /// <summary>
        /// Deserializes stream into an object
        /// </summary>
        /// <typeparam name="T">Type of object to serialize to</typeparam>
        /// <param name="stream">Stream to deserialize from</param>
        /// <returns>Object of type T</returns>
        public T Deserialize<T>(Stream stream) where T : class
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            return serializer.ReadObject(stream) as T;
        }

        public object Deserialize(string data, Type type)
        {
            using (var reader = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                var serializer = new DataContractJsonSerializer(type);
                return serializer.ReadObject(reader);
            }
        }
    }
}
