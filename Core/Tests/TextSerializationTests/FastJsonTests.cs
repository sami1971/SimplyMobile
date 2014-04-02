using System;
using SimplyMobile.Text;
using SimplyMobile.Text.FastJson;


#if WINDOWS_PHONE
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
#else
using NUnit.Framework;
#endif

namespace TextSerializationTests
{
    [TestFixture()]
    public class FastJsonTests : TestBase
    {
        protected override ITextSerializer Serializer { get { return new SimplyMobile.Text.ServiceStack.JsonSerializer(); } }

        protected override ITextSerializer Deserializer { get { return new JsonSerializer(new SimplyMobile.Text.ServiceStack.JsonSerializer()); } }
    }
}

