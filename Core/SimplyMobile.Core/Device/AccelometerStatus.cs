using System;

namespace SimplyMobile.Device
{
    public class AccelometerStatus : Vector3d
    {
        public AccelometerStatus ()
        {
        }

        public AccelometerStatus (double x, double y, double z) : base(x,y,z) {}

        public AccelometerStatus(AccelometerStatus status)
            : base(status.X, status.Y, status.Z)
        {

        }

        public override string ToString ()
        {
            return string.Format ("[Accelometer: X={0}, Y={1}, Z={2}]", X, Y, Z);
        }
    }
}

