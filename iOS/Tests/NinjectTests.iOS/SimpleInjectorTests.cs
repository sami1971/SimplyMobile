using System;
using SimplyMobile.IoC.SimpleInjector;

namespace NinjectTests
{
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

