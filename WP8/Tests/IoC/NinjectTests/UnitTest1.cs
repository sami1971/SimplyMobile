using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SimplyMobile.IoC.Ninject;
using Ninject;
using SimplyMobile.IoC;
using SimplyMobile.Text;

namespace NinjectTests
{
    [TestClass]
    public class UnitTest1
    {
        private IDependencyResolver Resolver { get { return new SimplyMobile.IoC.Ninject.Resolver(new StandardKernel()); } }

        [TestMethod]
        public void TestMethod1()
        {
            DependencyResolver.Current = Resolver;

            var newKernel = DependencyResolver.Current.GetService<IKernel>();

            Assert.IsNotNull(newKernel);

        }

        [TestMethod]
        public void GetJsonServices()
        {
            var jsonServices = Resolver.GetServices<IJsonSerializer>();

            foreach (var serializer in jsonServices)
            {
                Assert.IsTrue(serializer.Format == Format.Json);
            }
        }
    }
}
