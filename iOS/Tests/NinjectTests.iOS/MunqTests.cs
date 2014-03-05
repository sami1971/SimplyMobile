using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using ModernHttpClient;
#endif

using SimplyMobile.IoC.Munq;
using System.Net.Http;

namespace NinjectTests.iOS
{
    [TestClass]
    public class MunqTests
    {

#if !WINDOWS_PHONE
        [TestMethod]
        public void HttpClientTest()
        {
            var resolver = new Resolver();

            var modernClient = new HttpClient(new AFNetworkHandler());

            resolver.RegisterService<HttpClient>(modernClient);

            var client = resolver.GetService<HttpClient>();

            Assert.AreSame(modernClient, client);
        }
#endif

    }
}