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

            #if __ANDROID__
            var modernClient = new HttpClient(new OkHttpNetworkHandler());
            #else
            var modernClient = new HttpClient(new AFNetworkHandler());
            #endif
            resolver.RegisterService<HttpClient>(modernClient);

            var client = resolver.GetService<HttpClient>();

            Assert.AreSame(modernClient, client);
        }
#endif

    }
}