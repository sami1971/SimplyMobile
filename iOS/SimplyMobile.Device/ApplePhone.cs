using System;
using MonoTouch.UIKit;

namespace SimplyMobile.Device
{
    public class ApplePhone : IPhone
    {
        #region IPhone implementation
        public void DialNumber(string number)
        {
            UIApplication.SharedApplication.OpenUrl (new MonoTouch.Foundation.NSUrl ("tel:" + number));
        }
        #endregion
    }
}

