using NUnit.Framework;
using System;
using SimplyMobile.Text;
using SimplyMobile.Text.ServiceStack;

namespace TextSerializationTests
{
	[TestFixture ()]
	public class ServiceStackSanityTests : SanityCheckTests
	{
		protected override  ITextSerializer Serializer { get { return new JsonSerializer (); } }
	}
}

