using System;
using Android.App;
using Android.Content;
using Android.Telephony;

namespace SimplyMobile.Device
{
    /// <summary>
    /// Device Info class
    /// </summary>
    public static class Info
    {
        /// <summary>
        /// Gets a value indicating if the device has phone.
        /// </summary>
        /// <value><c>true</c> if has phone; otherwise, <c>false</c>.</value>
        public static bool HasPhone
        {
            get
            {
                var manager = Application.Context.GetSystemService(Context.TelephonyService) as TelephonyManager;
                return (manager != null && manager.PhoneType != PhoneType.None);
            }
        }
    }
}

