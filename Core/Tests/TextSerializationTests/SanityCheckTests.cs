using System;
using SimplyMobile.Text;

#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
#else
using NUnit.Framework;
#endif

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
			Assert.IsTrue(TestMethods.CanSerialize<Primitives>(this.Serializer, p));
		}

        [Test()]
        public void CanSerializeDateTime()
        {
            var p = DateTime.Now;
            Assert.IsTrue(TestMethods.CanSerialize<DateTime>(this.Serializer, p));
        }

        [Test()]
        public void CanSerializeDateTimeOffset()
        {
            var p = new DateTimeOffset(DateTime.Now);
            Assert.IsTrue(TestMethods.CanSerialize<DateTimeOffset>(this.Serializer, p));
        }

		[Test()]
		public void CanSerializeDates()
		{
			var p = DateTimeDto.Create (101);
			Assert.IsTrue(TestMethods.CanSerialize<DateTimeDto>(this.Serializer, p));
		}

		[Test()]
		public void CanSerializeSimple ()
		{
			var person = new Person () 
			{
				Id = 0,
				FirstName = "First",
				LastName = "Last"
			};
			Assert.IsTrue(TestMethods.CanSerialize<Person>(this.Serializer, person));
		}
	}
}

