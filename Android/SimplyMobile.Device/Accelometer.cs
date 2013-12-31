using System;
using Android.Hardware;
using SimplyMobile.Core;
using Android.Content;
using Android.App;

namespace SimplyMobile.Device
{
	public static partial class Accelometer
	{
		private static AccelometerMonitor monitor;

		private static AccelometerMonitor Monitor
		{
			get
			{
				return monitor ?? (monitor = new Accelometer.AccelometerMonitor ());
			}
		}

		static partial void StartMonitoring()
		{
			Monitor.Start();
		}

		static partial void StopMonitoring()
		{
			Monitor.Stop();
		}

		private class AccelometerMonitor  : Java.Lang.Object, ISensorEventListener, IMonitor
		{
			private SensorManager sensorManager;
			private Sensor accelerometer;

			#region ISensorEventListener implementation

			public void OnAccuracyChanged (Sensor sensor, SensorStatus accuracy)
			{
//				throw new NotImplementedException ();
			}

			public void OnSensorChanged (SensorEvent e)
			{
				if (e.Sensor.Type != SensorType.Accelerometer)
				{
					return;
				}

				var reading = new AccelometerStatus(e.Values[0], e.Values[1], e.Values[2]);

				Accelometer.LatestStatus = new AccelometerStatus (reading);

				Accelometer.readingAvailable.Invoke (this, reading);
			}

			#endregion

			#region IMonitor implementation

			public event EventHandler<EventArgs<bool>> OnActiveChanged;

			public event EventHandler<EventArgs<Exception>> OnException;

			public bool Start ()
			{
				if (this.Active)
				{
					return this.Active;
				}

				this.sensorManager = Application.Context.GetSystemService(Context.SensorService) as SensorManager;
				if (this.sensorManager == null)
				{
					this.OnException.Invoke (this, new Exception ("Unable to get sensor manager."));
					return false;
				}

				this.accelerometer = sensorManager.GetDefaultSensor(SensorType.Accelerometer);
                
				if (this.accelerometer == null)
				{
					this.sensorManager = null;
					this.OnException.Invoke (this, new Exception ("Unable to get Accelerometer."));
					return false;
				}

				this.sensorManager.RegisterListener(this, accelerometer, SensorDelay.Ui);

				return this.Active;
			}

			public void Stop ()
			{
				if (this.Active)
				{
					this.sensorManager.UnregisterListener(this);
					this.sensorManager = null;
					this.accelerometer = null;
				}
			}

			public bool Active
			{
				get
				{
					return (this.sensorManager != null && this.accelerometer != null);
				}
			}

			#endregion


		}
	}


}

