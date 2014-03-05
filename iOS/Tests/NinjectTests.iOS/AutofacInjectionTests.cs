using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SimplyMobile.IoC.Autofac;
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
                return new Resolver(); 
            }
        }
    }
}