using System;
using Intent = Android.Content.Intent;
using Uri = Android.Net.Uri;

namespace SimplyMobile.Device
{
    public class AndroidPhone : IPhone
    {
        #region IPhone implementation
        public void DialNumber(string number)
        {
            number.StartActivity(
                new Intent(
                    Intent.ActionDial, 
                    Uri.Parse("tel:" + number)));
        }
        #endregion
    }
}

