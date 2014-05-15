using System;
using Android.App;

namespace SimplyMobile.Device
{
    public class Screen : IScreen
    {
        public Screen ()
        {
            var dm = Application.Context.Resources.DisplayMetrics;
            this.Height = dm.HeightPixels;
            this.Width = dm.WidthPixels;
            this.Xdpi = dm.Xdpi;
            this.Ydpi = dm.Ydpi;
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
    }
}

