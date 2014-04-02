using System;

namespace SimplyMobile.Device
{
    using Core;

    public static partial class Accelometer
    {
        public static event EventHandler<EventArgs<AccelometerStatus>> ReadingAvailable
        {
            add
            {
                if (readingAvailable == null)
                {
                    StartMonitoring();
                }
                readingAvailable += value;
            }
            remove 
            { 
                readingAvailable -= value;
                if (readingAvailable == null)
                {
                    StopMonitoring();
                }
            }
        }

        public static AccelometerStatus LatestStatus { get; private set; }

        static partial void StartMonitoring();
        static partial void StopMonitoring();

        private static event EventHandler<EventArgs<AccelometerStatus>> readingAvailable;
    }
}

