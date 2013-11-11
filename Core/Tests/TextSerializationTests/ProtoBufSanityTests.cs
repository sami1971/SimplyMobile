
using System;
using SimplyMobile.Text;
using SimplyMobile.Text.ProtoBuffer;

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
	public class ProtoBufSanityTests : SanityCheckTests
	{
		protected override ITextSerializer Serializer { get { return new ProtoBufferSerializer (); } }
	}
}

