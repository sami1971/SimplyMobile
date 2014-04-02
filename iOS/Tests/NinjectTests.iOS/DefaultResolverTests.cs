using System;
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
    public class DefaultResolverTests : InjectionTests
    {
        #region implemented abstract members of InjectionTests
        protected override IDependencyResolver Resolver
        {
            get
            {
                return new DependencyResolver ();
            }
        }

        #endregion
    }
}

