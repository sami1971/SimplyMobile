using NUnit.Framework;
using System;
using SimplyMobile.Text;
using System.Collections.Generic;

namespace TextSerializationTests
{
	[TestFixture ()]
	public abstract class TestBase
	{
		protected abstract ITextSerializer Serializer { get; }

		[Test()]
		public void SerializationSpeed()
		{
			var serializer = this.Serializer;
			var count = 10000;
			var elapsedMs = Test.GetSerializationSpeed (count, serializer);
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
			var elapsedMs = Test.GetDeserializationSpeed (count, serializer);
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
			for (int n = 100; n < 100000; n++)
			{
				o.Add (Primitives.Create (n));
			}

			var elapsedMs = Test.GetSerializationSpeed (count, serializer, o, out ret);
			Console.WriteLine ("{0} took {1}ms serializing {2} iterations.",
			                   serializer,
			                   elapsedMs,
			                   count);

			List<Primitives> outValue;

			elapsedMs = Test.GetDeserializationSpeed<List<Primitives>> (count, serializer, ret, out outValue);
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
			var o = new List<DateTimeDto> ();
			for (int n = 100; n < 10000; n++)
			{
				o.Add (DateTimeDto.Create (n));
			}

			var elapsedMs = Test.GetSerializationSpeed (count, serializer, o, out ret);
			Console.WriteLine ("{0} took {1}ms serializing {2} iterations.",
			                   serializer,
			                   elapsedMs,
			                   count);

			List<DateTimeDto> outValue;

			elapsedMs = Test.GetDeserializationSpeed<List<DateTimeDto>> (count, serializer, ret, out outValue);
			Console.WriteLine ("{0} took {1}ms deserializing {2} iterations.",
			                   serializer,
			                   elapsedMs,
			                   count);
		}
	}
}

