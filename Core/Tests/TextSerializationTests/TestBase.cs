using System;
using SimplyMobile.Text;
using System.Collections.Generic;

#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
#else
using NUnit.Framework;
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

		[Test()]
		public void SerializationSpeed()
		{
			var serializer = this.Serializer;
			var count = 10000;
			var elapsedMs = TestMethods.GetSerializationSpeed (count, serializer);
			Console.WriteLine ("{0} took {1}ms serializing {2} iterations.",
			                   serializer,
			                   elapsedMs,
			                   count);
		}

		[Test()]
		public void DeserializationSpeed()
		{
			var serializer = this.Serializer;
			var count = 10000;
			var elapsedMs = TestMethods.GetDeserializationSpeed (count, serializer);
			Console.WriteLine ("{0} took {1}ms deserializing {2} iterations.",
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
			var o = new List<Primitives> ();
			for (int n = 100; n < 1000; n++)
			{
				o.Add (Primitives.Create (n));
			}

			var elapsedMs = TestMethods.GetSerializationSpeed (count, serializer, o, out ret);
			Console.WriteLine ("{0} took {1}ms serializing {2} iterations.",
			                   serializer,
			                   elapsedMs,
			                   count);

			List<Primitives> outValue;

			elapsedMs = TestMethods.GetDeserializationSpeed<List<Primitives>> (count, serializer, ret, out outValue);
			Console.WriteLine ("{0} took {1}ms deserializing {2} iterations.",
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
			for (var n = 100; n < 1000; n++)
			{
				o.Add(DateTimeDto.Create(n));
			}

			var elapsedMs = TestMethods.GetSerializationSpeed (count, serializer, o, out ret);
			Console.WriteLine ("{0} took {1}ms serializing {2} iterations.",
			                   serializer,
			                   elapsedMs,
			                   count);

			List<DateTimeDto> outValue;

			elapsedMs = TestMethods.GetDeserializationSpeed<List<DateTimeDto>> (count, serializer, ret, out outValue);
			Console.WriteLine ("{0} took {1}ms deserializing {2} iterations.",
			                   serializer,
			                   elapsedMs,
			                   count);
		}
	}
}

