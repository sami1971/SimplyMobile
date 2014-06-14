using System;
using Intent = Android.Content.Intent;
using Uri = Android.Net.Uri;
using Android.Telephony;
using Android.Content;

namespace SimplyMobile.Device
{
    public class AndroidPhone : IPhone
    {
        public static TelephonyManager Manager
        {
            get
            {
                return Context.TelephonyService.GetSystemService() as TelephonyManager;
            }
        }

        #region IPhone implementation

        public string CellularProvider
        {
            get
            {
                return AndroidPhone.Manager.NetworkOperatorName;
            }
        }

        public string ICC
        {
            get
            {
                return AndroidPhone.Manager.SimCountryIso;
            }
        }

        public string MCC
        {
            get
            {
                return AndroidPhone.Manager.NetworkOperator.Remove(3,3);
            }
        }

        public string MNC
        {
            get
            {
                return AndroidPhone.Manager.NetworkOperator.Remove(0,3);
            }
        }

        public bool? IsCellularDataEnabled
        {
            get
            {
                return null;
            }
        }

        public bool? IsCellularDataRoamingEnabled
        {
            get
            {
                return null;
            }
        }

        public bool? IsNetworkAvailable
        {
            get
            {
                return null;
            }
        }

        public void DialNumber(string number)
        {
            number.StartActivity(new Intent(Intent.ActionDial, Uri.Parse("tel:" + number)));
        }
        #endregion
    }
}

