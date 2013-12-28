using System;
using SimplyMobile.Text;
using SimplyMobile.Text.FastJson;

namespace TextSerializationTests
{
	public class FastJsonTests : TestBase
	{
		protected override ITextSerializer Serializer { get { return new JsonSerializer(); } }

		protected override ITextSerializer Deserializer { get { return new JsonSerializer(); } }
	}
}

