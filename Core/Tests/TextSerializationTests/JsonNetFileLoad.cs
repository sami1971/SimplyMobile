using System;
using SimplyMobile.Text.JsonNet;
using SimplyMobile.Text;
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
    public class JsonNetFileLoad : FileLoadTests
    {
        protected override ITextSerializer Deserializer { get { return new JsonSerializer(); } }
    }
}

