using System;

namespace SimplyMobile.Device
{
    public class Screen : IScreen
    {
        internal Screen (int height, int width, double xdpi, double ydpi)
        {
            this.Height = height;
            this.Width = width;
            this.Xdpi = xdpi;
            this.Ydpi = ydpi;
        }

        #region IScreen implementation

        public int Height
        {
            get;
            private set;
        }

        public int Width
        {
            get;
            private set;
        }

        public double Xdpi
        {
            get;
            private set;
        }

        public double Ydpi
        {
            get;
            private set;
        }

        #endregion

        public override string ToString()
        {
            return string.Format("[Screen: Height={0}, Width={1}, Xdpi={2}, Ydpi={3}]", Height, Width, Xdpi, Ydpi);
        }
    }
}

