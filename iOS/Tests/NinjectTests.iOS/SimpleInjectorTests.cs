using System;
using SimplyMobile.IoC.SimpleInjector;

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
    public class SimpleInjectorTests : InjectionTests
    {
        #region implemented abstract members of InjectionTests
        protected override SimplyMobile.IoC.IDependencyResolver Resolver
        {
            get
            {
                return new Resolver ();
            }
        }
        #endregion
    }
}

