using SimplyMobile.Text;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Web
{
    public class JsonClient : IRestClient
    {
        private IJsonSerializer serializer;
        private HttpClient httpClient;

        protected virtual HttpClient HttpClient 
        {
            get
            {
                return this.httpClient ?? (this.httpClient = new HttpClient());
            }
        }

        public JsonClient(IJsonSerializer serializer)
        {
            this.serializer = serializer;
        }

        public JsonClient(HttpClient httpClient, IJsonSerializer serializer)
        {
            this.httpClient = httpClient;
            this.serializer = serializer;
        }

        /// <summary>
        /// Gets or sets timeout in milliseconds
        /// </summary>
        public TimeSpan Timeout
        {
            get
            {
                return this.HttpClient.Timeout;
            }

            set
            {
                this.HttpClient.Timeout = value;
            }
        }

        public void AddHeader(string key, string value)
        {
            this.HttpClient.DefaultRequestHeaders.Add(key, value);
        }

        public void RemoveHeader(string key)
        {
            this.HttpClient.DefaultRequestHeaders.Remove(key);
        }

        public async Task<ServiceResponse<T>> PostAsync<T>(string address, object dto, Format format = Format.Json)
        {
            //// post asyncronously
            try
            {
                //// serialize DTO to string
                var content = this.serializer.Serialize(dto);

                var response = await this.HttpClient.PostAsync(
                    address,
                    new StringContent(content, Encoding.UTF8, "text/json"));
                return await GetResponse<T>(response, this.serializer);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<T>(
                    HttpStatusCode.InternalServerError,
                    ex);
            }
        }

        public async Task<ServiceResponse<T>> GetAsync<T>(string address, Format format = Format.Json)
        {
            try
            {
                var response = await this.HttpClient.GetAsync(address);
                return await GetResponse<T>(response, this.serializer);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<T>(
                    HttpStatusCode.InternalServerError,
                    ex);
            }
        }

        public async Task<ServiceResponse<T>> GetAsync<T>(string address, Dictionary<string, string> values, Format format = Format.Json)
        {
            try
            {
                var builder = new StringBuilder(address);
                builder.Append("?");

                foreach (var pair in values)
                {
                    builder.Append(string.Format("{0}={1}&amp;", pair.Key, pair.Value));
                }

                var response = await this.HttpClient.GetAsync(builder.ToString());
                return await GetResponse<T>(response, this.serializer);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<T>(
                    HttpStatusCode.InternalServerError,
                    ex);
            }
        }

        public void SetCustomSerializer<T>(ICustomSerializer<T> serializer)
        {
            throw new NotImplementedException();
        }

        public bool RemoveCustomSerializer(Type type)
        {
            throw new NotImplementedException();
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

