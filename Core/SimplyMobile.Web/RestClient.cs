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
        public RestClient()
		{
			this.serializers = new Dictionary<Format, ITextSerializer>();
			this.customSerializers = new Dictionary<Type, ITextSerializer>();
			this.client = new HttpClient();
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

        /// <summary>
        /// Gets or sets the base address.
        /// </summary>
        public Uri BaseAddress
        {
            get { return this.client.BaseAddress; }
            set { this.client.BaseAddress = value; }
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
        public void SetCustomSerializer(Type type, ITextSerializer serializer)
		{
////			if (this.serializers.ContainsKey (type))
////			{
				this.customSerializers.Remove (type);
////			}

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
			ITextSerializer serializer;
			if (!this.serializers.TryGetValue(format, out serializer))
			{
			    return new ServiceResponse<T>(
			        HttpStatusCode.NotAcceptable,
			        new Exception(string.Format("No serializers found for {0}", format)));
			}

            //// serialize DTO to string
			var content = serializer.Serialize(dto);
            //// post asyncronously
			var response = await this.client.PostAsync(
				address, 
				new StringContent(content, UTF8Encoding.UTF8, GetTextFormat(format)));

            return await this.GetResponse<T>(response, serializer);
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
            ITextSerializer serializer;
            if (this.serializers.TryGetValue(format, out serializer) == false)
            {
                return new ServiceResponse<T>(
                    HttpStatusCode.NotAcceptable,
                    new Exception(string.Format("No serializers found for {0}", format)));
            }
            
            var response = await this.client.GetAsync(address);

            return await this.GetResponse<T>(response, serializer);
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

        private async Task<ServiceResponse<T>> GetResponse<T>(HttpResponseMessage response, ITextSerializer serializer)
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

