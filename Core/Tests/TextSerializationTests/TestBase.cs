using System;
using SimplyMobile.Text;
using System.Collections.Generic;

#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
using DebugWrite = System.Diagnostics.Debug;
#else
using NUnit.Framework;
using DebugWrite = System.Console;
#endif

namespace TextSerializationTests
{
    /// <summary>
    /// The test base to use with custom serializers.
    /// </summary>
    [TestFixture ()]
	public abstract class TestBase
	{
	    /// <summary>
	    /// Gets the serializer to use.
	    /// </summary>
	    protected abstract ITextSerializer Serializer { get; }

        /// <summary>
        /// Gets the deserializer to use.
        /// </summary>
        protected abstract ITextSerializer Deserializer { get; }

        /// <summary>
        /// The serialization speed.
        /// </summary>
        [Test()]
		public void SerializationSpeed()
		{
			var serializer = this.Serializer;
			var count = 100000;
			var elapsedMs = TestMethods.GetSerializationSpeed(count, serializer);
            DebugWrite.WriteLine("{0} took {1}ms serializing {2} iterations.",
			                   serializer,
			                   elapsedMs,
			                   count);
		}

        /// <summary>
        /// The deserialization speed.
        /// </summary>
        [Test()]
		public void DeserializationSpeed()
		{
			var serializer = this.Serializer;
			var count = 100000;
            var elapsedMs = TestMethods.GetDeserializationSpeed(count, serializer, serializer);
            DebugWrite.WriteLine(
                "{0} took {1}ms deserializing {2} iterations.",
			                   serializer,
			                   elapsedMs,
			                   count);
		}

		[Test()]
		public void SerializationSpeedPrimitives()
		{
			string ret;
			var serializer = this.Serializer;
			var count = 1;
			var o = new List<Primitives>();
			for (int n = 100; n < 50000; n++)
			{
				o.Add (Primitives.Create (n));
			}

			var elapsedMs = TestMethods.GetSerializationSpeed(count, serializer, o, out ret, serializer);
            DebugWrite.WriteLine(
                "{0} took {1}ms serializing {2} iterations.",
			                   serializer,
			                   elapsedMs,
			                   count);

            IEnumerable<Primitives> outValue;

            elapsedMs = TestMethods.GetDeserializationSpeed(count, serializer, ret, out outValue, serializer);
            DebugWrite.WriteLine(
                "{0} took {1}ms deserializing {2} iterations.",
			                   serializer,
			                   elapsedMs,
			                   count);
		}

		[Test()]
		public void SerializationSpeedDates()
		{
			string ret;
			var serializer = this.Serializer;
			var count = 1;
			var o = new List<DateTimeDto>();
			for (var n = 100; n < 20000; n++)
			{
				o.Add(DateTimeDto.Create(n));
			}

            var elapsedMs = TestMethods.GetSerializationSpeed(count, serializer, o, out ret, serializer);
            DebugWrite.WriteLine(
                "{0} took {1}ms serializing {2} iterations.",
			                   serializer,
			                   elapsedMs,
			                   count);

            IEnumerable<DateTimeDto> outValue;

            elapsedMs = TestMethods.GetDeserializationSpeed(count, serializer, ret, out outValue, serializer);
            DebugWrite.WriteLine(
                "{0} took {1}ms deserializing {2} iterations.",
			                   serializer,
			                   elapsedMs,
			                   count);
		}
	}
}

