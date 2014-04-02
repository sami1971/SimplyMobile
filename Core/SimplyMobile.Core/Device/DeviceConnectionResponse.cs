using System;

namespace SimplyMobile.Device
{
    public class DeviceConnectionResponse
    {
        private DeviceConnectionResponse (bool success, DeviceConnectionException ex = null)
        {
            this.IsSuccess = success;
            this.Error = ex;
        }

        public static DeviceConnectionResponse Success { get { return new DeviceConnectionResponse(true); } }

        public static DeviceConnectionResponse Failure(DeviceConnectionException ex)
        {
            return new DeviceConnectionResponse(false, ex);
        }

        public bool IsSuccess { get; private set; }
        public DeviceConnectionException Error { get; private set; }
    }
}

