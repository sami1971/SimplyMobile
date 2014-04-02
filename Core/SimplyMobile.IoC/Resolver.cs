using System;
using System.Collections.Generic;

namespace SimplyMobile.IoC
{
    /// <summary>
    /// Resolver static class wraps DependencyResolver.Current services.
    /// </summary>
    public static class Resolver
    {
        /// <summary>
        /// Gets the first available service from current Dependency Resolver
        /// </summary>
        /// <typeparam name="T">Type of service to get</typeparam>
        /// <returns>First available service if there are any, otherwise null</returns>
        public static T GetService<T>() where T : class
        {
            return DependencyResolver.Current.GetService<T> ();
        }

        /// <summary>
        /// Gets all available services for the type from current Dependency Resolver
        /// </summary>
        /// <typeparam name="T">Type of service to get</typeparam>
        /// <returns>Enumerable list of available services</returns>
        public static IEnumerable<T> GetServices<T>() where T : class
        {
            return DependencyResolver.Current.GetServices<T> ();
        }
    }
}

