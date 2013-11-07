using System;
using SimplyMobile.Text.ProtoBuffer;
using SimplyMobile.Text;
using NUnit.Framework;

namespace TextSerializationTests
{
	[TestFixture ()]
	public class ProtoBufTests : TestBase
	{
		protected override ITextSerializer Serializer { get { return new ProtoBufferSerializer (); } }
	}
}

