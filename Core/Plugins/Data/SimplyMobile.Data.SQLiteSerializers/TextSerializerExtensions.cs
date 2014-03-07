using SimplyMobile.Text;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Data
{
    public static class TextSerializerExtensions
    {
        public static IBlobSerializer AsBlobSerializer(this ITextSerializer serializer, Encoding encoding = null)
        {
            return new BlobSerializerDelegate(
                obj => serializer.SerializeToBytes(obj, encoding),
                (data, type) => serializer.DeserializeFromBytes(data, type, encoding),
                serializer.CanDeserialize);
        }

        private static bool CanDeserialize(this ITextSerializer serializer, Type type)
        {
            return true;
        }
    }
}
