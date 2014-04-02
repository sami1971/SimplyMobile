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
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace SimplyMobile.Web
{
    using Text;

    [Obsolete("Use JsonClient instead")]
    /// <summary>
    /// The rest client.
    /// </summary>
    public class RestClient : IRestClient
    {
        /// <summary>
        /// The client.
        /// </summary>
        private readonly HttpClient client;

        /// <summary>
        /// Custom serializers.
        /// </summary>
        private readonly Dictionary<Type, ITextSerializer> customSerializers;

        /// <summary>
        /// Serializers for different formats.
        /// </summary>
        private readonly Dictionary<Format, ITextSerializer> serializers;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class.
        /// </summary>
        public RestClient(Uri baseAddress, ITextSerializer defaultSerializer = null)
        {
            this.serializers = new Dictionary<Format, ITextSerializer>();
            this.customSerializers = new Dictionary<Type, ITextSerializer>();

            var handler = new HttpClientHandler();

            try
            {
                if (handler.SupportsAutomaticDecompression)
                {
                    handler.AutomaticDecompression = 
                        DecompressionMethods.GZip |
                        DecompressionMethods.Deflate;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine (ex.Message);
            }

            this.client = new HttpClient(handler)
                {
                BaseAddress = baseAddress
                };

            if (defaultSerializer != null) 
            {
                this.serializers.Add (defaultSerializer.Format, defaultSerializer);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class.
        /// </summary>
        public RestClient(Uri baseAddress, IEnumerable<ITextSerializer> defaultSerializers = null)
        {
            this.serializers = new Dictionary<Format, ITextSerializer>();
            this.customSerializers = new Dictionary<Type, ITextSerializer>();

            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip |
                                                 DecompressionMethods.Deflate;
            }

            this.client = new HttpClient(handler)
            {
                BaseAddress = baseAddress
            };

            if (defaultSerializers != null) 
            {
                foreach (var serializer in defaultSerializers) 
                {
                    this.SetSerializer (serializer);
                }
            }
        }

        /// <summary>
        /// Gets or sets timeout in milliseconds
        /// </summary>
        public TimeSpan Timeout
        {
            get
            {
                return this.client.Timeout;
            }

            set
            {
               this.client.Timeout = value;
            }
        }

        public void AddHeader(string key, string value)
        {
            this.client.DefaultRequestHeaders.Add(key, value);
        }

        public void RemoveHeader(string key)
        {
            this.client.DefaultRequestHeaders.Remove(key);
        }

        public void SetSerializer(ITextSerializer serializer)
        {
            this.serializers.Remove(serializer.Format);
            this.serializers.Add(serializer.Format, serializer);
        }

        /// <summary>
        /// The set custom serializer.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="serializer">
        /// The serializer.
        /// </param>
        public void SetCustomSerializer<T>(ICustomSerializer<T> serializer)
        {
            var type = typeof(T);

            this.customSerializers.Remove (type);

            this.customSerializers.Add (type, serializer);
        }

        /// <summary>
        /// The remove custom serializer.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool RemoveCustomSerializer(Type type)
        {
            return this.customSerializers.Remove (type);
        }

        /// <summary>
        /// The post async method.
        /// </summary>
        /// <param name="address">
        /// The address.
        /// </param>
        /// <param name="dto">
        /// The dto.
        /// </param>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public async Task<ServiceResponse<T>> PostAsync<T>(string address, object dto, Format format = Format.Json)
        {
            ITextSerializer serializer = null, postSerializer = null, responseSerializer = null;

            if ((!this.customSerializers.TryGetValue(typeof(T), out responseSerializer) || !this.customSerializers.TryGetValue(dto.GetType(), out postSerializer)) && 
                !this.serializers.TryGetValue(format, out serializer))
            {
                return new ServiceResponse<T>(
                    HttpStatusCode.NotAcceptable,
                    new Exception(string.Format("No serializers found for {0}", format)));
            }

            //// serialize DTO to string
            var content = ((ITextSerializer)postSerializer ?? serializer).Serialize(dto);

            //// post asyncronously
            try
            {
                var s = responseSerializer as ICustomSerializer<T> ?? serializer;
                var response = await this.client.PostAsync(
                    address,
                    new StringContent(content, Encoding.UTF8, GetTextFormat(format)));
                return await GetResponse<T>(response, s);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<T>(
                    HttpStatusCode.InternalServerError,
                    ex);
            }
        }

        /// <summary>
        /// Async GET method
        /// </summary>
        /// <returns>The async task with .</returns>
        /// <param name="address">Address.</param>
        /// <param name="format">Format.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public async Task<ServiceResponse<T>> GetAsync<T>(string address, Format format)
        {
            ITextSerializer serializer = null, responseSerializer = null;
            if (!this.customSerializers.TryGetValue(typeof(T), out responseSerializer) && !this.serializers.TryGetValue(format, out serializer))
            {
                return new ServiceResponse<T>(
                    HttpStatusCode.NotAcceptable,
                    new Exception(string.Format("No serializers found for {0}", format)));
            }

            try
            {
                var response = await this.client.GetAsync(address);
                return await GetResponse<T>(response, responseSerializer as ICustomSerializer<T> ?? serializer);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<T>(
                    HttpStatusCode.InternalServerError,
                    ex);
            }
        }

        public async Task<ServiceResponse<T>> GetAsync<T> (string address, Dictionary<string, string> values, Format format)
        {
            ITextSerializer serializer = null, responseSerializer = null;
            if (!this.customSerializers.TryGetValue(typeof(T), out responseSerializer) && !this.serializers.TryGetValue(format, out serializer))
            {
                return new ServiceResponse<T>(
                    HttpStatusCode.NotAcceptable,
                    new Exception(string.Format("No serializers found for {0}", format)));
            }

            try
            {
                var builder = new StringBuilder(address);
                builder.Append("?");

                foreach (var pair in values)
                {
                    builder.Append(string.Format("{0}={1}&amp;", pair.Key, pair.Value));
                }

                var response = await this.client.GetAsync(builder.ToString());
                return await GetResponse<T>(response, responseSerializer as ICustomSerializer<T> ?? serializer);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<T>(
                    HttpStatusCode.InternalServerError,
                    ex);
            }
        }

        private static string GetTextFormat(Format format)
        {
            switch (format)
            {
            case Format.Json:
                return "text/json";
            case Format.Xml:
                return "text/xml";
            default:
                throw new NotImplementedException ();
            }
        }

        private static async Task<ServiceResponse<T>> GetResponse<T>(HttpResponseMessage response, ITextSerializer serializer)
        {
            var returnResponse = new ServiceResponse<T>(response.StatusCode);

            if (!response.IsSuccessStatusCode)
            {
                return returnResponse;
            }

            try
            {
                //// get response string
                returnResponse.Content = await response.Content.ReadAsStringAsync();
                //// serialize the response to object
                returnResponse.Value = serializer.Deserialize<T>(returnResponse.Content);
            }
            catch (Exception ex)
            {
                returnResponse.Error = ex;
            }

            return returnResponse;
        }
    }
}

