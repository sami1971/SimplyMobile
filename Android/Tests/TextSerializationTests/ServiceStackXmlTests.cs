using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyMobile.Text.ServiceStack;

namespace TextSerializationTests
{
    public class ServiceStackXmlTests : TestBase
    {
        protected override SimplyMobile.Text.ITextSerializer Serializer
        { 
            get { return new XmlSerializer(); }
        }

        protected override SimplyMobile.Text.ITextSerializer Deserializer
        {
            get { return new XmlSerializer(); }
        }
    }
}
