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

namespace SimplyMobile.IoC
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
        private readonly Dictionary<Type, List<object>> services;

        private readonly Dictionary<Type, List<Func<IDependencyResolver, object>>> registeredServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyResolver"/> class.
        /// </summary>
        public DependencyResolver()
        {
            this.services = new Dictionary<Type, List<object>>();
            this.registeredServices = new Dictionary<Type, List<Func<IDependencyResolver, object>>>();
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
            set 
            {
                if (instance != null && value != null)
                {
                    throw new InvalidOperationException("Resolver has already been set!");
                }
                instance = value; 
            }
        }

        /// <summary>
        /// Gets the first available service
        /// </summary>
        /// <typeparam name="T">Type of service to get</typeparam>
        /// <returns>First available service if there are any, otherwise null</returns>
        public T GetService<T>() where T : class
        {
            return this.GetServices<T>().FirstOrDefault();
            //var service = this.services.OfType<T>().FirstOrDefault();

            //if (service == null)
            //{
            //    List<Func<IDependencyResolver, object>> getter;
            //    if (this.registeredServices.TryGetValue(typeof(T), out getter))
            //    {
            //        service = getter.First()(this) as T;
            //    }
            //}

            //return service;
        }

        /// <summary>
        /// Gets all available services for the type
        /// </summary>
        /// <typeparam name="T">Type of service to get</typeparam>
        /// <returns>Enumerable list of available services</returns>
        public IEnumerable<T> GetServices<T>() where T : class
        {
            List<object> list;
            if (this.services.TryGetValue(typeof(T), out list))
            {
                foreach (var service in list.OfType<T>())
                {
                    yield return service as T;
                }
            }

            //var services = this.services.OfType<T>();
            
            List<Func<IDependencyResolver, object>> getter;
            if (this.registeredServices.TryGetValue(typeof(T), out getter))
            {
                foreach (var serviceFunc in getter)
                {
                    yield return serviceFunc(this) as T;
                }
                //return services.Union(getter.Select(a => a(this) as T));
            }

            //return services;
        }

        /// <summary>
        /// Registers a service provider
        /// </summary>
        /// <typeparam name="T">Type of the service</typeparam>
        /// <param name="service">Service provider</param>
        public IDependencyResolver RegisterService<T>(T service) where T : class
        {
            var type = typeof(T);
            List<object> list;

            if (!this.services.TryGetValue (type, out list))
            {
                list = new List<object> ();
                this.services.Add (type, list);
            }

            list.Add (service);
//            this.services.Add(service);
            return this;
        }


        public IDependencyResolver RegisterService<T, TImpl>()
            where T : class
            where TImpl : class, T
        {
            return RegisterService<T>(t => Activator.CreateInstance(typeof(TImpl)) as T);
        }

        public IDependencyResolver RegisterService<T>(Func<IDependencyResolver, T> func) where T : class
        {
            var type = typeof(T);
            List<Func<IDependencyResolver, object>> list;
            if (!this.registeredServices.TryGetValue(type, out list))
            {
                list = new List<Func<IDependencyResolver, object>>();
                this.registeredServices.Add(type, list);
            }

            list.Add(func);
            //this.registeredServices.Add (typeof(T), func);
            return this;
        }
    }
}