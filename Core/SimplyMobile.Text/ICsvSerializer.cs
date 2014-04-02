using System;
using System.IO;
using System.Collections.Generic;

namespace SimplyMobile.Text
{
    public interface ICsvSerializer
    {
        /// <summary>
        /// Gets the text format
        /// </summary>
        Format Format { get; }

        string Serialize<T> (IEnumerable<T> obj);

        T Deserialize<T> (Stream stream);
    }
}

