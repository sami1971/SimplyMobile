using System;
using System.ServiceModel;

namespace SimplyMobile.Plugins.WeatherWcfService
{
    public partial class WeatherForecastServiceClient
    {
        public static EndpointAddress DefaultEndpoint
        {
            get
            {
                return new EndpointAddress("http://www.restfulwebservices.net/wcf/WeatherForecastService.svc");
            }
        }

        public static BasicHttpBinding DefaultBindings
        {
            get
            {
                var timeout = TimeSpan.FromMilliseconds(5000);
                return new BasicHttpBinding()
                {
                    Name = "basicHttpBinding",
                    MaxReceivedMessageSize = 67108864,
                    ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas()
                    {
                        MaxArrayLength = 2147483646,
                        MaxStringContentLength = 5242880,
                    },
                    SendTimeout = timeout,
                    OpenTimeout = timeout,
                    ReceiveTimeout = timeout
                };
            }
        }

        public static WeatherForecastServiceClient Default
        {
            get { return new WeatherForecastServiceClient (DefaultBindings, DefaultEndpoint); }
        }
    }
}

