using System;
using MonoTouch.UIKit;

namespace SimplyMobile.Device
{
    public class Simulator : AppleDevice
    {
        internal Simulator ()
        {
            var b = UIScreen.MainScreen.Bounds;
            var h = b.Height * UIScreen.MainScreen.Scale;
            var w = b.Width * UIScreen.MainScreen.Scale;
            var dpi = UIScreen.MainScreen.Scale * 163;
            this.Screen = new Screen ((int)h, (int)w, dpi, dpi); 
        }
    }
}

