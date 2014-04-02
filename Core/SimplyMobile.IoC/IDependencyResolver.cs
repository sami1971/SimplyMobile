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

namespace SimplyMobile.IoC
{
    /// <summary>
    /// Dependency resolver interface
    /// </summary>
    public interface IDependencyResolver
    {
        /// <summary>
        /// Gets the first available service
        /// </summary>
        /// <typeparam name="T">Type of service to get</typeparam>
        /// <returns>First available service if there are any, otherwise null</returns>
        T GetService<T>() where T : class;

        /// <summary>
        /// Gets all available services for the type
        /// </summary>
        /// <typeparam name="T">Type of service to get</typeparam>
        /// <returns>Enumerable list of available services</returns>
        IEnumerable<T> GetServices<T>() where T : class;

        /// <summary>
        /// Registers an instance
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <param name="service">Instance of the service</param>
        /// <returns>Returns the resolver instance for chaining</returns>
        IDependencyResolver RegisterService<T>(T service) where T : class;

        /// <summary>
        /// Registers a runtime type for the service
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <typeparam name="TImpl">Runtime type to initialize</typeparam>
        /// <returns>Returns the resolver instance for chaining</returns>
        IDependencyResolver RegisterService<T, TImpl>()
            where T : class
            where TImpl : class, T;

        /// <summary>
        /// Registers a Func returning an instance of the service
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <param name="func">Func which returns a service instance</param>
        /// <returns>Returns the resolver instance for chaining</returns>
        IDependencyResolver RegisterService<T>(Func<IDependencyResolver, T> func) where T : class;

        /// <summary>
        /// Adds a dynamic getter for the service.
        /// </summary>
        /// <returns>The dependency resolver object</returns>
        /// <param name="getter">Getter func for the service.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        //IDependencyResolver AddDynamic<T>(Func<T> getter) where T : class;
    }
}