using System;
using SimplyMobile.Text.JsonNet;
using SimplyMobile.Text;
using NUnit.Framework;

namespace TextSerializationTests
{
	[TestFixture()]
	public class JsonNetFileLoad : FileLoadTests
	{
		protected override ITextSerializer Deserializer { get { return new JsonSerializer(); } }
	}
}

