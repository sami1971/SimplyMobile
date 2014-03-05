using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using ModernHttpClient;
#endif

using SimplyMobile.IoC.TinyIoC;
using SimplyMobile.Text;
using SimplyMobile.Text.ServiceStack;
using System.Net.Http;

namespace NinjectTests
{
    [TestClass]
    public class IoCResolveTests
    {
        private static Resolver autoRegistered;

        private static Resolver AutoRegistered
        {
            get
            {
                return autoRegistered ?? (autoRegistered = new Resolver(true));
            }
        }

        [TestMethod]
        public void TinyIocBasic()
        {
            var resolver = new Resolver();

            resolver.Container.Register<IJsonSerializer>(new JsonSerializer());
            //resolver.AddDynamic<IJsonSerializer>(() => new JsonSerializer());
            var serializer = resolver.GetService<IJsonSerializer>();

            Assert.IsNotNull(serializer);
        }

        [TestMethod]
        public void TinyIocAutoRegister()
        {
            var resolver = AutoRegistered;

            var serializer = resolver.GetService<IJsonSerializer>();

            Assert.IsNotNull(serializer);
        }

#if !WINDOWS_PHONE
        [TestMethod]
        public void GetHttpClient()
        {
            var resolver = AutoRegistered;

            var modernClient = new HttpClient(new AFNetworkHandler());
            //resolver.AddDynamic<HttpClient>(() => modernClient);
            resolver.Container.Register<HttpClient>(modernClient);
            var client = resolver.GetService<HttpClient>();

            Assert.AreSame(modernClient, client);
        }
#endif
    }
}
