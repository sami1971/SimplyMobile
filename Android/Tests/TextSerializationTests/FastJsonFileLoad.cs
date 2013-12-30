using System;
using SimplyMobile.Text.FastJson;
using SimplyMobile.Text;
using NUnit.Framework;

namespace TextSerializationTests
{
	[TestFixture()]
	public class FastJsonFileLoad : FileLoadTests
	{
		#region implemented abstract members of FileLoadTests

		protected override ITextSerializer Deserializer { get { return new JsonSerializer(); } }

		#endregion


	}
}

