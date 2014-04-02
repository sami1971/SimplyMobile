using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Jil;

namespace SimplyMobile.Text.Jil
{
    public class JsonSerializer : IJsonSerializer
    {
        private static Options options = new Options (false, false, false, DateTimeFormat.ISO8601, true, false);

        public JsonSerializer()
        {
        }

        public JsonSerializer(Options options)
        {
            JsonSerializer.options = options;
        }
         
        public Format Format
        {
            get { return Format.Json; }
        }

        public string Serialize<T>(T obj)
        {
            return JSON.Serialize(obj, options);
        }

        public T Deserialize<T>(string data)
        {
            using (var reader = new StringReader(data))
            {
                return JSON.Deserialize<T>(reader, options);
            }
        }
    }
}
