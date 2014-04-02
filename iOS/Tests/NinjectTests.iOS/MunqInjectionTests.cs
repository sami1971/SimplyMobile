using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SimplyMobile.IoC.Munq;

using SimplyMobile.Text;

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