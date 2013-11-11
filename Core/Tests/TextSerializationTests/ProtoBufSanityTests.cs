using NUnit.Framework;
using System;
using SimplyMobile.Text;
using SimplyMobile.Text.ProtoBuffer;

namespace TextSerializationTests
{
	[TestFixture ()]
	public class ProtoBufSanityTests : SanityCheckTests
	{
		protected override ITextSerializer Serializer { get { return new ProtoBufferSerializer (); } }
	}
}

