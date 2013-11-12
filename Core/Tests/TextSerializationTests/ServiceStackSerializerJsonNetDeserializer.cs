using System;
#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
#else
using NUnit.Framework;
#endif

namespace TextSerializationTests
{
    [TestFixture]
    public class ServiceStackSerializerJsonNetDeserializerSanity : SanityCheckTests
    {
        protected override SimplyMobile.Text.ITextSerializer Serializer
        {
            get { return new SimplyMobile.Text.ServiceStack.JsonSerializer(); }
        }

        protected override SimplyMobile.Text.ITextSerializer Deserializer
        {
            get { return new SimplyMobile.Text.JsonNet.JsonSerializer(); }
        }
    }

    [TestFixture]
    public class ServiceStackSerializerJsonNetDeserializerSpeed : TestBase
    {
        protected override SimplyMobile.Text.ITextSerializer Serializer
        {
            get { return new SimplyMobile.Text.ServiceStack.JsonSerializer(); }
        }

        protected override SimplyMobile.Text.ITextSerializer Deserializer
        {
            get { return new SimplyMobile.Text.JsonNet.JsonSerializer(); }
        }
    }
}