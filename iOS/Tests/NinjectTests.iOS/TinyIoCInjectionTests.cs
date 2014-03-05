using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SimplyMobile.IoC.TinyIoC;

namespace NinjectTests
{
    public class TinyIoCInjectionTests : InjectionTests
    {
        protected override SimplyMobile.IoC.IDependencyResolver Resolver
        {
            get { return new Resolver(true); }
        }
    }
}