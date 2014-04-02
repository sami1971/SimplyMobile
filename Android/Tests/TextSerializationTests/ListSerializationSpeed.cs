using System;
using NUnit.Framework;
using System.Collections.Generic;
using SimplyMobile.Text;

using DebugWrite = System.Console;

namespace TextSerializationTests
{
    [TestFixture ()]
    public abstract class ListSerializationSpeed
    {
        /// <summary>
        /// Gets the serializer to use.
        /// </summary>
        protected abstract ITextSerializer Serializer { get; }

        [Test()]
        public void SerializationSpeed()
        {
            string str;
            var serializer = this.Serializer;
            var count = 10;
            var elapsedMs = TestMethods.GetSerializationSpeed (count, serializer, this.GetList (), out str, serializer);
            DebugWrite.WriteLine("{0} took {1}ms serializing {2} iterations.",
                serializer,
                elapsedMs,
                count);
        }

        [Test()]
        public void DeserializationSpeed()
        {
            string str;
            var count = 10;
            List<Person> persons;
            var serializer = this.Serializer;
            var elapsedMs = TestMethods.GetSerializationSpeed (1, serializer, this.GetList (), out str, serializer);

            elapsedMs = TestMethods.GetDeserializationSpeed (count, serializer, str, out persons, serializer);

            DebugWrite.WriteLine("{0} took {1}ms deserializing {2} iterations.",
                serializer,
                elapsedMs,
                count);
        }

        private List<Person> GetList()
        {
            var list = new List<Person> ();

            for (var n = 0; n < 5000; n++)
            {
                list.Add (new Person () { Id = n, FirstName = "First_" + n, LastName = "Last_" + n });
            }

            return list;
        }
    }
}

