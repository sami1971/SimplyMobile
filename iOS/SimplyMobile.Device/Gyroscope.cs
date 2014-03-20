using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreMotion;

namespace SimplyMobile.Device
{
    using Core;

    public partial class Gyroscope
    {
        private readonly CMMotionManager manager;
        
        public Gyroscope(double updateInterval)
        {
            this.manager = new CMMotionManager()
            {
                GyroUpdateInterval = updateInterval
            };
        }

        public bool IsSupported
        {
            get
            {
                return this.manager.GyroAvailable;
            }
        }

        partial void Start()
        {
            this.Try(()=> this.manager.StartGyroUpdates(NSOperationQueue.CurrentQueue, this.UpdateHandler));
        }

        partial void Stop()
        {
            this.Try(()=> this.manager.StopGyroUpdates());
        }

        private void UpdateHandler(CMGyroData gyroData, NSError error)
        {
            if (error == null)
            {
                var rate = gyroData.RotationRate;
                this.readingAvailable.Invoke<GyroReading>(
                    gyroData,
                    new GyroReading(rate.x, rate.y, rate.z));
            }

            if (this.OnError != null)
            {
                this.OnError.Invoke<Exception>(this.manager, new Exception(error.LocalizedDescription));
            }
        }
    }
}