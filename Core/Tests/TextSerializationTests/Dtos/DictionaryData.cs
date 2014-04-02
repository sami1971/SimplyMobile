using System;
using System.Collections.Generic;

namespace TextSerializationTests
{
    public class DictionaryData
    {
        public IList<IDictionary<SomeKey, string>> Dictionaries { get; set; }
    }
}

