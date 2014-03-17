
using SimplyMobile.Core;
using System;

namespace SimplyMobile.Device
{
    public partial class Gyroscope : IGyroscope
    {
        public EventHandler<EventArgs<Exception>> OnError;

        public event EventHandler<EventArgs<GyroReading>> ReadingAvailable
        {
            add
            {
                if (readingAvailable == null)
                {
                    Start();
                }
                readingAvailable += value;
            }
            remove
            {
                readingAvailable -= value;
                if (readingAvailable == null)
                {
                    Stop();
                }
            }
        }
        partial void Start();
        partial void Stop();

        private event EventHandler<EventArgs<GyroReading>> readingAvailable;

        private void Try(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                if (this.OnError != null)
                {
                    this.OnError.Invoke<Exception>(action, ex);
                }
            }
        }
    }
}