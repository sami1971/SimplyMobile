using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace SimplyMobile.Web
{
    using Text;

    public class JsonHttpClient : IDisposable
    {
        private readonly HttpClient client;

        public IJsonSerializer Serializer { get; set; }

        public JsonHttpClient(IJsonSerializer serializer)
        {
            this.Serializer = serializer;
            this.client = new HttpClient ();
        }

        #region IDisposable implementation

        public void Dispose ()
        {
            this.client.Dispose ();
            this.Serializer = null;
        }

        #endregion

        public async Task<ServiceResponse<T>> PostAsync<T>(string address, object dto)
        {
            // serialize DTO to string
            var content = this.Serializer.Serialize (dto);
            // post asyncronously
            var response = await client.PostAsync (
                address, 
                new StringContent (content));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    // get response string
                    var responseString = await response.Content.ReadAsStringAsync ();
                    // return deserializer object
                    return new ServiceResponse<T>(
                        this.Serializer.Deserialize<T> (responseString),
                        response.StatusCode);
                }
                catch (Exception ex)
                {
                    return new ServiceResponse<T> (response.StatusCode, ex);
                }
            } 
            else
            {
                return new ServiceResponse<T> (response.StatusCode);
            }
        }
    }
}
