using System;
using System.Collections.Generic;
using System.Text;
using SimplyMobile.Text.Jil;
using NUnit.Framework;

namespace TextSerializationTests
{

    public class JilSanity : SanityCheckTests
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
