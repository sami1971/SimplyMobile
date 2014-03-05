using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimplyMobile.IoC.Autofac;
using Autofac;
using SimplyMobile.Text;
using SimplyMobile.Text.ServiceStack;

namespace NinjectTests.iOS
{
    public class AutofacInjectionTests : InjectionTests
    {
        protected override SimplyMobile.IoC.IDependencyResolver Resolver
        {
            get 
            {
                var builder = new ContainerBuilder();
                builder.RegisterType<JsonSerializer>().As<IJsonSerializer>();
                builder.Register(t => new MyService(t.Resolve<IJsonSerializer>())).As<IMyService>();
                return new Resolver(builder.Build()); 
            }
        }
    }
}