using System;
using NUnit.Framework;
using SimplyMobile.Text;

namespace TextSerializationTests
{
	[TestFixture ()]
	public abstract class SanityCheckTests
	{
		protected abstract ITextSerializer Serializer { get; }

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
	}
}

