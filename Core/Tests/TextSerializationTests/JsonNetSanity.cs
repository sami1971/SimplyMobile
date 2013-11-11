using System;
using SimplyMobile.Text;
using SimplyMobile.Text.JsonNet;

namespace TextSerializationTests
{
	public class JsonNetSanity : SanityCheckTests
	{
		protected override ITextSerializer Serializer { get { return new JsonSerializer (); } }
	}
}

