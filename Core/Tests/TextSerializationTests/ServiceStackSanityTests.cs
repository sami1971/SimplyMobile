
using System;
using SimplyMobile.Text;
using SimplyMobile.Text.ServiceStack;

#if WINDOWS_PHONE
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
#else
using TestFixture = NUnit.Framework.TestFixtureAttribute;
#endif

namespace TextSerializationTests
{
	[TestFixture ()]
	public class ServiceStackSanityTests : SanityCheckTests
	{
        protected override ITextSerializer Serializer { get { return new JsonSerializer(); } }

        protected override ITextSerializer Deserializer { get { return new JsonSerializer(); } }
	}
}

