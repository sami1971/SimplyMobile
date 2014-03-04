using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SimplyMobile.IoC.TinyIoC;
using SimplyMobile.Text;
using SimplyMobile.Text.ServiceStack;

namespace NinjectTests
{
    [TestClass]
    public class IoCResolveTests
    {
        [TestMethod]
        public void TinyIocBasic()
        {
            var resolver = new Resolver();

            resolver.Container.Register<IJsonSerializer>(new JsonSerializer());
            //resolver.AddDynamic<IJsonSerializer>(() => new JsonSerializer());
            var serializer = resolver.GetService<IJsonSerializer>();

            Assert.IsNotNull(serializer);
        }
    }
}
