using System;

using Serializer = ServiceStack.Text.CsvSerializer;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace SimplyMobile.Text.ServiceStack
{
    public class CsvSerializer : ICsvSerializer
    {
        #region ITextSerializer implementation

        public string Serialize<T> (IEnumerable<T> obj)
        {
            return Serializer.SerializeToCsv<T>(obj);
        }

        public T Deserialize<T> (Stream stream)
        {
            return Serializer.DeserializeFromStream<T>(stream);
        }

        public Format Format 
        {
            get 
            {
                return Format.Csv;
            }
        }
        #endregion
    }
}

