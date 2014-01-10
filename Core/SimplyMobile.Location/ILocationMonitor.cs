using System;
using System.Threading.Tasks;

namespace SimplyMobile.Location
{
    /// <summary>
    /// The LocationMonitor interface.
    /// </summary>
    public interface ILocationMonitor
    {
        /// <summary>
        /// The location changed event.
        /// </summary>
        event EventHandler<Coordinates> LocationChanged;

        /// <summary>
        /// Gets or sets the desired accuracy in meters.
        /// </summary>
        int DesiredAccuracyInMeters { get; set; }

        /// <summary>
        /// Gets or sets the location change threshold in meters.
        /// </summary>
        int LocationChangeThreshold { get; set; }

        /// <summary>
        /// The get coordinates asynchronously.
        /// </summary>
        /// <param name="timeout">
        /// The timeout value.
        /// </param>
        /// <returns>
        /// Coordinates asyncronously
        /// </returns>
        Task<Coordinates> GetCoordinatesAsync(TimeSpan timeout);
    }
}