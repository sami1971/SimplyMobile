using System;
using System.Linq;

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
            var resolver = this.Resolver;
            resolver.RegisterService<IJsonSerializer, JsonSerializer>().RegisterService<IMyService, MyService>();
            var myService = resolver.GetService<IMyService>();
            Assert.IsNotNull(myService);
            Assert.IsNotNull(myService.Serializer);
        }

        [Test]
        public void CanResolve()
        {
            var resolver = this.Resolver;
            resolver.RegisterService<IJsonSerializer, JsonSerializer>();
            resolver.RegisterService<IMyService>(r => new MyService(r.GetService<IJsonSerializer>()));
            var myService = resolver.GetService<IMyService>();
            Assert.IsNotNull(myService);
            Assert.IsNotNull(myService.Serializer);
        }

        [Test]
        public void CanResolveMultipleFromInstance()
        {
            var json = new JsonSerializer();
            var xml = new XmlSerializer();
            var resolver = this.Resolver;

            resolver.RegisterService<ITextSerializer>(json)
                .RegisterService<ITextSerializer>(xml);

            var count = resolver.GetServices<ITextSerializer>().Count();

            Assert.AreEqual(2, count, "Wrong count of serializers");

            var jsonSerializer = resolver.GetService<ITextSerializer>();

            Assert.IsTrue(jsonSerializer.GetType().IsAssignableFrom(typeof(JsonSerializer)), "First type is incorrect");

            var serializers = resolver.GetServices<ITextSerializer>().ToList();

            Assert.IsTrue(serializers.ElementAt(0).GetType().IsAssignableFrom(typeof(JsonSerializer)), "First serializer is incorrect");
            Assert.IsTrue(serializers.ElementAt(1).GetType().IsAssignableFrom(typeof(XmlSerializer)), "First serializer is incorrect"); 
        }

        [Test]
        public void CanResolveMultipleFromType()
        {
            var resolver = this.Resolver.RegisterService<ITextSerializer, JsonSerializer>()
                .RegisterService<ITextSerializer, XmlSerializer>();

            var count = resolver.GetServices<ITextSerializer>().Count();

            Assert.AreEqual(2, count, "Wrong count of serializers");

            var jsonSerializer = resolver.GetService<ITextSerializer>();

            Assert.IsTrue(jsonSerializer.GetType().IsAssignableFrom(typeof(JsonSerializer)), "First type is incorrect");

            var serializers = resolver.GetServices<ITextSerializer>().ToList();

            Assert.IsTrue(serializers[0].GetType().IsAssignableFrom(typeof(JsonSerializer)), "First serializer is incorrect");
            Assert.IsTrue(serializers[1].GetType().IsAssignableFrom(typeof(XmlSerializer)), "Second serializer is incorrect"); 
        }

        [Test]
        public void ResolvesByCorrectTypeFromInstance()
        {
            var resolver = this.Resolver;
            var json = new JsonSerializer();
            var xml = new XmlSerializer();

            resolver.RegisterService<IJsonSerializer> (json);
            resolver.RegisterService<ITextSerializer> (xml);

            var textSerializer = resolver.GetService<ITextSerializer> ();

            Assert.AreSame (xml, textSerializer);
        }

        [Test]
        public void ResolvesByCorrectTypeFromType()
        {
            var resolver = this.Resolver;

            resolver.RegisterService<IJsonSerializer, JsonSerializer> ();
            resolver.RegisterService<ITextSerializer, XmlSerializer> ();

            var textSerializer = resolver.GetService<ITextSerializer> ();

            Assert.IsTrue(textSerializer.GetType().IsAssignableFrom(typeof(XmlSerializer)), "Serializer is incorrect");
        }
    }
}