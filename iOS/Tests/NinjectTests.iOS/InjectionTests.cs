using System;

#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
#else
using NUnit.Framework;
#endif

using SimplyMobile.Text;
using SimplyMobile.IoC;
using SimplyMobile.Text.ServiceStack;

namespace NinjectTests
{
    public interface IMyService
    {
        IJsonSerializer Serializer { get; }
    }

    public class MyService : IMyService
    {
        public IJsonSerializer Serializer
        {
            get;
            set;
        }

        public MyService(IJsonSerializer serializer)
        {
            this.Serializer = serializer;
        }

        public MyService()
        {

        }
    }

    [TestFixture]
    public abstract class InjectionTests
    {
        protected abstract IDependencyResolver Resolver { get; }

        [Test]
        public void CanAutoResolve()
        {
            //this.Resolver.RegisterService<IJsonSerializer>(new JsonSerializer());
            var myService = this.Resolver.GetService<IMyService>();
            Assert.IsNotNull(myService);
            Assert.IsNotNull(myService.Serializer);
        }

        [Test]
        public void CanResolveWithoutConstructorInjection()
        {
            this.Resolver.RegisterService<IJsonSerializer, JsonSerializer>();
            this.Resolver.RegisterService<IMyService, MyService>();

            this.CanAutoResolve();
        }

        [Test]
        public void CanResolve()
        {
            this.Resolver.RegisterService<IJsonSerializer, JsonSerializer>();
            this.Resolver.RegisterService<IMyService>(r => new MyService(r.GetService<IJsonSerializer>()));
        }
    }
}