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
        public Format Format
        {
            get { return Format.Json; }
        }

        public string Serialize<T>(T obj)
        {
            using (var memoryStream = new MemoryStream())
            using (var reader = new StreamReader(memoryStream))
            {
                var serializer = new DataContractJsonSerializer(obj.GetType());
                serializer.WriteObject(memoryStream, obj);
                memoryStream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        public T Deserialize<T>(string data)
        {
            return (T)this.Deserialize(data, typeof(T));
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
