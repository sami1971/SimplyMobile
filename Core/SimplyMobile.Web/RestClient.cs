using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace SimplyMobile.Web
{
	using Text;

	public class RestClient
	{
		private readonly HttpClient client;
		private readonly Dictionary<Type, ITextSerializer> customSerializers;
		private readonly Dictionary<Format, ITextSerializer> serializers;

		public RestClient ()
		{
			this.serializers = new Dictionary<Format, ITextSerializer> ();
			this.customSerializers = new Dictionary<Type, ITextSerializer> ();
			this.client = new HttpClient ();
		}

		public void SetCustomSerializer(Type type, ITextSerializer serializer)
		{
//			if (this.serializers.ContainsKey (type))
//			{
				this.customSerializers.Remove (type);
//			}

			this.customSerializers.Add (type, serializer);
		}

		public bool RemoveCustomSerializer(Type type)
		{
			return this.customSerializers.Remove (type);
		}

		public async Task<ServiceResponse<T>> PostAsync<T>(string address, object dto, Format format = Format.Json)
		{
			ITextSerializer serializer;
			if (this.serializers.TryGetValue (format, out serializer) == false)
			{
				throw new Exception ("Replace this with status message");
			}

			// serialize DTO to string
			var content = serializer.Serialize (dto);
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
						serializer.Deserialize<T> (responseString),
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

