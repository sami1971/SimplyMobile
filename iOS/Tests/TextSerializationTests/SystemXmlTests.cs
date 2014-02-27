using System;
using SimplyMobile.Text.SystemXml;

#if WINDOWS_PHONE
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
#else
using TestFixture = NUnit.Framework.TestFixtureAttribute;
#endif

namespace TextSerializationTests
{
	[TestFixture ()]
	public class SystemXmlTests : TestBase
	{
		#region implemented abstract members of TestBase

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

	public class SystemXmlSanity : SanityCheckTests
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
}

