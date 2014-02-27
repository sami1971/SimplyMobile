using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;

namespace SimplyMobile.Text.RuntimeSerializer
{
    public class XmlSerializer : IXmlSerializer
    {
        public Format Format
        {
            get { return Format.Xml; }
        }

        public string Serialize<T>(T obj)
        {
            using (var memoryStream = new MemoryStream())
            using (var reader = new StreamReader(memoryStream))
            {
                var serializer = new DataContractSerializer(obj.GetType());
                serializer.WriteObject(memoryStream, obj);
                memoryStream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        public T Deserialize<T>(string data)
        {
            using (var reader = XmlReader.Create(new StringReader(data)))
            {
                var serializer = new DataContractSerializer(typeof(T));
                return (T)serializer.ReadObject(reader);
            }
        }
    }
}
