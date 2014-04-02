using System;
using SimplyMobile.Text.FastJson;
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
    public class FastJsonFileLoad : FileLoadTests
    {
        #region implemented abstract members of FileLoadTests

        protected override ITextSerializer Deserializer { get { return new JsonSerializer(new SimplyMobile.Text.ServiceStack.JsonSerializer()); } }

        #endregion
    }
}

