using System;
using System.Net;

namespace SimplyMobile.Web
{
    /// <summary>
    /// The service response.
    /// </summary>
    /// <typeparam name="T">
    /// Type of DTO to expect from service
    /// </typeparam>
    public class ServiceResponse<T>
	{
		public HttpStatusCode StatusCode { get; private set; }

		public T Value { get; internal set; }

        public string Content { get; internal set; }

        public Exception Error { get; internal set; }

		public ServiceResponse (T value, HttpStatusCode statusCode)
		{
			this.Value = value;
			this.StatusCode = statusCode;
		}

		public ServiceResponse (HttpStatusCode statusCode, Exception error = null)
		{
			this.StatusCode = statusCode;
			this.Error = error;
		}
	}
}

