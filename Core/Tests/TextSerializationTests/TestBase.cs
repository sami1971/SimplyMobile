using NUnit.Framework;
using System;
using SimplyMobile.Text;

namespace TextSerializationTests
{
	[TestFixture ()]
	public abstract class TestBase
	{
		protected abstract ITextSerializer Serializer { get; }

		[Test ()]
		public void CanSerializeInterface ()
		{
			Assert.IsTrue(Test.CanSerialize(this.Serializer));
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
	}
}

