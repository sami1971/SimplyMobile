using System;

namespace SimplyMobile.Device
{
    public class DeviceConnectionException : Exception
    {
        public DeviceConnectionException (object sender, string message, Exception innerException)
            : base(message, innerException)
        {
            this.Sender = sender;
        }

        public object Sender { get; private set; }
    }
}

