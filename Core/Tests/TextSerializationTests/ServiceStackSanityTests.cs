//
//  Copyright 2013, Sami M. Kallio
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//

using SimplyMobile.Text;
using SimplyMobile.Text.ServiceStack;

#if WINDOWS_PHONE
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
#else
using TestFixture = NUnit.Framework.TestFixtureAttribute;
#endif

namespace TextSerializationTests
{
    [TestFixture ()]
    public class ServiceStackSanityTests : SanityCheckTests
    {
        protected override ITextSerializer Serializer { get { return new JsonSerializer(); } }

        protected override ITextSerializer Deserializer { get { return new JsonSerializer(); } }
    }

    [TestFixture ()]
    public class ServiceStackXmlSanityTests : SanityCheckTests
    {
        protected override ITextSerializer Serializer { get { return new XmlSerializer(); } }

        protected override ITextSerializer Deserializer { get { return new XmlSerializer(); } }
    }
}

