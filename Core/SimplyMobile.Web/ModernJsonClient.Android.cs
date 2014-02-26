using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using ModernHttpClient;

namespace SimplyMobile.Web
{
    public class ModernJsonClient : JsonClient
    {
        private HttpClient client;

        protected override HttpClient HttpClient
        {
            get 
            { 
                return this.client ?? (this.client = new HttpClient(new OkHttpNetworkHandler()));
            }
        }
    }
}
