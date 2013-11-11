using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#if WINDOWS_PHONE
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
#else
    [TestFixture ()]
#endif

namespace TextSerializationTests
{

    [TestFixture ()]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
