using System;
using System.Collections.Generic;
using System.Text;
using SimplyMobile.Text.Jil;

namespace TextSerializationTests
{
    public class JilTests : TestBase
    {
        protected override SimplyMobile.Text.ITextSerializer Serializer
        {
            get { return new JsonSerializer(); }
        }

        protected override SimplyMobile.Text.ITextSerializer Deserializer
        {
            get { return new JsonSerializer(); }
        }
    }
}
