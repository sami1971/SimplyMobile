using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplyMobile.IoC;

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
    public class TinyIoCInjectionTests : InjectionTests
    {
        private IDependencyResolver resolver;

        protected override IDependencyResolver Resolver
        {
            get
            {
                return resolver ?? (resolver = new SimplyMobile.IoC.TinyIoC.Resolver());
            }
        }
    }
}