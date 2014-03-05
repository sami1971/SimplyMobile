using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SimplyMobile.IoC.Munq;

using SimplyMobile.Text;

namespace NinjectTests.iOS
{
    public class MunqInjectionTests : InjectionTests
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