using System;
using Jil;
using SimplyMobile.Text.Jil;
using NUnit.Framework;

namespace TextSerializationTests
{
    [TestFixture()]
    public class JilFileLoad : FileLoadTests
    {
        #region implemented abstract members of FileLoadTests

        protected override SimplyMobile.Text.ITextSerializer Deserializer
        {
            get { return new JsonSerializer(new Options(false, true)); }
        }

        #endregion


    }
}

