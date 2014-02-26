using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Net.Http;
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
                return this.client ?? (this.client = new HttpClient(new AFNetworkHandler()));
            }
        }
    }
}