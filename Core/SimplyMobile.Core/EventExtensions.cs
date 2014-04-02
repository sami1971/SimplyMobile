using System;

namespace SimplyMobile.Core
{
    /// <summary>
    /// Event extensions.
    /// </summary>
    public static class EventExtensions
    {
        /// <summary>
        /// Raise the specified event.
        /// </summary>
        /// <param name="handler">Event handler.</param>
        /// <param name="sender">Sender object.</param>
        /// <param name="value">Argument value.</param>
        /// <typeparam name="T">The value type.</typeparam>
        public static void Invoke<T>(this EventHandler<EventArgs<T>> handler, object sender, T value)
        {
            if (handler != null)
            {
                handler (sender, new EventArgs<T> (value));
            }
        }
    }
}

