using System;
using System.Threading.Tasks;
using SimplyMobile.Text;

namespace SimplyMobile.Web
{
    /// <summary>
    /// The RestClient interface.
    /// </summary>
    public interface IRestClient
    {
        /// <summary>
        /// Gets or sets timeout
        /// </summary>
        TimeSpan Timeout { get; set; }

        /// <summary>
        /// Add request header.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        void AddHeader(string key, string value);

        /// <summary>
        /// Remove request header.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        void RemoveHeader(string key);

		/// <summary>
		/// Async POST method.
		/// </summary>
		/// <returns>The async task.</returns>
		/// <param name="address">Address of the service.</param>
		/// <param name="dto">DTO to post.</param>
        /// <param name="format">Format of the request.</param>
		/// <typeparam name="T">The type of object to be returned.</typeparam>
        Task<ServiceResponse<T>> PostAsync<T>(string address, object dto, Format format);

		/// <summary>
		/// Async GET method
		/// </summary>
		/// <returns>The async task with .</returns>
        /// <param name="address">Address of the service.</param>
		/// <param name="format">Format of the request.</param>
        /// <typeparam name="T">The type of object to be returned.</typeparam>
        Task<ServiceResponse<T>> GetAsync<T>(string address, Format format);
    }
}