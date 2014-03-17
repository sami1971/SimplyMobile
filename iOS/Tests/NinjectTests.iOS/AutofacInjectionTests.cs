using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SimplyMobile.IoC.Autofac;
using SimplyMobile.Text;
using SimplyMobile.Text.ServiceStack;
#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
#else
using NUnit.Framework;
#endif

namespace NinjectTests
{
    [TestFixture]
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