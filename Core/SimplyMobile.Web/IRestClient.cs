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
using System.Threading.Tasks;
using SimplyMobile.Text;
using System.Collections.Generic;

namespace SimplyMobile.Web
{
    /// <summary>
    /// The RestClient interface.
    /// </summary>
    public interface IRestClient
    {
        /// <summary>
        /// Gets or sets timeout
        /// </summary>
        TimeSpan Timeout { get; set; }

        /// <summary>
        /// Gets the base address.
        /// </summary>
        //Uri BaseAddress { get; }

        /// <summary>
        /// Add request header.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        void AddHeader(string key, string value);

        /// <summary>
        /// Remove request header.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        void RemoveHeader(string key);

        /// <summary>
        /// Async POST method.
        /// </summary>
        /// <returns>The async task.</returns>
        /// <param name="address">Address of the service.</param>
        /// <param name="dto">DTO to post.</param>
        /// <param name="format">Format of the request.</param>
        /// <typeparam name="T">The type of object to be returned.</typeparam>
        Task<ServiceResponse<T>> PostAsync<T>(string address, object dto, Format format);

        /// <summary>
        /// Async GET method
        /// </summary>
        /// <returns>The async task with .</returns>
        /// <param name="address">Address of the service.</param>
        /// <param name="format">Format of the request.</param>
        /// <typeparam name="T">The type of object to be returned.</typeparam>
        Task<ServiceResponse<T>> GetAsync<T>(string address, Format format);

        Task<ServiceResponse<T>> GetAsync<T> (string address, Dictionary<string, string> values, Format format);

        //void SetCustomSerializer<T>(ICustomSerializer<T> serializer);

        //bool RemoveCustomSerializer (Type type);
    }
}