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

		[Test ()]
		public void CanSerializeSimple ()
		{
			var person = new Person () 
			{
				Id = 0,
				FirstName = "First",
				LastName = "Last"
			};
			Assert.IsTrue(Test.CanSerialize<Person>(this.Serializer, person));
		}
		
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
		public void CanSerializePrimitive()
		{
			var p = Primitives.Create (10);
			Assert.IsTrue(Test.CanSerialize<Primitives>(this.Serializer, p));
		}

		[Test()]
		public void CanSerializeDates()
		{
			var p = DateTimeDto.Create (101);
			Assert.IsTrue(Test.CanSerialize<DateTimeDto>(this.Serializer, p));
		}

		[Test()]
		public void SerializationSpeedPrimitives()
		{
			string ret;
			var serializer = this.Serializer;
			var count = 1;
			var o = new List<Primitives> ();
			for (int n = int.MinValue; n < int.MaxValue; n++)
			{
				o.Add (Primitives.Create (n));
			}

			var elapsedMs = Test.GetSerializationSpeed (count, serializer, o, out ret);
			Console.WriteLine ("{0} took {1}ms serializing {2} iterations.",
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
			for (int n = int.MinValue; n < int.MaxValue; n++)
			{
				o.Add (DateTimeDto.Create (n));
			}

			var elapsedMs = Test.GetSerializationSpeed (count, serializer, o, out ret);
			Console.WriteLine ("{0} took {1}ms serializing {2} iterations.",
			                   serializer,
			                   elapsedMs,
			                   count);
		}
	}
}

