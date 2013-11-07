using NUnit.Framework;
using System;

using SimplyMobile.Text.JsonNet;
using SimplyMobile.Text;

namespace TextSerializationTests
{
	[TestFixture ()]
	public class JsonNetTests : TestBase
	{
		protected override ITextSerializer Serializer { get { return new JsonSerializer (); } }
	}
}

