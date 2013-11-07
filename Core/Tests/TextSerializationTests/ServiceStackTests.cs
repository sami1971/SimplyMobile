using NUnit.Framework;
using System;

using SimplyMobile.Text.ServiceStack;
using SimplyMobile.Text;

namespace TextSerializationTests
{
	[TestFixture ()]
	public class ServiceStackTests : TestBase
	{
		protected override  ITextSerializer Serializer { get { return new JsonSerializer (); } }
	}
}

