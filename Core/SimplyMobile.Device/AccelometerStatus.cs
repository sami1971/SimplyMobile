using System;

namespace SimplyMobile.Device
{
	public class AccelometerStatus
	{
		public AccelometerStatus ()
		{
		}

		public AccelometerStatus (double x, double y, double z)
		{
			this.X = x;
			this.Y = y;
			this.Z = Z;
		}

		public AccelometerStatus(AccelometerStatus status)
			: this(status.X, status.Y, status.Z)
		{

		}

		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public override string ToString ()
		{
			return string.Format ("[Accelometer: X={0}, Y={1}, Z={2}]", X, Y, Z);
		}
	}
}

