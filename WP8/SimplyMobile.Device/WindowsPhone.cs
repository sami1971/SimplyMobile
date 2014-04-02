using Microsoft.Phone.Tasks;
using SimplyMobile.Core;
using SimplyMobile.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Device
{
    public class WindowsPhone : IPhone
    {
        public void DialNumber(string number)
        {
            new PhoneCallTask() { PhoneNumber = number }.Show();
        }
    }
}
