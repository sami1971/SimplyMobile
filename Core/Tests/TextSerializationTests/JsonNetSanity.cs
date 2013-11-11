using System;
using SimplyMobile.Text;
using SimplyMobile.Text.JsonNet;
#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
#else
using NUnit.Framework;
#endif

namespace TextSerializationTests
{
    [TestFixture()]
	public class JsonNetSanity : SanityCheckTests
	{
		protected override ITextSerializer Serializer { get { return new JsonSerializer (); } }
	}
}

