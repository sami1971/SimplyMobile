using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SimplyMobile.IoC.Munq;

using SimplyMobile.Text;
using SimplyMobile.Text.JsonNet;

namespace NinjectTests.iOS
{
    public class MunqInjectionTests : InjectionTests
    {
        protected override SimplyMobile.IoC.IDependencyResolver Resolver
        {
            get 
            {
                var resolver = new Resolver();
                //resolver.RegisterService<IJsonSerializer>(new JsonSerializer());
                return resolver; 
            }
        }
    }
}