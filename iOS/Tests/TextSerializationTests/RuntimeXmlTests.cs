using System;
#if WINDOWS_PHONE
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
#else
using TestFixture = NUnit.Framework.TestFixtureAttribute;
#endif

using SimplyMobile.Text.RuntimeSerializer;

namespace TextSerializationTests
{
    [TestFixture ()]
    public class RuntimeXmlTests : TestBase
    {
        #region implemented abstract members of TestBase

        protected override SimplyMobile.Text.ITextSerializer Serializer
        {
            get
            {
                return new XmlSerializer ();
            }
        }

        protected override SimplyMobile.Text.ITextSerializer Deserializer
        {
            get
            {
                return new XmlSerializer ();
            }
        }

        #endregion
    }

    [TestFixture ()]
    public class RuntimeJsonTests : TestBase
    {
        #region implemented abstract members of TestBase

        protected override SimplyMobile.Text.ITextSerializer Serializer
        {
            get
            {
                return new JsonSerializer ();
            }
        }

        protected override SimplyMobile.Text.ITextSerializer Deserializer
        {
            get
            {
                return new JsonSerializer ();
            }
        }

        #endregion
    }

    public class RuntimeXmlSanity : SanityCheckTests
    {
        #region implemented abstract members of SanityCheckTests
        protected override SimplyMobile.Text.ITextSerializer Serializer
        {
            get
            {
                return new XmlSerializer();
            }
        }

        protected override SimplyMobile.Text.ITextSerializer Deserializer
        {
            get
            {
                return new XmlSerializer();
            }
        }
        #endregion
    }

    public class RuntimeJsonSanity : SanityCheckTests
    {
        #region implemented abstract members of SanityCheckTests
        protected override SimplyMobile.Text.ITextSerializer Serializer
        {
            get
            {
                return new JsonSerializer();
            }
        }

        protected override SimplyMobile.Text.ITextSerializer Deserializer
        {
            get
            {
                return new JsonSerializer();
            }
        }
        #endregion
    }
}

