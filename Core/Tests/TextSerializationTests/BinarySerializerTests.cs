using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestFixtureAttribute = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using TestAttribute = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
using AssertClass = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.Assert;
using SetupAttribute = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestInitializeAttribute;
using TearDownAttribute = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestCleanupAttribute;
using AssertionExceptionClass = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.UnitTestAssertException;
#else
using TestFixtureAttribute = NUnit.Framework.TestFixtureAttribute;
using TestAttribute = NUnit.Framework.TestAttribute;
using AssertClass = NUnit.Framework.Assert;
using SetupAttribute = NUnit.Framework.SetUpAttribute;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;
using TestFixtureSetUpAttribute = NUnit.Framework.TestFixtureSetUpAttribute;
using TearDownAttribute = NUnit.Framework.TearDownAttribute;
using AssertionExceptionClass = NUnit.Framework.AssertionException;
using IsClass = NUnit.Framework.Is;
#endif

using SimplyMobile.Text;

namespace TextSerializationTests
{
    [TestFixture]
    public class BinarySerializerTests
    {
        [Test]
        public void TestTypes()
        {

        }
    }
}
