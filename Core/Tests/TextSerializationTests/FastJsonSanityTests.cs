using System;
using SimplyMobile.Text;
using SimplyMobile.Text.FastJson;

namespace TextSerializationTests
{
    public class FastJsonSanityTests : SanityCheckTests
    {
        protected override ITextSerializer Serializer { get { return new SimplyMobile.Text.ServiceStack.JsonSerializer(); } }

        protected override ITextSerializer Deserializer { get { return this.Serializer; } }
    }
}

