using System;
using System.Net;

namespace SimplyMobile.Web
{
	public class ServiceResponse<T>
	{
		public HttpStatusCode StatusCode { get; private set; }

		public T Value { get; private set; }

		public Exception Error { get; private set; }

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

