using System;
using SimplyMobile.IoC;

namespace NinjectTests
{
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

