// 
//  Copyright 2013, Sami M. Kallio
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplyMobile.Core
{
    /// <summary>
    /// Provides dependency services at runtime
    /// </summary>
    public class DependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// Static instance handle
        /// </summary>
        private static IDependencyResolver instance;

        /// <summary>
        /// List of services
        /// </summary>
        private readonly List<object> services;

        private readonly Dictionary<Type, Func<object>> registeredServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyResolver"/> class.
        /// </summary>
        public DependencyResolver()
        {
            this.services = new List<object>();
            this.registeredServices = new Dictionary<Type, Func<object>>();
        }

        /// <summary>
        /// Gets or sets the current dependency resolver
        /// </summary>
        /// <remarks>
        /// This was designed to allow us to use other dependency resolvers as well as the default
        /// </remarks>
        public static IDependencyResolver Current
        {
            get { return instance ?? (instance = new DependencyResolver()); }
            set { instance = value; }
        }

        /// <summary>
        /// Gets the first available service
        /// </summary>
        /// <typeparam name="T">Type of service to get</typeparam>
        /// <returns>First available service if there are any, otherwise null</returns>
        public T GetService<T>() where T : class
        {
            var service = this.services.OfType<T>().FirstOrDefault();

            if (service == null)
            {
                Func<object> getter;
                if (this.registeredServices.TryGetValue(typeof(T), out getter))
                {
                    service = getter.Invoke() as T;
                }
            }

            return service;
        }

        /// <summary>
        /// Gets all available services for the type
        /// </summary>
        /// <typeparam name="T">Type of service to get</typeparam>
        /// <returns>Enumerable list of available services</returns>
        public IEnumerable<T> GetServices<T>() where T : class
        {
            return this.services.OfType<T>();
        }

        /// <summary>
        /// Sets a service provider
        /// </summary>
        /// <typeparam name="T">Type of the service</typeparam>
        /// <param name="service">Service provider</param>
        public void SetService<T>(T service) where T : class
        {
            this.services.Add(service);
        }

        /// <summary>
        /// Adds a dynamic getter for the service.
        /// </summary>
        /// <returns>The dependency resolver object</returns>
        /// <param name="getter">Getter func for the service.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public IDependencyResolver AddDynamic<T>(Func<T> getter) where T : class
        {
            this.registeredServices.Add(typeof(T), getter);
            return this;
        }
    }
}