using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if WINDOWS_PHONE
using TestFixtureAttribute = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using TestAttribute = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
using AssertClass = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.Assert;
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

namespace SimplyMobile.UnitTestCore
{
    public class TestFixture : TestFixtureAttribute{}

    public class Test : TestAttribute{}

    public class Assert : AssertClass { }

    public class SetUp : SetupAttribute { }

    public class Description : DescriptionAttribute 
    {
        public Description(string description) : base(description) { }
    }

    public class TestFixtureSetUp : TestFixtureSetUpAttribute { }

    public class TearDown : TearDownAttribute { }

    public class AssertionException : AssertionExceptionClass 
    {
        public AssertionException(string message) : base(message) { }
    }

    public class Is : IsClass { }
}
