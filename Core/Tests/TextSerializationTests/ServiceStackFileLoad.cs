using System;
using SimplyMobile.Text.ServiceStack;
using SimplyMobile.Text;
using NUnit.Framework;

namespace TextSerializationTests
{
	[TestFixture()]
	public class ServiceStackFileLoad : FileLoadTests
	{
		protected override ITextSerializer Deserializer { get { return new JsonSerializer(); } }
	}
}

